using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake3D.Views;
using Snake3D.Models;
using Snake3D.Controllers;

namespace Snake3D.Controllers
{
    public class GameController : MonoBehaviour
    {

        #region ---------------------------------- SerializeField -------------------------

#pragma warning disable 649

        [SerializeField]
        private GameModel gameModel;

        [SerializeField]
        private GameView gameView;

        [SerializeField]
        private CameraController cameraController;
        [SerializeField]
        private WorldController worldController;

        [SerializeField]
        private SnakeController snakeController;

#pragma warning restore 649

        #endregion --------------------------------------------------------------------------

        #region ----------------------------------Private Fields ----------------------------

        private int totalScore;

        #endregion --------------------------------------------------------------------------


        #region ---------------------------------- Public Fields ----------------------------

        [HideInInspector]
        public GameState gameState;

        #endregion --------------------------------------------------------------------------

        #region ---------------------------------- Private Methods ---------------------------

        private void Start()
        {
            gameState = GameState.MENU;
        }

        private void SpawnPizza()
        {
            gameView.SpawnPizza(worldController.GetPizzaPosition(), gameModel.pizzaStayTime);
        }

        #endregion --------------------------------------------------------------------------


        #region ---------------------------------- Public Methods ----------------------------

        public void StartGame()
        {
            // Init Snake after World Setup is done
            // Sequence ----------
            // Spawn World -> Init Snake -> Then Game Starts
            totalScore = 0;
            StartCoroutine(worldController.SpawnWorld(() =>
            {
                snakeController.InitSnake((snakeTransform) =>
                {
                    cameraController.SetTarget(snakeTransform);
                    gameState = GameState.GAME;
                    SpawnPizza();
                }
                );
            }
            ));
        }

        public void OnPizzaReset(bool isCollected)
        {
            if (isCollected)
            {
                gameView.UpdateScore(++totalScore);
            }
            SpawnPizza();
        }

        public void OnGameComplete()
        {
            gameState = GameState.MENU;
            gameView.OnGameComplete(gameModel.resetGameTimer);
        }

        #endregion --------------------------------------------------------------------------

    }

}
