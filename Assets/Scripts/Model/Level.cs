using UnityEngine;

namespace Ren.Model
{

    public class Level : MonoBehaviour
    {

        [SerializeField]
        private Marker markerPlayer;
        public Marker MarkerPlayer => markerPlayer;

        [SerializeField]
        private Marker markerEnemy;
        public Marker MarkerEnemy => markerEnemy;

        [SerializeField]
        private Transform markerExit;

    }
}
