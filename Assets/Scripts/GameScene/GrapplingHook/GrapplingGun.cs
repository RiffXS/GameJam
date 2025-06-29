using Player;
using UnityEngine;
using Utils;

namespace GameScene.GrapplingHook
{
    public class GrapplingGun : MonoBehaviour
    {
        [Header("Scripts Ref:")]
        public GrapplingRope grappleRope;

        [Header("Layers Settings:")]
        [SerializeField] private bool grappleToAll;
        [SerializeField] private int grappableLayerNumber = 9;

        [Header("Main Camera:")]
        private Camera Cam => Helpers.Cam;

        [Header("Transform Ref:")]
        public Transform gunHolder;
        public Transform gunPivot;
        public Transform firePoint;

        [Header("Physics Ref:")]
        public SpringJoint2D springJoint2D;
        public new Rigidbody2D rigidbody;

        [Header("Rotation:")]
        [SerializeField] private bool rotateOverTime = true;
        [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

        [Header("Distance:")]
        [SerializeField] private bool hasMaxDistance;
        [SerializeField] private float maxDistance = 20;

        private enum LaunchType
        {
            TransformLaunch,
            PhysicsLaunch
        }

        [Header("Launching:")]
        [SerializeField] private bool launchToPoint = true;
        [SerializeField] private LaunchType launchType = LaunchType.PhysicsLaunch;
        [SerializeField] private float launchSpeed = 1;

        [Header("No Launch To Point")]
        [SerializeField] private bool autoConfigureDistance;
        [SerializeField] private float targetDistance = 3;
        [SerializeField] private float targetFrequency = 1;

        [HideInInspector] public bool canGrapple = true;
        [HideInInspector] public Vector2 grapplePoint;
        [HideInInspector] public Vector2 grappleDistanceVector;
        private FallingPlatform _fallingPlatform;

        private void Start()
        {
            grappleRope.enabled = false;
            springJoint2D.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canGrapple)
            {
                SetGrapplePoint();
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (launchToPoint && grappleRope.isGrappling)
                {
                    if (launchType == LaunchType.TransformLaunch)
                    {
                        Vector2 firePointDistance = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = grapplePoint - firePointDistance;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                grappleRope.enabled = false;
                springJoint2D.enabled = false;
                rigidbody.gravityScale = 1;
                _fallingPlatform?.Fall();
                _fallingPlatform = null;
            }
        }

        void SetGrapplePoint()
        {
            Vector2 distanceVector = Cam.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
            if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
                GameObject hitGameObject = hit.transform.gameObject;
                if (hitGameObject.layer == grappableLayerNumber || grappleToAll)
                {
                    if (Vector2.Distance(hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                    {
                        grapplePoint = hit.point;
                        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                        grappleRope.enabled = true;
                        
                        if (hitGameObject.CompareTag("FallingPlatform"))
                        {
                            _fallingPlatform = hitGameObject.GetComponent<FallingPlatform>();
                        }
                    }
                }
            }
        }

        public void Grapple()
        {
            springJoint2D.autoConfigureDistance = false;
            if (!launchToPoint && !autoConfigureDistance)
            {
                springJoint2D.distance = targetDistance;
                springJoint2D.frequency = targetFrequency;
            }
            if (!launchToPoint)
            {
                if (autoConfigureDistance)
                {
                    springJoint2D.autoConfigureDistance = true;
                    springJoint2D.frequency = 0;
                }

                springJoint2D.connectedAnchor = grapplePoint;
                springJoint2D.enabled = true;
            }
            else
            {
                switch (launchType)
                {
                    case LaunchType.PhysicsLaunch:
                        springJoint2D.connectedAnchor = grapplePoint;

                        Vector2 distanceVector = firePoint.position - (Vector3)grapplePoint;

                        var player = PlayerController.I;
                        switch (distanceVector.x)
                        {
                            case > 0 when player.transform.localScale.x > 0:
                            {
                                var scale = new Vector3(-10, 0);
                                player.transform.localScale += scale;
                                break;
                            }
                            case < 0 when player.transform.localScale.x < 0:
                            {
                                var scale = new Vector3(10, 0);
                                player.transform.localScale += scale;
                                break;
                            }
                        }
                        springJoint2D.frequency = launchSpeed;
                        springJoint2D.enabled = true;
                        break;
                    case LaunchType.TransformLaunch:
                        rigidbody.gravityScale = 0;
                        rigidbody.linearVelocity = Vector2.zero;
                        break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (firePoint != null && hasMaxDistance)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(firePoint.position, maxDistance);
            }
        }
    }
}
