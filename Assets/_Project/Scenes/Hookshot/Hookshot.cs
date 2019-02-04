using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// How the hookshot should work:
/// Firing the hookshot - The player stands still when firing so set player movement speed to 0 while the hookshot is firing.
/// The hookshot has a specific distance to travel and if it hits anything in that distance it checks if the object being hit is a hookshot target.
/// If it is a hookshot target move the player towards the contact point and let them move again, otherwise retract the hookshot then allow the player to move again.
/// 
/// </summary>

[ExecuteInEditMode]
public class Hookshot : WieldableObject
{
    public float MaxDistance = 20f;
    public float ProjectileSpeed = 4f;
    public LineRenderer Line;
    public Transform chainStartPoint;
    public Transform Hook;    
    public UnityEvent[] Events;
    public Camera cam;

    Vector3[] points;

    bool hookshotIsFiring;

    private void Start()
    {
        points = new Vector3[2];
        cam = FindObjectOfType<Camera>();
        Line.widthMultiplier = 0;
    }
    
    private void LateUpdate()
    {
        points[0] = chainStartPoint.position;
        points[1] = Hook.position;        

        Line.SetPositions(points);
    }

    public override void ObjectAction()
    {
        if (!hookshotIsFiring)
        {
            hookshotIsFiring = true;
            StartCoroutine(FiringHookshot());           
        }
        
    }    

    private IEnumerator FiringHookshot()
    {
        //force the player to stop moving
        Events[0].Invoke();
        //fatten the line renderer
        Line.widthMultiplier = 1f;

        //play firing sound
        
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        //Debug.DrawRay(ray.origin, ray.direction * MaxDistance, Color.red, 2f);
        Vector3 targetPosition = ray.origin + ray.direction * MaxDistance;

        //move the hook forward by its speed until distance reached        
        float elapsedTime = 0;
        elapsedTime = 0;

        Vector3 startingPos = Hook.position;
        while(elapsedTime < ProjectileSpeed)
        {
            Hook.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / ProjectileSpeed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        //float t = 0;
        //while (t < 1)
        //{
        //    t += Time.deltaTime / ProjectileSpeed;
        //    Hook.position = Vector3.Lerp(Hook.position, targetPosition, t);
        //    yield return null;
        //}

        //float currentDistance = Vector3.Distance(Hook.position, targetPosition);
        //for (float i = 0.0f; i < 1.0f; i += (ProjectileSpeed * Time.deltaTime) / currentDistance)
        //{
        //    Debug.Log(i);
        //    Hook.position = Vector3.Lerp(Hook.position, targetPosition, i);
        //    yield return null;
        //}

        //if reached distance, retract hook
        Hook.localPosition = new Vector3(1, 0, 0);
        //regardless of result let the player move again
        Events[1].Invoke();
        //Defatten the line renderer
        Line.widthMultiplier = 0f;
        hookshotIsFiring = false;
        yield return null;        
    }
}
