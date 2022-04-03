using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ren.Presenter;

namespace Ren.View
{

    public class GameStateView : MonoBehaviour
    {

        #region Inspector Fields

        [SerializeField]
        private GameObject viewParentStateOngoing;

        [SerializeField]
        private GameObject viewParentStateOver;

        [Space]

        [SerializeField]
        private GameObject viewStateFailed;

        [SerializeField]
        private GameObject viewStateCompleted;

        #endregion

        private void Awake()
        {
            GameStateManager.OnStateDefeat += OnStateDefeat;
            GameStateManager.OnStateWin += OnStateWin;
            GameStateManager.OnStateOngoing += OnStateOngoing;
        }

        #region Class Implementations

        private void OnStateOverChange(bool isDefeated)
        {
            viewParentStateOngoing.SetActive(false);
            viewParentStateOver.SetActive(true);

            viewStateCompleted.SetActive(!isDefeated);
            viewStateFailed.SetActive(isDefeated);
        }

        #endregion

        #region State callbacks

        private void OnStateOngoing()
        {
            viewParentStateOver.SetActive(false);
            viewParentStateOngoing.SetActive(true);
        }

        private void OnStateDefeat()
        {
            OnStateOverChange(true);
        }

        private void OnStateWin()
        {
            OnStateOverChange(false);
        }

        #endregion

    }

}