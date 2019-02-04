using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour {

    //if the object had one of these offset it by the attachoffset.
    public Vector3 AttachPositionOffset;
    public Vector3 AttachRotationOffset;
    public Collider[] colls;
}
