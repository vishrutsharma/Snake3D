using System;
using UnityEngine;
using Snake3D.Models;
using System.Collections;
using Snake3D.Components;
using System.Collections.Generic;

namespace Snake3D.Controllers
{
    public class SnakeController : MonoBehaviour
    {
        #region ----------------------------------------------- SerializeFields ----------------------------------------

#pragma warning disable 649

        [SerializeField]
        private SnakeModel snakeModel;

        [SerializeField]
        private List<GameObject> snakeParts;

        [SerializeField]
        private GameObject snakeHead;

        [SerializeField]
        private Transform snakeParentTransform;

        [SerializeField]
        private JoystickComponent joystick;

        [SerializeField]
        private GameController gameController;

#pragma warning restore 649

        #endregion -------------------------------------------------------------------------------------------------------


        #region ----------------------------------------------- Private Fields -------------------------------------------

        private Rigidbody[] snakePartsRigidbody;
        private Transform[] followTransforms;
        private Rigidbody snakeHeadRigidbody;
        private Vector3 currentVelocity;

        #endregion ---------------------------------------------------------------------------------------------------------


        #region ----------------------------------------------- Private Methods -------------------------------------------

        /// <summary>
        /// Intialize Snake Mesh and its children
        /// </summary>
        private void Start()
        {
            snakeHeadRigidbody = snakeHead.GetComponent<Rigidbody>();
            snakePartsRigidbody = new Rigidbody[snakeParts.Count];
            followTransforms = new Transform[snakeParts.Count];

            for (int i = snakeParts.Count - 1; i >= 0; i--)
            {
                snakePartsRigidbody[i] = snakeParts[i].GetComponent<Rigidbody>();
                followTransforms[i] = snakeParts[i].transform.GetChild(0).transform;
            }

            //Reset Parents
            for (int j = 0; j < snakeParts.Count; j++)
            {
                snakeParts[j].transform.SetParent(snakeParentTransform);
            }
            snakeHead.transform.SetParent(snakeParentTransform);
        }


        /// <summary>
        /// Handles Snake's Head and Body Parts Movement and Rotation
        /// </summary>
        private void FixedUpdate()
        {
            snakeHead.transform.rotation = Quaternion.Slerp(snakeHead.transform.rotation,
                                                            Quaternion.Euler(0, joystick.SteeringAngle, 0),
                                                            snakeModel.rotationSpeed * Time.fixedDeltaTime
                                                           );

            if (gameController.gameState == GameState.GAME)
            {
                //Handling Snake Head Movement
                currentVelocity = snakeHead.transform.forward * snakeModel.movementSpeed * Time.fixedDeltaTime;
                snakeHeadRigidbody.GetComponent<Rigidbody>().velocity = currentVelocity;
            }

            //Handling Snake Parts Movement;
            for (int i = 0; i < snakeParts.Count; i++)
            {
                if (gameController.gameState == GameState.GAME)
                {
                    currentVelocity = snakePartsRigidbody[i].transform.forward * snakeModel.movementSpeed * Time.fixedDeltaTime;
                    snakePartsRigidbody[i].velocity = currentVelocity;
                }
                Vector3 dir = followTransforms[i].transform.position - snakeParts[i].transform.position;
                dir.Normalize();
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                snakeParts[i].transform.rotation = Quaternion.Slerp(snakeParts[i].transform.rotation,
                                                                    lookRotation,
                                                                    snakeModel.rotationSpeed * Time.fixedDeltaTime
                                                                   );

            }
        }

        #endregion ---------------------------------------------------------------------------------------------------------


        #region ----------------------------------------------- Public Methods -------------------------------------------

        public void InitSnake(Action<Transform> OnInitSnake)
        {
            for (int s = 0; s < snakePartsRigidbody.Length; s++)
            {
                snakePartsRigidbody[s].isKinematic = false;
                snakePartsRigidbody[s].useGravity = true;
            }
            snakeHeadRigidbody.isKinematic = false;
            snakeHeadRigidbody.useGravity = true;
            snakeParentTransform.gameObject.SetActive(true);
            OnInitSnake(snakeHead.transform);
        }

        public void HandleWallCollision(ContactPoint contactPoint)
        {
            // Vector3 reflectedVector = Vector3.Reflect(snakeHeadRigidbody.velocity, contactPoint.normal);
            // snakeHeadRigidbody.transform.rotation = Quaternion.LookRotation(reflectedVector);
            // snakeHeadRigidbody.AddRelativeForce(snakeHead.transform.forward * 15, ForceMode.Impulse);
        }

        public GameState GetGameState()
        {
            return gameController.gameState;
        }
        #endregion ---------------------------------------------------------------------------------------------------------

    }
}

