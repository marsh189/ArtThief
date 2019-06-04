using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGen.stopGeneration)   //SPAWN RANDOM ROOM
        {
            Instantiate(levelGen.rooms[0], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
