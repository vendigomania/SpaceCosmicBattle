using UnityEngine;

namespace MatchThreeMachine
{
	public sealed class TilesRow : MonoBehaviour
	{
		public TileVisualizator[] tiles;

		public int maxCapacity = 0;

		public void SetTiles(TileVisualizator[] _tiles)
        {
			if (maxCapacity == 0)
				return;

			tiles = _tiles;
        }
	}
}
