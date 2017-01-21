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

    public Timer clock;

    public int m_N_Citta = 4;
    public int m_Secondi = 10;

    public int m_current_touch = 0;
    public int m_current_city;


    void Start()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
        Raggio = GameObject.Instantiate(Raggio);
        clone = GameObject.Instantiate(Raggio);
        m_current_city = m_N_Citta;
    }

    private void Update()
    {
        if(m_current_city == 0)
        {
            clock.isPaused = true;
            StartCoroutine(PrintPunteggio());
        }
    }

    IEnumerator PrintPunteggio()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Finish!");
    }

    void LateUpdate()
    {
        cam = Camera.main.GetComponent<anamorph>().pe;
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
            m_current_touch++;
            Debug.DrawLine(hit.point, Vector3.up, Color.white);
            mirino.GetComponent<Player>().Pulse(hit.point);
            Debug.Log(hit.collider.gameObject.name);
            if (!hit.collider.tag.Equals("World"))
            {
                m_current_city--;
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
