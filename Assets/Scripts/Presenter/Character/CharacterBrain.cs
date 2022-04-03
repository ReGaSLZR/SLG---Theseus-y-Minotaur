using Ren.Constants;
using Ren.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class CharacterBrain : MonoBehaviour
    {

        protected CharacterMovement movement;

        #region Inspector Fields

        [SerializeField]
        protected CharacterWallDetector detectorUp;

        [SerializeField]
        protected CharacterWallDetector detectorDown;

        [SerializeField]
        protected CharacterWallDetector detectorLeft;

        [SerializeField]
        protected CharacterWallDetector detectorRight;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            movement = GetComponent<CharacterMovement>();
        }

        protected virtual void Start()
        {
            movement.RegisterOnMoveFinished(OnFinishMovement);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.TELEPORTER))
            {
                if (collision.gameObject.TryGetComponent<Teleporter>(out var teleporter))
                {
                    movement.CancelMovement();
                    teleporter.ExecuteOneTimeSkill(transform);
                    OnFinishMovement();
                }
            }
        }

        #endregion

        protected abstract void OnFinishMovement();

    }

}