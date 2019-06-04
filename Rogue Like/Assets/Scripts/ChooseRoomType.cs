using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRoomType : MonoBehaviour
{
    public GameObject[] objects; //index 3 --> starting room
    public bool isStartingRoom;

    // Start is called before the first frame update
    void Start()
    {
        if(!isStartingRoom)
        {
            int rand = Random.Range(0, objects.Length);
            GameObject instance = Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else
        {
            GameObject instance = Instantiate(objects[3], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
    }
}
