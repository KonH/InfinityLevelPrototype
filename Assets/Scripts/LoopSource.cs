using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoopSource : MonoBehaviour, IChunkSource {
	public List<ChunkHolder> AllHolders = new List<ChunkHolder>();

	int _index = 0;

	public ChunkHolder GetHolder(ChunkHolder current, ChunkOffset dir) {
		_index++;
		if( _index > AllHolders.Count - 1 ) {
			_index = 0;
		}
		return AllHolders[_index];
	}
}
