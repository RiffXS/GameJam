using Player;

namespace Foods
{
    public class NormalFoodObject : FoodObject
    {
        protected override void PlayerEntered()
        {
            PlayerController player = PlayerController.I;
            
            player.TransformTo(player.currentTransformation + 1);
        
            Destroy(gameObject);
        }
    }
}
