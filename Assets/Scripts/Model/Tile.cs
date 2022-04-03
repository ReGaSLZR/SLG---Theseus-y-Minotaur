using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Model
{

    public class Tile : MonoBehaviour
    {

        #region Statics

        public const float GRID_SIDE = 1f; //TODO maybe put this in a scriptable object later..

        #endregion

        #region Private Variables

        private Vector3 gridSize = new Vector3(GRID_SIDE, GRID_SIDE, GRID_SIDE);

        #endregion

        #region Inspector Fields

        [SerializeField]
        private bool shouldHideInRuntime = false;

        [SerializeField]
        private bool shouldSelfRename = true;

        [SerializeField]
        [Tooltip("Value is ignored if 'shouldSelfRename' is FALSE.")]
        private string namePrefix = "Tile";

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (shouldHideInRuntime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying && transform.hasChanged)
            {
                SnapToGrid();
            }
        }

        #endregion

        private void SnapToGrid()
        {
            transform.position = new Vector3(
                Mathf.Round(transform.position.x / gridSize.x) * gridSize.x,
                Mathf.Round(transform.position.y / gridSize.y) * gridSize.y,
                Mathf.Round(transform.position.z / gridSize.z) * gridSize.z
                );

            if (shouldSelfRename)
            {
                gameObject.name = namePrefix + " " + transform.position.x + ", " + transform.position.y;
            }
        }

    }

}