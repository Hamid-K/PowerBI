using System;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000C3 RID: 195
	internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0002BBB2 File Offset: 0x00029DB2
		public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0002BBCF File Offset: 0x00029DCF
		[DebuggerBrowsable(3)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x040001B1 RID: 433
		private IProducerConsumerCollection<T> m_collection;
	}
}
