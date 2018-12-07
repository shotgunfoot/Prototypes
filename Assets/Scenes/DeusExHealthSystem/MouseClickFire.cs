using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickFire : MonoBehaviour
{

    Camera _cam;

    public float Damage;

    // Use this for initialization
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 1f);
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.tag == "Hitbox")
                {
                    hit.collider.GetComponent<Limb>().DamageLimb(Damage);
                }
            }
        }
    }
}
