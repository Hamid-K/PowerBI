using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Internal.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000C0 RID: 192
	internal sealed class SystemThreadingCollections_ConcurrentStackDebugView<T>
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x0002BB88 File Offset: 0x00029D88
		public SystemThreadingCollections_ConcurrentStackDebugView(ConcurrentStack<T> stack)
		{
			if (stack == null)
			{
				throw Error.ArgumentNull("stack");
			}
			this.m_stack = stack;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0002BBA5 File Offset: 0x00029DA5
		public T[] Items
		{
			get
			{
				return this.m_stack.ToArray();
			}
		}

		// Token: 0x040001B0 RID: 432
		private ConcurrentStack<T> m_stack;
	}
}
