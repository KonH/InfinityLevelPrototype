using UnityEngine;
using System.Collections;

public interface IChunkSource {
	ChunkHolder GetHolder(ChunkHolder current, Direction dir);
}
