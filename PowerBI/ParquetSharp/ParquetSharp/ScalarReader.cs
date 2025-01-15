using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000049 RID: 73
	internal sealed class ScalarReader<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> : ILogicalBatchReader<TLogical> where TPhysical : struct
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00006EE8 File Offset: 0x000050E8
		public ScalarReader([Nullable(new byte[] { 1, 0 })] ColumnReader<TPhysical> physicalReader, [Nullable(new byte[] { 1, 1, 0 })] LogicalRead<TLogical, TPhysical>.Converter converter, LogicalStreamBuffers<TPhysical> buffers, short definitionLevel)
		{
			this._physicalReader = physicalReader;
			this._converter = converter;
			this._buffers = buffers;
			this._definitionLevel = definitionLevel;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006F10 File Offset: 0x00005110
		public int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TLogical> destination)
		{
			int num = 0;
			while (num < destination.Length && this._physicalReader.HasNext)
			{
				int num2 = Math.Min(destination.Length - num, this._buffers.Length);
				int num3;
				checked
				{
					long num4;
					num3 = (int)this._physicalReader.ReadBatch(unchecked((long)num2), this._buffers.DefLevels, this._buffers.RepLevels, this._buffers.Values, out num4);
					this._converter(this._buffers.Values.AsSpan(0, (int)num4), this._buffers.DefLevels, destination.Slice(num, num3), this._definitionLevel);
				}
				num += num3;
			}
			return num;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006FE8 File Offset: 0x000051E8
		public bool HasNext()
		{
			return this._physicalReader.HasNext;
		}

		// Token: 0x04000080 RID: 128
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnReader<TPhysical> _physicalReader;

		// Token: 0x04000081 RID: 129
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalRead<TLogical, TPhysical>.Converter _converter;

		// Token: 0x04000082 RID: 130
		private readonly LogicalStreamBuffers<TPhysical> _buffers;

		// Token: 0x04000083 RID: 131
		private readonly short _definitionLevel;
	}
}
