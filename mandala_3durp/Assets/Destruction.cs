using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Destruction : MonoBehaviour
{
    public GameObject brokenObject;
    public GameObject canvas;
    public ParticleSystem dustParticles; // Assign your dust particle system in the Inspector

    public Pen penScript;


    IEnumerator ExplosionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dustParticles.Play();
    }


    IEnumerator DestroyBrokenWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Destroy(brokenObject);
        brokenObject.SetActive(false);
        StartCoroutine(ExplosionWithDelay(7.0f));
    }

    void Start()
    {
        canvas.SetActive(true);
        brokenObject.SetActive(false);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            if (GetComponent<Collider>().tag == "Target")
            {
                canvas.SetActive(false);
                brokenObject.SetActive(true);
                penScript.ClearStrokes();
                //Vector3 myVector = new Vector3(1, 30, 2);
                //GameObject wallBroken = Instantiate(brokenObject, myVector, transform.rotation);
                //Destroy(GetComponent<Collider>().gameObject);

                // Trigger the particle system


                // Destroy the canvs object after a delay
                if (brokenObject != null)
                {
                    print("HELLO WORLD!");
                    StartCoroutine(DestroyBrokenWithDelay(5.0f));
                }
            }
        }

    }

}