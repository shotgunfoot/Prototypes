using UnityEngine;
using System.Collections;
using EZCameraShake;

public class ExplosiveProjectile : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject Explosion;
    public AudioSource audio;
    public AudioClip clip;

    public float ShakeMagnitude;
    public float ShakeRoughness;
    public float ShakeFadeIn;
    public float ShakeFadeOut;
    public float ExplosionRadius;
    public float ExplosionForce;
    public float LaunchModifier;

    private bool spawnedExplosive = false;

    private void Start()
    {
        Destroy(gameObject, 15f);
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(rotationSpeed, 0, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {       
        GetComponent<MeshRenderer>().enabled = false;
        if (!spawnedExplosive)
        {
            spawnedExplosive = true;
            GameObject explosive = Instantiate(Explosion, transform.position, Quaternion.identity);
            audio.PlayOneShot(clip);
            CameraShaker.Instance.ShakeOnce(ShakeMagnitude, ShakeRoughness, ShakeFadeIn, ShakeFadeOut);
            Destroy(explosive, 5f);
            Destroy(gameObject, 5f);
        }

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
    }
}
