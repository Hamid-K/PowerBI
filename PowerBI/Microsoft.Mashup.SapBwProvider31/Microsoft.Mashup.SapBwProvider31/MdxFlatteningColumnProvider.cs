using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000016 RID: 22
	internal sealed class MdxFlatteningColumnProvider : MdxColumnProvider
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000048C8 File Offset: 0x00002AC8
		public MdxFlatteningColumnProvider(SapBwCommand command, MdxCommand mdxCommand, string cubeName)
			: base(command, mdxCommand, cubeName)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000048D4 File Offset: 0x00002AD4
		public override bool TryRetrieveColumns(string datasetId, ColumnInfos columnInfos)
		{
			this.firstDataProvider = MdxFlatteningColumnProvider.MdxFlatteningDataProvider.NewMdxFlatteningDataProvider(this.command, this.mdxCommand.DataSetId, 0, true);
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			if (this.firstDataProvider.DataTable != null)
			{
				int? num = null;
				foreach (IRfcStructure rfcStructure in this.firstDataProvider.DataTable)
				{
					if (num == null)
					{
						num = new int?(rfcStructure[1].GetInt());
					}
					int? num2 = num;
					int @int = rfcStructure[1].GetInt();
					if (!((num2.GetValueOrDefault() == @int) & (num2 != null)))
					{
						break;
					}
					dictionary[rfcStructure[0].GetInt()] = rfcStructure[3].GetString();
				}
			}
			foreach (IRfcStructure rfcStructure2 in this.firstDataProvider.HeaderTable)
			{
				string @string = rfcStructure2[2].GetString();
				string text;
				if (!dictionary.TryGetValue(rfcStructure2[0].GetInt(), out text))
				{
					text = (@string.ToUpperInvariant().Contains("[MEASURES]") ? "FLTP" : rfcStructure2[3].GetString());
				}
				this.Add(new MdxColumn((text == "FLTP") ? MdxColumnType.KeyFigureValue : MdxColumnType.MandatoryDimensionProperty, @string, text, 0, null, null, -1));
			}
			return true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A7C File Offset: 0x00002C7C
		public override DbDataReader GetReader()
		{
			return new MdxFlatteningColumnProvider.MdxFlatteningDataReader(this.command, this.mdxCommand, this, this.firstDataProvider);
		}

		// Token: 0x04000041 RID: 65
		private const int ColumnIndex = 0;

		// Token: 0x04000042 RID: 66
		private const int RowIndex = 1;

		// Token: 0x04000043 RID: 67
		private const int DataIndex = 2;

		// Token: 0x04000044 RID: 68
		private const int ValueDataTypeIndex = 3;

		// Token: 0x04000045 RID: 69
		private const int CellStatus = 4;

		// Token: 0x04000046 RID: 70
		private const int FlatDataStartRow = 0;

		// Token: 0x04000047 RID: 71
		private MdxFlatteningColumnProvider.MdxFlatteningDataProvider firstDataProvider;

		// Token: 0x02000051 RID: 81
		private sealed class MdxFlatteningDataProvider
		{
			// Token: 0x06000334 RID: 820 RVA: 0x0000CE5F File Offset: 0x0000B05F
			private MdxFlatteningDataProvider(IRfcFunction getDataFunction, bool includeHeaderTable, int endRow)
			{
				this.getDataFunction = getDataFunction;
				this.includeHeaderTable = includeHeaderTable;
				this.endRow = endRow;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000335 RID: 821 RVA: 0x0000CE7C File Offset: 0x0000B07C
			public int EndRow
			{
				get
				{
					return this.endRow;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0000CE84 File Offset: 0x0000B084
			public IRfcTable HeaderTable
			{
				get
				{
					if (!this.includeHeaderTable)
					{
						return null;
					}
					return this.getDataFunction.GetTable("HEADER");
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
			public IRfcTable DataTable
			{
				get
				{
					return this.getDataFunction.GetTable("DATA");
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
			public static MdxFlatteningColumnProvider.MdxFlatteningDataProvider NewMdxFlatteningDataProvider(SapBwCommand command, string dataSetId, int startRow, bool includeHeaderTable)
			{
				int num = startRow + command.SapBwConnection.BatchSize - 1;
				IRfcFunction function = command.SapBwConnection.GetFunction(includeHeaderTable ? "BAPI_MDDATASET_GET_FLAT_DATA" : "BAPI_MDDATASET_GET_FS_DATA", true);
				function.SetValue("DATASETID", dataSetId);
				function.SetValue("START_ROW", startRow);
				function.SetValue("END_ROW", num);
				command.SapBwConnection.InvokeFunction(function, true, command, true);
				return new MdxFlatteningColumnProvider.MdxFlatteningDataProvider(function, includeHeaderTable, num);
			}

			// Token: 0x04000233 RID: 563
			private readonly IRfcFunction getDataFunction;

			// Token: 0x04000234 RID: 564
			private readonly bool includeHeaderTable;

			// Token: 0x04000235 RID: 565
			private readonly int endRow;
		}

		// Token: 0x02000052 RID: 82
		private sealed class MdxFlatteningDataReader : MdxDataReader
		{
			// Token: 0x06000339 RID: 825 RVA: 0x0000CF28 File Offset: 0x0000B128
			public MdxFlatteningDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider, MdxFlatteningColumnProvider.MdxFlatteningDataProvider firstDataProvider)
				: base(command, mdxCommand, columnProvider, 0)
			{
				this.decimalSeparator = ((this.connection.SapBwUser.DecimalNotation == SapBwDecimalNotation.DotDecimalSeparatorCommaThousands) ? '.' : ',');
				this.scaleMeasures = command.GetParameterValueOrDefault("SCALEMEASURES", false);
				this.dataProvider = firstDataProvider;
				if (this.dataProvider == null)
				{
					this.startRow = 0;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600033A RID: 826 RVA: 0x0000CF87 File Offset: 0x0000B187
			private MdxFlatteningColumnProvider.MdxFlatteningDataProvider DataProvider
			{
				get
				{
					if (this.dataProvider == null)
					{
						this.dataProvider = MdxFlatteningColumnProvider.MdxFlatteningDataProvider.NewMdxFlatteningDataProvider(this.command, this.mdxCommand.DataSetId, this.startRow, false);
					}
					return this.dataProvider;
				}
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000CFBC File Offset: 0x0000B1BC
			protected override void EnsureInitialized()
			{
				if (this.rowEnumerator == null)
				{
					if (this.DataProvider.DataTable == null || this.DataProvider.DataTable.RowCount == 0)
					{
						this.rowEnumerator = Enumerable.Empty<object[]>().GetEnumerator();
						return;
					}
					this.rowEnumerator = this.EnumerateFlatData();
				}
			}

			// Token: 0x0600033C RID: 828 RVA: 0x0000D00D File Offset: 0x0000B20D
			private IEnumerator<object[]> EnumerateFlatData()
			{
				int num = -1;
				object[] array = null;
				foreach (IRfcStructure cell in this.DataProvider.DataTable)
				{
					if (num != cell[1].GetInt())
					{
						if (array != null)
						{
							yield return array;
						}
						array = this.columnProvider.StartBuildingRecord();
						num = cell[1].GetInt();
					}
					int @int = cell[0].GetInt();
					MdxColumn mdxColumn = this.columnProvider[@int];
					string text = cell[2].GetString();
					if (this.scaleMeasures && mdxColumn.IsKeyFigure)
					{
						text = this.FormattedToValue(text);
					}
					object obj;
					if (mdxColumn.TryExtractValue(text, out obj))
					{
						array[@int] = obj;
					}
					cell = null;
				}
				IEnumerator<IRfcStructure> enumerator = null;
				this.startRow = this.DataProvider.EndRow + 1;
				this.dataProvider = null;
				if (array != null)
				{
					this.columnProvider.FinishBuildingRecord(array);
					yield return array;
				}
				yield break;
				yield break;
			}

			// Token: 0x0600033D RID: 829 RVA: 0x0000D01C File Offset: 0x0000B21C
			private string FormattedToValue(string formatted)
			{
				if (string.IsNullOrEmpty(formatted))
				{
					return formatted;
				}
				bool flag = false;
				bool flag2 = false;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in formatted)
				{
					if (char.IsDigit(c))
					{
						flag2 = true;
						stringBuilder.Append(c);
					}
					else if (c == '-')
					{
						if (flag2)
						{
							throw new FormatException(Resources.InvalidCharacterWhileParsingValue(c, formatted));
						}
						flag2 = true;
						stringBuilder.Append(c);
					}
					else if (c == this.decimalSeparator)
					{
						if (flag)
						{
							throw new FormatException(Resources.InvalidCharacterWhileParsingValue(c, formatted));
						}
						stringBuilder.Append('.');
						flag = true;
					}
					else if (!MdxFlatteningColumnProvider.MdxFlatteningDataReader.validChars.Contains(c) && flag2)
					{
						break;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04000236 RID: 566
			private const char Dot = '.';

			// Token: 0x04000237 RID: 567
			private const char Comma = ',';

			// Token: 0x04000238 RID: 568
			private const char Minus = '-';

			// Token: 0x04000239 RID: 569
			private const char Space = ' ';

			// Token: 0x0400023A RID: 570
			private static readonly HashSet<char> validChars = new HashSet<char> { '.', ',', ' ' };

			// Token: 0x0400023B RID: 571
			private readonly char decimalSeparator;

			// Token: 0x0400023C RID: 572
			private readonly bool scaleMeasures;

			// Token: 0x0400023D RID: 573
			private MdxFlatteningColumnProvider.MdxFlatteningDataProvider dataProvider;

			// Token: 0x0400023E RID: 574
			private int startRow;
		}
	}
}
