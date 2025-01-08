using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Vector3 respawn;
    public GameObject highlight;
    public float highlightSize;
    public Color hoverColor;
    public Color highlightColor;

    public bool targeted = false;
    public bool hovered = false;


    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        highlight.transform.localScale = Vector3.one * highlightSize;

        if (hovered)
        {
            highlight.SetActive(true);
            highlight.GetComponent<Renderer>().material.color = hoverColor;
        }
        else
        {
            highlight.SetActive(false);
        }
        if (targeted)
        {
            highlight.SetActive(true);
            highlight.GetComponent<Renderer>().material.color = highlightColor;
        }
        else if (!hovered)
        {
            highlight.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;

        // if its a pocket respawn the ball and return a message
        if (obj.GetComponent<PocketScript>() != null)
        {
            transform.position = respawn;

            Debug.Log(obj.GetComponent<PocketScript>().msg);
        }

    }

    // highlight when hovering
    public void Hover()
    {
        hovered = true;
    }

    // highlight when clicked
    public void Target()
    {
        if (targeted)
        {
            targeted = false;
        }
        else
        {
            targeted = true;
        }
    }
}
