using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 cam;
    public Transform world;
    public bool hitted = false;

    public GameObject Effect;

    void Start()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
    }

    void LateUpdate()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        //Debug.DrawRay(cam, world.position - cam, Color.green);
    }

    public void shoot()
    {
        Handheld.Vibrate();
        Debug.Log("SHOOOOOOOT!");
        hitted = true;
        RaycastHit hit;
        Ray raggio = new Ray(cam, world.position - cam);
        int layermask = 1 << 8;
        layermask = ~layermask;
        if (Physics.Raycast(raggio, out hit, Mathf.Infinity,layermask))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (!hit.collider.tag.Equals("World"))
            {
                GameObject tmp = GameObject.Instantiate(Effect);
                tmp.transform.position = hit.point;
                tmp.transform.LookAt(-(world.position - cam));
                tmp.SetActive(true);
                foreach (ParticleSystem ps in tmp.GetComponentsInChildren<ParticleSystem>())
                {
                    ps.Play();
                }

                StartCoroutine(shutParticle(tmp));
                Destroy(hit.collider.gameObject);
            }

        }


    }

    IEnumerator shutParticle(GameObject o)
    {
        yield return new WaitForSeconds(3f);
        foreach (ParticleSystem ps in o.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }
    }

}
