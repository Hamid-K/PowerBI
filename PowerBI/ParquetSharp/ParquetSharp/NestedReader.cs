using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class NestedReader<[Nullable(2)] TItem> : ILogicalBatchReader<Nested<TItem>>
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00007378 File Offset: 0x00005578
		public NestedReader(ILogicalBatchReader<TItem> innerReader, int bufferLength)
		{
			this._innerReader = innerReader;
			this._buffer = new TItem[bufferLength];
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007394 File Offset: 0x00005594
		public unsafe int ReadBatch([Nullable(new byte[] { 0, 0, 1 })] Span<Nested<TItem>> destination)
		{
			int i = 0;
			while (i < destination.Length)
			{
				int num = Math.Min(destination.Length - i, this._buffer.Length);
				int num2 = this._innerReader.ReadBatch(this._buffer.AsSpan(0, num));
				for (int j = 0; j < num2; j++)
				{
					*destination[i + j] = new Nested<TItem>(this._buffer[j]);
				}
				i += num2;
				if (num2 < num)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007424 File Offset: 0x00005624
		public bool HasNext()
		{
			return this._innerReader.HasNext();
		}

		// Token: 0x0400008A RID: 138
		private readonly ILogicalBatchReader<TItem> _innerReader;

		// Token: 0x0400008B RID: 139
		private readonly TItem[] _buffer;
	}
}
