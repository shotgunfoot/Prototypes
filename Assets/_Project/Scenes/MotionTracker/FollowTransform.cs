using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform Target;
    private void FixedUpdate()
    {
        if (Target != null)
        {
            transform.position = Target.position;
        }
    }
}