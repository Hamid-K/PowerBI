using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002AD RID: 685
	public interface ILimitedCapacityQueue<T>
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001279 RID: 4729
		int MaxCapacity { get; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600127A RID: 4730
		int Count { get; }

		// Token: 0x0600127B RID: 4731
		T Dequeue();

		// Token: 0x0600127C RID: 4732
		bool Enqueue(T item);

		// Token: 0x0600127D RID: 4733
		T Peek();
	}
}
