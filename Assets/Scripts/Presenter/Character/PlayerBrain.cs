using UnityEngine;

using Ren.Constants;
using Ren.Enum;

namespace Ren.Presenter
{

    public class PlayerBrain : CharacterBrain
    {

        #region Unity Callbacks

        private void Update()
        {
            if (!MovesManager.Instance.IsPlayerTurn)
            {
                return;
            }

            if (IsInputDisallowed())
            {
                return;
            }

            DetectMovementInput();
            DetectSpecialInput();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.CompareTag(Tags.FINISH))
            {
                Debug.Log($"LEVEL FINISHED!");
                GameStateManager.Instance.ExecuteState(GameState.Completed);
            }
            else if (collision.CompareTag(Tags.ENEMY))
            {
                Debug.Log($"LEVEL FAILED!");
                GameStateManager.Instance.ExecuteState(GameState.Failed);
            }
        }

        #endregion

        #region Client Implementation

        protected override void OnFinishMovement()
        {
            MovesManager.Instance.SetIsPlayerTurn(false);
        }

        private bool IsInputDisallowed()
        {
            return movement.IsMoving || !GameStateManager.Instance.IsOngoing();
        }

        private void DetectSpecialInput()
        {
            if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyUndo))
            {
                movement.CancelMovement();
                MovesManager.Instance.UndoLastMove();
            }

            if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyWait))
            {
                MovesManager.Instance.RegisterPlayerLastPosition();
                MovesManager.Instance.SetIsPlayerTurn(false);
            }
        }

        private void DetectMovementInput()
        {
            if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyUp) && !detectorUp.IsBlocked)
            {
                MovesManager.Instance.RegisterPlayerLastPosition();
                movement.MoveTo(Direction.Up);
            }
            else if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyDown) && !detectorDown.IsBlocked)
            {
                MovesManager.Instance.RegisterPlayerLastPosition();
                movement.MoveTo(Direction.Down);
            }
            else if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyLeft) && !detectorLeft.IsBlocked)
            {
                MovesManager.Instance.RegisterPlayerLastPosition();
                movement.MoveTo(Direction.Left);
            }
            else if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyRight) && !detectorRight.IsBlocked)
            {
                MovesManager.Instance.RegisterPlayerLastPosition();
                movement.MoveTo(Direction.Right);
            }
        }

        #endregion

    }

}