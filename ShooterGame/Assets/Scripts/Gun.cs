using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public Camera fpsCam;
    //force back
    public float impactForce = 40f;

    //shooting particle
    public ParticleSystem gunParticle;
    //impact particle
    public GameObject imapactEffect;

 
    // Update is called once per frame
    void Update()
    {
        //if we click the left button we shoot
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
            gunParticle.Play();
        }
    }

    void Shoot()
    {
        //making the bullet travel to a object
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
            Debug.Log(hit.transform.name);

            //if we shot a target it should take damage,  however, if it isnt an gameobject it wont take damage
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
            }

            //if the bullet hits an object and there is an rigidbody, it will have force back
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //once the bullet hits an object there is an effect
            GameObject ImapctGO = Instantiate(imapactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //once it has the impact, the impact effect will go for 2 seconds and disaper
            Destroy(ImapctGO, 2f);
        }
    }
}
