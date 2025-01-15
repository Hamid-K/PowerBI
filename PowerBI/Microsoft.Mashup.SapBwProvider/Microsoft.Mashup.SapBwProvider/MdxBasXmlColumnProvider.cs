using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000020 RID: 32
	internal sealed class MdxBasXmlColumnProvider : MdxColumnProvider
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000073A8 File Offset: 0x000055A8
		public MdxBasXmlColumnProvider(SapBwCommand command, MdxCommand mdxCommand, string cubeName)
			: base(command, mdxCommand, cubeName)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000073B4 File Offset: 0x000055B4
		public override bool TryRetrieveColumns(string datasetId, ColumnInfos columnInfos)
		{
			IRfcFunction function = this.connection.GetFunction("RSR_MDX_BXML_GET_INFO", true);
			function.SetValue("DATASETID", datasetId);
			this.connection.InvokeFunction(function, true, this.command, true);
			IRfcTable table = function.GetTable("DATA_INFO");
			HashSet<int> selectedOrdinals = null;
			Dictionary<string, string> dictionary = MdxBasXmlColumnProvider.ExtractColumns(table, function.GetTable("HEADER"), delegate(IRfcStructure data, string columnName, int columnOrdinal, string fieldName, MdxColumnType isKeyFigure)
			{
				if (columnInfos != null && columnInfos.ColumnNames.Contains(columnName))
				{
					if (selectedOrdinals == null)
					{
						selectedOrdinals = new HashSet<int>();
					}
					selectedOrdinals.Add(columnOrdinal);
				}
			});
			Dictionary<string, string[]> dictionary2 = new Dictionary<string, string[]>();
			IRfcFunction function2 = this.connection.GetFunction("RSR_MDX_BXML_SET_BINDING", true);
			function2.SetValue("DATASETID", datasetId);
			IRfcTable table2 = function2.GetTable("BINDING");
			table2.Append(table.RowCount);
			int num = 0;
			foreach (IRfcStructure rfcStructure in table)
			{
				int @int = rfcStructure.GetInt(0);
				table2.CurrentIndex = num;
				table2.SetValue(0, @int);
				if (selectedOrdinals == null || selectedOrdinals.Count == 0 || selectedOrdinals.Contains(@int))
				{
					table2.SetValue(1, 'X');
				}
				string @string = rfcStructure.GetString(1);
				if (@string.StartsWith("C", StringComparison.Ordinal))
				{
					HashSet<MdxBasXmlColumnProvider.CellFormat> hashSet = new HashSet<MdxBasXmlColumnProvider.CellFormat> { MdxBasXmlColumnProvider.CellFormat.Value };
					string text;
					if (columnInfos == null)
					{
						hashSet.Add(MdxBasXmlColumnProvider.CellFormat.Unit);
					}
					else if (dictionary.TryGetValue(@string, out text))
					{
						string text2 = Utils.BuildColumnName(text, "UNIT_OF_MEASURE");
						if (columnInfos.ColumnNames.Contains(Utils.BuildColumnName(text, "FORMAT_STRING")))
						{
							hashSet.Add(MdxBasXmlColumnProvider.CellFormat.Unit);
							hashSet.Add(MdxBasXmlColumnProvider.CellFormat.Status);
							string text3 = Utils.BuildColumnName(text, "CELL_STATUS");
							dictionary2.Add(text, new string[] { text2, text3 });
						}
						else if (columnInfos.ColumnNames.Contains(text2))
						{
							hashSet.Add(MdxBasXmlColumnProvider.CellFormat.Unit);
						}
						if (columnInfos.ColumnNames.Contains(Utils.BuildColumnName(text, "FORMATTED_VALUE")))
						{
							hashSet.Add(MdxBasXmlColumnProvider.CellFormat.FormattedValue);
						}
					}
					table2.SetValue(2, MdxBasXmlColumnProvider.GetCellFormat(0, hashSet.ToArray<MdxBasXmlColumnProvider.CellFormat>()));
				}
				num++;
			}
			this.connection.InvokeFunction(function2, true, this.command, true);
			this.connection.InvokeFunction(function, true, this.command, true);
			MdxBasXmlColumnProvider.ExtractColumns(function.GetTable("DATA_INFO"), function.GetTable("HEADER"), delegate(IRfcStructure data, string columnName, int columnOrdinal, string fieldName, MdxColumnType columnType)
			{
				this.Add(new MdxColumn(columnType, columnName, data[2].GetString(), data[3].GetInt(), new int?(data[4].GetInt()), fieldName, -1));
			});
			base.AddFormatStringColumns(dictionary2);
			return true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007680 File Offset: 0x00005880
		private static Dictionary<string, string> ExtractColumns(IRfcTable dataInfo, IRfcTable headerInfo, Action<IRfcStructure, string, int, string, MdxColumnType> processColumn)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
			foreach (IRfcStructure rfcStructure in headerInfo)
			{
				dictionary2[rfcStructure.GetInt(0)] = rfcStructure.GetString(2);
			}
			foreach (IRfcStructure rfcStructure2 in dataInfo)
			{
				int @int = rfcStructure2[0].GetInt();
				string @string = rfcStructure2[1].GetString();
				MdxColumnType mdxColumnType = MdxBasXmlColumnProvider.ToColumnType(@string);
				string text;
				if (dictionary2.TryGetValue(@int, out text) && mdxColumnType == MdxColumnType.KeyFigureValue)
				{
					text = MdxBasXmlColumnProvider.CleanColumnName(text);
				}
				dictionary[@string] = text;
				processColumn(rfcStructure2, text, @int, @string, mdxColumnType);
			}
			return dictionary;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007770 File Offset: 0x00005970
		public override DbDataReader GetReader()
		{
			if (this.command.SapBwCommandType == SapBwCommandType.MdxBasXml)
			{
				return new MdxBasXmlDataReader(this.command, this.mdxCommand, this);
			}
			return new MdxBasXmlCompressedDataReader(this.command, this.mdxCommand, this);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000077A6 File Offset: 0x000059A6
		private static MdxColumnType ToColumnType(string fieldName)
		{
			if (fieldName != null)
			{
				if (fieldName.StartsWith("CEL", StringComparison.Ordinal))
				{
					return MdxColumnType.KeyFigureValue;
				}
				if (fieldName.StartsWith("CUC", StringComparison.Ordinal))
				{
					return MdxColumnType.UnitOfMeasureCellProperty;
				}
				if (fieldName.StartsWith("CFT", StringComparison.Ordinal))
				{
					return MdxColumnType.FormattedValueCellProperty;
				}
			}
			return MdxColumnType.MandatoryDimensionProperty;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000077DC File Offset: 0x000059DC
		private static int GetCellFormat(int value, params MdxBasXmlColumnProvider.CellFormat[] formats)
		{
			int num = value;
			foreach (MdxBasXmlColumnProvider.CellFormat cellFormat in formats)
			{
				num |= 1 << (int)cellFormat;
			}
			return num;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007809 File Offset: 0x00005A09
		private static string CleanColumnName(string columnName)
		{
			if (columnName != null && columnName.EndsWith(".VALUE", StringComparison.OrdinalIgnoreCase))
			{
				columnName = columnName.Substring(0, columnName.LastIndexOf('.'));
			}
			return columnName;
		}

		// Token: 0x04000097 RID: 151
		private static readonly int cellFormatValueUnit = MdxBasXmlColumnProvider.GetCellFormat(0, new MdxBasXmlColumnProvider.CellFormat[]
		{
			MdxBasXmlColumnProvider.CellFormat.Value,
			MdxBasXmlColumnProvider.CellFormat.Unit
		});

		// Token: 0x04000098 RID: 152
		private const int DataInfoColumnOrdinal = 0;

		// Token: 0x04000099 RID: 153
		private const int DataInfoFieldName = 1;

		// Token: 0x0400009A RID: 154
		private const int DataInfoDataType = 2;

		// Token: 0x0400009B RID: 155
		private const int DataInfoLength = 3;

		// Token: 0x0400009C RID: 156
		private const int DataInfoDecimals = 4;

		// Token: 0x0400009D RID: 157
		private const int BindingTableColumnOrdinal = 0;

		// Token: 0x0400009E RID: 158
		private const int BindingTableTransfer = 1;

		// Token: 0x0400009F RID: 159
		private const int BindingTableCellFormat = 2;

		// Token: 0x040000A0 RID: 160
		private const int HeaderTableColumnOrdinal = 0;

		// Token: 0x040000A1 RID: 161
		private const int HeaderTableRowOrdinal = 1;

		// Token: 0x040000A2 RID: 162
		private const int HeaderTableData = 2;

		// Token: 0x02000067 RID: 103
		private enum CellFormat
		{
			// Token: 0x040002D7 RID: 727
			Value = 7,
			// Token: 0x040002D8 RID: 728
			FormattedValue = 6,
			// Token: 0x040002D9 RID: 729
			Unit = 5,
			// Token: 0x040002DA RID: 730
			Status = 4
		}
	}
}
