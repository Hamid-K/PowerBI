using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks
{
	// Token: 0x02000018 RID: 24
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000024CE File Offset: 0x000006CE
		void IProducerConsumerQueue<T>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000024D7 File Offset: 0x000006D7
		bool IProducerConsumerQueue<T>.TryDequeue([MaybeNullWhen(false)] out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000024E0 File Offset: 0x000006E0
		bool IProducerConsumerQueue<T>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024E8 File Offset: 0x000006E8
		int IProducerConsumerQueue<T>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000024F0 File Offset: 0x000006F0
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}
	}
}
