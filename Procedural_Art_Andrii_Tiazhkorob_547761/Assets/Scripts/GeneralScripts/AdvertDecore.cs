using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;

public class AdvertDecore : MonoBehaviour
{
    public GameObject[] advertDecorations;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        int decorationIndex = Random.Range(0, advertDecorations.Length);

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 4f))
        {
            GameObject newAd = Instantiate(advertDecorations[decorationIndex], gameObject.transform);
        }

    }
}

