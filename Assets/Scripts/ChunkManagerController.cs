using UnityEngine;
using System.Collections;

public class ChunkManagerController : MonoBehaviour {
	public enum ChunkManagerMode {
		None,
		Resources,
		Scenes
	}

	[Header("Setup")]
	public ChunkManagerMode Mode      = ChunkManagerMode.None;
	public Transform        Root      = null;
	public float            ChunkSize = 0;

	[Header("Runtime")]
	[SerializeField]
	Vector3     CurrentPos   = Vector3.zero;
	[SerializeField]
	ChunkHolder CurrentChunk = null;

	IChunkManager _manager = null; 
	IChunkSource  _source  = null;

	IChunkManager CreateManager() {
		switch( Mode ) {
			case ChunkManagerMode.Resources : return new ResourcesChunkManager();
			case ChunkManagerMode.Scenes    : return new SceneChunkManager();
		}
		return null;
	}

	void Awake() {
		_manager = CreateManager();
		_source = GetSource();
		CurrentPos = Root.position;
		CurrentChunk = null;
		AddChunk(Direction.North);
	}

	IChunkSource GetSource() {
		return GetComponentInChildren<IChunkSource>();
	}

	public void AddChunk(Direction dir) {
		_manager.SpawnChunk(GetNextPos(CurrentPos, dir), _source.GetHolder(CurrentChunk, dir));
	}

	Vector3 GetNextPos(Vector3 current_pos, Direction dir) {
		var direction = GetDirVector(dir);
		return direction * ChunkSize;
	}

	Vector3 GetDirVector(Direction dir) {
		switch( dir ) {
			case Direction.North     : return Vector3.forward;
			case Direction.South     : return Vector3.back;
			case Direction.West      : return Vector3.left;
			case Direction.East      : return Vector3.right;
			case Direction.NorthEast : return GetDirVector(Direction.North) + GetDirVector(Direction.East);
			case Direction.NorthWest : return GetDirVector(Direction.North) + GetDirVector(Direction.West);
			case Direction.SouthEast : return GetDirVector(Direction.South) + GetDirVector(Direction.East);
			case Direction.SouthWest : return GetDirVector(Direction.South) + GetDirVector(Direction.West);
		}
		return Vector3.zero;
	}
}
