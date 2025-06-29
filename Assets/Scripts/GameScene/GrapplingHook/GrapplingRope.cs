using UnityEngine;

namespace GameScene.GrapplingHook
{
    public class GrapplingRope : MonoBehaviour
    {
        [Header("General References:")]
        public GrapplingGun grapplingGun;
        public LineRenderer lineRenderer;

        [Header("General Settings:")]
        [SerializeField] private int precision = 40;
        [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

        [Header("Rope Animation Settings:")]
        public AnimationCurve ropeAnimationCurve;
        [Range(0.01f, 4)] [SerializeField] private float startWaveSize = 2;
        float _waveSize;

        [Header("Rope Progression:")]
        public AnimationCurve ropeProgressionCurve;
        [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

        float _moveTime;

        [HideInInspector] public bool isGrappling = true;

        bool _straightLine = true;

        private void OnEnable()
        {
            _moveTime = 0;
            lineRenderer.positionCount = precision;
            _waveSize = startWaveSize;
            _straightLine = false;

            LinePointsToFirePoint();

            lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            lineRenderer.enabled = false;
            isGrappling = false;
        }

        private void LinePointsToFirePoint()
        {
            for (int i = 0; i < precision; i++)
            {
                lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
            }
        }

        private void Update()
        {
            _moveTime += Time.deltaTime;
            DrawRope();
        }

        void DrawRope()
        {
            if (!_straightLine)
            {
                if (Mathf.Abs(lineRenderer.GetPosition(precision - 1).x - grapplingGun.grapplePoint.x) <= 0.04)
                {
                    _straightLine = true;
                }
                else
                {
                    DrawRopeWaves();
                }
            }
            else
            {
                if (!isGrappling)
                {
                    grapplingGun.Grapple();
                    isGrappling = true;
                }
                if (_waveSize > 0)
                {
                    _waveSize -= Time.deltaTime * straightenLineSpeed;
                    DrawRopeWaves();
                }
                else
                {
                    _waveSize = 0;

                    if (lineRenderer.positionCount != 2) { lineRenderer.positionCount = 2; }

                    DrawRopeNoWaves();
                }
            }
        }

        void DrawRopeWaves()
        {
            for (int i = 0; i < precision; i++)
            {
                float delta = i / (precision - 1f);
                Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * (ropeAnimationCurve.Evaluate(delta) * _waveSize);
                Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
                Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(_moveTime) * ropeProgressionSpeed);

                lineRenderer.SetPosition(i, currentPosition);
            }
        }

        void DrawRopeNoWaves()
        {
            lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
            lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
        }
    }
}
