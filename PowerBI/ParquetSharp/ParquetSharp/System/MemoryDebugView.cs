using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x020000CB RID: 203
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00019F40 File Offset: 0x00018140
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00019F54 File Offset: 0x00018154
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00019F64 File Offset: 0x00018164
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		// Token: 0x0400022A RID: 554
		private readonly ReadOnlyMemory<T> _memory;
	}
}
