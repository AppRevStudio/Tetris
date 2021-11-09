using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPos;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    private int nextPieceIndex = -1;
    public Vector3Int nextSpawnPos;

    [SerializeField]
    Tile[] tetrominoTiles;
    Tile nextTile;
    bool randomTiles;

    [SerializeField]
    private TMPro.TMP_Text levelText;
    [SerializeField]
    private TMPro.TMP_Text rowClearText;
    [SerializeField]
    private TMPro.TMP_Text currentScoreText;
    [SerializeField]
    private TMPro.TMP_Text highScoreText;
    [SerializeField]
    private TMPro.TMP_Text tetrisFragmentText;

    private int fragments;

    private int currentLevel = 0;
    private int rowsCleared = 0;
    private int currentScore = 0;
    private int highScore = 0;

    float currentTime;

    private bool gameOver = false;
    [SerializeField]
    private GameObject gameOverPanel;

    AudioSource musicSource;
    [SerializeField]
    AudioClip[] songs;

    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioClip moveSound;
    [SerializeField]
    AudioClip rotateSound;
    [SerializeField]
    AudioClip lockSound;
    [SerializeField]
    AudioClip clearSound;
    [SerializeField]
    AudioClip tetrisSound;
    [SerializeField]
    AudioClip levelUpSound;
    [SerializeField]
    AudioClip gameOverSound;

    float volume;

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
        musicSource = GetComponent<AudioSource>();

        // looping through and initializing all tetrominos
        for (int i = 0; i <  this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        levelText.text = currentLevel.ToString();
        rowClearText.text = rowsCleared.ToString();
        currentScoreText.text = currentScore.ToString();
        highScore = PlayerPrefs.GetInt("TetrisHighScore");
        highScoreText.text = highScore.ToString();

        fragments = PlayerPrefs.GetInt("TetrisFragments");
        tetrisFragmentText.text = fragments.ToString();

        musicSource.volume = PlayerPrefs.GetFloat("TetrisVolume");
        SelectSong();

        volume = PlayerPrefs.GetFloat("TetrisVolume");

        UpdateTetrominoSpeed();

        if (PlayerPrefs.GetInt("TetrisColor") == 1)
        {
            randomTiles = false;
        }
        else
        {
            randomTiles = true;
        } 

        gameOverPanel.SetActive(false);

        SpawnPiece();
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        currentTime += Time.deltaTime;
    }

    // pick random piece to spawn
    public void SpawnPiece()
    {
        if (gameOver)
        {
            return;
        }

        TetrominoData data;

        if (nextPieceIndex == -1)
        {
            int random = Random.Range(0, this.tetrominos.Length);
            data = this.tetrominos[random];
            if (randomTiles)
            {
                data.tile = GetRandomTile();
            }
        }
        else
        {
            data = this.tetrominos[nextPieceIndex];
            if (randomTiles)
            {
                data.tile = nextTile;
            }
        }

        SelectPiece();

        // get reference to game piece
        this.activePiece.Initialize(this, this.spawnPos, data);

        if (isValidPosition(this.activePiece, this.spawnPos))
        {
            Set(this.activePiece);
        }
        else
        {
            GameOver();
        }
    }

    // Selects the next piece to be played and displays it
    private void SelectPiece()
    {
        TetrominoData priorData;

        if (!(nextPieceIndex == -1)) // if prior nextPiece is displayed, clear it
        {
            priorData = this.tetrominos[nextPieceIndex];

            for (int i = 0; i < priorData.cells.Length; i++)
            {
                Vector3Int tilePos = (Vector3Int)priorData.cells[i] + (Vector3Int)nextSpawnPos;
                this.tilemap.SetTile(tilePos, null);
            }
        }

        // pick random index within tetrominos, fetch that tetromino
        int random = Random.Range(0, this.tetrominos.Length);
        nextPieceIndex = random;

        TetrominoData data = this.tetrominos[nextPieceIndex];

        if (randomTiles)
        {
            data.tile = GetRandomTile();
            nextTile = data.tile;
        }

        for (int i = 0; i < data.cells.Length; i++) // render nextPiece
        {
            Vector3Int tilePos = (Vector3Int)data.cells[i] + (Vector3Int)nextSpawnPos;
            this.tilemap.SetTile(tilePos, data.tile);
        }
    }

    Tile GetRandomTile()
    {
        int randomTile = Random.Range(0, tetrominoTiles.Length);

        return tetrominoTiles[randomTile];
    }

    private void GameOver()
    {
        gameOver = true;

        if ((int)currentTime > PlayerPrefs.GetInt("TetrisTopTime"))
        {
            PlayerPrefs.SetInt("TetrisTopTime", (int)currentTime);
        }

        int totalTime = PlayerPrefs.GetInt("TetrisTimeSpent");
        totalTime += (int)currentTime;
        PlayerPrefs.SetInt("TetrisTimeSpent", totalTime);

        PlayGameOverSound();

        gameOverPanel.SetActive(true);
    }

    public bool IsGameOver()
    {
        return gameOver;
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

    // checks if any lines full, for each full row it clears the line and shifts everything down
    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int linesCleared = 0;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                rowsCleared++;
                rowClearText.text = rowsCleared.ToString();

                linesCleared++;
                CalculateLevel();
            }
            else
            {
                row++;
            }
        }

        if (linesCleared != 0)
        {
            CalculateScore(linesCleared);
        }
    }

    // checks to see if row is full of tiles
    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(pos))
            {
                return false;
            }
        }

        return true;
    }

    // clears a full row of tiles and shifts everything else down once
    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) // loops through and clears row
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(pos, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(pos);

                pos = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(pos, above);
            }

            row++;
        }

        int fragmentsEarned = Random.Range(0, 6);
        UpdateFragments(fragmentsEarned);
    }

    private void CalculateLevel()
    {
        int priorLevel = currentLevel;

        currentLevel = rowsCleared / 10;
        levelText.text = currentLevel.ToString();

        if (rowsCleared > PlayerPrefs.GetInt("TetrisHighRow"))
        {
            PlayerPrefs.SetInt("TetrisHighRow", rowsCleared);
        }

        if (priorLevel != currentLevel)
        {
            PlayLevelUpSound();
        }

        UpdateTetrominoSpeed();
    }

    void UpdateTetrominoSpeed()
    {
        float stepDelay;

        switch(currentLevel)
        {
            case 0:
                stepDelay = 0.8f;
                break;
            case 1:
                stepDelay = 0.72f;
                break;
            case 2:
                stepDelay = 0.64f;
                break;
            case 3:
                stepDelay = 0.55f;
                break;
            case 4:
                stepDelay = 0.47f;
                break;
            case 5:
                stepDelay = 0.39f;
                break;
            case 6:
                stepDelay = 0.3f;
                break;
            case 7:
                stepDelay = 0.22f;
                break;
            case 8:
                stepDelay = 0.14f;
                break;
            case 9:
                stepDelay = 0.1f;
                break;
            case 10:
            case 11:
            case 12:
                stepDelay = 0.09f;
                break;
            case 13:
            case 14:
            case 15:
                stepDelay = 0.07f;
                break;
            case 16:
            case 17:
            case 18:
                stepDelay = 0.05f;
                break;
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
                stepDelay = 0.04f;
                break;
            default:
                stepDelay = 0.017f;
                break;
        }

        activePiece.UpdateStepDelay(stepDelay);
    }

    private void CalculateScore(int linesCleared)
    {
        if (linesCleared == 4)
        {
            PlayTetrisSound();
        }
        else
        {
            PlayClearSound();
        }

        switch(linesCleared)
        {
            case 1:
                currentScore += (40 * (currentLevel + 1));
                break;
            case 2:
                currentScore += (100 * (currentLevel + 1));
                break;
            case 3:
                currentScore += (300 * (currentLevel + 1));
                break;
            case 4:
                currentScore += (1200 * (currentLevel + 1));
                break;
            default:
                Debug.LogError("Invalid LinesCleared case! Board script --> CalculateScore Method");
                break;
        }

        currentScoreText.text = currentScore.ToString();
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("TetrisHighScore", highScore);
        }
    }

    void UpdateFragments(int fragmentsEarned)
    {
        fragments += fragmentsEarned;
        PlayerPrefs.SetInt("TetrisFragments", fragments);

        tetrisFragmentText.text = fragments.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void SelectSong()
    {
        musicSource.clip = songs[PlayerPrefs.GetInt("TetrisSong")];
        musicSource.Play();
    }

    public void PlayMoveSound()
    {
        sfxSource.PlayOneShot(moveSound, volume);
    }

    public void PlayRotateSound()
    {
        sfxSource.PlayOneShot(rotateSound, volume);
    }

    public void PlayLockSound()
    {
        sfxSource.PlayOneShot(lockSound, volume);
    }

    public void PlayClearSound()
    {
        sfxSource.PlayOneShot(clearSound, volume);
    }

    public void PlayTetrisSound()
    {
        sfxSource.PlayOneShot(tetrisSound, volume);
    }

    public void PlayLevelUpSound()
    {
        sfxSource.PlayOneShot(levelUpSound, volume);
    }

    public void PlayGameOverSound()
    {
        sfxSource.PlayOneShot(gameOverSound, volume);
    }
}
