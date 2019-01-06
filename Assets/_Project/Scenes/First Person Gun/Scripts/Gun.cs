using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WieldableObject
{
    public GameObject ProjectilePrefab;
    public Transform ProjectileOrigin;
    public float ProjectileForce;
    public AudioSource audio;
    public AudioClip clip;

    private void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    private void ShootProjectile()
    {
        GameObject obj = Instantiate(ProjectilePrefab, ProjectileOrigin.position, ProjectileOrigin.rotation);
        obj.GetComponent<Rigidbody>().AddForce(ProjectileOrigin.forward * ProjectileForce, ForceMode.Impulse);
        audio.PlayOneShot(clip);
    }

    public override void ObjectAction()
    {
        ShootProjectile();
    }
    
}
