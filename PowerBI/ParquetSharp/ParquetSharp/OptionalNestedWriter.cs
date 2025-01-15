using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OptionalNestedWriter<[Nullable(2)] TItem, [IsUnmanaged, Nullable(0)] TPhysical> : ILogicalBatchWriter<Nested<TItem>?> where TPhysical : struct
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00007E50 File Offset: 0x00006050
		public OptionalNestedWriter(ILogicalBatchWriter<TItem> firstInnerWriter, ILogicalBatchWriter<TItem> innerWriter, [Nullable(new byte[] { 1, 0 })] ColumnWriter<TPhysical> physicalWriter, [Nullable(0)] LogicalStreamBuffers<TPhysical> buffers, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			this._firstInnerWriter = firstInnerWriter;
			this._innerWriter = innerWriter;
			this._physicalWriter = physicalWriter;
			this._buffers = buffers;
			this._definitionLevel = definitionLevel;
			this._repetitionLevel = repetitionLevel;
			this._firstRepetitionLevel = firstRepetitionLevel;
			this._buffer = new TItem[buffers.Length];
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007EB0 File Offset: 0x000060B0
		public unsafe void WriteBatch([Nullable(new byte[] { 0, 0, 1 })] ReadOnlySpan<Nested<TItem>?> values)
		{
			if (this._buffers.DefLevels == null)
			{
				throw new Exception("Expected non-null definition levels when writing nullable nested values");
			}
			short num = this._definitionLevel - 1;
			ILogicalBatchWriter<TItem> logicalBatchWriter = this._firstInnerWriter;
			int i = 0;
			while (i < values.Length)
			{
				int num2 = Math.Min(values.Length - i, this._buffer.Length);
				int num3 = num2;
				for (int j = 0; j < num2; j++)
				{
					Nested<TItem>? nested = *values[i + j];
					if (nested == null)
					{
						num3 = j;
						break;
					}
					this._buffer[j] = nested.Value.Value;
				}
				if (num3 > 0)
				{
					logicalBatchWriter.WriteBatch(this._buffer.AsSpan(0, num3));
					i += num3;
				}
				num2 = Math.Min(values.Length - i, this._buffers.Length);
				int num4 = num2;
				for (int k = 0; k < num2; k++)
				{
					Nested<TItem>? nested2 = *values[i + k];
					if (nested2 != null)
					{
						num4 = k;
						break;
					}
				}
				if (num4 > 0)
				{
					for (int l = 0; l < num4; l++)
					{
						this._buffers.DefLevels[l] = num;
					}
					if (this._buffers.RepLevels != null)
					{
						for (int m = 0; m < num4; m++)
						{
							this._buffers.RepLevels[m] = this._repetitionLevel;
						}
						if (i == 0)
						{
							this._buffers.RepLevels[0] = this._firstRepetitionLevel;
						}
					}
					this._physicalWriter.WriteBatch(num4, this._buffers.DefLevels.AsSpan(0, num4), (this._buffers.RepLevels == null) ? null : this._buffers.RepLevels.AsSpan(0, num4), Array.Empty<TPhysical>());
					i += num4;
				}
				logicalBatchWriter = this._innerWriter;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly ILogicalBatchWriter<TItem> _firstInnerWriter;

		// Token: 0x040000A9 RID: 169
		private readonly ILogicalBatchWriter<TItem> _innerWriter;

		// Token: 0x040000AA RID: 170
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnWriter<TPhysical> _physicalWriter;

		// Token: 0x040000AB RID: 171
		[Nullable(0)]
		private readonly LogicalStreamBuffers<TPhysical> _buffers;

		// Token: 0x040000AC RID: 172
		private readonly short _definitionLevel;

		// Token: 0x040000AD RID: 173
		private readonly short _repetitionLevel;

		// Token: 0x040000AE RID: 174
		private readonly short _firstRepetitionLevel;

		// Token: 0x040000AF RID: 175
		private readonly TItem[] _buffer;
	}
}
