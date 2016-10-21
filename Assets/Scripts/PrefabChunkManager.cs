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

	public void SpawnChunk(Vector3 pos, ChunkHolder holder) {
		var prefab = GetPrefab(holder.Name);
		Instantiate(prefab, pos, Quaternion.identity);
	}
}
