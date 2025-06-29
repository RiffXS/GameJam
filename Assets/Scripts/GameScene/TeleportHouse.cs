using UnityEngine;

namespace GameScene
{
    public class TeleportHouse : MonoBehaviour
    {
        private bool _playerInRange;
        private Transform _playerTransform;
        [SerializeField] private GameObject destinationHouse;
        [SerializeField] private float yOffset = 0.1f;
        [SerializeField] private bool turnedToRight = true;

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (!_playerInRange) return;
            if (!Input.GetKeyDown(KeyCode.Return)) return;
            _playerTransform.position = destinationHouse.transform.position - new Vector3(0, yOffset, 0);
            _playerTransform.localScale = new Vector3((turnedToRight ? _playerTransform.localScale.x : -_playerTransform.localScale.x), _playerTransform.localScale.y, _playerTransform.localScale.z);
            _playerInRange = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
            }
        }
    }
}
