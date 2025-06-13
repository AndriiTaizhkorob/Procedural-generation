using UnityEngine;

namespace Demo
{
    public class SimpleBuilding : Shape
    {
        public int buildingHeight = -1; // The total building height (=#stocks) - value should be the same for all stocks
        public float stockHeight = 1; // The height of one stock. Change this value depending on the height of your stock prefabs
                                      // If buildingHeight is negative, a random building height will be chosen between these two limits:
        public int maxHeight = 5;
        public int minHeight = 1;

        public GameObject cityGrid;

        public GameObject[] groundPrefabs;
        public GameObject[] stockPrefabs; // Your stock prefabs (make sure they all have the same height)
        public GameObject[] roofPrefabs; // Your roof prefabs (may have different height)

        int stockNumber = 0; // The number of stocks that have already been spawned below this

        public float chance= 0;

        private void Start()
        {
            cityGrid = GameObject.Find("GridCitySpawner");
        }

        public void Initialize(int pBuildingHeight, float pStockHeight, int pStockNumber, GameObject[] pGroundPrefabs, GameObject[] pStockPrefabs, GameObject[] pRoofPrefabs)
        {
            buildingHeight = pBuildingHeight;
            stockHeight = pStockHeight;
            stockNumber = pStockNumber;
            groundPrefabs = pGroundPrefabs;
            stockPrefabs = pStockPrefabs;
            roofPrefabs = pRoofPrefabs;
        }

        // Returns a random game object chosen from a given gameobject array
        GameObject ChooseRandom(GameObject[] choices)
        {
            int index = Random.Range(0, choices.Length);

            return choices[index];
        }

        protected override void Execute()
        {

            if (buildingHeight < 0)
            { // This is only done once for the root symbol
                //buildingHeight = Random.Range(minHeight, maxHeight + 1);
                buildingHeight = (int)(maxHeight * cityGrid.GetComponent<Map>().heightMap[(int)gameObject.transform.position.z / cityGrid.GetComponent<GridCity>().rowWidth, (int)gameObject.transform.position.x / cityGrid.GetComponent<GridCity>().columnWidth] / chance + 1);

            }

            if (stockNumber < buildingHeight)
            {

                if (stockNumber == 0)
                {
                    GameObject newGround = SpawnPrefab(ChooseRandom(groundPrefabs));
                }
                else if (stockNumber > 0)
                {
                    if (buildingHeight < 40)
                    {
                        GameObject newStock = SpawnPrefab(ChooseRandom(stockPrefabs));
                    }

                    if (buildingHeight >= 40)
                    {
                        if (stockNumber > buildingHeight * 0.4)
                        {
                            GameObject newStock = SpawnPrefab(ChooseRandom(stockPrefabs));
                        }
                        else
                        {
                            GameObject newGround = SpawnPrefab(ChooseRandom(groundPrefabs));
                        }
                    }
                }
                // First spawn a new stock...

                //  ...and then continue with the remainder of the building, right above the spawned stock:

                // Create a new symbol - make sure to increase the y-coordinate:
                SimpleBuilding remainingBuilding = CreateSymbol<SimpleBuilding>("stock", new Vector3(0, stockHeight, 0));
                // Pass the parameters to the new symbol (component), but increase the stockNumber:
                remainingBuilding.Initialize(buildingHeight, stockHeight, stockNumber + 1, groundPrefabs, stockPrefabs, roofPrefabs);
                // ...and continue with the shape grammar:
                remainingBuilding.Generate(buildDelay);
            }
            else
            {
                // Spawn a roof and stop:
                GameObject newRoof = SpawnPrefab(ChooseRandom(roofPrefabs));
            }
        }
    }
}
