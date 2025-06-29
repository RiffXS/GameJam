using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
    
        [SerializeField] private PlayerObject[] playerObjects;
        [HideInInspector] public int currentTransformation;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    
        public void Transformation(int transformation)
        {
            Debug.Log(transformation + " " + currentTransformation);
            playerObjects[transformation].transform.position = playerObjects[currentTransformation].transform.position;
            playerObjects[transformation].gameObject.SetActive(false);
            playerObjects[currentTransformation].gameObject.SetActive(true);
            currentTransformation = transformation;
        }

        void Start()
        {
            for (int i = 0; i < playerObjects.Length; i++)
            {
                if (i == currentTransformation) playerObjects[i].gameObject.SetActive(true);
                else playerObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
