using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoard : MonoBehaviour
{
    public GameObject blackPiece;
    public GameObject whitePiece;

    public int boardX = 8, boardZ = 8;
    public float pieceRadius = 0.5f;

    public Piece[,] pieces;
    private int halfBoardX, halfBoardZ;
    private float pieceDiameter;
    private Vector3 bottomLeft;

    // Use this for initialization
    void Start()
    {
        // Calculate a few values
        halfBoardX = boardX / 2;
        halfBoardZ = boardZ / 2;
        pieceDiameter = pieceRadius * 2;
        bottomLeft = -Vector3.right * halfBoardX - Vector3.forward * halfBoardZ;

        CreateGrid();
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

    // Update is called once per frame
    void Update()
    {

    }
}
