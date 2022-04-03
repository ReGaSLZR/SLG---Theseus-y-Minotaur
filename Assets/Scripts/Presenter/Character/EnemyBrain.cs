using Ren.Enum;
using System.Collections;
using UnityEngine;

namespace Ren.Presenter
{

    public class EnemyBrain : CharacterBrain
    {

        [SerializeField]
        private int tilesPerTurn = 2;

        [SerializeField]
        [Range(0f, 1f)]
        private float movementLogicDelay = 0.1f;

        private int tilesWalked;

        #region Unity Callbacks

        protected override void Start()
        {
            base.Start();
            MovesManager.OnIsPlayerTurn += OnIsPlayerTurn;
        }

        #endregion

        #region Client Implementation

        protected override void OnFinishMovement()
        {
            tilesWalked++;

            if (tilesWalked < tilesPerTurn)
            {
                StartCoroutine(C_DoMove());
            }
            else
            {
                MovesManager.Instance.SetIsPlayerTurn(true);
            }
        }

        private void OnIsPlayerTurn(bool isPlayerTurn)
        {
            if (!isPlayerTurn && GameStateManager.Instance.IsOngoing()
                )
            {
                tilesWalked = 0;
                MovesManager.Instance.RegisterEnemyLastPosition();
                StartCoroutine(C_DoMove());
            }
        }

        private IEnumerator C_DoMove()
        {
            yield return new WaitForSeconds(movementLogicDelay);

            if (ShouldMoveHorizontally(out var horiDirection) && CanMoveTo(horiDirection))
            {
                movement.MoveTo(horiDirection);
            }
            else if (ShouldMoveVertically(out var vertDirection) && CanMoveTo(vertDirection))
            {
                movement.MoveTo(vertDirection);
            }
            else 
            {
                MovesManager.Instance.SetIsPlayerTurn(true);
            }
        }

        private bool ShouldMoveHorizontally(out Direction horizontalDirection)
        {
            var playerPosX = MovesManager.Instance.PlayerPosition.x;
            var canMove = playerPosX != transform.position.x;
            horizontalDirection = canMove 
                ? ((playerPosX > transform.position.x) ? Direction.Right : Direction.Left)
                : Direction.Up;
            return canMove;
        }

        private bool ShouldMoveVertically(out Direction verticalDirection)
        {
            var playerPosY = MovesManager.Instance.PlayerPosition.y;
            var canMove = playerPosY != transform.position.y;
            verticalDirection = canMove
                ? ((playerPosY > transform.position.y) ? Direction.Up : Direction.Down)
                : Direction.Up;
            return canMove;
        }

        private bool CanMoveTo(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return !detectorUp.IsBlocked;
                case Direction.Down:
                    return !detectorDown.IsBlocked;
                case Direction.Left:
                    return !detectorLeft.IsBlocked;
                case Direction.Right:
                    return !detectorRight.IsBlocked;
                default:
                    return false;
            }
        }

        #endregion

    }

}
