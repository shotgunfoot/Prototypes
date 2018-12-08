using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransformWithOffset : MonoBehaviour {

	public Transform Target;

	public Transform OffsetPoint;

	private void Update() {
		transform.position = Target.position + OffsetPoint.position;
		transform.rotation = Target.rotation;
	}
}
