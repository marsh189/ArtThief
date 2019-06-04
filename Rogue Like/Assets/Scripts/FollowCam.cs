using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    private Transform target;
    Vector3 m_DesiredPosition;




    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            try
            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
            catch
            {
                Debug.Log("No player active");
            }
        }
        else
        {
            m_DesiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = m_DesiredPosition;
        }
    }
}
