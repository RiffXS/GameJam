using System;
using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallWait = 2f;
    public float destroyWait = 1f;

    private bool _isFalling;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
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
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyWait);
    }

    public void Fall()
    {
        StartCoroutine(FallCoroutine());
    }
}
