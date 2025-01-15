using System;
using System.Threading;

namespace AngleSharp
{
	// Token: 0x0200000F RID: 15
	public interface IEventLoop
	{
		// Token: 0x06000068 RID: 104
		ICancellable Enqueue(Action<CancellationToken> action, TaskPriority priority);

		// Token: 0x06000069 RID: 105
		void Spin();

		// Token: 0x0600006A RID: 106
		void CancelAll();
	}
}
