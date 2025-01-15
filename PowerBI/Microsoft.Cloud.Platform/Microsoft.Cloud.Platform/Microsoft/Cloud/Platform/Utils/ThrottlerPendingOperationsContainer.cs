using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002AE RID: 686
	internal abstract class ThrottlerPendingOperationsContainer : ILimitedCapacityQueue<ThrottlerAsyncResult>
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00040752 File Offset: 0x0003E952
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0004075A File Offset: 0x0003E95A
		internal LinkedList<ThrottlerAsyncResult> PendingOperations { get; private set; }

		// Token: 0x06001280 RID: 4736 RVA: 0x00040763 File Offset: 0x0003E963
		public ThrottlerPendingOperationsContainer(int maxCapacity)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxCapacity, "maxCapacity");
			this.MaxCapacity = maxCapacity;
			this.PendingOperations = new LinkedList<ThrottlerAsyncResult>();
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00040788 File Offset: 0x0003E988
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x00040790 File Offset: 0x0003E990
		public int MaxCapacity { get; private set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00040799 File Offset: 0x0003E999
		public int Count
		{
			get
			{
				return this.PendingOperations.Count;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000407A6 File Offset: 0x0003E9A6
		public virtual ThrottlerAsyncResult Dequeue()
		{
			ThrottlerAsyncResult throttlerAsyncResult = this.Peek();
			if (throttlerAsyncResult != null)
			{
				this.PendingOperations.RemoveFirst();
			}
			return throttlerAsyncResult;
		}

		// Token: 0x06001285 RID: 4741
		public abstract bool Enqueue(ThrottlerAsyncResult item);

		// Token: 0x06001286 RID: 4742 RVA: 0x000407BC File Offset: 0x0003E9BC
		[CanBeNull]
		public virtual ThrottlerAsyncResult Peek()
		{
			if (this.PendingOperations.Count == 0)
			{
				return null;
			}
			return this.PendingOperations.First.Value;
		}
	}
}
