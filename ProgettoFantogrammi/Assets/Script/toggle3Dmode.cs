using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle3Dmode : MonoBehaviour
{
    public Stereo script3d;
    public anamorph anamorphscript;

    void Start()
    {
        script3d = GetComponent<Stereo>();
        anamorphscript = GetComponent<anamorph>();
    }

    public void toggle3d()
    {
        script3d.enabled = !script3d.enabled;
        anamorphscript.enabled = !anamorphscript.enabled;
        if (script3d.enabled == true)
        {
            transform.position = new Vector3(0f, 3.99f, -6.41f);
            transform.rotation = Quaternion.Euler(new Vector3(41.671f, 0f, 0f));
        }
        else
        {
            transform.position = new Vector3(0f, 6.209999f, -8.909999f);
            transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        }
    }
}
