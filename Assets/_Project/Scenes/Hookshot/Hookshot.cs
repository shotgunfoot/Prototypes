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
    public Transform HookOrigin;
    public bool DebugMode = false;

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

        Vector3 startingPos = Hook.position;
        while(elapsedTime < ProjectileSpeed)
        {
            Hook.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / ProjectileSpeed));
            elapsedTime += Time.deltaTime;

            Collider[] colls = Physics.OverlapSphere(Hook.position, 0.39f);

            if(colls.Length > 0)
            {
                foreach (Collider coll in colls)
                {
                    if(coll.tag == "HookTarget")
                    {
                        
                    }
                    else
                    {         
                        //play a clang sound and end the firing early so it loops backwards
                        elapsedTime = 100f;
                        targetPosition = Hook.position;
                        break;
                    }
                }
            }
            
            yield return new WaitForEndOfFrame();
        }

        elapsedTime = 0;        
        while(elapsedTime < ProjectileSpeed)
        {
            Hook.position = Vector3.Lerp(targetPosition, HookOrigin.position, (elapsedTime / ProjectileSpeed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Hook.position = HookOrigin.position;

        //if reached distance, retract hook
        //Hook.localPosition = new Vector3(1, 0, 0);
        //regardless of result let the player move again
        Events[1].Invoke();
        //Defatten the line renderer
        Line.widthMultiplier = 0f;
        hookshotIsFiring = false;
        yield return null;        
    }

    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Hook.position, 0.39f);
        }
    }
        
}
