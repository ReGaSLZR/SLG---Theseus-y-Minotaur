using Ren.Base;
using Ren.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{

    public class PlayerInputManager : BaseSingleton<PlayerInputManager>
    {

        #region Inspector Fields

        [SerializeField]
        private PlayerInputKeys inputKeys;

        #endregion

        #region Accessors

        public PlayerInputKeys InputKeys => inputKeys;

        #endregion

    }

}