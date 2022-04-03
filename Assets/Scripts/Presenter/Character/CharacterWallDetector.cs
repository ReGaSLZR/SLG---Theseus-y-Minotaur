using Ren.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{

    [RequireComponent(typeof(Collider2D))]
    public class CharacterWallDetector : MonoBehaviour
    {

        public bool IsBlocked { private set; get; }

        private int previousBlockHash;

        #region Unity Callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.WALL))
            {
                previousBlockHash = collision.gameObject.GetHashCode();
                IsBlocked = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.WALL) && 
                previousBlockHash == collision.gameObject.GetHashCode())
            {
                IsBlocked = false;
            }
        }

        #endregion

    }

}