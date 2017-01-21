using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 cam;
    public Transform world;
    public bool hitted = false;

    public GameObject Effect;
    public GameObject mirino;

    public GameObject Raggio;
    private GameObject clone;

    void Start()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        Raggio = GameObject.Instantiate(Raggio);
        clone = GameObject.Instantiate(Raggio);
    }

    void LateUpdate()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        RaycastHit hitted;
        Raggio.transform.position = cam;
        Raggio.transform.LookAt(world.position);
        clone.transform.position = cam;
        clone.transform.LookAt(world.position);

        Debug.DrawRay(cam, world.position - cam, Color.green);
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
        if (Physics.Raycast(raggio, out hit, Vector3.Distance(world.position,cam),layermask))
        {

            Debug.DrawLine(hit.point, Vector3.up, Color.white);
            mirino.GetComponent<Player>().Pulse(hit.point);
            Debug.Log(hit.collider.gameObject.name);
            if (!hit.collider.tag.Equals("World"))
            {
                GameObject tmp = GameObject.Instantiate(Effect);
                tmp.transform.SetParent(world);
                tmp.transform.position = hit.transform.position;
                tmp.transform.LookAt(cam);
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
        yield return new WaitForSeconds(1f);
        o.transform.GetChild(0).GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2f);
        foreach (ParticleSystem ps in o.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }
    }

}
