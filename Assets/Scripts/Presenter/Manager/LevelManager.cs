using Ren.Base;
using Ren.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{

    public class LevelManager : BaseSingleton<LevelManager>
    {

        public static event Action<int> OnLevelChanged;

        [SerializeField]
        private List<Level> levels = new List<Level>();

        public int LevelIndex { private set; get; } = 0;

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();

            if (levels.Count == 0)
            {
                Debug.LogError($"{GetType().Name}.Awake():" +
                    $" NO LEVELS DEFINED!");
            }
        }

        private void Start()
        {
            LevelIndex = 0;
            ChangeLevelAndNotify();
        }

        #endregion

        #region Client Implementation

        private void HideAllLevels()
        {
            foreach (var level in levels)
            {
                level.gameObject.SetActive(false);
            }
        }

        private void ChangeLevelAndNotify()
        {
            HideAllLevels();
            GetCurrentLevel().gameObject.SetActive(true);
            OnLevelChanged?.Invoke(LevelIndex);
        }

        #endregion

        #region Public API

        public Level GetCurrentLevel()
        {
            return levels[LevelIndex];
        }

        public bool LoadNextLevel()
        {
            if (LevelIndex < levels.Count - 1)
            {
                LevelIndex++;
                ChangeLevelAndNotify();
                return true;
            }

            return false;
        }

        public bool LoadPreviousLevel()
        {
            if (LevelIndex > 0)
            {
                LevelIndex--;
                ChangeLevelAndNotify();
                return true;
            }

            return false;
        }

        public void RestartLevel()
        {
            OnLevelChanged?.Invoke(LevelIndex);
        }

        #endregion

    }

}