using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004E RID: 78
	internal sealed class OptionalReader<[IsUnmanaged] TPhysical, [Nullable(2)] TLogical, TItem> : ILogicalBatchReader<TItem?> where TPhysical : struct where TItem : struct
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00007518 File Offset: 0x00005718
		public OptionalReader([Nullable(new byte[] { 1, 0 })] ILogicalBatchReader<TItem> innerReader, [Nullable(new byte[] { 1, 1, 0 })] BufferedReader<TLogical, TPhysical> bufferedReader, short definitionLevel)
		{
			this._innerReader = innerReader;
			this._bufferedReader = bufferedReader;
			this._definitionLevel = definitionLevel;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007538 File Offset: 0x00005738
		public unsafe int ReadBatch(Span<TItem?> destination)
		{
			TItem[] array = new TItem[1];
			for (int i = 0; i < destination.Length; i++)
			{
				if (this._bufferedReader.IsEofDefinition)
				{
					return i;
				}
				if (this._bufferedReader.GetCurrentDefinition().Item1 >= this._definitionLevel)
				{
					this._innerReader.ReadBatch(array);
					*destination[i] = new TItem?(array[0]);
				}
				else
				{
					*destination[i] = null;
					this._bufferedReader.NextDefinition();
				}
			}
			return destination.Length;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000075E8 File Offset: 0x000057E8
		public bool HasNext()
		{
			return this._innerReader.HasNext();
		}

		// Token: 0x0400008F RID: 143
		[Nullable(new byte[] { 1, 0 })]
		private readonly ILogicalBatchReader<TItem> _innerReader;

		// Token: 0x04000090 RID: 144
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly BufferedReader<TLogical, TPhysical> _bufferedReader;

		// Token: 0x04000091 RID: 145
		private readonly short _definitionLevel;
	}
}
