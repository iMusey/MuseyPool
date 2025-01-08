using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public float interval;
    public float count = 0;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        count = interval;
    }

    // Update is called once per frame
    void Update()
    {
        // count down
        count -= Time.deltaTime;

        // when countdown <= 0 add force
        if (count <= 0)
        {
            count = interval;

            // add force
            // random normal vector in x/z plane, apply force to ball
            float dir = Random.Range(0, 360) * Mathf.Deg2Rad; // random degrees on circle
            Vector3 normal = new Vector3(Mathf.Cos(dir),0, Mathf.Sin(dir));

            transform.GetComponent<Rigidbody>().AddForce(normal * force, ForceMode.Impulse);
        }


    }
}
