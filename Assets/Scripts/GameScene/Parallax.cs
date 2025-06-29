using UnityEngine;
using Utils;

namespace GameScene
{
    public class ParallaxController : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private Transform _cam; //Main Camera
        private Vector3 _camStartPos;
        private float _distance; //jarak antara start _camera posisi dan current posisi

        private GameObject[] _backgrounds;
        private Material[] _mat;
        private float[] _backSpeed;

        private float _farthestBack;

        [Range(0.01f, 0.05f)]
        public float parallaxSpeed;

        // Start is called before the first frame update
        void Start()
        {
            _cam = Helpers.Cam.transform;
            _camStartPos = _cam.position;

            int backCount = transform.childCount;
            _mat = new Material[backCount];
            _backSpeed = new float[backCount];
            _backgrounds = new GameObject[backCount];

            for (int i = 0; i < backCount; i++)
            {
                _backgrounds[i] = transform.GetChild(i).gameObject;
                _mat[i] = _backgrounds[i].GetComponent<Renderer>().material;
            }

            BackSpeedCalculate(backCount);
        }

        void BackSpeedCalculate(int backCount)
        {
            for (int i = 0; i < backCount; i++) //find the farthest background
            {
                if((_backgrounds[i].transform.position.z - _cam.position.z) > _farthestBack)
                {
                    _farthestBack = _backgrounds[i].transform.position.z - _cam.position.z;
                }
            }

            for (int i = 0; i < backCount; i++) //set the speed of bacground
            {
                _backSpeed[i] = 1 - (_backgrounds[i].transform.position.z - _cam.position.z) / _farthestBack;
            }
        }

        private void LateUpdate()
        {
            _distance = _cam.position.x - _camStartPos.x;
            transform.position = new Vector3(_cam.position.x, transform.position.y, 0);

            for (var i = 0; i < _backgrounds.Length; i++)
            {
                float speed = _backSpeed[i] * parallaxSpeed;
                _mat[i].SetTextureOffset(MainTex, new Vector2(_distance, 0) * speed);
            }
        }

    
    }
}