using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 4f;
    public float waitTime;
    public Transform moveSpot;

    private float timer;

    public float halfRoomSize;
    private float minX, maxX, minY, maxY;

    public float horizontal;
    public float vertical;
    public Animator anim;
    private Rigidbody2D rb;

    public GameObject sight;

    void Start()
    {
        timer = waitTime;
        minX = GetComponent<Transform>().position.x - halfRoomSize;
        maxX = GetComponent<Transform>().position.x + halfRoomSize;
        minY = GetComponent<Transform>().position.y - halfRoomSize;
        maxY = GetComponent<Transform>().position.y + halfRoomSize;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        Vector2 lookDir = moveSpot.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (Vector2.Distance(transform.position, moveSpot.position) > 0.2f)
        {
            sight.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10000 * Time.deltaTime);
        }

        Vector3 direction = (moveSpot.position - transform.position).normalized;

        Debug.Log(direction);

        
        if (direction.x == 0 && direction.y < 0)
        {
            horizontal = 0;
            vertical = -1;
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            horizontal = 1;
            vertical = -1;
        }
        else if (direction.x > 0 && direction.y == 0)
        {
            horizontal = 1;
            vertical = 0;
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            horizontal = 1;
            vertical = 1;

        }
        else if (direction.x == 0 && direction.y > 0)
        {
            horizontal = 0;
            vertical = 1;
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            horizontal = -1;
            vertical = 1;
        }
        else if (direction.x < 0 && direction.y == 0)
        {
            horizontal = -1;
            vertical = 0;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            horizontal = -1;
            vertical = -1;
        }

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);

    
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (timer <= 0)
            {
                timer = waitTime;
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                
            }
            else
            {
                anim.SetFloat("speed", 0);
                timer -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetFloat("speed", speed);
        }
    }

}
