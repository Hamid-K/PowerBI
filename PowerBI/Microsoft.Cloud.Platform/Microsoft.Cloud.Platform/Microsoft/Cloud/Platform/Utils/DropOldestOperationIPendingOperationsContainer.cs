using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B0 RID: 688
	internal class DropOldestOperationIPendingOperationsContainer : ThrottlerPendingOperationsContainer
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x000407DD File Offset: 0x0003E9DD
		public DropOldestOperationIPendingOperationsContainer(int maxCapacity)
			: base(maxCapacity)
		{
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00040820 File Offset: 0x0003EA20
		public override bool Enqueue(ThrottlerAsyncResult item)
		{
			ThrottlerAsyncResult result = null;
			if (base.PendingOperations.Count >= base.MaxCapacity)
			{
				result = this.Dequeue();
				result.IsQueueFull = true;
			}
			base.PendingOperations.AddLast(item);
			item.EnqueuedDateTime = DateTime.UtcNow;
			if (result != null)
			{
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					result.SignalCompletion(false, new ThrottlerCancelOperationException());
				}, WaitOrNot.DontWait, "Throttler.Enqueue");
			}
			return true;
		}
	}
}
