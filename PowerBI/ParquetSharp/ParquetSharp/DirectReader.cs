using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000048 RID: 72
	internal sealed class DirectReader<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> : ILogicalBatchReader<TLogical> where TPhysical : struct
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00006E60 File Offset: 0x00005060
		public DirectReader([Nullable(new byte[] { 1, 0 })] ColumnReader<TPhysical> physicalReader, [Nullable(new byte[] { 1, 1, 0 })] LogicalRead<TLogical, TPhysical>.DirectReader directReader)
		{
			this._physicalReader = physicalReader;
			this._directReader = directReader;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006E78 File Offset: 0x00005078
		public int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TLogical> destination)
		{
			int num = 0;
			while (num < destination.Length && this._physicalReader.HasNext)
			{
				int num2 = destination.Length - num;
				int num3 = checked((int)this._directReader(this._physicalReader, destination.Slice(num, num2)));
				num += num3;
			}
			return num;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006ED8 File Offset: 0x000050D8
		public bool HasNext()
		{
			return this._physicalReader.HasNext;
		}

		// Token: 0x0400007E RID: 126
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnReader<TPhysical> _physicalReader;

		// Token: 0x0400007F RID: 127
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalRead<TLogical, TPhysical>.DirectReader _directReader;
	}
}
