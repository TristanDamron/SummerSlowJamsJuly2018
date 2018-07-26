using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour {
	[SerializeField]
	private SkinnedMeshRenderer _mesh;

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Light") {			
			_mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.tag == "Light") {
			_mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}
	}
}
