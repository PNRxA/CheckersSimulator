using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData
{
    public int gridX, gridZ;
    public PieceData()
    {
        gridX = 0;
        gridZ = 0;
    }
    public PieceData(int x, int z)
    {
        gridX = x;
        gridZ = z;
    }
}

public class Piece : MonoBehaviour
{
    public PieceData pieceData = new PieceData();
    public int gridX
    {
        get
        {
            return gridX;
        }
        set
        {
            pieceData.gridX = value;
            gridX = value;
        }
    }
    public int gridZ
    {
        get
        {
            return gridZ;
        }
        set
        {
            pieceData.gridZ = value;
            gridZ = value;
        }
    }
}
