using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ArrayReader<[IsUnmanaged, Nullable(0)] TPhysical, [Nullable(2)] TLogical, [Nullable(2)] TItem> : ILogicalBatchReader<TItem[]> where TPhysical : struct
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000707C File Offset: 0x0000527C
		public ArrayReader(ILogicalBatchReader<TItem> innerReader, [Nullable(new byte[] { 1, 1, 0 })] BufferedReader<TLogical, TPhysical> bufferedReader, short definitionLevel, short repetitionLevel, bool innerNodeIsOptional)
		{
			this._innerReader = innerReader;
			this._bufferedReader = bufferedReader;
			this._definitionLevel = definitionLevel;
			this._repetitionLevel = repetitionLevel;
			this._innerNodeIsOptional = innerNodeIsOptional;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000070AC File Offset: 0x000052AC
		public unsafe int ReadBatch([Nullable(new byte[] { 0, 2, 1 })] Span<TItem[]> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				if (this._bufferedReader.IsEofDefinition)
				{
					return i;
				}
				global::System.ValueTuple<short, short> currentDefinition = this._bufferedReader.GetCurrentDefinition();
				if (currentDefinition.Item1 > this._definitionLevel)
				{
					if (typeof(TItem) == typeof(TLogical))
					{
						*destination[i] = this.ReadLogicalTypeArray() as TItem[];
					}
					else
					{
						*destination[i] = this.ReadInnerTypeArray();
					}
				}
				else if (currentDefinition.Item1 == this._definitionLevel)
				{
					*destination[i] = Array.Empty<TItem>();
					this._bufferedReader.NextDefinition();
				}
				else
				{
					*destination[i] = null;
					this._bufferedReader.NextDefinition();
				}
			}
			return destination.Length;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000719C File Offset: 0x0000539C
		private TItem[] ReadInnerTypeArray()
		{
			List<TItem> list = new List<TItem>();
			TItem[] array = new TItem[1];
			bool flag = true;
			while (!this._bufferedReader.IsEofDefinition)
			{
				global::System.ValueTuple<short, short> currentDefinition = this._bufferedReader.GetCurrentDefinition();
				if (!flag && currentDefinition.Item2 <= this._repetitionLevel)
				{
					break;
				}
				this._innerReader.ReadBatch(array);
				list.Add(array[0]);
				flag = false;
			}
			return list.ToArray();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007218 File Offset: 0x00005418
		private TLogical[] ReadLogicalTypeArray()
		{
			List<TLogical[]> list = new List<TLogical[]>();
			short num = (this._innerNodeIsOptional ? (this._definitionLevel + 2) : (this._definitionLevel + 1));
			short num2 = this._repetitionLevel + 1;
			bool flag = true;
			while (!this._bufferedReader.IsEofDefinition)
			{
				ReadOnlySpan<TLogical> readOnlySpan;
				bool flag2 = this._bufferedReader.ReadValuesAtRepetitionLevel(num2, num, flag, out readOnlySpan);
				if (flag2 && flag)
				{
					return readOnlySpan.ToArray();
				}
				flag = false;
				list.Add(readOnlySpan.ToArray());
				if (flag2)
				{
					break;
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			int num3 = 0;
			foreach (TLogical[] array in list)
			{
				num3 += array.Length;
			}
			int num4 = 0;
			TLogical[] array2 = new TLogical[num3];
			foreach (TLogical[] array3 in list)
			{
				array3.CopyTo(array2, num4);
				num4 += array3.Length;
			}
			return array2;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007368 File Offset: 0x00005568
		public bool HasNext()
		{
			return !this._bufferedReader.IsEofDefinition;
		}

		// Token: 0x04000085 RID: 133
		private readonly ILogicalBatchReader<TItem> _innerReader;

		// Token: 0x04000086 RID: 134
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly BufferedReader<TLogical, TPhysical> _bufferedReader;

		// Token: 0x04000087 RID: 135
		private readonly short _definitionLevel;

		// Token: 0x04000088 RID: 136
		private readonly short _repetitionLevel;

		// Token: 0x04000089 RID: 137
		private readonly bool _innerNodeIsOptional;
	}
}
