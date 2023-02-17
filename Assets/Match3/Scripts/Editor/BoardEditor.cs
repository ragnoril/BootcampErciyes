using PlasticGui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CandyGame
{

    public class BoardEditor : EditorWindow
    {
        int _boardWidth;
        int _boardHeight;

        int[] _gameBoard;

        List<string> _brushes;
        List<Sprite> _sprites;
        Sprite _spriteToAdd;
        int _selectedBrush;

        bool _isBoardSet;

        Vector2 _scrollPos;

        [MenuItem("Tools/Board Editor")]
        static void Init()
        {
            BoardEditor window = (BoardEditor) EditorWindow.GetWindow(typeof(BoardEditor));
            window.Show();
        }

        private void OnEnable()
        {
            _brushes = new List<string>();
            _sprites = new List<Sprite>();
            _isBoardSet = false;
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Board Editor for Candy Game");

            GUILayout.Space(10);

            _boardWidth = EditorGUILayout.IntField("Board Width", _boardWidth);
            _boardHeight = EditorGUILayout.IntField("Board Height", _boardHeight);

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            _spriteToAdd = (Sprite)EditorGUILayout.ObjectField("Tile Sprite", _spriteToAdd, typeof(Sprite));
            if (GUILayout.Button("Add Sprite"))
            {
                _sprites.Add(_spriteToAdd);
                _brushes.Add(_spriteToAdd.name);
                _spriteToAdd = null;
            }
            EditorGUILayout.EndHorizontal();

            _selectedBrush = EditorGUILayout.Popup("Selected Tile", _selectedBrush, _brushes.ToArray());

            GUILayout.Space(10);
            if (GUILayout.Button("Create Board"))
            {
                CreateGameBoard();
            }

            
            if (_isBoardSet)
            {
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                DrawGameBoard();
                EditorGUILayout.EndScrollView();

                if (GUILayout.Button("Save Board"))
                {
                    SaveGameBoard();
                }
            }

            
        }

        private void SaveGameBoard()
        {
            Debug.Log("Create Board Data SO");
            BoardDataSO boardData = ScriptableObject.CreateInstance<BoardDataSO>();
            boardData.BoardHeight = _boardHeight;
            boardData.BoardWidth = _boardWidth;

            boardData.GameBoard = new int[boardData.BoardWidth * boardData.BoardHeight];
            for (int i = 0; i < _gameBoard.Length; i++)
            {
                boardData.GameBoard[i] = _gameBoard[i];
            }

            boardData.TilePrefabSprites = new Sprite[_sprites.Count];
            for (int i = 0; i < _sprites.Count; i++)
            {
                boardData.TilePrefabSprites[i] = _sprites[i];
            }

            string path = "Assets/Match3/ScriptableObjects/Board_New2.asset";
            AssetDatabase.CreateAsset(boardData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }

        private void CreateGameBoard()
        {
            _gameBoard = new int[_boardHeight * _boardWidth];
            for (int i = 0; i < _gameBoard.Length; i++)
            {
                _gameBoard[i] = -1;
            }
            _isBoardSet = true;
        }

        private void DrawGameBoard()
        {
            GUILayout.BeginVertical();

            for (int j = 0; j < _boardHeight; j++)
            {
                GUILayout.BeginHorizontal();
                for (int i = 0; i < _boardWidth; i++)
                {
                    if (GUILayout.Button(CreateButtonTexture(_gameBoard[(_boardWidth * j) + i]),GUILayout.Width(60), GUILayout.Height(60)))
                    {
                        _gameBoard[(_boardWidth * j) + i] = _selectedBrush;
                    }
                }
                GUILayout.EndHorizontal();

            }

            GUILayout.EndVertical();
        }

        private GUIContent CreateButtonTexture(int tileId)
        {
            GUIContent buttonContent;

            switch(tileId)
            {
                case -1:
                    buttonContent = new GUIContent(" ");
                    break;
                default:
                    //Texture2D buttonTex = _sprites[tileId].texture;
                    //buttonContent = new GUIContent(buttonTex);
                    buttonContent = new GUIContent(tileId.ToString());
                    break;
            }

            return buttonContent;
        }
    }

}