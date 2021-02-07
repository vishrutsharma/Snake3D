using UnityEngine;
using Snake3D.Models;
using System.Collections;
using System.Collections.Generic;

namespace Snake3D.Controllers
{
    public class CameraController : MonoBehaviour
    {
        #region ----------------------------------------- SerializeFields ----------------------------------

#pragma warning disable 649

        [SerializeField]
        private float targetFollowSmoothTime;

        [SerializeField]
        private GameController gameController;

        [SerializeField]
        public Vector3 vectorOffset;

#pragma warning restore 649

        #endregion -----------------------------------------------------------------------------------------

        #region ----------------------------------------- Private Fields ----------------------------------
        private Vector3 directionVector;
        private Vector3 refVelocity;
        private Transform followTransform;
        private Transform cameraTransform;
        private Vector3 desiredPos;

        #endregion -----------------------------------------------------------------------------------------

        #region ----------------------------------------- Private Methods ----------------------------------
        void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            if (gameController.gameState == GameState.GAME)
            {
                desiredPos = followTransform.position + vectorOffset;
                Vector3 direction = followTransform.transform.position - cameraTransform.position;
                direction.Normalize();

                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,
                                                            Quaternion.LookRotation(direction),
                                                            targetFollowSmoothTime * Time.deltaTime);

                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position,
                                                              desiredPos,
                                                              ref refVelocity,
                                                              targetFollowSmoothTime * Time.deltaTime);
                
            }
        }

        #endregion -----------------------------------------------------------------------------------------

        #region ----------------------------------------- Private Methods ----------------------------------

        public void SetTarget(Transform snakeTransform)
        {
            followTransform = snakeTransform;
            directionVector = cameraTransform.position - followTransform.position;
            directionVector.y = 0;
        }


        #endregion -----------------------------------------------------------------------------------------

    }

}
