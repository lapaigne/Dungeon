using System.Collections.Generic;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var queue = new Queue<SinglyLinkedList<Point>>();
		var visited = new HashSet<Point>();

		var possible = new List<(int, int)>
		{
			(0, 1),
			(1, 0),
			(0, -1),
			(-1, 0)
		};

		visited.Add(start);
		queue.Enqueue(new SinglyLinkedList<Point>(start));

		while (queue.Count > 0)
		{
			var point = queue.Dequeue();
			
			if (!map.InBounds(point.Value)) continue;
			if (map.Dungeon[point.Value.X, point.Value.Y] != MapCell.Empty) continue;

			foreach (var chest in chests)
				if (chest == point.Value) yield return point;
			
			foreach (var (x, y) in possible)
			{
				var next = new Point(point.Value.X + x, point.Value.Y + y);
                if (!map.InBounds(next)) continue;
                if (!visited.Contains(next))
				{
					queue.Enqueue(new SinglyLinkedList<Point>(next, point));
					visited.Add(next);
				}		
            }
		}

		yield break;
	}
}