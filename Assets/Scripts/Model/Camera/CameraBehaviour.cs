using UnityEngine;

namespace Model.Camera
{
    public sealed class CameraBehaviour : MonoBehaviour
    {
        #region Fields

        [Header("Camera")]
        [Tooltip("Current relative offset to the target.")]
        public Vector3 offset = new Vector3(0, 1, 2);
        [Tooltip("Smoothing factor for rapid changes on the Y-axis.")]
        public float heightDamping = 2.0f;
        [Tooltip("Smoothing factor for the rotation.")]
        public float rotationSnapTime = 0.3f;
        [Tooltip("Smoothing factor for the distance on the Z-axis.")]
        public float distanceSnapTime;

        public float TimeMoving = 1f;

        private float usedDistance;
        private float wantedRotationAngle;
        private float wantedHeight;
        private float currentRotationAngle;
        private float currentHeight;
        private Vector3 wantedPosition;
        private Vector3 velocity;

        #endregion


        #region Methods
        public void FollowToTarget(Transform target)
        {

            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, Time.deltaTime / TimeMoving);
            //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.rotation, Time.deltaTime / TimeMoving);


            wantedRotationAngle = target.eulerAngles.y;
            currentRotationAngle = gameObject.transform.eulerAngles.y;
            currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref velocity.y, rotationSnapTime);

            wantedHeight = target.position.y + offset.y;
            currentHeight = gameObject.transform.position.y;
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            wantedPosition = target.position;
            wantedPosition.y = currentHeight;

            usedDistance = Mathf.SmoothDampAngle(usedDistance, offset.z, ref velocity.z, distanceSnapTime);

            wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

            gameObject.transform.position = wantedPosition;

            gameObject.transform.LookAt(target.position);
        }
        #endregion

    }
}
