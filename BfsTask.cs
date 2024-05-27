using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

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
			
			if (point.Value.X < 0 || point.Value.Y < 0 || point.Value.X >= map.Dungeon.GetLength(0) || point.Value.X >= map.Dungeon.GetLength(1)) continue;
			if (map.Dungeon[point.Value.X, point.Value.Y] == MapCell.Wall) continue;

			if (visited.Contains(point.Value)) yield return point;

			foreach (var (x, y) in possible)
			{ 
					
				var next = new Point(point.Value.X + x, point.Value.Y + y);

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