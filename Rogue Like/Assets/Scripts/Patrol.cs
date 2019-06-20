using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 4f;
    public float waitTime;
    public Transform[] moveSpots;
    public Animator anim;

    public GameObject sight;

    private float timer;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private int randomIndex;

    void Start()
    {
        timer = waitTime;
        randomIndex = Random.Range(0, moveSpots.Length);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomIndex].position, speed * Time.deltaTime);

        Vector2 lookDir = moveSpots[randomIndex].position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (Vector2.Distance(transform.position, moveSpots[randomIndex].position) > 0.2f)
        {
            sight.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10000 * Time.deltaTime);
        }

        Vector3 direction = (moveSpots[randomIndex].position - transform.position).normalized;
        
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

    
        if (Vector2.Distance(transform.position, moveSpots[randomIndex].position) < 0.2f)
        {
            if (timer <= 0)
            {
                timer = waitTime;
                int last = randomIndex;
                randomIndex = Random.Range(0, moveSpots.Length);
                while (randomIndex == last)
                {
                    randomIndex = Random.Range(0, moveSpots.Length);
                }
                
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
