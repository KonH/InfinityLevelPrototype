using UnityEngine;
using System.Collections;

public interface IChunkManager {
	void SpawnChunk(Vector3 pos, ChunkHolder holder);
}
