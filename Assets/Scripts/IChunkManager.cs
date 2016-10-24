using UnityEngine;
using System.Collections;

public interface IChunkManager {
	ChunkStore SpawnChunk(Vector3 pos, ChunkHolder holder);
	void DespawnChunk(ChunkStore store);
}
