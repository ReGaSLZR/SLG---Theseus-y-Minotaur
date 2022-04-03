using Ren.Presenter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ren.View
{

    public class GameStatsView : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI textLevelNumber;

        [SerializeField]
        private TextMeshProUGUI textMoveCount;

        private void Awake()
        {
            MovesManager.OnMoveCountChanged +=
                moveCount => textMoveCount.text = moveCount.ToString();

            LevelManager.OnLevelChanged += levelIndex => 
                textLevelNumber.text = (levelIndex + 1).ToString();
        }

    }

}