using UnityEngine;
using System.Collections;

public class ChunkManagerController : MonoBehaviour {
	public enum ChunkManagerMode {
		None,
		Resources,
		Scenes
	}

	public ChunkManagerMode Mode = ChunkManagerMode.None;
	public Transform        Root = null;

	public IChunkManager Manager { get; private set; }

	IChunkManager CreateManager() {
		switch( Mode ) {
			case ChunkManagerMode.Resources : return new ResourcesChunkManager();
			case ChunkManagerMode.Scenes    : return new SceneChunkManager();
		}
		return null;
	}

	void Awake() {
		Manager = CreateManager();
		// TEST
		Manager.SpawnChunk(Root.position, new ChunkHolder(){ Name = "Chunk_0" });
	}
}
