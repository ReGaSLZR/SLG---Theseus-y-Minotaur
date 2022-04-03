using UnityEngine;

namespace Ren.Model
{

    [CreateAssetMenu(fileName = "Player Input Keys", menuName = "Ren / Create Input Keys")]
    public class PlayerInputKeys : ScriptableObject
    {

        [Header("Movement Keys")]

        [SerializeField]
        private KeyCode keyUp = KeyCode.UpArrow;
        public KeyCode KeyUp => keyUp;

        [SerializeField]
        private KeyCode keyDown = KeyCode.DownArrow;
        public KeyCode KeyDown => keyDown;

        [SerializeField]
        private KeyCode keyLeft = KeyCode.LeftArrow;
        public KeyCode KeyLeft => keyLeft;

        [SerializeField]
        private KeyCode keyRight = KeyCode.RightArrow;
        public KeyCode KeyRight => keyRight;

        [Header("Other Keys")]

        [SerializeField]
        private KeyCode keyUndo = KeyCode.Backspace;
        public KeyCode KeyUndo => keyUndo;

        [SerializeField]
        private KeyCode keyWait = KeyCode.Space;
        public KeyCode KeyWait => keyWait;

        [Header("Level Keys")]

        [SerializeField]
        private KeyCode keyRestart = KeyCode.Return;
        public KeyCode KeyRestart => keyRestart;

        [SerializeField]
        private KeyCode keyPrevious = KeyCode.Return;
        public KeyCode KeyPrevious => keyPrevious;

        [SerializeField]
        private KeyCode keyNext = KeyCode.Return;
        public KeyCode KeyNext => keyNext;

    }

}