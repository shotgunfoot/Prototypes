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

        // if (targets.Count > 0)
        // {
        //     distances.Clear();
        //     directions.Clear();
        //     foreach (GameObject obj in targets)
        //     {
        //         Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
        //         Vector2 targetPos = new Vector2(obj.transform.position.x, obj.transform.position.z);

        //         //find out the distance from the center of this object to the target.
        //         distances.Add(Vector2.Distance(thisPos, targetPos));
        //         directions.Add((thisPos - targetPos).normalized); //normalize the directions to be a value between 0 and 1;
        //     }


        //     for (int i = 0; i < targets.Count; i++)
        //     {
        //         blips[i].image.enabled = targets[i].GetComponent<Rigidbody>().velocity.magnitude > 0.1f;

        //         //earlier we normalized the directions between 0 and 1. Now we need to scale them to the world canvas.
        //         directions[i] = directions[i] * WorldUIScale; //scale the direction by the WorldUIScale
        //         Vector2 blipPosition = directions[i] * distances[i];
        //         blips[i].transform.localPosition = new Vector3(-blipPosition.x, -blipPosition.y, 0);
        //     }

        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
            {
                Transform target = other.GetComponent<Transform>();

                Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);

                float distance = Vector2.Distance(thisPos, targetPos);
                Vector2 direction = (thisPos - targetPos).normalized;

                direction = direction * WorldUIScale;

                Vector2 blipPosition = direction * distance;

                TrackerBlip blip = Instantiate(blipPrefab, center);
                blip.transform.localPosition = new Vector3(-blipPosition.x, -blipPosition.y, 0);
                audioSource.PlayOneShot(audioClips.sounds[1]);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
                TrackerBlip tB = Instantiate(blipPrefab, center);
                blips.Add(tB);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // if (targets.Contains(other.gameObject))
        // {
        //     targets.Remove(other.gameObject);
        //     Destroy(blips[0].gameObject);
        //     blips.RemoveAt(0);
        // }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
