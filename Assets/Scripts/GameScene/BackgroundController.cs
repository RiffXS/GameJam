using UnityEngine;

namespace GameScene
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] Parallax[] parallaxArray;

        public void ChangeStateParallax(bool estado)
        {
            foreach (Parallax p in parallaxArray)
            {
                p.MudarEstadoParallax(estado);
            }
        }
    }
}

