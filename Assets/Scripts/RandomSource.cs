using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSource : MonoBehaviour, IChunkSource {
	public List<ChunkHolder> AllHolders = new List<ChunkHolder>();

	public ChunkHolder GetHolder(ChunkHolder current, ChunkOffset dir) {
		return AllHolders[Random.Range(0, AllHolders.Count)];
	}
}
