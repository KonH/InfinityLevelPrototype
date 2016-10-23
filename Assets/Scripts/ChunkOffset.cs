using UnityEngine;
using System.Collections;

public struct ChunkOffset {
	public int X;
	public int Z;

	public ChunkOffset(int x, int z) {
		X = x;
		Z = z;
	}
		
	/*
	 * -- -- --- --
	 * __ -1  0  1
	 *  1 NW _N_ NE
	 *  0 _W --- E_
	 * -1 SW _S_ SE
	 * -- -- --- --
	 */

	public static ChunkOffset operator +(ChunkOffset o1, ChunkOffset o2) {
		return new ChunkOffset(o1.X + o2.X, o1.Z + o2.Z);
	}

	public static ChunkOffset North     { get { return new ChunkOffset( 0,  1); } }
	public static ChunkOffset NorthEast { get { return new ChunkOffset( 1,  1); } }
	public static ChunkOffset East      { get { return new ChunkOffset( 1,  0); } }
	public static ChunkOffset SouthEast { get { return new ChunkOffset( 1, -1); } }
	public static ChunkOffset South     { get { return new ChunkOffset( 0, -1); } }
	public static ChunkOffset SouthWest { get { return new ChunkOffset(-1, -1); } }
	public static ChunkOffset West      { get { return new ChunkOffset(-1,  0); } }
	public static ChunkOffset NorthWest { get { return new ChunkOffset(-1,  1); } }
}
