using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject ExplosionParticleEffect;
    public AudioSource Audio;
    public AudioClip Clip;
    public float ExplosionForce;
    public float ExplosionRadius;
    public float LaunchModifier;
    public bool ShowDebugExplosionRadius;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }


    public void Explode()
    {
        //hide the mesh
        GetComponent<MeshRenderer>().enabled = false;
        //disable the collider
        GetComponent<Collider>().enabled = false;
        //spawn the explosive effect
        GameObject obj = Instantiate(ExplosionParticleEffect, transform.position, Quaternion.identity);
        //play an explosion sound
        Audio.PlayOneShot(Clip);
        
        //Push anything with a rigidbody away from the explosion center.
        Collider[] colls = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach(Collider coll in colls)
        {
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 direction = coll.transform.position - transform.position;
                float distance = Vector3.Distance(coll.transform.position, transform.position);
                rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, LaunchModifier, ForceMode.Impulse);
            }
        }

        //shake dat camera
        CameraShaker.Instance.ShakeOnce(5, 5, .1f, 2);

        //destroy this object and the instanated particle after x seconds
        Destroy(gameObject, 5f);
        Destroy(obj, 5f);
    }

    private void OnDrawGizmos()
    {
        if (ShowDebugExplosionRadius)
        {
            Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
        }        
    }
}
