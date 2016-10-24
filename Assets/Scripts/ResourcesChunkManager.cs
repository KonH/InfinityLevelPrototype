using UnityEngine;
using System.Collections;

public class ResourcesChunkManager : IChunkManager {
	
	public ChunkStore SpawnChunk(Vector3 pos, ChunkHolder holder) {
		var prefab = Resources.Load(holder.Name) as GameObject;
		if( prefab ) {
			var instance = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
			return new ChunkStore(holder, instance);
		}
		return null;
	}
	
	public void DespawnChunk(ChunkStore store) {
		throw new System.NotImplementedException();
	}
}
