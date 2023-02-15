using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CandyGame
{

    public class UIManager : MonoBehaviour
    {
        private GameManager _manager;

        public TMP_Text MoveText;
        public TMP_Text PopText;

        public void Init(GameManager gameManager)
        {
            _manager = gameManager;

            _manager.Board.OnBoardMove += MoveUpdate;
            _manager.Board.OnTilesPopped += PopUpdate;
        }

        private void MoveUpdate(int moveCount)
        {
            MoveText.text = "Move: " + moveCount.ToString();
        }

        private void PopUpdate(int popCount)
        {
            PopText.text = "Pop: " + popCount.ToString();
        }
    }

}