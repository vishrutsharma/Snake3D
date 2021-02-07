using UnityEngine;
using UnityEngine.UI;
using Snake3D.Controllers;
using Snake3D.Models;

namespace Snake3D.Views
{
    public class WorldView : MonoBehaviour
    {
        #region ------------------------------- SerializeFields -----------------------------

#pragma warning disable 649

        [SerializeField]
        private Canvas worldCanvas;

#pragma warning restore 649

        #endregion --------------------------------------------------------------------------

        #region ------------------------------- Private Fields -----------------------------

        private WorldController worldController;
        private bool isVisited = false;

        #endregion --------------------------------------------------------------------------

        #region ------------------------------- Public Fields -----------------------------

        [HideInInspector] public int id;

        #endregion --------------------------------------------------------------------------

        #region ------------------------------- Private Methods -----------------------------

        /// <summary>
        /// Update Grid Block status on visit
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (worldController.GetGameState() == GameState.GAME)
            {
                if (other.gameObject.tag == GameTags.snakeHeadTag && !isVisited)
                {
                    // worldCanvas.gameObject.SetActive(true);
                    worldCanvas.worldCamera = Camera.main;
                    isVisited = true;
                    worldController.OnVisit(id);
                }
            }
        }

        #endregion -------------------------------------------------------------------------

        #region ------------------------------- Public Methods -----------------------------

        /// <summary>
        /// Initialize Block View
        /// </summary>
        /// <param name="id"></param>
        /// <param name="worldController"></param>
        public void InitView(int id, WorldController worldController)
        {
            this.id = id;
            this.worldController = worldController;

        }
        #endregion -------------------------------------------------------------------------

    }
}

