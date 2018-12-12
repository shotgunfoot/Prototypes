using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTracker : MonoBehaviour
{

    List<GameObject> targets;
    List<float> distances;
    List<Vector3> directions;
    List<TrackerBlip> blips;

    public TrackerBlip blipPrefab;

    private float WorldUIScale;

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

        WorldUIScale = canvas.GetComponent<RectTransform>().rect.width / GetComponent<BoxCollider>().size.x;
    }

    private void Update()
    {
        center.localRotation = Quaternion.Euler(0, 0, player.rotation.eulerAngles.y);

        if (targets.Count > 0)
        {
            distances.Clear();
            directions.Clear();
            foreach (GameObject obj in targets)
            {
                Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetPos = new Vector2(obj.transform.position.x, obj.transform.position.z);

                //find out the distance from the center of this object to the target.
                distances.Add(Vector2.Distance(thisPos, targetPos));
                directions.Add((thisPos - targetPos).normalized);
            }


            for (int i = 0; i < targets.Count; i++)
            {
                blips[i].image.enabled = targets[i].GetComponent<Rigidbody>().velocity.magnitude > 0.1f; //only show the blip if the corresponding enemy is

                directions[i] = directions[i] * WorldUIScale;
                Vector2 blipPosition = directions[i] * distances[i];
                blips[i].transform.localPosition = new Vector3(blipPosition.y, -blipPosition.x, 0);
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
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
            Destroy(blips[0].gameObject);
            blips.RemoveAt(0);
        }
    }

}
