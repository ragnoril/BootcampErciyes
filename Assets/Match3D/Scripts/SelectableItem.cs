using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3D
{
    public enum ItemType
    {
        Apple,
        Avocado,
        Bag,
        Banana,
        Barrel,
        Beet,
        Can,
        Cake,
        Carton,
        Fries,
        Grapes,
        Glass,
        Icecream,
        Ketchup,
        Mustard,
        Soup,
        Taco,
        Turkey,
        Wine
    }

    public class SelectableItem : MonoBehaviour
    {
        public ItemType ItemId;

        public GameManager Manager;

        private Vector3 _screenPoint;
        private Vector3 _offset;

        Rigidbody _rigidbody;

        public void Init(GameManager gameManager)
        {
            Manager = gameManager;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnMouseDown()
        {
            _screenPoint = Manager.Cam.WorldToScreenPoint(transform.position);
            _offset = transform.position - Manager.Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = Manager.Cam.ScreenToWorldPoint(curScreenPoint) + _offset;
            curPosition.y = 5f;
            transform.position = curPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ItemCheck")
            {
                Manager.ItemPlacedInsideChecker(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "ItemCheck")
            {
                Manager.ItemRemovedFromChecker(this);
            }
        }


    }
}
