using UnityEngine;

namespace Player
{
    public class PlayerObject : MonoBehaviour
    {
        [SerializeField] private FeetController feetController;
        [SerializeField] private new Rigidbody2D rigidbody;
        
        public void FreezePlayer(bool freeze)
        {
            feetController.IsGrounded(!freeze);
            rigidbody.simulated = !freeze;
        }
        
        private void Update()
        {
            gameObject.transform.parent.transform.position = gameObject.transform.position;
        }
    }
}
