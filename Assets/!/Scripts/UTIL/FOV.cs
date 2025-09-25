//script of FOV Tutorial(Unity) - https://youtu.be/ztdj0KUnGuY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;

    public Transform target;


    void Update()
    {
        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

        if (angle < fovAngle / 2)
        {
            if (r.collider.CompareTag("Agent"))
            {
                // WE SPOTTED THE PLAYER!
                print("SEEN!");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
            }
            else
            {
                print("we dont seen");
            }
        }

    }
}
