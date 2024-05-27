using System.Collections.Generic;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var queue = new Queue<Point>();
		var visited = new HashSet<Point>();
		var paths = new Dictionary<Point, SinglyLinkedList<Point>>();

		visited.Add(start);
		queue.Enqueue(start);
		paths.Add(start, new SinglyLinkedList<Point>(start));

		while (queue.Count > 0)
		{
			var point = queue.Dequeue();
			
			if (point.X < 0 || point.Y < 0 || point.X >= map.Dungeon.GetLength(0) || point.X >= map.Dungeon.GetLength(1)) continue;
			if (map.Dungeon[point.X, point.Y] != MapCell.Empty) continue;
			

			for (int i = -1; i <= 1; i++)
			{
                for (int j = -1; j <= 1; j++)
				{
					if (i != 0 && j != 0) continue;
					
					var next = new Point(point.X + i, point.Y + j);

					if (visited.Contains(next)) continue;

					queue.Enqueue(next);
					visited.Add(next);
					paths.Add(next, new SinglyLinkedList<Point>(next, paths[point]));
					
				}
            }
		}

		foreach (var chest in chests)
		{
			if (!paths.ContainsKey(chest)) yield break;
			yield return paths[chest];
		}
	}
}