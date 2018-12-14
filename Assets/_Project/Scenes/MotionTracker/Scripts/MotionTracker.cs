using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTracker : MonoBehaviour
{

    List<GameObject> targets;
    List<float> distances;
    List<Vector3> directions;
    List<TrackerBlip> blips;
    float WorldUIScale;
    AudioSource audioSource;
    WaitForSeconds one;

    public AudioClips audioClips;

    public TrackerBlip blipPrefab;

    public Transform player;
    public Canvas canvas;

    public Transform blip;
    public Transform center;

    private void Start()
    {
        targets = new List<GameObject>();
        distances = new List<float>();
        directions = new List<Vector3>();
        blips = new List<TrackerBlip>();
        audioSource = GetComponent<AudioSource>();

        //scale for box collider
        //WorldUIScale = canvas.GetComponent<RectTransform>().rect.width / GetComponent<BoxCollider>().size.x;

        //scale for sphere collider
        WorldUIScale = canvas.GetComponent<RectTransform>().rect.width / (GetComponent<SphereCollider>().radius * 2);
        one = new WaitForSeconds(1);
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            audioSource.PlayOneShot(audioClips.sounds[0]);
            yield return one;
        }
    }

    private void Update()
    {
        //rotate the radar center to match the players facing direction
        center.localRotation = Quaternion.Euler(0, 0, player.rotation.eulerAngles.y);
    }

    private bool WithinAngle(Transform target, float angle)
    {
        Vector3 direction = target.position - transform.position;

        if (Vector3.Angle(transform.forward, direction) < angle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (WithinAngle(other.transform, 90))
            {
                if (other.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
                {
                    Transform target = other.GetComponent<Transform>();

                    Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
                    Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);

                    float distance = Vector2.Distance(targetPos, thisPos);
                    Vector2 direction = (thisPos - targetPos);

                    //normalise the direction between 0 and 1 then scale it with the UI.
                    direction = direction.normalized * WorldUIScale;

                    Vector2 blipPosition = direction * distance;

                    TrackerBlip blip = Instantiate(blipPrefab, center);
                    blip.transform.localPosition = new Vector3(-blipPosition.x, -blipPosition.y, 0);
                    audioSource.PlayOneShot(audioClips.sounds[1]);


                }
            }
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
