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
	Vector3     CurrentPos   = Vector3.zero;
	ChunkHolder CurrentChunk = null;

	IChunkManager   _manager = null;
	IChunkSource    _source  = null;
	ChunkDictionary _history = null;

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
		_history = new ChunkDictionary();
	}

	IChunkSource GetSource() {
		return GetComponentInChildren<IChunkSource>();
	}

	void AddChunk(Direction dir) {
		if( !_history.ContainsKey(dir) ) {
			var holder = _source.GetHolder(CurrentChunk, dir);
			_manager.SpawnChunk(GetNextPos(CurrentPos, dir), holder);
			var newDict = new ChunkDictionary();
			newDict.Current = holder;
			newDict.Add(Inverse(dir), _history);
			_history.Add(dir, newDict);
		}
	}

	bool ChangeChunk(Direction dir) {
		ChunkDictionary newHistory;
		_history.TryGetValue(dir, out newHistory);
		if( newHistory != null ) {
			_history = newHistory;
			CurrentPos = GetNextPos(CurrentPos, dir);
			return true;
		}
		return false;
	}

	Vector3 GetNextPos(Vector3 current_pos, Direction dir) {
		var direction = GetDirVector(dir);
		return current_pos +  direction * ChunkSize;
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

	Direction Inverse(Direction dir) {
		switch( dir ) {
			case Direction.North     : return Direction.South;
			case Direction.South     : return Direction.North;
			case Direction.West      : return Direction.East;
			case Direction.East      : return Direction.West;
			case Direction.NorthEast : return Direction.SouthWest;
			case Direction.NorthWest : return Direction.SouthEast;
			case Direction.SouthEast : return Direction.NorthWest;
			case Direction.SouthWest : return Direction.NorthEast;
		}
		throw new UnityException("Wrong direction");
	}

	public void CheckAgent(ChunkAgent agent) {
		var delta = agent.Position - CurrentPos;
		Debug.Log(delta);
		CheckForGenerate(delta);
		CheckForChange(delta);
	}

	void CheckForGenerate(Vector3 delta) {
		var xUp   = delta.x > ChunkSize/4;
		var xDown = delta.x < -ChunkSize/4;
		var zUp   = delta.z > ChunkSize/4;
		var zDown = delta.z < -ChunkSize/4;
		if( xUp ) {
			AddChunk(Direction.East);
			if( zUp ) {
				AddChunk(Direction.NorthEast);
			}
			if( zDown ) {
				AddChunk(Direction.SouthEast);
			}
		}
		if( xDown ) {
			AddChunk(Direction.West);
			if( zUp ) {
				AddChunk(Direction.NorthWest);
			}
			if( zDown ) {
				AddChunk(Direction.SouthWest);
			}
		}
		if( zUp ) {
			AddChunk(Direction.North);
		}
		if( zDown ) {
			AddChunk(Direction.South);
		}
	}

	bool CheckForChange(Vector3 delta) {
		var xUp   = delta.x > ChunkSize/2;
		var xDown = delta.x < -ChunkSize/2;
		var zUp   = delta.z > ChunkSize/2;
		var zDown = delta.z < -ChunkSize/2;
		if( xUp ) {
			if( zUp ) {
				return ChangeChunk(Direction.NorthEast);
			}
			if( zDown ) {
				return ChangeChunk(Direction.SouthEast);
			}
			return ChangeChunk(Direction.East);
		}
		if( xDown ) {
			if( zUp ) {
				return ChangeChunk(Direction.NorthWest);
			}
			if( zDown ) {
				return ChangeChunk(Direction.SouthWest);
			}
			return ChangeChunk(Direction.West);
		}
		if( zUp ) {
			return ChangeChunk(Direction.North);
		}
		if( zDown ) {
			return ChangeChunk(Direction.South);
		}
		return false;
	}
}
