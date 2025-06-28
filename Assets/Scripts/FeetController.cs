using System;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    [SerializeField] GrapplingGun grapplingGun;

    void IsGrounded(bool grounded)
    {
        grapplingGun.canGrapple = grounded;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            IsGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsGrounded(false);
    }
}
