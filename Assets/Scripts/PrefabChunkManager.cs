using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabChunkManager : MonoBehaviour, IChunkManager {
	public List<GameObject> Prefabs = new List<GameObject>();

	GameObject GetPrefab(string name) {
		for( int i = 0; i < Prefabs.Count; i++ ) {
			if( Prefabs[i].name == name ) {
				return Prefabs[i];
			}
		}
		return null;
	}

	public ChunkStore SpawnChunk(Vector3 pos, ChunkHolder holder) {
		var prefab = GetPrefab(holder.Name);
		var instance = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
		return new ChunkStore(holder, instance);
	}
		
	public void DespawnChunk(ChunkStore store) {
		if( store != null ) {
			Destroy(store.GameObject);
		}
	}
}
