using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x020000D0 RID: 208
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0001CF64 File Offset: 0x0001B164
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001CF7C File Offset: 0x0001B17C
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001CF94 File Offset: 0x0001B194
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04000236 RID: 566
		private readonly T[] _array;
	}
}
