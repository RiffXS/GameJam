using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Foods
{
    public abstract class FoodObject : MonoBehaviour
    {
        private bool _playerInRange;
        private UIManager _uiManager => UIManager.I;

        protected abstract IEnumerator EatFoodCoroutine();


        private void Update()
        {
            if (!_playerInRange) return;
            if (!Input.GetKeyDown(KeyCode.Return)) return;
            EatFood();
            _playerInRange = false;
        }
        
        private void EatFood()
        {
            _uiManager.ControlEnterInteraction(false);
            // sound effect of eating
            StartCoroutine(EatFoodCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = true;
                _uiManager.ControlEnterInteraction(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
                _uiManager.ControlEnterInteraction(false);
            }
        }
    }
}
