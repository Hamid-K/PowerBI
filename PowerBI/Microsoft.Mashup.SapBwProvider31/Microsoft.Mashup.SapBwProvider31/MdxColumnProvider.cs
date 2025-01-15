using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000023 RID: 35
	internal abstract class MdxColumnProvider : ColumnProvider
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007982 File Offset: 0x00005B82
		protected MdxColumnProvider(SapBwCommand command, MdxCommand mdxCommand, string cubeName)
		{
			this.connection = (SapBwConnection)command.Connection;
			this.command = command;
			this.mdxCommand = mdxCommand;
			this.cubeName = cubeName;
			this.computedColumnIndices = new List<int>();
		}

		// Token: 0x060001B1 RID: 433
		public abstract bool TryRetrieveColumns(string datasetId, ColumnInfos columnInfos);

		// Token: 0x060001B2 RID: 434
		public abstract DbDataReader GetReader();

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000079BB File Offset: 0x00005BBB
		private FormatStringProvider FormatStringProvider
		{
			get
			{
				if (this.formatStringProvider == null)
				{
					this.formatStringProvider = new FormatStringProvider(this.connection, this.cubeName, this.columns);
				}
				return this.formatStringProvider;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000079E8 File Offset: 0x00005BE8
		public void AddFormatStringColumns(Dictionary<string, string[]> columnsForFormatStringProvider)
		{
			foreach (KeyValuePair<string, string[]> keyValuePair in columnsForFormatStringProvider)
			{
				int[] array;
				if (this.TryGetIndices(keyValuePair, out array))
				{
					string text = Utils.BuildColumnName(keyValuePair.Key, "FORMAT_STRING");
					this.Add(new MdxColumn(MdxColumnType.FormatStringCellProperty, text, "CHAR", 143, null, null, -1)
					{
						ValueProviderKind = new ValueProviderKind?(ValueProviderKind.FormatStringProvider),
						ValueProviderIndices = array
					});
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007A84 File Offset: 0x00005C84
		private bool TryGetIndices(KeyValuePair<string, string[]> measure, out int[] indices)
		{
			int num;
			int num2;
			if (!base.TryGetColumnIndex(measure.Key, out num) || !base.TryGetColumnIndex(measure.Value[0], out num2))
			{
				indices = null;
				return false;
			}
			int num3;
			if (measure.Value.Length == 2 && base.TryGetColumnIndex(measure.Value[0], out num3))
			{
				indices = new int[] { num, num2, num3 };
				return true;
			}
			indices = new int[] { num, num2 };
			return true;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007B00 File Offset: 0x00005D00
		public void ApplyColumnInfos(ColumnInfos columnInfos)
		{
			foreach (MdxColumn mdxColumn in this.columns)
			{
				ColumnInfo columnInfo;
				if (columnInfos != null && columnInfos.TryGetColumnInfo(mdxColumn.ColumnName, out columnInfo))
				{
					int num;
					int num2;
					if (mdxColumn.MdxColumnType == MdxColumnType.KeyFigureValue)
					{
						mdxColumn.ValueType = new SapBwDataType?(columnInfo.DataType);
						mdxColumn.Precision = columnInfo.Precision;
					}
					else if (columnInfo.KeyName != null && base.TryGetColumnIndex(columnInfo.KeyName, out num) && base.TryGetColumnIndex(mdxColumn.ColumnName, out num2))
					{
						mdxColumn.DataType = columnInfo.DataType;
						mdxColumn.DataTypeName = MdxColumn.DataTypeToName[columnInfo.DataType];
						mdxColumn.ValueProviderIndices = new int[] { num };
						mdxColumn.ValueProviderKind = new ValueProviderKind?(ValueProviderKind.ParsingProvider);
					}
				}
				if (mdxColumn.ValueProviderKind != null)
				{
					this.computedColumnIndices.Add(mdxColumn.ColumnOrdinal);
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007C20 File Offset: 0x00005E20
		public override void FinishBuildingRecord(object[] row)
		{
			if (row == null)
			{
				return;
			}
			foreach (int num in this.computedColumnIndices)
			{
				MdxColumn mdxColumn = this.columns[num];
				ValueProviderKind? valueProviderKind = mdxColumn.ValueProviderKind;
				if (valueProviderKind != null)
				{
					ValueProviderKind valueOrDefault = valueProviderKind.GetValueOrDefault();
					if (valueOrDefault != ValueProviderKind.ParsingProvider)
					{
						if (valueOrDefault == ValueProviderKind.FormatStringProvider)
						{
							int num2 = mdxColumn.ValueProviderIndices[0];
							object obj = row[num2];
							int num3 = mdxColumn.ValueProviderIndices[1];
							object obj2 = row[num3];
							string text;
							if (obj2 == DBNull.Value)
							{
								text = null;
							}
							else
							{
								text = obj2 as string;
							}
							string text2 = null;
							if (mdxColumn.ValueProviderIndices.Length == 3)
							{
								int num4 = mdxColumn.ValueProviderIndices[2];
								text2 = row[num4] as string;
							}
							row[num] = this.FormatStringProvider.ComputeFormatString(obj, text, text2, num2, false);
							continue;
						}
					}
					else
					{
						object obj3;
						if (MdxColumnProvider.TryParseValue(mdxColumn.DataType, row[mdxColumn.ValueProviderIndices[0]] as string, out obj3))
						{
							row[num] = obj3;
							continue;
						}
						continue;
					}
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007D48 File Offset: 0x00005F48
		private static bool TryParseValue(SapBwDataType dataType, string text, out object value)
		{
			if (string.IsNullOrEmpty(text))
			{
				value = DBNull.Value;
				return true;
			}
			if (dataType == SapBwDataType.Dats)
			{
				string text2;
				if (MdxColumnProvider.TryExtractValueFromIdentifier(text, "yyyyMMdd".Length, out text2))
				{
					DateTime dateTime;
					if (DateTime.TryParseExact(text2, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
					{
						value = dateTime;
						return true;
					}
					if (text2 == "00000000")
					{
						value = DateTime.MinValue;
						return true;
					}
				}
				throw new SapBwException(Resources.FailedToParseDate(text));
			}
			if (dataType != SapBwDataType.Tims)
			{
				value = null;
				return false;
			}
			string text3;
			TimeSpan timeSpan;
			if (MdxColumnProvider.TryExtractValueFromIdentifier(text, "hhmmss".Length, out text3) && TimeSpan.TryParseExact(text3, "hhmmss", CultureInfo.InvariantCulture, TimeSpanStyles.None, out timeSpan))
			{
				value = timeSpan;
				return true;
			}
			throw new SapBwException(Resources.FailedToParseTime(text));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007E1C File Offset: 0x0000601C
		private static bool TryExtractValueFromIdentifier(string text, int extractLength, out string value)
		{
			int num = text.LastIndexOf("[", StringComparison.Ordinal);
			if (num != -1 && text.Length >= num + extractLength + 1)
			{
				value = text.Substring(num + 1, extractLength);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x040000A4 RID: 164
		protected readonly SapBwConnection connection;

		// Token: 0x040000A5 RID: 165
		protected readonly SapBwCommand command;

		// Token: 0x040000A6 RID: 166
		protected readonly MdxCommand mdxCommand;

		// Token: 0x040000A7 RID: 167
		private readonly string cubeName;

		// Token: 0x040000A8 RID: 168
		private readonly List<int> computedColumnIndices;

		// Token: 0x040000A9 RID: 169
		private FormatStringProvider formatStringProvider;
	}
}
