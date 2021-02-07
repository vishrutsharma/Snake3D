using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake3D.Controllers;
using Snake3D.Models;

namespace Snake3D.Components
{
    public class CollisionDetector : MonoBehaviour
    {

#pragma warning disable 649

        [SerializeField]
        private SnakeController snakeController;

#pragma warning restore 649

        private void OnCollisionEnter(Collision col)
        {
            if (snakeController.GetGameState() == GameState.GAME)
            {
                if (col.gameObject.tag == GameTags.wallsTag)
                {
                    snakeController.HandleWallCollision(col.GetContact(0));
                }
            }
        }

    }
}
