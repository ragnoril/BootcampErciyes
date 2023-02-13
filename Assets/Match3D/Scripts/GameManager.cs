using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3D
{
    public class GameManager : MonoBehaviour
    {
        public UIManager UI;

        public GameObject[] Prefabs;

        public Transform Pool;

        public List<SelectableItem> Items;

        public Camera Cam;

        /*
        public SelectableItem SelectedItem;
        private Vector3 _screenPoint;
        private Vector3 _offset;
        */

        public List<SelectableItem> ItemsToCheck;

        public event Action<SelectableItem> OnItemInsideChecker;
        public event Action<SelectableItem> OnItemLeftChecker;
        public event Action OnItemMatch;
        public event Action OnGameOver;

        public int Score;

        private void Start()
        {
            Score = 0;

            UI.Init(this);

            Items = new List<SelectableItem>();
            ItemsToCheck = new List<SelectableItem>();
            GenerateItems();

            OnItemInsideChecker += NewItemInsideChecker;
            OnItemLeftChecker += ItemLeftChecker;
        }

        private void ItemLeftChecker(SelectableItem item)
        {
            if (ItemsToCheck.Contains(item))
                ItemsToCheck.Remove(item);
        }

        private void NewItemInsideChecker(SelectableItem item)
        {
            if (!ItemsToCheck.Contains(item))
                ItemsToCheck.Add(item);

            if (ItemsToCheck.Count == 2)
            {
                if (ItemsToCheck[0].ItemId == ItemsToCheck[1].ItemId)
                {
                    HandleMatch();
                    CheckWinCondition();
                }
            }
        }

        private void CheckWinCondition()
        {
            if (Items.Count == 0)
            {
                OnGameOver?.Invoke();
            }
        }


        private void HandleMatch()
        {
            foreach(SelectableItem item in ItemsToCheck)
            {
                Items.Remove(item);
                Destroy(item.gameObject);
            }

            ItemsToCheck.Clear();
            Score += 200;
            OnItemMatch?.Invoke();
        }

        public void ItemPlacedInsideChecker(SelectableItem item)
        {
            OnItemInsideChecker?.Invoke(item);
        }

        public void ItemRemovedFromChecker(SelectableItem item)
        {
            OnItemLeftChecker?.Invoke(item);
        }

        /*
        private void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                Ray mouseRay = Cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out hitInfo))
                {
                    Debug.Log("hit: " + hitInfo);
                    SelectableItem item = hitInfo.collider.GetComponent<SelectableItem>();
                    if (item != null)
                    {
                        SelectedItem = item;
                        _screenPoint = Cam.WorldToScreenPoint(SelectedItem.transform.position);
                        _offset = SelectedItem.transform.position - Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SelectedItem = null;
            }

            if (Input.GetMouseButton(0) && SelectedItem != null)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                Vector3 curPosition = Cam.ScreenToWorldPoint(curScreenPoint)+ _offset;
                curPosition.y = 2.5f;
                SelectedItem.transform.position = curPosition;
            }
        }
            */

        public void GenerateItems()
        {
            foreach(GameObject prefab in Prefabs)
            {
                GenerateItemInRandomPlace(prefab);
                GenerateItemInRandomPlace(prefab);
            }
        }

        private void GenerateItemInRandomPlace(GameObject prefab)
        {
            Vector3 objPos = Pool.transform.position;
            objPos.y = 1f;
            objPos.x += UnityEngine.Random.insideUnitCircle.x * 5f;
            objPos.z += UnityEngine.Random.insideUnitCircle.y * 5f;

            GameObject go = Instantiate(prefab, objPos, Quaternion.identity);
            go.transform.SetParent(Pool);
            SelectableItem item = go.GetComponent<SelectableItem>();
            item.Init(this);
            Items.Add(item);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(0);
        }
    }

}