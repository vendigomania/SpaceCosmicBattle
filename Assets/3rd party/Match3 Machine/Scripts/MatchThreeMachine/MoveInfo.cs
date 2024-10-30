namespace MatchThreeMachine
{
	public sealed class MoveInfo
	{
		public readonly int startX;
		public readonly int startY;
		
		public readonly int endX;
		public readonly int endY;

		public MoveInfo(int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0)
		{
			startX = x1;
			startY = y1;
			
			endX = x2;
			endY = y2;
		}
	}
}
