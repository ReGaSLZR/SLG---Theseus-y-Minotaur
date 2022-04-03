using UnityEngine;

namespace Ren.Presenter
{

    public class LevelBrain : MonoBehaviour
    {

        #region Unity Callbacks

        private void Awake()
        {
            LevelManager.OnLevelChanged += OnLevelChanged;
        }

        private void Update()
        {
            if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyRestart))
            {
                GameStateManager.Instance.ExecuteState(Enum.GameState.Ongoing);
                LevelManager.Instance.RestartLevel();
                RefreshCharacterPositionsAndMoves();
            }
            else if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyPrevious))
            {
                var isSuccess = LevelManager.Instance.LoadPreviousLevel();

                if (isSuccess)
                {
                    GameStateManager.Instance.ExecuteState(Enum.GameState.Ongoing);
                    RefreshCharacterPositionsAndMoves();
                }
            }
            else if (Input.GetKeyDown(PlayerInputManager.Instance.InputKeys.KeyNext))
            {
                var isSuccess = LevelManager.Instance.LoadNextLevel();

                if (isSuccess)
                {
                    GameStateManager.Instance.ExecuteState(Enum.GameState.Ongoing);
                    RefreshCharacterPositionsAndMoves();
                }
            }
        }

        #endregion

        #region Client Implementation

        private void OnLevelChanged(int levelIndex)
        {
            RefreshCharacterPositionsAndMoves();
        }

        private void RefreshCharacterPositionsAndMoves()
        {
            var currentLevel = LevelManager.Instance.GetCurrentLevel();

            MovesManager.Instance.Player.position = currentLevel.MarkerPlayer.transform.position;
            MovesManager.Instance.Enemy.position = currentLevel.MarkerEnemy.transform.position;
            MovesManager.Instance.ResetCharactersState();
        }

        #endregion
    }

}