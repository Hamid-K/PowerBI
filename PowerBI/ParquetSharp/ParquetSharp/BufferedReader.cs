using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200000E RID: 14
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class BufferedReader<TLogical, [IsUnmanaged, Nullable(0)] TPhysical> where TPhysical : struct, object
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000245C File Offset: 0x0000065C
		public BufferedReader([Nullable(1)] ColumnReader reader, [Nullable(new byte[] { 1, 1, 0 })] LogicalRead<TLogical, TPhysical>.Converter converter, [Nullable(new byte[] { 1, 0 })] TPhysical[] values, short[] defLevels, short[] repLevels, short leafDefinitionLevel, bool nullableLeafValues)
		{
			this._columnReader = reader;
			this._converter = converter;
			this._values = values;
			this._defLevels = defLevels;
			this._repLevels = repLevels;
			this._leafDefinitionLevel = leafDefinitionLevel;
			this._logicalValues = new TLogical[values.Length];
			this._nullableLeafValues = nullableLeafValues;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024B8 File Offset: 0x000006B8
		[NullableContext(1)]
		public TLogical ReadValue()
		{
			if ((long)this._valueIndex >= this._numValues && !this.FillBuffer())
			{
				throw new Exception("Attempt to read past end of column.");
			}
			int num;
			if (!this._nullableLeafValues)
			{
				int valueIndex = this._valueIndex;
				this._valueIndex = valueIndex + 1;
				num = valueIndex;
			}
			else
			{
				num = this._valueIndex;
			}
			int num2 = num;
			return this._logicalValues[num2];
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002528 File Offset: 0x00000728
		public bool ReadValuesAtRepetitionLevel(short repetitionLevel, short definitionLevel, bool atRepetitionStart, [Nullable(new byte[] { 0, 1 })] out ReadOnlySpan<TLogical> values)
		{
			if (this._defLevels == null)
			{
				throw new InvalidOperationException("definition levels not defined");
			}
			if (this._repLevels == null)
			{
				throw new InvalidOperationException("repetition levels not defined");
			}
			if ((long)this._levelIndex >= this._numLevels && !this.FillBuffer())
			{
				throw new Exception("Attempt to read past end of column.");
			}
			int num = (atRepetitionStart ? (this._levelIndex + 1) : this._levelIndex);
			long num2 = this._numLevels;
			int num3 = num;
			while ((long)num3 < this._numLevels)
			{
				if (this._repLevels[num3] < repetitionLevel)
				{
					num2 = (long)num3;
					break;
				}
				if (!this._nullableLeafValues && this._defLevels[num3] < definitionLevel)
				{
					throw new Exception("Definition levels read from file do not match up with schema.");
				}
				num3++;
			}
			int num4 = (int)(num2 - (long)this._levelIndex);
			values = new ReadOnlySpan<TLogical>(this._logicalValues, this._valueIndex, num4);
			this._levelIndex = (int)num2;
			this._valueIndex += num4;
			return num2 < this._numLevels;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002640 File Offset: 0x00000840
		[NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "DefLevel", "RepLevel" })]
		public global::System.ValueTuple<short, short> GetCurrentDefinition()
		{
			if ((long)this._levelIndex >= this._numLevels && !this.FillBuffer())
			{
				throw new Exception("Attempt to read past end of column.");
			}
			short[] defLevels = this._defLevels;
			short num = ((defLevels != null) ? defLevels[this._levelIndex] : 0);
			short[] repLevels = this._repLevels;
			return new global::System.ValueTuple<short, short>(num, (repLevels != null) ? repLevels[this._levelIndex] : 0);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000026B4 File Offset: 0x000008B4
		public bool IsEofDefinition
		{
			get
			{
				return (long)this._levelIndex >= this._numLevels && !this._columnReader.HasNext;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026D8 File Offset: 0x000008D8
		public void NextDefinition()
		{
			this._levelIndex++;
			if (this._nullableLeafValues)
			{
				this._valueIndex++;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002704 File Offset: 0x00000904
		private bool FillBuffer()
		{
			ColumnReader<TPhysical> columnReader = (ColumnReader<TPhysical>)this._columnReader;
			if ((long)this._levelIndex < this._numLevels || (long)this._valueIndex < this._numValues)
			{
				throw new Exception("Values and indices out of sync.");
			}
			if (columnReader.HasNext)
			{
				long num;
				this._numLevels = columnReader.ReadBatch((long)this._values.Length, this._defLevels, this._repLevels, this._values, out num);
				this._valueIndex = 0;
				this._levelIndex = 0;
				this._numValues = (this._nullableLeafValues ? this._numLevels : num);
				Span<short> span = (this._nullableLeafValues ? ((this._defLevels == null) ? null : this._defLevels.AsSpan(0, (int)this._numLevels)) : Array.Empty<short>());
				this._converter(this._values.AsSpan(0, (int)this._numValues), span, this._logicalValues.AsSpan(0, (int)this._numValues), this._leafDefinitionLevel);
			}
			return this._numLevels > 0L;
		}

		// Token: 0x04000013 RID: 19
		[Nullable(1)]
		private readonly ColumnReader _columnReader;

		// Token: 0x04000014 RID: 20
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalRead<TLogical, TPhysical>.Converter _converter;

		// Token: 0x04000015 RID: 21
		[Nullable(new byte[] { 1, 0 })]
		private readonly TPhysical[] _values;

		// Token: 0x04000016 RID: 22
		[Nullable(1)]
		private readonly TLogical[] _logicalValues;

		// Token: 0x04000017 RID: 23
		private readonly short[] _defLevels;

		// Token: 0x04000018 RID: 24
		private readonly short[] _repLevels;

		// Token: 0x04000019 RID: 25
		private readonly short _leafDefinitionLevel;

		// Token: 0x0400001A RID: 26
		private readonly bool _nullableLeafValues;

		// Token: 0x0400001B RID: 27
		private long _numValues;

		// Token: 0x0400001C RID: 28
		private int _valueIndex;

		// Token: 0x0400001D RID: 29
		private long _numLevels;

		// Token: 0x0400001E RID: 30
		private int _levelIndex;
	}
}
