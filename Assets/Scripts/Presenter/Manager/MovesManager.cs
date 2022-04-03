using Ren.Base;
using System.Collections.Generic;
using UnityEngine;
using Ren.Enum;
using System;

namespace Ren.Presenter
{

    public class MovesManager : BaseSingleton<MovesManager>
    {

        #region Static Events

        public static event Action<bool> OnIsPlayerTurn;
        public static event Action<int> OnMoveCountChanged;

        #endregion

        #region Accessors

        public bool IsPlayerTurn { private set; get; }
        public int MoveCount { private set; get; }
        public Vector3 PlayerPosition => Player.position;

        public Transform Enemy { private set; get; }
        public Transform Player { private set; get; }

        #endregion

        #region Private Fields

        private Stack<Vector3> stackPlayerMoves = new Stack<Vector3>();
        private Stack<Vector3> stackEnemyMoves = new Stack<Vector3>();

        #endregion

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();
            CollectCharacters();
        }

        private void Start()
        {
            ResetCharactersState();
        }

        #endregion

        #region Client Implementation

        private void CollectCharacters()
        {
            Enemy = FindObjectOfType<EnemyBrain>().transform;
            Player = FindObjectOfType<PlayerBrain>().transform;
        }

        #endregion

        #region Public API

        public void ResetCharactersState()
        {
            SetIsPlayerTurn(true);
            ClearMoves();
        }

        public void SetIsPlayerTurn(bool isPlayerTurn)
        {
            IsPlayerTurn = isPlayerTurn;
            OnIsPlayerTurn?.Invoke(isPlayerTurn);
            
            if (isPlayerTurn)
            {
                MoveCount++;
                OnMoveCountChanged?.Invoke(MoveCount);
            }
        }

        public void RegisterPlayerLastPosition()
        {
            stackPlayerMoves.Push(Player.position);
        }

        public void RegisterEnemyLastPosition()
        {
            stackEnemyMoves.Push(Enemy.position);
        }

        public void ClearMoves()
        {
            stackPlayerMoves.Clear();
            stackEnemyMoves.Clear();

            MoveCount = 0;
            OnMoveCountChanged?.Invoke(MoveCount);
        }

        public void UndoLastMove()
        {
            if (stackPlayerMoves.Count == 0)
            {
                Debug.Log($"{GetType().Name}.UndoLastMove(): Request denied. " +
                    $"No moves performed earlier.");
                return;
            }

            Player.position = stackPlayerMoves.Peek();
            stackPlayerMoves.Pop();

            Enemy.position = stackEnemyMoves.Peek();
            stackEnemyMoves.Pop();

            MoveCount--;
            OnMoveCountChanged?.Invoke(MoveCount);
        }

        #endregion

    }

}