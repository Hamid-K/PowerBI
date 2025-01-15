using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004A RID: 74
	internal sealed class LeafReader<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> : ILogicalBatchReader<TLogical> where TPhysical : struct
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00006FF8 File Offset: 0x000051F8
		public LeafReader([Nullable(new byte[] { 1, 1, 0 })] BufferedReader<TLogical, TPhysical> bufferedReader)
		{
			this._bufferedReader = bufferedReader;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007008 File Offset: 0x00005208
		public unsafe int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TLogical> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				if (this._bufferedReader.IsEofDefinition)
				{
					return i;
				}
				*destination[i] = this._bufferedReader.ReadValue();
				this._bufferedReader.NextDefinition();
			}
			return destination.Length;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000706C File Offset: 0x0000526C
		public bool HasNext()
		{
			return !this._bufferedReader.IsEofDefinition;
		}

		// Token: 0x04000084 RID: 132
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly BufferedReader<TLogical, TPhysical> _bufferedReader;
	}
}
