using Player;
using UnityEngine;

namespace Foods
{
    public class NormalFoodObject : FoodObject
    {
        protected override void PlayerEntered()
        {
            PlayerController player = PlayerController.Instance;
            
            player.Transformation(player.currentTransformation + 1);
        
            Destroy(gameObject);
        }
    }
}
