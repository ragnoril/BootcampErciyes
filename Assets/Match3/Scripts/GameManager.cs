using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyGame
{

    public class GameManager : MonoBehaviour
    {

        public BoardManager Board;
        public UIManager UI;


        private void Start()
        {
            Board.Init(this);
            UI.Init(this);
        }

    }

}