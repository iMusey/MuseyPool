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

    public KeyCode forward;
    public KeyCode left;
    public KeyCode backward;
    public KeyCode right;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetBall();

        ManualMovement();

        CameraControls();
    }

    void TargetBall()
    {
        // shoot ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 0.1f);

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

                    // when you target a ball, look towards it

                    // radius based on distance to ball
                    radius = Vector3.Distance(target.transform.position, transform.position);

                    // tilt based on y position
                    tilt = -Mathf.Asin((target.transform.position.y - transform.position.y)/radius);

                    // theta based on
                    theta = Mathf.Atan2((transform.position.z - target.transform.position.z), (transform.position.x - target.transform.position.x));
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

    void ManualMovement()
    {
        if (Input.GetKey(forward))
        {
            if (target != null)
            {
                target.targeted = false;
                target = null;
            }
            center.x -= Time.deltaTime * speed;
        }
        if (Input.GetKey(left))
        {
            if (target != null)
            {
                target.targeted = false;
                target = null;
            }
            center.z -= Time.deltaTime * speed;
        }
        if (Input.GetKey(backward))
        {
            if (target != null)
            {
                target.targeted = false;
                target = null;
            }
            center.x += Time.deltaTime * speed;
        }
        if (Input.GetKey(right))
        {
            if (target != null)
            {
                target.targeted = false;
                target = null;
            }
            center.z += Time.deltaTime * speed;
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

        // scrolling for radius
        radius -= Input.mouseScrollDelta.y;

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
