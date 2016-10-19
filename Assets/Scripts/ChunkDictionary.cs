using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkDictionary : Dictionary<Direction, ChunkDictionary> {
	public ChunkHolder Current;
}
