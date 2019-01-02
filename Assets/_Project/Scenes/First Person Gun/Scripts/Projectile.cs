using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject Explosion;
    public AudioSource audio;
    public AudioClip clip;

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
            Destroy(explosive, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
