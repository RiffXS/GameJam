using System.Collections;
using Player;
using UnityEngine;

namespace Foods
{
    public class NormalFoodObject : FoodObject
    {
        protected override IEnumerator EatFoodCoroutine()
        {
            yield return new WaitForSeconds(2);
            
            PlayerController player = PlayerController.I;
            player.TransformTo(player.currentTransformation + 1);
        
            Destroy(gameObject);
        }
    }
}
