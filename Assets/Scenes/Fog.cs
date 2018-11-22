using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{

    public int width = 10;
    public int height = 10;
    public float cellSize = .32f;

    private float widthOffset;
    private float heightOffset;
    private Vector3 offset;

    [Required]
    public Transform origin;
    [Required]
    public GameObject blackTile;    

    // Use this for initialization
    void Start()
    {        
        if (transform.childCount > 1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i));
            }
        }

        offset = new Vector3(-width / 2 * cellSize, -height / 2 * cellSize, -1);

        for (int i = 0; i < width; i++)
        {
            for (int f = 0; f < height; f++)
            {
                Vector3 position = new Vector3(origin.position.x + i * cellSize, origin.position.y + f * cellSize, 0);
                GameObject tile = Instantiate(blackTile, position, Quaternion.identity, transform);
                tile.transform.position += offset;
                tile.name = "" + i + ", " + f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
