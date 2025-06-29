using System;
using System.Collections;
using UnityEngine;
using Utils.Singleton;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerObject[] playerObjects;
        
        [HideInInspector] public int currentTransformation;
        
        private GameObject currentPlayer;
        protected override void Awake()
        {
            base.Awake();
            for (var i = 0; i < playerObjects.Length; i++)
            {
                currentPlayer = playerObjects[i].gameObject;
                playerObjects[i].gameObject.SetActive(i == currentTransformation);
            }
        }

        private void Update()
        {
            transform.position = currentPlayer.transform.position;
        }

        public void TransformTo(int transformation)
        {
            StartCoroutine(Transformation(transformation));
        }

        private IEnumerator Transformation(int transformation)
        {
            currentPlayer = playerObjects[currentTransformation].gameObject;
            playerObjects[currentTransformation].FreezePlayer(true);
            yield return new WaitForSeconds(2f);
            playerObjects[currentTransformation].FreezePlayer(false);
            
            currentPlayer.gameObject.SetActive(true);
            currentPlayer.gameObject.SetActive(false);
            currentTransformation = transformation;
        }
    }
}
