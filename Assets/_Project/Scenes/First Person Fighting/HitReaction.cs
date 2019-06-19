using UnityEngine;

public class HitReaction : MonoBehaviour
{

    [SerializeField] private float forcePower = 5;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void AddForceAtHitLocation(Vector3 hitPos, Vector3 hitDirection)
    {
        rb.AddForceAtPosition(hitDirection * forcePower, hitPos, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Debug.Log("Adding Force!");
            AddForceAtHitLocation(other.contacts[0].point, other.contacts[0].normal);
        }

    }
}

