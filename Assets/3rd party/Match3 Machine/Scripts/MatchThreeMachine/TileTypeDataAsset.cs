using UnityEngine;

namespace MatchThreeMachine
{
	[CreateAssetMenu(menuName = "Match 3 Machine/Tile Type Asset")]
	public sealed class TileTypeDataAsset : ScriptableObject
	{
		public int id;

		public Sprite sprite;
	}
}
