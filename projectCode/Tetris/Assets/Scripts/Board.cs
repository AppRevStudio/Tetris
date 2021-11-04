using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPos;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2); // bottom left corner
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        // looping through and initializing all tetrominos
        for (int i = 0; i <  this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    // pick random piece to spawn
    public void SpawnPiece()
    {
        // pick random index within tetrominos, fetch that tetromino
        int random = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];

        // get reference to game piece
        this.activePiece.Initialize(this, this.spawnPos, data);
        Set(this.activePiece);
    }

    // render piece onto board
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePos, piece.data.tile);
        }
    }

    // clear piece from board
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePos, null);
        }
    }

    // checks if piece can move to new position, true if yes, false if no
    public bool isValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePos = piece.cells[i] + position;

            // check if tile is in bounds
            if (!bounds.Contains((Vector2Int)tilePos))
            {
                return false;
            }

            // check if tile is already filled
            if (this.tilemap.HasTile(tilePos))
            {
                return false;
            }
        }

        return true;
    }
}
