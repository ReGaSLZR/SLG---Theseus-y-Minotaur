using Ren.Base;
using Ren.Constants;
using Ren.Enum;
using Ren.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Presenter
{
    public class TileManager : BaseSingleton<TileManager>
    {

        private Dictionary<Vector3, Tile> tiles = new Dictionary<Vector3, Tile>();

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();
            CollectTiles();
        }

        #endregion

        #region Public API

        /// <summary>
        /// Gets the tile at the specified direction from the given origin.
        /// Returns origin itself if there is no such tile on the given direction.
        /// </summary>
        public Vector3 GetTileDestination(Vector3 origin, Direction direction)
        {
            var destination = origin;

            switch (direction)
            {
                case Direction.Up:
                    destination = new Vector3(origin.x, origin.y + Tile.GRID_SIDE, origin.z);
                    break;
                case Direction.Down:
                    destination = new Vector3(origin.x, origin.y - Tile.GRID_SIDE, origin.z);
                    break;
                case Direction.Left:
                    destination = new Vector3(origin.x - Tile.GRID_SIDE, origin.y, origin.z);
                    break;
                case Direction.Right:
                    destination = new Vector3(origin.x + Tile.GRID_SIDE, origin.y, origin.z);
                    break;
                default:
                    Debug.Log($"{GetType().Name}.GetTileDestination(): Unhandled direction {direction}", gameObject);
                    break;
            }

            return tiles.ContainsKey(destination) ? destination : origin;
        }

        #endregion

        #region Client Implementation

        private void CollectTiles()
        {
            var tiles = FindObjectsOfType<Tile>();
            foreach (var tile in tiles)
            {
                if (!this.tiles.ContainsKey(tile.transform.position) && tile.CompareTag(Tags.TILE))
                {
                    this.tiles.Add(tile.transform.position, tile);
                }
            }
        }

        #endregion

    }

}
