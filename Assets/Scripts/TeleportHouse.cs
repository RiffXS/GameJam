using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TeleportHouse : MonoBehaviour
{
    private bool playerInRange;
    private Transform playerTransform;
    [SerializeField] private GameObject destinationHouse;
    [SerializeField] private float yOffset = 0.1f;
    [SerializeField] private bool turnedToRight = true;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playerTransform.position = destinationHouse.transform.position - new Vector3(0, yOffset, 0);
                playerTransform.localScale = new Vector3((turnedToRight ? playerTransform.localScale.x : -playerTransform.localScale.x), playerTransform.localScale.y, playerTransform.localScale.z);
                playerInRange = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
