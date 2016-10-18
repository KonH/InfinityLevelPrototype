using UnityEngine;
using System.Collections;

public class ResourcesChunkManager : IChunkManager {
	
	public void SpawnChunk(Vector3 pos, ChunkHolder holder) {
		var prefab = Resources.Load(holder.Name) as GameObject;
		if( prefab ) {
			GameObject.Instantiate(prefab, pos, Quaternion.identity);
		}
	}
	
}
