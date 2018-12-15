using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/* If the motion tracker is in world space then the localRotation z axis of the center point for the blips should follow the parent transform's y rotation
    otherwise its an overlay one and in that case should use the player's y rotaion for the center point's z localRotation
    If confused, see prototype project for examples.
*/
///

public class MotionTracker : MonoBehaviour
{
    float canvasScale;
    AudioSource audioSource;
    WaitForSeconds one;
    public AudioClips AudioClips;
    public TrackerBlip BlipPrefab;
    public Transform Player;
    public Canvas Canvas;    
    public Transform Center;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //scale for box collider
        //WorldUIScale = canvas.GetComponent<RectTransform>().rect.width / GetComponent<BoxCollider>().size.x;

        //scale for sphere collider
        canvasScale = Canvas.GetComponent<RectTransform>().rect.width / (GetComponent<SphereCollider>().radius * 2);
        one = new WaitForSeconds(1);
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            audioSource.PlayOneShot(AudioClips.sounds[0]);
            yield return one;
        }
    }

    private void Update()
    {
        //rotate the radar center to match the players facing direction
        Center.localRotation = Quaternion.Euler(0, 0, Player.rotation.eulerAngles.y);
    }

    private bool WithinAngle(Transform target, float angle)
    {
        Vector3 direction = target.position - transform.position;
        //we want to ignore the y value when checking angle.
        direction.y = 0;
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
                    direction = direction.normalized * canvasScale;

                    Vector2 blipPosition = direction * distance;

                    TrackerBlip blip = Instantiate(BlipPrefab, Center);
                    blip.transform.localPosition = new Vector3(-blipPosition.x, -blipPosition.y, 0);
                    audioSource.PlayOneShot(AudioClips.sounds[1]);
                }
            }
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
