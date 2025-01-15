using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x02000012 RID: 18
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005E1B File Offset: 0x0000401B
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005E30 File Offset: 0x00004030
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005E45 File Offset: 0x00004045
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly T[] _array;
	}
}
