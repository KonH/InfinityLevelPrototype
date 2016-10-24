using UnityEngine;
using System.Collections;

public class ChunkStore {
	ChunkHolder _holder     = null;
	GameObject  _gameObject = null;

	public ChunkHolder Holder { 
		get { 
			return _holder; 
		} 
	}

	public GameObject GameObject { 
		get { 
			return _gameObject; 
		} 
		set { 
			_gameObject = value; 
		} 
	}

	public ChunkStore(ChunkHolder holder) {
		_holder = holder;
	}

	public ChunkStore(ChunkHolder holder, GameObject gameObject) {
		_holder     = holder;
		_gameObject = gameObject;
	}
}