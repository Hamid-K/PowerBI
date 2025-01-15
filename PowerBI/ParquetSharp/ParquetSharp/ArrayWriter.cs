using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ArrayWriter<[Nullable(2)] TItem, [IsUnmanaged, Nullable(0)] TPhysical> : ILogicalBatchWriter<TItem[]> where TPhysical : struct
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00007C50 File Offset: 0x00005E50
		public ArrayWriter(ILogicalBatchWriter<TItem> firstElementWriter, ILogicalBatchWriter<TItem> elementWriter, [Nullable(new byte[] { 1, 0 })] ColumnWriter<TPhysical> physicalWriter, bool optionalArrays, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			this._firstElementWriter = firstElementWriter;
			this._elementWriter = elementWriter;
			this._physicalWriter = physicalWriter;
			this._optionalArrays = optionalArrays;
			this._definitionLevel = definitionLevel;
			this._firstRepetitionLevel = firstRepetitionLevel;
			this._repetitionLevel = repetitionLevel;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007C90 File Offset: 0x00005E90
		public unsafe void WriteBatch([Nullable(new byte[] { 0, 1, 1 })] ReadOnlySpan<TItem[]> values)
		{
			short[] array = new short[] { this._definitionLevel };
			short[] array2 = new short[] { this._definitionLevel - 1 };
			ILogicalBatchWriter<TItem> logicalBatchWriter = this._firstElementWriter;
			short[] array3 = new short[] { this._firstRepetitionLevel };
			for (int i = 0; i < values.Length; i++)
			{
				TItem[] array4 = *values[i];
				if (array4 != null)
				{
					if (array4.Length != 0)
					{
						logicalBatchWriter.WriteBatch(array4);
					}
					else
					{
						this._physicalWriter.WriteBatch(1, array, array3, Array.Empty<TPhysical>());
					}
				}
				else
				{
					if (!this._optionalArrays)
					{
						throw new InvalidOperationException("Cannot write a null array value for a required array column");
					}
					this._physicalWriter.WriteBatch(1, array2, array3, Array.Empty<TPhysical>());
				}
				if (i == 0)
				{
					logicalBatchWriter = this._elementWriter;
					array3[0] = this._repetitionLevel;
				}
			}
		}

		// Token: 0x0400009E RID: 158
		private readonly ILogicalBatchWriter<TItem> _firstElementWriter;

		// Token: 0x0400009F RID: 159
		private readonly ILogicalBatchWriter<TItem> _elementWriter;

		// Token: 0x040000A0 RID: 160
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnWriter<TPhysical> _physicalWriter;

		// Token: 0x040000A1 RID: 161
		private readonly short _firstRepetitionLevel;

		// Token: 0x040000A2 RID: 162
		private readonly short _repetitionLevel;

		// Token: 0x040000A3 RID: 163
		private readonly short _definitionLevel;

		// Token: 0x040000A4 RID: 164
		private readonly bool _optionalArrays;
	}
}
