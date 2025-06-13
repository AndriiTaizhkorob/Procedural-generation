// Version 2023
//  (Updates: supports different root positions)
using UnityEngine;

namespace Demo {
	public class GridCity : MonoBehaviour {
		public int rows = 10;
		public int columns = 10;
		public int rowWidth = 10;
		public int columnWidth = 10;
		public float grayScaleNumber;
        public GameObject[] buildingPrefabs;
        public Transform[] angles;

        public float buildDelaySeconds = 0.1f;

		void Start() {
			Generate();
		}

		void Update() {
			if (Input.GetKeyDown(KeyCode.G)) {
				DestroyChildren();
				Generate();
			}
		}

		void DestroyChildren() {
			for (int i = 0; i<transform.childCount; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		void Generate() {
			for (int row = 0; row<rows; row++) {
				for (int col = 0; col<columns; col++) {
					// Create a new building, chosen randomly from the prefabs:
					int buildingIndex = 0;

                    //grayScaleNumber = gameObject.GetComponent<Map>().heightMap[row, col];
					grayScaleNumber = gameObject.GetComponent<Map>().heightMap[row, col];

                    if (grayScaleNumber >= 0.57f)
                    {
                        buildingIndex = 2;
                    }
                    else if (grayScaleNumber < 0.57f && grayScaleNumber >= 0.35f)
                    {
                        buildingIndex = 1;
                    }
                    else if (grayScaleNumber < 0.36f)
                    {
                        buildingIndex = 0;
                    }

					//buildingIndex = Random.Range(0, buildingPrefabs.Length);
                    GameObject newBuilding = Instantiate(buildingPrefabs[buildingIndex], transform);

					// Place it in the grid:
					newBuilding.transform.localPosition = new Vector3(col * columnWidth, 0, row*rowWidth);
					newBuilding.transform.localRotation = angles[Random.Range(0, angles.Length)].rotation;

					// If the building has a Shape (grammar) component, launch the grammar:
					Shape shape = newBuilding.GetComponent<Shape>();
					if (shape!=null) {
						shape.Generate(buildDelaySeconds);
					}
				}
			}
		}
	}
}