using System.Collections;
using UnityEngine;
using Utils.Singleton;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerObject[] playerObjects;
        
        [HideInInspector] public int currentTransformation;
        
        protected override void Awake()
        {
            base.Awake();
            for (var i = 0; i < playerObjects.Length; i++)
            {
                playerObjects[i].gameObject.SetActive(i == currentTransformation);
            }
        }
        
        public void TransformTo(int transformation)
        {
            StartCoroutine(Transformation(transformation));
        }

        private IEnumerator Transformation(int transformation)
        {
            playerObjects[currentTransformation].FreezePlayer(true);
            yield return new WaitForSeconds(2f);
            playerObjects[currentTransformation].FreezePlayer(false);
            
            playerObjects[transformation].transform.position = playerObjects[currentTransformation].transform.position;
            playerObjects[transformation].gameObject.SetActive(true);
            playerObjects[currentTransformation].gameObject.SetActive(false);
            currentTransformation = transformation;
        }
    }
}
