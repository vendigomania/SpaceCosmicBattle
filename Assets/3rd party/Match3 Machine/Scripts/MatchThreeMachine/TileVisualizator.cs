using UnityEngine;
using UnityEngine.UI;

namespace MatchThreeMachine
{
	public sealed class TileVisualizator : MonoBehaviour
	{
		public int xPosition;
		public int yPosition;

		public Image icon;

		public Button button;
		public Button buttonRefresh;

		private TileTypeDataAsset _typeData;

		public TileTypeDataAsset Type
		{
			get => _typeData;

			set
			{
				if (_typeData == value) return;

				_typeData = value;

				icon.sprite = _typeData.sprite;

				if (buttonRefresh != null)
					buttonRefresh = button;
			}
		}

		public TileData Data => new TileData(xPosition, yPosition, _typeData.id);
	}
}
