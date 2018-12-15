using UnityEngine;
using UnityEngine.UI;

public class TrackerBlip : MonoBehaviour
{   
    private void Start()
    {        
        Destroy(gameObject, .5f);
    }

}
