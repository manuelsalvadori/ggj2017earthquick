using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirino3d : MonoBehaviour
{
    Vector3 cam;
    public Transform world;
    public Transform mirino;

    void Start()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
    }

    void LateUpdate()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        Debug.DrawRay(cam, world.position - cam, Color.cyan);
        RaycastHit hit;
        Ray raggio = new Ray(cam, world.position - cam);

        int layermask = 1 << 8;

        if (Physics.Raycast(raggio, out hit, Mathf.Infinity, layermask))
        {
            mirino.position = new Vector3(hit.point.x, mirino.position.y, hit.point.z);

        }
    }
}
