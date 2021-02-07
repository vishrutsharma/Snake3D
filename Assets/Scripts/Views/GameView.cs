using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Snake3D.Components;
using Snake3D.Controllers;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Snake3D.Views
{
    public class GameView : MonoBehaviour
    {
        #region  ------------------------------- SerializeField -----------------------------

#pragma warning disable 649

        [Header("Game UI")]

        [SerializeField]
        private GameObject menuPanel;

        [SerializeField]
        private GameObject pausePanel;

        [SerializeField]
        private GameObject gamePanel;

        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button exitButton;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button homeButton;
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [Space]
        [Space]
        [SerializeField]
        private GameController gameController;

        [SerializeField]
        private GameObject pizzaPrefab;

        [SerializeField]
        private ParticleSystem gameCompleteParticle;

#pragma warning restore 649

        #endregion --------------------------------------------------------------------------

        #region  ------------------------------- Private Fields -----------------------------

        private GameObject pizzaOb;

        #endregion --------------------------------------------------------------------------

        #region  ------------------------------- Private Methods -----------------------------

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayClick);
            exitButton.onClick.AddListener(OnExitClick);
            resumeButton.onClick.AddListener(OnResumeClick);
            pauseButton.onClick.AddListener(OnPauseClick);
            homeButton.onClick.AddListener(GoToHome);
        }

        private void OnPlayClick()
        {
            menuPanel.SetActive(false);
            gamePanel.SetActive(true);
            scoreText.text = "0";
            gameController.StartGame();
        }

        private void OnExitClick()
        {
            Application.Quit();
        }

        private void OnResumeClick()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            gamePanel.SetActive(true);
        }

        private void OnPauseClick()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            gamePanel.SetActive(false);
        }

        private void GoToHome()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        #endregion --------------------------------------------------------------------------


        #region  ------------------------------- Public Methods -----------------------------

        public void UpdateScore(int score)
        {
            scoreText.text = score + "";
        }

        public void OnGameComplete(float resetTimer)
        {
            gamePanel.SetActive(false);
            gameCompleteParticle.Play();
            Invoke("GoToHome", resetTimer);
        }

        public void SpawnPizza(Vector3 spawnPos, float stayTime)
        {
            if (pizzaOb != null)
                Destroy(pizzaOb);

            pizzaOb = Instantiate(pizzaPrefab);
            spawnPos.y = 0;
            pizzaOb.transform.position = spawnPos + Vector3.up * 0.5f;
            pizzaOb.GetComponent<PizzaComponent>().InitPizza(stayTime, gameController);
        }

        #endregion --------------------------------------------------------------------------

    }
}

