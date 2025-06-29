using UnityEngine;

namespace Foods
{
    public class FoodObject : MonoBehaviour
    {
        protected virtual void PlayerEntered() { }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerEntered();
            }
        }
    }
}
