using UnityEngine;
using System.Collections;

public class ChunkAgent : MonoBehaviour {
	public ChunkManagerController Controller;
	public Vector3                Position;

	Transform _trans;

	void Awake() {
		_trans = transform;
	}

	// Update is called once per frame
	void Update () {
		Position = _trans.position;
		Controller.CheckAgent(this);
	}
}
