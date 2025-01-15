using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004D RID: 77
	internal sealed class OptionalNestedReader<[IsUnmanaged] TPhysical, [Nullable(2)] TLogical, [Nullable(2)] TItem> : ILogicalBatchReader<Nested<TItem>?> where TPhysical : struct
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00007434 File Offset: 0x00005634
		[NullableContext(1)]
		public OptionalNestedReader(ILogicalBatchReader<TItem> innerReader, [Nullable(new byte[] { 1, 1, 0 })] BufferedReader<TLogical, TPhysical> bufferedReader, short definitionLevel)
		{
			this._innerReader = innerReader;
			this._bufferedReader = bufferedReader;
			this._definitionLevel = definitionLevel;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007454 File Offset: 0x00005654
		public unsafe int ReadBatch([Nullable(new byte[] { 0, 0, 1 })] Span<Nested<TItem>?> destination)
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
					*destination[i] = new Nested<TItem>?(new Nested<TItem>(array[0]));
				}
				else
				{
					*destination[i] = null;
					this._bufferedReader.NextDefinition();
				}
			}
			return destination.Length;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007508 File Offset: 0x00005708
		public bool HasNext()
		{
			return this._innerReader.HasNext();
		}

		// Token: 0x0400008C RID: 140
		[Nullable(1)]
		private readonly ILogicalBatchReader<TItem> _innerReader;

		// Token: 0x0400008D RID: 141
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly BufferedReader<TLogical, TPhysical> _bufferedReader;

		// Token: 0x0400008E RID: 142
		private readonly short _definitionLevel;
	}
}
