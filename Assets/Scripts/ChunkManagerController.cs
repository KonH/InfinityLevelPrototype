using UnityEngine;
using System.Collections;

public class ChunkManagerController : MonoBehaviour {
	public enum ChunkManagerMode {
		None,
		Prefabs,
		Resources,
		Scenes
	}

	[Header("Setup")]
	public ChunkManagerMode Mode      = ChunkManagerMode.None;
	public Transform        Root      = null;
	public float            ChunkSize = 0;

	[Header("Runtime")]
	[SerializeField]
	Vector3     CurrentPos    = Vector3.zero;
	[SerializeField]
	ChunkOffset CurrentOffset = new ChunkOffset();
	ChunkHolder CurrentChunk  = null;

	IChunkManager   _manager  = null;
	IChunkSource    _source   = null;
	ChunkStorage    _storage  = null;

	IChunkManager CreateManager() {
		switch( Mode ) {
			case ChunkManagerMode.Prefabs   : return GetComponent<PrefabChunkManager>();
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
		_storage = new ChunkStorage();
		_storage.AddChunk(new ChunkOffset(0, 0), null);
	}

	IChunkSource GetSource() {
		return GetComponentInChildren<IChunkSource>();
	}

	void AddChunk(ChunkOffset offset) {
		if( !_storage.ContainsChunk(CurrentOffset + offset) ) {
			var holder = _source.GetHolder(CurrentChunk, offset);
			_manager.SpawnChunk(GetNextPos(CurrentPos, offset), holder);
			_storage.AddChunk(CurrentOffset + offset, holder);
		}
	}

	bool ChangeChunk(ChunkOffset offset) {
		CurrentOffset = new ChunkOffset(CurrentOffset.X + offset.X, CurrentOffset.Z + offset.Z);
		CurrentPos = GetNextPos(CurrentPos, offset);
		return true;
	}

	Vector3 GetNextPos(Vector3 current_pos, ChunkOffset offset) {
		var direction = GetDirVector(offset);
		return current_pos + direction * ChunkSize;
	}

	Vector3 GetDirVector(ChunkOffset offset) {
		return new Vector3(offset.X, 0, offset.Z);
	}

	public void CheckAgent(ChunkAgent agent) {
		var delta = agent.Position - CurrentPos;
		CheckForGenerate(delta);
		CheckForChange(delta);
	}

	void CheckForGenerate(Vector3 delta) {
		var xUp   = delta.x > ChunkSize/4;
		var xDown = delta.x < -ChunkSize/4;
		var zUp   = delta.z > ChunkSize/4;
		var zDown = delta.z < -ChunkSize/4;
		if( xUp ) {
			AddChunk(ChunkOffset.East);
			if( zUp ) {
				AddChunk(ChunkOffset.NorthEast);
			}
			if( zDown ) {
				AddChunk(ChunkOffset.SouthEast);
			}
		}
		if( xDown ) {
			AddChunk(ChunkOffset.West);
			if( zUp ) {
				AddChunk(ChunkOffset.NorthWest);
			}
			if( zDown ) {
				AddChunk(ChunkOffset.SouthWest);
			}
		}
		if( zUp ) {
			AddChunk(ChunkOffset.North);
		}
		if( zDown ) {
			AddChunk(ChunkOffset.South);
		}
	}

	bool CheckForChange(Vector3 delta) {
		var xUp   = delta.x > ChunkSize/2;
		var xDown = delta.x < -ChunkSize/2;
		var zUp   = delta.z > ChunkSize/2;
		var zDown = delta.z < -ChunkSize/2;
		if( xUp ) {
			if( zUp ) {
				return ChangeChunk(ChunkOffset.NorthEast);
			}
			if( zDown ) {
				return ChangeChunk(ChunkOffset.SouthEast);
			}
			return ChangeChunk(ChunkOffset.East);
		}
		if( xDown ) {
			if( zUp ) {
				return ChangeChunk(ChunkOffset.NorthWest);
			}
			if( zDown ) {
				return ChangeChunk(ChunkOffset.SouthWest);
			}
			return ChangeChunk(ChunkOffset.West);
		}
		if( zUp ) {
			return ChangeChunk(ChunkOffset.North);
		}
		if( zDown ) {
			return ChangeChunk(ChunkOffset.South);
		}
		return false;
	}
}
