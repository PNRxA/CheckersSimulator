using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("CheckerBoardData")]
public class CheckerBoardData
{
    [XmlArray("Pieces")]
    [XmlArrayItem("Piece")]
    public PieceData[] pieces;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(CheckerBoardData));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static CheckerBoardData Load(string path)
    {
        var serializer = new XmlSerializer(typeof(CheckerBoardData));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as CheckerBoardData;
        }
    }
}

public class CheckerBoard : MonoBehaviour
{
    public string fileName;
    public GameObject blackPiece;
    public GameObject whitePiece;

    public int boardX = 8, boardZ = 8;
    public float pieceRadius = 0.5f;

    public Piece[,] pieces;
    private int halfBoardX, halfBoardZ;
    private float pieceDiameter;
    private Vector3 bottomLeft;
    private CheckerBoardData data;

    // Use this for initialization
    void Start()
    {
        // Calculate a few values
        halfBoardX = boardX / 2;
        halfBoardZ = boardZ / 2;
        pieceDiameter = pieceRadius * 2;
        bottomLeft = -Vector3.right * halfBoardX - Vector3.forward * halfBoardZ;

        string path = Application.persistentDataPath + "/" + fileName;
        //data = CheckerBoardData.Load(path);
        CreateGrid();
        data = new CheckerBoardData();
        data.Save(path);
    }

    void CreateGrid()
    {
        // Initialize 2D array
        pieces = new Piece[boardX, boardZ];

        #region Generate White Pieces
        // Loop through board columns and skip 2 each time
        for (int x = 0; x < boardX; x += 2)
        {
            // Loop throgh first 3 rows
            for (int z = 0; z < 3; z++)
            {
                // Check even row
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                // Generate piece
                GeneratePiece(whitePiece, gridX, gridZ);
            }
        }
        #endregion

        #region Generate Black Pieces
        // Loop through board columns and skip 2 each time
        for (int x = 0; x < boardX; x += 2)
        {
            // Loop throgh first 3 rows
            for (int z = boardZ - 3; z < boardZ; z++)
            {
                // Check even row
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                // Generate piece
                GeneratePiece(blackPiece, gridX, gridZ);
            }
        }

        #endregion
    }

    void GeneratePiece(GameObject piecePrefab, int x, int z)
    {
        // Create instance of piece
        GameObject clone = Instantiate(piecePrefab);
        // Set the paren to be this transform
        clone.transform.SetParent(transform);
        // Get the Piece component from clone
        Piece piece = clone.GetComponent<Piece>();
        // Place the piece 
        PlacePiece(piece, x, z);
    }

    void PlacePiece(Piece piece, int x, int z)
    {
        // Calculate ofset for piece based on cordinates
        float xOffset = x * pieceDiameter + pieceRadius;
        float zOffset = z * pieceDiameter + pieceRadius;
        // Set pieces's new grid coordinate
        piece.gridX = x;
        piece.gridZ = z;
        // Move piece physically to board coordinate
        piece.transform.position = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;

        // Set pieces aray slot
        pieces[x, z] = piece;
    }

    public void PlacePiece(Piece piece, Vector3 position)
    {
        // Translate position to cordinate in array
        float percentX = (position.x + halfBoardX) / boardX;
        float percentZ = (position.z + halfBoardZ) / boardZ;

        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((boardX - 1) * percentX);
        int z = Mathf.RoundToInt((boardZ - 1) * percentZ);

        // Get oldPiece from that cordinate
        Piece oldPiece = pieces[x, z];

        // If there is an oldPiece in the slot curently
        if (oldPiece != null)
        {
            // Swap Pieces
            SwapPieces(piece, oldPiece);
        }
        else
        {
            // Place Pieces
            int oldX = piece.gridX;
            int oldZ = piece.gridZ;
            pieces[oldX, oldZ] = null;
            PlacePiece(piece, x, z);
        }
    }

    void SwapPieces(Piece pieceA, Piece pieceB)
    {
        // Check if pieceA or pieceB is null
        if (pieceA == null || pieceB == null)
        {
            return; // Exit the function
        }
        // pieceA grid pos
        int pAX = pieceA.gridX;
        int pAZ = pieceA.gridZ;
        // pieceB grid pos
        int pBX = pieceB.gridX;
        int pBZ = pieceB.gridZ;
        // Swap pieces
        PlacePiece(pieceA, pBX, pBZ);
        PlacePiece(pieceB, pAX, pAZ);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
