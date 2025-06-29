using UnityEngine;

namespace Player
{
    public class PlayerObject : MonoBehaviour
    {
        private void Update()
        {
            gameObject.transform.parent.transform.position = gameObject.transform.position;
        }
    }
}
