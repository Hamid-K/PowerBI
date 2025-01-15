using System;
using System.Diagnostics;

namespace System.Buffers
{
	// Token: 0x020000E1 RID: 225
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00022C34 File Offset: 0x00020E34
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = (ref sequence).ToArray<T>();
			int num = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory in sequence)
			{
				num++;
			}
			ReadOnlyMemory<T>[] array = new ReadOnlyMemory<T>[num];
			int num2 = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory2 in sequence)
			{
				array[num2] = readOnlyMemory2;
				num2++;
			}
			this._segments = new ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments
			{
				Segments = array
			};
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00022CD0 File Offset: 0x00020ED0
		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00022CD8 File Offset: 0x00020ED8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04000259 RID: 601
		private readonly T[] _array;

		// Token: 0x0400025A RID: 602
		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		// Token: 0x0200014F RID: 335
		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002C72C File Offset: 0x0002A92C
			// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0002C734 File Offset: 0x0002A934
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments { get; set; }
		}
	}
}
