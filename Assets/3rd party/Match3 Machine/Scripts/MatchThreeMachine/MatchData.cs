namespace MatchThreeMachine
{
	public sealed class MatchData
	{
		public int TypeId { get; private set; }

		public int Count { get; private set; }

		public readonly TileData[] MatchedTiles;

		public MatchData(TileData _origin, TileData[] _horizontalTiles, TileData[] _verticalTiles)
		{
			TypeId = _origin.TypeId;

			if (_horizontalTiles.Length >= 2 && _verticalTiles.Length >= 2)
			{
				MatchedTiles = new TileData[_horizontalTiles.Length + _verticalTiles.Length + 1];

				MatchedTiles[0] = _origin;

				_horizontalTiles.CopyTo(MatchedTiles, 1);

				_verticalTiles.CopyTo(MatchedTiles, _horizontalTiles.Length + 1);
			}
			else if (_horizontalTiles.Length >= 2)
			{
				MatchedTiles = new TileData[_horizontalTiles.Length + 1];

				MatchedTiles[0] = _origin;

				_horizontalTiles.CopyTo(MatchedTiles, 1);
			}
			else if (_verticalTiles.Length >= 2)
			{
				MatchedTiles = new TileData[_verticalTiles.Length + 1];

				MatchedTiles[0] = _origin;

				_verticalTiles.CopyTo(MatchedTiles, 1);
			}
			else MatchedTiles = null;

			Count = MatchedTiles?.Length ?? -1;
		}
	}
}
