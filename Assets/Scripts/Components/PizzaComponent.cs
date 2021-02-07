using UnityEngine;
using Snake3D.Models;
using Snake3D.Controllers;

namespace Snake3D.Components
{
    public class PizzaComponent : MonoBehaviour
    {
        #region  ------------------------------------ SerializeFields ---------------------------------

#pragma warning disable 649

        [SerializeField]
        private float amplitude;

        [SerializeField]
        private float waveFrequency;

        [SerializeField]
        private float rotationSpeed;

#pragma warning restore 649

        #endregion --------------------------------------------------------------------------------------

        #region ---------------------------------- Private Fields ----------------------------------------

        private Vector3 initialPosition;
        private GameController gameController;
        private float elapsedTime = 0;
        private float stayDuration;
        private bool enableTimer;


        #endregion ---------------------------------------------------------------------------------------



        #region ---------------------------------- Private Fields -----------------------------------------

        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            transform.localPosition = initialPosition - Vector3.up * Mathf.Sin(Time.time * waveFrequency) * amplitude;

            if (enableTimer)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime % 60 > stayDuration)
                {
                    enableTimer = false;
                    gameController.OnPizzaReset(false);
                }
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == GameTags.snakeHeadTag)
            {
                gameController.OnPizzaReset(true);
            }
        }

        #endregion -----------------------------------------------------------------------------------------

        #region ---------------------------------- Public Fields -------------------------------------------
        public void InitPizza(float stayTime, GameController gameController)
        {
            this.gameController = gameController;
            stayDuration = stayTime;
            enableTimer = true;
        }

        #endregion -------------------------------------------------------------------------------------------
    }
}

