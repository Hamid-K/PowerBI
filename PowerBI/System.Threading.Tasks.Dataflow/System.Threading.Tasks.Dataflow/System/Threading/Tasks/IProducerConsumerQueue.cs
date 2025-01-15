using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks
{
	// Token: 0x02000017 RID: 23
	internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000041 RID: 65
		void Enqueue(T item);

		// Token: 0x06000042 RID: 66
		bool TryDequeue([MaybeNullWhen(false)] out T result);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67
		bool IsEmpty { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68
		int Count { get; }

		// Token: 0x06000045 RID: 69
		int GetCountSafe(object syncObj);
	}
}
