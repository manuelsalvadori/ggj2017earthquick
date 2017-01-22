using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Vector3 cam;
    public Transform world;
    public bool hitted = false;

    public GameObject Effect;
    public GameObject mirino;

    public GameObject Raggio;
    private GameObject clone;

    public GameObject button;
    public GameObject explosion;

    public GameObject primo;
    public GameObject secondo;
    public GameObject terzo;


    public Timer clock;

    public Text punt;

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
        yield return new WaitForSeconds(4f);
        //Time.timeScale = 0;
        float punteggio = m_Secondi * 1000 / clock.counterTime * m_N_Citta / m_current_touch;
        Debug.Log("Finish!");
        primo.transform.parent.gameObject.SetActive(true);
        if (punteggio >= 1000)
        {
            primo.transform.GetChild(1).gameObject.SetActive(true);
            secondo.transform.GetChild(1).gameObject.SetActive(true);
            terzo.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (punteggio >= 700 && punteggio < 1000)
        {
            primo.transform.GetChild(1).gameObject.SetActive(true);
            secondo.transform.GetChild(1).gameObject.SetActive(true);
            terzo.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            primo.transform.GetChild(1).gameObject.SetActive(true);
            secondo.transform.GetChild(0).gameObject.SetActive(true);
            terzo.transform.GetChild(0).gameObject.SetActive(true);
        }
        punt.gameObject.SetActive(true);
        punt.text = Mathf.Ceil(punteggio).ToString();

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

        GetComponents<AudioSource>()[1].Play();
        //Handheld.Vibrate();
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
                GetComponents<AudioSource>()[0].Play();

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
        if (m_current_city == 0)
        {
            world.GetComponent<TouchRotation>().enabled = false;
            StartCoroutine(destroyPlanet());
        }

    }

    IEnumerator destroyPlanet()
    {
        yield return new WaitForSeconds(2.2f);
        Destroy(button);
        GetComponents<AudioSource>()[3].Play();
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        world.gameObject.SetActive(false);
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
