using UnityEngine;

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
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (i == transformation)
            {
                playerObjects[i].gameObject.SetActive(true);
            }
            else
            {
                playerObjects[i].gameObject.SetActive(false);
            }
        }
    }

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transformation(currentTransformation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
