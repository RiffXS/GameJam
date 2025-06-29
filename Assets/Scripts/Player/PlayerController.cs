using UnityEngine;
using Utils.Singleton;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private PlayerObject[] playerObjects;
        [HideInInspector] public int currentTransformation;
    
        public void Transformation(int transformation)
        {
            playerObjects[transformation].transform.position = playerObjects[currentTransformation].transform.position;
            playerObjects[transformation].gameObject.SetActive(true);
            playerObjects[currentTransformation].gameObject.SetActive(false);
            currentTransformation = transformation;
        }

        private void Start()
        {
            for (var i = 0; i < playerObjects.Length; i++)
            {
                playerObjects[i].gameObject.SetActive(i == currentTransformation);
            }
        }
    }
}
