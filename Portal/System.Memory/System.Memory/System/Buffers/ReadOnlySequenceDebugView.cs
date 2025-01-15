using System;
using System.Diagnostics;

namespace System.Buffers
{
	// Token: 0x02000023 RID: 35
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000B440 File Offset: 0x00009640
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = (in sequence).ToArray<T>();
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000B4CD File Offset: 0x000096CD
		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000B4D5 File Offset: 0x000096D5
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04000081 RID: 129
		private readonly T[] _array;

		// Token: 0x04000082 RID: 130
		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		// Token: 0x0200003E RID: 62
		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060002BC RID: 700 RVA: 0x00012AC5 File Offset: 0x00010CC5
			// (set) Token: 0x060002BD RID: 701 RVA: 0x00012ACD File Offset: 0x00010CCD
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments { get; set; }
		}
	}
}
