using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x0200000D RID: 13
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000332F File Offset: 0x0000152F
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003343 File Offset: 0x00001543
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003352 File Offset: 0x00001552
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly ReadOnlyMemory<T> _memory;
	}
}
