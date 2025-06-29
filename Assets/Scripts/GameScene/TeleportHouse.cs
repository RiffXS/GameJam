using System.Collections;
using UnityEngine;
using Utils;
using UI;

namespace GameScene
{
    public class TeleportHouse : MonoBehaviour
    {
        private bool _playerInRange;
        private Transform _playerTransform;
        [SerializeField] private GameObject destinationHouse;
        [SerializeField] private float yOffset = 0.1f;
        [SerializeField] private bool turnedToRight = true;

        private BlackScreenController _blackScreenController => BlackScreenController.I;
        private UIManager _uiManager => UIManager.I;
        
        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        }

        private void Update()
        {
            if (!_playerInRange) return;
            if (!Input.GetKeyDown(KeyCode.Return)) return;
            StartCoroutine(TeleportPlayer());
            _playerInRange = false;
            
        }

        IEnumerator TeleportPlayer()
        {
            _playerTransform.GetComponent<Rigidbody2D>().simulated = false;
            _blackScreenController.FadeInBlack();
            yield return new WaitForSeconds(Helpers.BlackFadeTime);
            _playerTransform.localPosition = destinationHouse.transform.position - new Vector3(0, yOffset, 0);
            _playerTransform.localScale = new Vector3((turnedToRight ? _playerTransform.localScale.x : -_playerTransform.localScale.x), _playerTransform.localScale.y, _playerTransform.localScale.z);
            yield return new WaitForSeconds(Helpers.BlackFadeTime * 2);
            _blackScreenController.FadeOutBlack();
            yield return new WaitForSeconds(Helpers.BlackFadeTime);
            _playerTransform.GetComponent<Rigidbody2D>().simulated = true;
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
