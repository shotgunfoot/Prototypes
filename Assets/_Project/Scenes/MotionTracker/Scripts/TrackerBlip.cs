using UnityEngine;
using UnityEngine.UI;

public class TrackerBlip : MonoBehaviour
{
    public Image image;
    bool isMoving;
    Vector3 oldPosition;

    private void Start()
    {
        image = GetComponent<Image>();
        Destroy(gameObject, .5f);
    }

}
