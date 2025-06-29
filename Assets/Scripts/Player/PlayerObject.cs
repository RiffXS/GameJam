using UnityEngine;

namespace Player
{
    public class PlayerObject : MonoBehaviour
    {
        void Update()
        {
            gameObject.transform.parent.transform.position = gameObject.transform.position;
        }
    }
}
