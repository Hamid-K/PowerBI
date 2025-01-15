using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class NestedWriter<[Nullable(2)] TItem> : ILogicalBatchWriter<Nested<TItem>>
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00007D9C File Offset: 0x00005F9C
		public NestedWriter(ILogicalBatchWriter<TItem> firstInnerWriter, ILogicalBatchWriter<TItem> innerWriter, int bufferLength)
		{
			this._firstInnerWriter = firstInnerWriter;
			this._innerWriter = innerWriter;
			this._buffer = new TItem[bufferLength];
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public void WriteBatch([Nullable(new byte[] { 0, 0, 1 })] ReadOnlySpan<Nested<TItem>> values)
		{
			int i = 0;
			ILogicalBatchWriter<TItem> logicalBatchWriter = this._firstInnerWriter;
			while (i < values.Length)
			{
				int num = Math.Min(values.Length - i, this._buffer.Length);
				for (int j = 0; j < num; j++)
				{
					this._buffer[j] = values[i + j].Value;
				}
				logicalBatchWriter.WriteBatch(this._buffer.AsSpan(0, num));
				i += num;
				logicalBatchWriter = this._innerWriter;
			}
		}

		// Token: 0x040000A5 RID: 165
		private readonly ILogicalBatchWriter<TItem> _firstInnerWriter;

		// Token: 0x040000A6 RID: 166
		private readonly ILogicalBatchWriter<TItem> _innerWriter;

		// Token: 0x040000A7 RID: 167
		private readonly TItem[] _buffer;
	}
}
