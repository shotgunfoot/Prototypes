using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject ExplosionParticleEffect;
    public AudioSource Audio;
    public AudioClip Clip;
    public float ExplosionForce;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }


    public void Explode()
    {
        //spawn the explosive effect
        GameObject obj = Instantiate(ExplosionParticleEffect, transform.position, Quaternion.identity);
        //play an explosion sound
        Audio.PlayOneShot(Clip);
        //Push anything with a rigidbody away from the explosion center.


        //destroy this object and the instanated particle after x seconds
        Destroy(gameObject, 5f);
        Destroy(obj, 5f);
    }
}
