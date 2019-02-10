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
    public Transform HookPlayerEnd;
    public bool DebugMode = false;

    Vector3[] points;    
    bool hookshotIsFiring;
    bool moveToHook;
    Transform player;

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

    public override void OnPickUpAction()
    {
        player = GetComponentInParent<PlayerMovementController>().transform;
        
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
                        //break out of the check and set the elapsed time something high to break the while loop
                        elapsedTime = 100f;
                        moveToHook = true;
                        break;
                    }
                    else
                    {         
                        //play a clang sound and end the firing early so it loops backwards
                        elapsedTime = 100f;
                        moveToHook = false;
                        targetPosition = Hook.position;
                        break;
                    }
                }
            }
            
            yield return new WaitForEndOfFrame();
        }

        //If the hook hits a collider with the tag HookTarget then "moveToHook" will be true and we move the player towards the hook's player position
        //which is just in front of where the hook ends up, otherwise we'd put the player exactly where the hook ends, and thats no good. They might end up
        //stuck inside whatever obstacle we zippin to.
        if (moveToHook)
        {
            elapsedTime = 0;
            Vector3 currentPlayerPosition = player.position;
            Vector3 targetLocation = HookPlayerEnd.position;
            while(elapsedTime < ProjectileSpeed)
            {
                player.position = Vector3.Lerp(currentPlayerPosition, targetLocation, (elapsedTime / ProjectileSpeed));
                Hook.position = targetLocation; //we keep telling the hook to stay where it is, otherwise it moves forward as the player moves forward.
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            elapsedTime = 0;
            while (elapsedTime < ProjectileSpeed)
            {
                Hook.position = Vector3.Lerp(targetPosition, HookOrigin.position, (elapsedTime / ProjectileSpeed));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        moveToHook = false;
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
