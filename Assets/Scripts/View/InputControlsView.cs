using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ren.Presenter;

namespace Ren.View
{

    public class InputControlsView : MonoBehaviour
    {

        [Header("Movement Keys")]

        [SerializeField]
        private TextMeshProUGUI textUp;

        [SerializeField]
        private TextMeshProUGUI textDown;

        [SerializeField]
        private TextMeshProUGUI textLeft;

        [SerializeField]
        private TextMeshProUGUI textRight;

        [Header("Other Keys")]

        [SerializeField]
        private TextMeshProUGUI textUndo;

        [SerializeField]
        private TextMeshProUGUI textWait;

        [Header("Level Keys")]

        [SerializeField]
        private TextMeshProUGUI[] textRestart;

        [SerializeField]
        private TextMeshProUGUI[] textPrevious;

        [SerializeField]
        private TextMeshProUGUI[] textNext;

        #region Unity Callbacks

        private void Awake()
        {
            SetViewInputMovement();
            SetViewInputOthers();
            SetViewInputLevel();
        }  

        #endregion

        #region Client Implementation

        private void SetViewInputMovement()
        {
            textUp.text = PlayerInputManager.Instance.InputKeys.KeyUp.ToString();
            textDown.text = PlayerInputManager.Instance.InputKeys.KeyDown.ToString();
            textLeft.text = PlayerInputManager.Instance.InputKeys.KeyLeft.ToString();
            textRight.text = PlayerInputManager.Instance.InputKeys.KeyRight.ToString();
        }

        private void SetViewInputOthers()
        {
            textUndo.text = PlayerInputManager.Instance.InputKeys.KeyUndo.ToString();
            textWait.text = PlayerInputManager.Instance.InputKeys.KeyWait.ToString();
        }


        private void SetViewInputLevel()
        {
            foreach (var restart in textRestart)
            {
                restart.text = PlayerInputManager.Instance.InputKeys.KeyRestart.ToString();
            }

            foreach (var previous in textPrevious)
            {
                previous.text = PlayerInputManager.Instance.InputKeys.KeyPrevious.ToString();
            }

            foreach (var next in textNext)
            {
                next.text = PlayerInputManager.Instance.InputKeys.KeyNext.ToString();
            }
        }

        #endregion

    }

}