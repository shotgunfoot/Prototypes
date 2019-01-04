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
    }
}
