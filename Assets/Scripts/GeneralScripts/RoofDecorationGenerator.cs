using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;

public class RoofDecorationGenerator : MonoBehaviour
{
    public GameObject[] roofDecorations;
    public Transform[] angles;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        int decorationIndex = Random.Range(0, roofDecorations.Length);
        GameObject newDecoration = Instantiate(roofDecorations[decorationIndex], angles[Random.Range(0, angles.Length)]);
    }
}
