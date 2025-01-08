using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float radius;
    public float height;
    public float tilt;
    public float theta = 0;
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Create New Position
        Vector3 pos = center;
        pos.x += Mathf.Cos(theta) * radius;
        pos.y += height;
        pos.z += Mathf.Sin(theta) * radius;

        // Rotate camera angle
        transform.rotation = Quaternion.Euler(tilt, (-(theta * Mathf.Rad2Deg) - 90), 0);

        // Set New Position
        transform.position = pos;

        // Increment pos based on speed
        theta += speed * Time.deltaTime;
        
        if (theta >= 360)
        {
            theta = 0;
        }

    }
}
