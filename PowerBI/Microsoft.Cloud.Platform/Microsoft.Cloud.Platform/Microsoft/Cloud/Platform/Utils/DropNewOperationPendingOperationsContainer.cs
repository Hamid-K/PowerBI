using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002AF RID: 687
	internal class DropNewOperationPendingOperationsContainer : ThrottlerPendingOperationsContainer
	{
		// Token: 0x06001287 RID: 4743 RVA: 0x000407DD File Offset: 0x0003E9DD
		public DropNewOperationPendingOperationsContainer(int maxCapacity)
			: base(maxCapacity)
		{
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000407E6 File Offset: 0x0003E9E6
		public override bool Enqueue(ThrottlerAsyncResult item)
		{
			if (base.PendingOperations.Count < base.MaxCapacity)
			{
				base.PendingOperations.AddLast(item);
				item.EnqueuedDateTime = DateTime.UtcNow;
				return true;
			}
			item.IsQueueFull = true;
			return false;
		}
	}
}
