using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkStorage {
	Dictionary<int, Dictionary<int, ChunkStore>> storage = new Dictionary<int, Dictionary<int, ChunkStore>>();

	public bool ContainsChunk(ChunkOffset offset) {
		Dictionary<int, ChunkStore> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict != null ) {
			return dict.ContainsKey(offset.Z);
		}
		return false;
	}

	public ChunkStore GetStore(ChunkOffset offset) {
		Dictionary<int, ChunkStore> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict != null ) {
			if( dict.ContainsKey(offset.Z) ) {
				return dict[offset.Z];
			}
		}
		return null;
	}

	public void AddChunk(ChunkOffset offset, ChunkStore store) {
		Dictionary<int, ChunkStore> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict == null ) {
			dict = new Dictionary<int, ChunkStore>();
			storage.Add(offset.X, dict);
		}
		if( !dict.ContainsKey(offset.Z) ) {
			dict.Add(offset.Z, store);
		}
	}

	public void RemoveChunk(ChunkOffset offset) {
		Dictionary<int, ChunkStore> dict;
		storage.TryGetValue(offset.X, out dict);
		if( dict != null ) {
			if( dict.ContainsKey(offset.Z) ) {
				dict.Remove(offset.Z);
			}
		}
	}
}