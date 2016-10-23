using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkStorage {
	Dictionary<int, Dictionary<int, ChunkHolder>> storage = new Dictionary<int, Dictionary<int, ChunkHolder>>();

	public bool ContainsChunk(ChunkOffset offset) {
		Dictionary<int, ChunkHolder> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict != null ) {
			return dict.ContainsKey(offset.Z);
		}
		return false;
	}

	public void AddChunk(ChunkOffset offset, ChunkHolder holder) {
		Dictionary<int, ChunkHolder> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict == null ) {
			dict = new Dictionary<int, ChunkHolder>();
			storage.Add(offset.X, dict);
		}
		if( !dict.ContainsKey(offset.Z) ) {
			dict.Add(offset.Z, holder);
		}
	}
}
