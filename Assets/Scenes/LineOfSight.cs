using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineOfSight : MonoBehaviour
{

    /// <summary>
    /// what we want to do:
    /// Fire a number of rays towards the camera and hit the tilemap called 'line of sight' and disable the ones being hit by turning off the tile in that grid cell.
    /// Always turn the grid back on if it isn't being hit.
    /// Fire said rays in a square grid around centered around the player
    /// 
    /// </summary>

    public int gridSize;

    [Required]
    public Transform player;

    public float cellSize = .32f;
    private Vector3[,] raycastPoints;    

    private void Update()
    {
        for(int i = 0; i < gridSize; i++)
        {
            for(int f = 0; f < gridSize; f++)
            {
                Vector3 position = new Vector3(player.position.x + i * cellSize, player.position.y + f * cellSize, 0);
                Vector3 gridOffset = new Vector3(-gridSize / 2 * cellSize, -gridSize / 2 * cellSize, 0);
                position += gridOffset;
                Vector3 finalPosition = position;

                Debug.DrawRay(finalPosition, Vector3.back * 5f, Color.white);
                RaycastHit hit;
                Physics.Raycast(finalPosition, Vector3.back, out hit, 5f);           

                if(hit.collider != null)
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
    
}
