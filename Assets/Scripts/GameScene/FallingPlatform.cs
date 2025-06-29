using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameScene
{
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private float fallWait = 2f;
        [SerializeField] private float destroyWait = 1f;
        [SerializeField] private SpriteRenderer spriteRenderer;
    
        private bool _isFalling;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isFalling && other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(FallCoroutine());
            }
        }

        private IEnumerator FallCoroutine()
        {
            _isFalling = true;
            yield return new WaitForSeconds(fallWait);
            spriteRenderer.DOFade(0, destroyWait).OnComplete(()=>Destroy(gameObject, destroyWait));
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Fall()
        {
            StartCoroutine(FallCoroutine());
        }
    }
}
