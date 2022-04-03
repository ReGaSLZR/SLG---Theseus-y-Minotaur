using Ren.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{

    public class CharacterMovement : MonoBehaviour
    {

        #region Inspector Fields

        [SerializeField]
        [Range(0.1f, 5f)]
        private float movementDuration = 0.25f;

        #endregion

        #region Accessors

        public bool IsMoving { private set; get; }
        
        #endregion

        #region Private Fields

        private Action listenersOnFinishMovement;
        private Vector3 cachedDestination;

        #endregion

        #region Public API

        public void RegisterOnMoveFinished(Action listener)
        {
            if (listener == null)
            {
                return;
            }

            listenersOnFinishMovement += listener;
        }

        public void MoveTo(Direction direction)
        {
            if (IsMoving)
            {
                return;
            }

            StartCoroutine(C_Move(TileManager.Instance.GetTileDestination(transform.position, direction)));
        }

        public void CancelMovement()
        {
            if (IsMoving)
            {
                StopAllCoroutines();
                transform.position = cachedDestination;
                
                IsMoving = false;
                listenersOnFinishMovement?.Invoke();
            }
        }

        #endregion

        #region Client Implementation

        private IEnumerator C_Move(Vector3 destination)
        {
            cachedDestination = destination;
            IsMoving = true;

            var startTime = Time.time;
            var finishTime = startTime + movementDuration;
            var origin = transform.position;

            while (finishTime > Time.time)
            {
                var percent = (Time.time - startTime) / movementDuration;
                transform.position = Vector3.Lerp(origin, destination, percent);

                yield return null;
            }

            transform.position = destination; //just in case. failsafe.
            IsMoving = false;

            listenersOnFinishMovement?.Invoke();
        }

        #endregion

    }

}