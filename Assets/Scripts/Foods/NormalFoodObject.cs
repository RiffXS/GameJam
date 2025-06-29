using UnityEngine;

namespace Foods
{
    public class NormalFoodObject : FoodObject
    {
        protected override void PlayerEntered(PlayerController player)
        {
            player.Transformation(player.currentTransformation + 1);
        
            Destroy(gameObject);
        }
    }
}
