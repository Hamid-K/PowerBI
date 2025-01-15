using System;
using System.Threading;

namespace AngleSharp.Extensions
{
	// Token: 0x020000EA RID: 234
	internal static class EventLoopExtensions
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x000348C0 File Offset: 0x00032AC0
		public static void Enqueue(this IEventLoop loop, Action action, TaskPriority priority = TaskPriority.Normal)
		{
			if (loop != null)
			{
				loop.Enqueue(delegate(CancellationToken c)
				{
					action();
				}, priority);
				return;
			}
			action();
		}
	}
}
