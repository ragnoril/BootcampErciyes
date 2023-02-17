using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyGame
{

    public class BoardManager : MonoBehaviour
    {
        public int BoardWidth;
        public int BoardHeight;

        public int[] GameBoard;

        public string TilePrefabName;
        public Sprite[] TilePrefabSprites;
        public string EmptyTilePrefabName;
        public string ExplosionPrefabName;

        public Tile SelectedTile;
        public Tile SwapTile;

        public int MoveCount;
        public int PopCount;

        public event Action<int> OnBoardMove;
        public event Action<int> OnTilesPopped;

        private GameManager _manager;

        public void InitBoard(BoardDataSO boardData)
        {
            MoveCount = 0;
            PopCount = 0;

            BoardWidth = boardData.BoardWidth;
            BoardHeight = boardData.BoardHeight;
            GameBoard = new int[BoardWidth * BoardHeight];
            for(int i = 0; i < boardData.GameBoard.Length; i++)
            {
                GameBoard[i] = boardData.GameBoard[i];
            }

            TilePrefabSprites = new Sprite[boardData.TilePrefabSprites.Length];
            for (int i = 0; i < boardData.TilePrefabSprites.Length; i++)
            {
                TilePrefabSprites[i] = boardData.TilePrefabSprites[i];
            }

            //FillBoardRandom();
            RenderBoard();

        }

        public void FillBoardRandom()
        {
            for(int i =0; i < GameBoard.Length; i++)
            {
                GameBoard[i] = UnityEngine.Random.Range(0, TilePrefabSprites.Length);
            }
        }


        public void RenderBoard()
        {
            for (int j = 0; j < BoardHeight; j++)
            {
                for (int i = 0; i < BoardWidth; i++)
                {
                    GameObject emptyTile = Instantiate(Resources.Load<GameObject>(EmptyTilePrefabName));
                    emptyTile.transform.SetParent(transform);
                    emptyTile.transform.localPosition = new Vector3(i, -j, 0f);

                    int tileId = GameBoard[GetBoardPosition(i, j)];

                    if (tileId < 0) return;
                    CreateTile(tileId, i, j);
                }
            }
        }

        public void CreateTile(int tileId, int x, int y)
        {
            GameObject tileObject = Instantiate(Resources.Load<GameObject>(TilePrefabName));
            tileObject.transform.SetParent(transform);
            tileObject.transform.localPosition = new Vector3(x, -y, 0f);

            Tile tile = tileObject.GetComponent<Tile>();
            tile.TileType = tileId;
            tile.SetSprite(TilePrefabSprites[tileId]);
        }

        public int GetBoardPosition(int x, int y)
        {
            return (y * BoardWidth) + x;
        }

        public Tile GetTile(int x, int y)
        {
            Collider2D tileHit = Physics2D.OverlapPoint(new Vector2(transform.position.x + x, transform.position.y - y));

            if (tileHit == null) return null;

            return tileHit.GetComponent<Tile>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hitInfo.collider != null)
                {
                    SelectedTile = hitInfo.collider.GetComponent<Tile>();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                int selectedX = SelectedTile.GetX();
                int selectedY = SelectedTile.GetY();

                int swipeX = (int)Mathf.Clamp(Mathf.Round(mousePos.x - SelectedTile.transform.position.x), -1f, 1f);
                int swipeY = (int)Mathf.Clamp(Mathf.Round(SelectedTile.transform.position.y - mousePos.y), -1f, 1f);

                int x = selectedX + swipeX;
                int y = selectedY + swipeY;

                SwapTile = GetTile(x, y);
                
                if (SwapTile != null)
                {
                    int selectedPos = GetBoardPosition(selectedX, selectedY);
                    int swapPos = GetBoardPosition(SwapTile.GetX(), SwapTile.GetY());

                    GameBoard[selectedPos] = SwapTile.TileType;
                    GameBoard[swapPos] = SelectedTile.TileType;

                    Vector3 tempPos = SwapTile.transform.position;
                    SwapTile.transform.position = SelectedTile.transform.position;
                    SelectedTile.transform.position = tempPos;

                    CheckForCombos();
                    
                    StartCoroutine(HandleEmptySpaces());

                    MoveCount += 1;
                    OnBoardMove(MoveCount);
                }
                
            }
        }

        IEnumerator HandleEmptySpaces()
        {
            yield return new WaitForSeconds(1f);
            CheckForEmptySpaces();
            yield return new WaitForSeconds(0.5f);
            FillEmptySpaces();
        }

        private void FillEmptySpaces()
        {
            for(int i = 0; i < BoardWidth; i++)
            {
                for (int j = 0; j < BoardHeight; j++)
                {
                    int pos = GetBoardPosition(i, j);
                    if (GameBoard[pos] < 0)
                    {
                        GameBoard[pos] = UnityEngine.Random.Range(0, TilePrefabSprites.Length);
                        CreateTile(GameBoard[pos], i, j);
                    }
                }
            }
        }

        private void CheckForEmptySpaces()
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                for (int j = (BoardHeight - 2); j > -1; j--)
                {
                    if (GameBoard[GetBoardPosition(i, j)] < 0) continue;

                    Tile tile = GetTile(i, j);
                    int y = j + 1;

                    while (y < BoardHeight && GameBoard[GetBoardPosition(i, y)] < 0)
                    {
                        if (tile == null)
                        {
                            tile = GetTile(i, y - 1);
                        }

                        tile.transform.localPosition = new Vector2(i, -y);
                        GameBoard[GetBoardPosition(i, y - 1)] = -1;
                        GameBoard[GetBoardPosition(i, y)] = tile.TileType;

                        y++;
                    }
                }
            }
        }

        private void CheckForCombos()
        {
            PopCount += CheckCombosForTile(SelectedTile);
            PopCount += CheckCombosForTile(SwapTile);
            OnTilesPopped?.Invoke(PopCount);
        }

        private int CheckCombosForTile(Tile comboTile)
        {
            int popCount = 0;
            int comboX = comboTile.GetX();
            int comboY = comboTile.GetY();

            List<Tile> comboH = CheckCombosHorizontal(comboX, comboY, comboTile.TileType);
            List<Tile> comboV = CheckCombosVertical(comboX, comboY, comboTile.TileType);

            bool isComboTilePops = false;

            if (comboH.Count > 1)
            {
                foreach (Tile tile in comboH)
                {
                    popCount += 1;
                    PopTile(tile);
                }

                isComboTilePops = true;
            }

            if (comboV.Count > 1)
            {
                foreach (Tile tile in comboV)
                {
                    popCount += 1;
                    PopTile(tile);
                }

                isComboTilePops = true;
            }

            if (isComboTilePops)
            {
                popCount += 1;
                PopTile(SelectedTile);
            }

            return popCount;
        }

        private List<Tile> CheckCombosVertical(int x, int y, int tileType)
        {
            List<Tile> comboList = new List<Tile>();

            int preY = y - 1;
            int postY = y + 1;

            while (preY > -1 || postY < BoardHeight)
            {
                if (preY > -1)
                {
                    int prePos = GetBoardPosition(x, preY);
                    if (GameBoard[prePos] == tileType)
                    {
                        Tile tile = GetTile(x, preY);
                        if (tile != null)
                        {
                            comboList.Add(tile);
                            preY -= 1;
                        }
                    }
                    else
                    {
                        preY = -1;
                    }
                }

                if (postY < BoardHeight)
                {
                    int postPos = GetBoardPosition(x, postY);
                    if (GameBoard[postPos] == tileType)
                    {
                        Tile tile = GetTile(x, postY);
                        if (tile != null)
                        {
                            comboList.Add(tile);
                            postY += 1;
                        }
                    }
                    else
                    {
                        postY = BoardHeight;
                    }
                }
            }

            return comboList;
        }

        private List<Tile> CheckCombosHorizontal(int x, int y, int tileType)
        {
            List<Tile> comboList = new List<Tile>();

            int preX = x - 1;
            int postX = x + 1;

            while (preX > -1 || postX < BoardWidth) 
            {
                if (preX > -1)
                {
                    int prePos = GetBoardPosition(preX, y);
                    if (GameBoard[prePos] == tileType)
                    {
                        Tile tile = GetTile(preX, y);
                        if (tile != null)
                        {
                            comboList.Add(tile);
                            preX -= 1;
                        }
                    }
                    else
                    {
                        preX = -1;
                    }
                }

                if (postX < BoardWidth)
                {
                    int postPos = GetBoardPosition(postX, y);
                    if (GameBoard[postPos] == tileType)
                    {
                        Tile tile = GetTile(postX, y);
                        if (tile != null)
                        {
                            comboList.Add(tile);
                            postX += 1;
                        }
                    }
                    else
                    {
                        postX = BoardWidth;
                    }
                }
            }

            return comboList;
        }

        private void PopTile(Tile tile)
        {
            int tileX = tile.GetX();
            int tileY = tile.GetY();

            GameBoard[GetBoardPosition(tileX, tileY)] = -1;

            GameObject explosion = Instantiate(Resources.Load<GameObject>(ExplosionPrefabName));
            explosion.transform.position = tile.transform.position;

            Destroy(explosion, 1f);
            Destroy(tile.gameObject);
        }

        public void Init(GameManager gameManager, BoardDataSO boardData)
        {
            _manager = gameManager;

            InitBoard(boardData);
        }
    }
}