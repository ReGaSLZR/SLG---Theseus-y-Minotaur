using Ren.Base;
using Ren.Enum;
using System;
using UnityEngine;

namespace Ren.Presenter
{

    public class GameStateManager : BaseSingleton<GameStateManager>
    {
       
        public static event Action OnStateOngoing;
        public static event Action OnStateDefeat;
        public static event Action OnStateWin;

        public GameState CurrentState { private set; get; }

        #region Unity Callbacks

        private void Start()
        {
            ExecuteState(GameState.Ongoing);
        }

        #endregion

        #region Public API

        public bool IsOngoing()
        {
            return CurrentState == GameState.Ongoing;
        }

        public void ExecuteState(GameState state)
        {
            CurrentState = state;

            switch (state)
            {
                case GameState.Ongoing:
                    OnStateOngoing?.Invoke();
                    break;
                case GameState.Completed:
                    OnStateWin?.Invoke();
                    break;
                case GameState.Failed:
                    OnStateDefeat?.Invoke();
                    break;
                default:
                    Debug.Log($"{GetType().Name}.ExecuteState(): Unhandled state {state}");
                    break;
            }
            
        }

        #endregion

    }

}