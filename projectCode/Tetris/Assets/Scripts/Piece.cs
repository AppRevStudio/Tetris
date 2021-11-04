using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        this.board.Clear(this);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }

        // Soft drop
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }

        // Hard drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        this.board.Set(this);
    }

    // keeps moving down until can't move down any more
    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    // checks if position to move to is valid, if yes then move piece in that direction
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPos = this.position;
        newPos.x += translation.x;
        newPos.y += translation.y;

        bool valid = this.board.isValidPosition(this, newPos);

        if (valid)
        {
            this.position = newPos;
        }

        return valid;
    }
}
