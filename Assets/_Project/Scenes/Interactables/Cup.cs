using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : WieldableObject
{
    public float forceMultiplier = 10f;
    [SerializeField] private AudioClip breakNoise;
    private Rigidbody rb;
    private bool thrown;
    private AudioSource source;
    [SerializeField] private MeshRenderer mesh;
    public GameObject brokenCup;
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    public override void ObjectAction()
    {
        Throw();
    }

    private void Throw()
    {
        GetComponentInParent<Hand>().DropItem();
        rb.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
        StartCoroutine(Throwroutine());
    }

    public IEnumerator Throwroutine()
    {
        thrown = true;
        yield return new WaitForSeconds(5f);
        thrown = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (thrown)
        {
            Break();
        }
    }

    private void Break()
    {
        Instantiate(brokenCup, transform.position, transform.rotation);
        thrown = false;
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        source.PlayOneShot(breakNoise);
        mesh.enabled = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(5f);
    }
}
