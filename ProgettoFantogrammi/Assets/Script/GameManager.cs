using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 cam;
    public Transform world;

    void Start()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
    }

    void LateUpdate()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        Debug.DrawRay(cam, world.position - cam, Color.green);
    }

    public void shoot()
    {
        Debug.Log("SHOOOOOOOT!");
        RaycastHit hit;
        Ray raggio = new Ray(cam, world.position - cam);

        if (Physics.Raycast(raggio, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (!hit.collider.tag.Equals("World"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

}
