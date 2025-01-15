using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FEE RID: 8174
	internal static class ParquetColumnBuffer
	{
		// Token: 0x060110F3 RID: 69875 RVA: 0x003AD743 File Offset: 0x003AB943
		public static IParquetColumnBuffer<T> CreateDense<T>(short maxDefinitionLevel, short maxRepetitionLevel, int maxRowCount)
		{
			return new ParquetColumnBuffer.ParquetDenseColumnBuffer<T>(maxDefinitionLevel, maxRepetitionLevel, maxRowCount, maxRowCount);
		}

		// Token: 0x060110F4 RID: 69876 RVA: 0x003AD74E File Offset: 0x003AB94E
		public static IParquetColumnBuffer<T> CreateDense<T>(short maxDefinitionLevel, short maxRepetitionLevel, int maxValuesCount, int maxLevelsCount)
		{
			return new ParquetColumnBuffer.ParquetDenseColumnBuffer<T>(maxDefinitionLevel, maxRepetitionLevel, maxValuesCount, maxLevelsCount);
		}

		// Token: 0x02001FEF RID: 8175
		private class ParquetDenseColumnBuffer<T> : IParquetColumnBuffer<T>
		{
			// Token: 0x060110F5 RID: 69877 RVA: 0x003AD75C File Offset: 0x003AB95C
			public ParquetDenseColumnBuffer(short maxDefinitionLevel, short maxRepetitionLevel, int maxValuesCount, int maxLevelsCount)
			{
				this.maxDefinitionLevel = maxDefinitionLevel;
				this.maxRepetitionLevel = maxRepetitionLevel;
				this.values = new T[maxValuesCount];
				if (maxDefinitionLevel > 0 || maxRepetitionLevel > 0)
				{
					this.defLevels = new short[maxLevelsCount];
				}
				if (maxRepetitionLevel > 0)
				{
					this.repLevels = new short[maxLevelsCount];
				}
			}

			// Token: 0x17002D0C RID: 11532
			// (get) Token: 0x060110F6 RID: 69878 RVA: 0x003AD7AF File Offset: 0x003AB9AF
			public short MaxDefinitionLevel
			{
				get
				{
					return this.maxDefinitionLevel;
				}
			}

			// Token: 0x17002D0D RID: 11533
			// (get) Token: 0x060110F7 RID: 69879 RVA: 0x003AD7B7 File Offset: 0x003AB9B7
			public short MaxRepetitionLevel
			{
				get
				{
					return this.maxRepetitionLevel;
				}
			}

			// Token: 0x17002D0E RID: 11534
			// (get) Token: 0x060110F8 RID: 69880 RVA: 0x003AD7BF File Offset: 0x003AB9BF
			public T[] Values
			{
				get
				{
					return this.values;
				}
			}

			// Token: 0x17002D0F RID: 11535
			// (get) Token: 0x060110F9 RID: 69881 RVA: 0x003AD7C7 File Offset: 0x003AB9C7
			// (set) Token: 0x060110FA RID: 69882 RVA: 0x003AD7D0 File Offset: 0x003AB9D0
			public int ValuesOffset
			{
				get
				{
					return this.valuesOffset;
				}
				set
				{
					int num = this.valuesOffset + this.ValuesCount;
					this.valuesOffset = value;
					this.valuesCount = Math.Max(0, num - value);
				}
			}

			// Token: 0x17002D10 RID: 11536
			// (get) Token: 0x060110FB RID: 69883 RVA: 0x003AD801 File Offset: 0x003ABA01
			// (set) Token: 0x060110FC RID: 69884 RVA: 0x003AD809 File Offset: 0x003ABA09
			public int ValuesCount
			{
				get
				{
					return this.valuesCount;
				}
				set
				{
					this.valuesCount = value;
				}
			}

			// Token: 0x17002D11 RID: 11537
			// (get) Token: 0x060110FD RID: 69885 RVA: 0x003AD812 File Offset: 0x003ABA12
			public short[] DefinitionLevels
			{
				get
				{
					return this.defLevels;
				}
			}

			// Token: 0x17002D12 RID: 11538
			// (get) Token: 0x060110FE RID: 69886 RVA: 0x003AD81A File Offset: 0x003ABA1A
			public short[] RepetitionLevels
			{
				get
				{
					return this.repLevels;
				}
			}

			// Token: 0x17002D13 RID: 11539
			// (get) Token: 0x060110FF RID: 69887 RVA: 0x003AD822 File Offset: 0x003ABA22
			// (set) Token: 0x06011100 RID: 69888 RVA: 0x003AD82C File Offset: 0x003ABA2C
			public int LevelsOffset
			{
				get
				{
					return this.levelsOffset;
				}
				set
				{
					int num = this.levelsOffset + this.LevelsCount;
					this.levelsOffset = value;
					this.levelsCount = Math.Max(0, num - value);
				}
			}

			// Token: 0x17002D14 RID: 11540
			// (get) Token: 0x06011101 RID: 69889 RVA: 0x003AD85D File Offset: 0x003ABA5D
			// (set) Token: 0x06011102 RID: 69890 RVA: 0x003AD865 File Offset: 0x003ABA65
			public int LevelsCount
			{
				get
				{
					return this.levelsCount;
				}
				set
				{
					this.levelsCount = value;
				}
			}

			// Token: 0x17002D15 RID: 11541
			// (get) Token: 0x06011103 RID: 69891 RVA: 0x003AD86E File Offset: 0x003ABA6E
			private int LevelsLength
			{
				get
				{
					if (this.DefinitionLevels != null)
					{
						return this.DefinitionLevels.Length;
					}
					if (this.RepetitionLevels != null)
					{
						return this.RepetitionLevels.Length;
					}
					return int.MaxValue;
				}
			}

			// Token: 0x06011104 RID: 69892 RVA: 0x003AD898 File Offset: 0x003ABA98
			public int Read(ColumnReader reader, int batchSize = 2147483647)
			{
				batchSize = Math.Min(batchSize, Math.Min(this.Values.Length - this.ValuesCount - this.ValuesOffset, this.LevelsLength - this.LevelsCount - this.LevelsOffset));
				ArraySegment arraySegment = new ArraySegment(this.Values, this.ValuesOffset + this.ValuesCount, batchSize);
				ArraySegment<short>? arraySegment2 = ((this.DefinitionLevels != null) ? new ArraySegment<short>?(new ArraySegment<short>(this.DefinitionLevels, this.LevelsOffset + this.LevelsCount, batchSize)) : null);
				ArraySegment<short>? arraySegment3 = ((this.RepetitionLevels != null) ? new ArraySegment<short>?(new ArraySegment<short>(this.RepetitionLevels, this.LevelsOffset + this.LevelsCount, batchSize)) : null);
				long num2;
				long num = reader.ReadBatch((long)batchSize, arraySegment2, arraySegment3, arraySegment, out num2);
				this.valuesCount += (int)num2;
				this.levelsCount += (int)num;
				return (int)num;
			}

			// Token: 0x06011105 RID: 69893 RVA: 0x003AD990 File Offset: 0x003ABB90
			public void Write(ColumnWriter writer)
			{
				int num = Math.Max(this.ValuesCount, this.LevelsCount);
				ArraySegment arraySegment = new ArraySegment(this.Values, this.ValuesOffset, this.ValuesCount);
				ArraySegment<short>? arraySegment2 = ((this.DefinitionLevels != null) ? new ArraySegment<short>?(new ArraySegment<short>(this.DefinitionLevels, this.LevelsOffset, this.LevelsCount)) : null);
				ArraySegment<short>? arraySegment3 = ((this.RepetitionLevels != null) ? new ArraySegment<short>?(new ArraySegment<short>(this.RepetitionLevels, this.LevelsOffset, this.LevelsCount)) : null);
				writer.WriteBatch(num, arraySegment2, arraySegment3, arraySegment);
				this.valuesOffset += this.valuesCount;
				this.valuesCount = 0;
				this.levelsOffset += this.levelsCount;
				this.levelsCount = 0;
			}

			// Token: 0x06011106 RID: 69894 RVA: 0x003ADA68 File Offset: 0x003ABC68
			public void Clear()
			{
				this.valuesOffset = 0;
				this.valuesCount = 0;
				this.levelsOffset = 0;
				this.levelsCount = 0;
			}

			// Token: 0x0400672A RID: 26410
			private readonly short maxDefinitionLevel;

			// Token: 0x0400672B RID: 26411
			private readonly short maxRepetitionLevel;

			// Token: 0x0400672C RID: 26412
			private readonly T[] values;

			// Token: 0x0400672D RID: 26413
			private readonly short[] defLevels;

			// Token: 0x0400672E RID: 26414
			private readonly short[] repLevels;

			// Token: 0x0400672F RID: 26415
			private int valuesOffset;

			// Token: 0x04006730 RID: 26416
			private int levelsOffset;

			// Token: 0x04006731 RID: 26417
			private int valuesCount;

			// Token: 0x04006732 RID: 26418
			private int levelsCount;
		}
	}
}
