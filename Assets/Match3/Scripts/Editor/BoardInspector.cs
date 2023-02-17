using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace CandyGame
{

    [CustomEditor(typeof(BoardManager))]
    public class BoardInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            BoardManager manager = (BoardManager)target;

            DrawDefaultInspector();
            EditorGUILayout.HelpBox("This is a board manager!", MessageType.Info);
            GUILayout.Space(20);
            EditorGUILayout.HelpBox("This is also a board manager!", MessageType.Warning);

            if (GUILayout.Button("Fill Board Randomly"))
            {
                manager.GameBoard = new int[manager.BoardHeight * manager.BoardWidth];
                manager.FillBoardRandom();
                manager.RenderBoard();
            }

            if (GUILayout.Button("Create New Board Data"))
            {
                CreateBoardData(manager);
            }

            if (GUILayout.Button("Open Board Editor"))
            {

                BoardEditor window = (BoardEditor)EditorWindow.GetWindow(typeof(BoardEditor));
                window.Show();
            }
        }

        private void CreateBoardData(BoardManager manager)
        {
            Debug.Log("Create Board Data SO");
            BoardDataSO boardData = ScriptableObject.CreateInstance<BoardDataSO>();
            boardData.BoardHeight = manager.BoardHeight;
            boardData.BoardWidth = manager.BoardWidth;

            boardData.GameBoard = new int[boardData.BoardWidth * boardData.BoardHeight];
            for (int i = 0; i < manager.GameBoard.Length; i++)
            {
                boardData.GameBoard[i] = manager.GameBoard[i];
            }

            boardData.TilePrefabSprites = new Sprite[manager.TilePrefabSprites.Length];
            for (int i = 0; i < manager.TilePrefabSprites.Length; i++)
            {
                boardData.TilePrefabSprites[i] = manager.TilePrefabSprites[i];
            }

            string path = "Assets/Match3/ScriptableObjects/Board_New.asset";
            AssetDatabase.CreateAsset(boardData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }
    }
}