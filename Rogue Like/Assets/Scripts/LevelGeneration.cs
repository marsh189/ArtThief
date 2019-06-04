using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public Transform startingPosition;
    public GameObject[] rooms; //index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT, index 4 --> starting room, index 5 --> end room
    public int moveAmount = 10;
    public float startTimeBtwnRoom = 0.25f;
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;
    public LayerMask room;

    private float timeBtwnRoom;
    private int direction;
    private int downCounter;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.position;
        Instantiate(rooms[4], transform.position, Quaternion.identity);

        direction = 1;
    }

    void Update()
    {
        
        if (timeBtwnRoom <= 0 && !stopGeneration)
        {
            Move();
            timeBtwnRoom = startTimeBtwnRoom;
        }
        else
        {
            timeBtwnRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        if(direction == 1 || direction == 2) //MOVE RIGHT
        {
            
            if (transform.position.x < maxX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length - 2);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);

                if(direction == 3)
                {
                    direction = 2;
                }
                else if(direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //MOVE LEFT
        {
            
            if (transform.position.x > minX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length - 2);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 5) //MOVE DOWN
        {

            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if (roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter < 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottonRoom = Random.Range(1, 4);

                        if (randBottonRoom == 2)
                        {
                            randBottonRoom = 1;
                        }
                        Instantiate(rooms[randBottonRoom], transform.position, Quaternion.identity);

                    }
                    else
                    {

                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);

                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;


                int rand = Random.Range(2, 3);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else if (transform.position.y == minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 5)
                {
                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                    Instantiate(rooms[5], transform.position, Quaternion.identity);
                    stopGeneration = true;
                }
            }
            else
            {
                stopGeneration = true;
            }
        }

    }
}
