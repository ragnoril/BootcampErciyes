using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyGame
{

    public class GameManager : MonoBehaviour
    {
        public BoardDataSO[] Boards;

        public int LevelIndex;


        public BoardManager Board;
        public UIManager UI;


        private void Start()
        {
            Board.Init(this, Boards[LevelIndex % Boards.Length]);
            UI.Init(this);

        }

    }

}