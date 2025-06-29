using GameScene.GrapplingHook;
using UnityEngine;

namespace Player
{
    public class FeetController : MonoBehaviour
    {
        [SerializeField] GrapplingGun grapplingGun;

        private void IsGrounded(bool grounded)
        {
            grapplingGun.canGrapple = grounded;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("FallingPlatform"))
            {
                IsGrounded(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsGrounded(false);
        }
    }
}
