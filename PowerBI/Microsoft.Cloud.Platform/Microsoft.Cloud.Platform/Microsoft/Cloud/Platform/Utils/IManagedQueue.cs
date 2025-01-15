using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000234 RID: 564
	public interface IManagedQueue<T> : IShuttable
	{
		// Token: 0x06000EB0 RID: 3760
		T Dequeue(int millisecondsTimeout);

		// Token: 0x06000EB1 RID: 3761
		T Dequeue();

		// Token: 0x06000EB2 RID: 3762
		bool TryDequeue(out T item);

		// Token: 0x06000EB3 RID: 3763
		void Enqueue(T item);

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000EB4 RID: 3764
		int Count { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000EB5 RID: 3765
		int MaxCapacity { get; }

		// Token: 0x06000EB6 RID: 3766
		void Start();
	}
}
