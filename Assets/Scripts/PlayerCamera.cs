using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Ray ray;
    RaycastHit hit;

    public BallScript target;
    public BallScript hover;

    //camera controls
    public Vector3 looking;
    public Vector3 panStart;
    public Vector3 center;
    public float speed;
    public float radius;
    public float tilt;
    public float theta;
    public float dTheta;
    public float dTilt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetBall();

        CameraControls();
    }

    void TargetBall()
    {
        // shoot ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 0.1f);

        // hover or target
        if (hover != null)
        {
            hover.hovered = false;
            hover = null;
        }

        if (Physics.Raycast(ray, out hit, 999))
        {
                
            if (hit.transform.GetComponent<BallScript>() != null)
            {
                BallScript temp = hit.transform.GetComponentInChildren<BallScript>();

                // Target the ball if left click
                if (Input.GetMouseButtonDown(0))
                {
                    // if its a different ball change the target
                    if (!temp.Equals(target))
                    {
                        target = null;
                    }
                    target = temp;
                    target.targeted = true;
                    center = target.transform.position;
                }
                else // otherwise hover it
                {
                    hover = temp;
                    hover.hovered = true;
                }
            }
            else if (Input.GetMouseButtonDown(0) && target != null)
            {
                target.targeted = false;
                target = null;
            }
        }
    }

    void CameraControls()
    {
        // Pan Camera

        // any x and z movement changes theta
        // any y movement changes the tilt
        if (Input.GetMouseButtonDown(1))
        {
            panStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            // current mouse position
            looking = Input.mousePosition;

            // get deltas by subtracting the last frame from the current frame.
            dTheta = (panStart.x - looking.x) /1920;
            dTilt = (panStart.y - looking.y)/1080;

            // define a new previous frame
            panStart = Input.mousePosition;


            looking = (new Vector3(dTheta, dTilt, 0)) * speed;

            // move camera
            theta += looking.x;
            tilt += looking.y;
        }

        // Create New Position based on theta tilt and radius
        Vector3 pos = Vector3.zero;
        pos.x += Mathf.Sin(90 * Mathf.Deg2Rad - tilt) * Mathf.Cos(theta);
        pos.y += Mathf.Sin(tilt);
        pos.z += Mathf.Sin(90 * Mathf.Deg2Rad - tilt) * Mathf.Sin(theta);
        pos *= radius;
        pos += center;
        //pos = Vector3.ClampMagnitude(pos, radius);

        // Rotate camera angle
        transform.rotation = Quaternion.Euler(tilt * Mathf.Rad2Deg, (-(theta * Mathf.Rad2Deg) - 90), 0);

        // Set New Position
        transform.position = pos;
    }
}
