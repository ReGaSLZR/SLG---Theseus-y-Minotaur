using Ren.Presenter;
using UnityEngine;

namespace Ren.Model
{

    public class Teleporter : Tile
    {

        [SerializeField]
        private Teleporter partneredTile;
        public Teleporter PartneredTile => partneredTile;

        private void Awake()
        {
            LevelManager.OnLevelChanged += _ => SetEnabled(true);
        }

        private void SetEnabled(bool isEnabled)
        {
            PartneredTile.gameObject.SetActive(isEnabled);
            gameObject.SetActive(isEnabled);
        }

        public void ExecuteOneTimeSkill(Transform victim)
        {
            victim.position = PartneredTile.transform.position;
            SetEnabled(false);
        }

    }

}