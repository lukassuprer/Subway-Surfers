using UnityEngine;

public class StartOfTile : MonoBehaviour
{
    private TileGenerator tileGenerator;

    private void Awake()
    {
        tileGenerator = FindObjectOfType<TileGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tileGenerator.TilesCrossed++;
        }
    }
}
