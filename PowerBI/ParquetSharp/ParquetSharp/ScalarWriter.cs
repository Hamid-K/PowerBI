using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000051 RID: 81
	internal sealed class ScalarWriter<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> : ILogicalBatchWriter<TLogical> where TPhysical : struct
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00007A90 File Offset: 0x00005C90
		public ScalarWriter([Nullable(new byte[] { 1, 0 })] ColumnWriter<TPhysical> physicalWriter, LogicalStreamBuffers<TPhysical> buffers, [Nullable(2)] ByteBuffer byteBuffer, [Nullable(new byte[] { 1, 1, 0 })] LogicalWrite<TLogical, TPhysical>.Converter converter, short definitionLevel, short repetitionLevel, short firstRepetitionLevel, bool optional)
		{
			this._physicalWriter = physicalWriter;
			this._buffers = buffers;
			this._byteBuffer = byteBuffer;
			this._converter = converter;
			this._optional = optional;
			this._definitionLevel = definitionLevel;
			this._repetitionLevel = repetitionLevel;
			this._firstRepetitionLevel = firstRepetitionLevel;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public void WriteBatch([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TLogical> values)
		{
			int i = 0;
			short num = this._definitionLevel - 1;
			bool flag = true;
			while (i < values.Length)
			{
				int num2 = Math.Min(values.Length - i, this._buffers.Length);
				this._converter(values.Slice(i, num2), this._buffers.DefLevels, this._buffers.Values, num);
				if (this._buffers.RepLevels != null)
				{
					for (int j = 0; j < num2; j++)
					{
						this._buffers.RepLevels[j] = this._repetitionLevel;
					}
					if (flag)
					{
						this._buffers.RepLevels[0] = this._firstRepetitionLevel;
					}
				}
				if (!this._optional && this._buffers.DefLevels != null)
				{
					for (int k = 0; k < num2; k++)
					{
						this._buffers.DefLevels[k] = this._definitionLevel;
					}
				}
				this._physicalWriter.WriteBatch(num2, this._buffers.DefLevels, this._buffers.RepLevels, this._buffers.Values);
				i += num2;
				ByteBuffer byteBuffer = this._byteBuffer;
				if (byteBuffer != null)
				{
					byteBuffer.Clear();
				}
				flag = false;
			}
		}

		// Token: 0x04000096 RID: 150
		[Nullable(2)]
		private readonly ByteBuffer _byteBuffer;

		// Token: 0x04000097 RID: 151
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalWrite<TLogical, TPhysical>.Converter _converter;

		// Token: 0x04000098 RID: 152
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnWriter<TPhysical> _physicalWriter;

		// Token: 0x04000099 RID: 153
		private readonly LogicalStreamBuffers<TPhysical> _buffers;

		// Token: 0x0400009A RID: 154
		private readonly short _definitionLevel;

		// Token: 0x0400009B RID: 155
		private readonly short _repetitionLevel;

		// Token: 0x0400009C RID: 156
		private readonly short _firstRepetitionLevel;

		// Token: 0x0400009D RID: 157
		private readonly bool _optional;
	}
}
