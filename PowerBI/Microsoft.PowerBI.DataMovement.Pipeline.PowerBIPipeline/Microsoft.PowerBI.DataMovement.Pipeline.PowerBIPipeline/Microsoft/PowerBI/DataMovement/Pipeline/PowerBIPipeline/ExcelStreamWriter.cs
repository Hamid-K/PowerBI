using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000009 RID: 9
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ExcelStreamWriter : DataStreamWriter
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		public ExcelStreamWriter(Stream sheetOutputStream, Stream tableOutput, Stream styleStream, ExcelStreamWriterMetadata tableMetadata)
			: base(sheetOutputStream)
		{
			this.m_schemaTableStreamWriter = new StreamWriter(tableOutput, new UTF8Encoding(false), 4096);
			this.m_styleStream = styleStream;
			this.m_columnsFormatting = tableMetadata.ColumnsFormatting;
			this.m_excelCellFormattingSchema = new Tuple<string, string>[this.m_columnsFormatting.Count];
			this.m_stylesDoc = XDocument.Load(this.m_styleStream);
			IList<int> list2;
			if (tableMetadata.Ordering == null)
			{
				IList<int> list = Enumerable.Range(0, tableMetadata.PrimarySelectsMap.Count).ToList<int>();
				list2 = list;
			}
			else
			{
				list2 = tableMetadata.Ordering;
			}
			this.m_ordering = list2;
			this.m_tableDescription = tableMetadata.TableDescription;
			if (this.m_excelCellFormattingSchema.Length != tableMetadata.PrimarySelectsMap.Count)
			{
				throw new ArgumentException("formatting elements number should be the identical to column numbers");
			}
			if (this.m_ordering.Any((int index) => index < 0 || index >= tableMetadata.PrimarySelectsMap.Count))
			{
				throw new ArgumentException("ordering indexes are out of range");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002649 File Offset: 0x00000849
		private XNamespace StylesNamespace
		{
			get
			{
				return this.m_stylesDoc.Root.Name.Namespace;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002660 File Offset: 0x00000860
		private XElement StylesStyleSheetElement
		{
			get
			{
				if (this.m_stylesStyleSheetElement == null)
				{
					this.m_stylesStyleSheetElement = this.m_stylesDoc.Element(this.StylesNamespace + "styleSheet");
				}
				return this.m_stylesStyleSheetElement;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002691 File Offset: 0x00000891
		private XElement StylesNumFmtsElement
		{
			get
			{
				if (this.m_stylesNumFmtsElement == null)
				{
					this.m_stylesNumFmtsElement = this.StylesStyleSheetElement.Element(this.StylesNamespace + "numFmts");
				}
				return this.m_stylesNumFmtsElement;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000026C2 File Offset: 0x000008C2
		private XElement StylesCellXfsElement
		{
			get
			{
				if (this.m_stylesCellXfsElement == null)
				{
					this.m_stylesCellXfsElement = this.StylesStyleSheetElement.Element(this.StylesNamespace + "cellXfs");
				}
				return this.m_stylesCellXfsElement;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026F4 File Offset: 0x000008F4
		internal override async Task WriteHeader(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			await this.OnSheetBegin(outputStreamWriter);
			await this.WriteFiltersDescription(this.m_tableDescription, outputStreamWriter);
			await this.WriteColumnNames(schemaTable, outputStreamWriter);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002747 File Offset: 0x00000947
		internal override Task WriteFooter(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			return this.OnSheetEnd(outputStreamWriter);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002750 File Offset: 0x00000950
		internal override async Task WriteTableAsync(IRawDataPageReader pageReader)
		{
			await base.WriteTableAsync(pageReader);
			await this.WriteTableSchema(pageReader.SchemaTables.First<DataTable>());
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000279C File Offset: 0x0000099C
		internal override async Task WritePageData(List<IColumn> columns, int rowsCount, StreamWriter outputStreamWriter)
		{
			if (columns == null || columns.Count == 0)
			{
				throw new ArgumentException("columns should contain at least one column", "columns");
			}
			if (rowsCount != 0)
			{
				for (int row = 0; row < rowsCount; row++)
				{
					await outputStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<row r=\"{0}\">", this.m_currentRowIndex));
					foreach (int num in this.m_ordering)
					{
						string valueStr;
						Tuple<string, string> excelCellElement = this.BuildExcelCellElement(num, ExcelStreamWriter.EscapeString(columns[num].GetObject(row), out valueStr));
						await outputStreamWriter.WriteAsync(excelCellElement.Item1);
						await outputStreamWriter.WriteAsync(valueStr);
						await outputStreamWriter.WriteAsync(excelCellElement.Item2);
						valueStr = null;
						excelCellElement = null;
					}
					IEnumerator<int> enumerator = null;
					await outputStreamWriter.WriteAsync("</row>");
					this.m_currentRowIndex++;
				}
				await outputStreamWriter.FlushAsync();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027F8 File Offset: 0x000009F8
		private async Task WriteFiltersDescription(string desc, StreamWriter outputStreamWriter)
		{
			await outputStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<row r=\"{0}\">", this.m_currentRowIndex));
			await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[3].Item1);
			await outputStreamWriter.WriteAsync(SecurityElement.Escape(desc));
			await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[3].Item2);
			await outputStreamWriter.WriteAsync("</row>");
			this.m_currentRowIndex++;
			await outputStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<row r=\"{0}\">", this.m_currentRowIndex));
			await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[0].Item1);
			await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[0].Item2);
			await outputStreamWriter.WriteAsync("</row>");
			this.m_currentRowIndex++;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000284C File Offset: 0x00000A4C
		private async Task WriteColumnNames(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			await outputStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<row r=\"{0}\">", this.m_currentRowIndex));
			List<string> list = this.GenerateOrderedAndUniqueColumnNames(schemaTable, this.m_ordering);
			foreach (string columnName in list)
			{
				await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[3].Item1);
				await outputStreamWriter.WriteAsync(columnName);
				await outputStreamWriter.WriteAsync(ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[3].Item2);
				columnName = null;
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			await outputStreamWriter.WriteAsync("</row>");
			this.m_currentRowIndex++;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028A0 File Offset: 0x00000AA0
		private async Task OnSheetBegin(StreamWriter outputStreamWriter)
		{
			await outputStreamWriter.WriteAsync("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			await outputStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<worksheet {0} xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" mc:Ignorable=\"x14ac\" xmlns:x14ac=\"http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac\">", "xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\""));
			await outputStreamWriter.WriteAsync("<dimension ref=\"A1\"/>");
			await outputStreamWriter.WriteAsync("<sheetViews><sheetView tabSelected=\"1\" workbookViewId=\"0\"/></sheetViews>");
			await outputStreamWriter.WriteAsync("<sheetFormatPr defaultRowHeight=\"15\" x14ac:dyDescent=\"0.25\"/>");
			await outputStreamWriter.WriteAsync("<sheetData xml:space=\"preserve\">");
			await outputStreamWriter.FlushAsync();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028E4 File Offset: 0x00000AE4
		private async Task OnSheetEnd(StreamWriter outputStreamWriter)
		{
			await outputStreamWriter.WriteAsync("</sheetData>");
			await outputStreamWriter.WriteAsync("<pageMargins left=\"0.7\" right=\"0.7\" top=\"0.75\" bottom=\"0.75\" header=\"0.3\" footer=\"0.3\"/>");
			await outputStreamWriter.WriteAsync("<tableParts count=\"1\"><tablePart r:id=\"rId1\"/></tableParts>");
			await outputStreamWriter.WriteAsync("</worksheet>");
			await outputStreamWriter.FlushAsync();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002928 File Offset: 0x00000B28
		private static string GetTableRange(long rowNumber, int columnsNumber)
		{
			string text = (3L + rowNumber - 1L).ToString();
			for (int i = columnsNumber; i > 0; i = (i - 1) / ExcelStreamWriter.alphabetLength)
			{
				text = ((char)(65 + (ushort)((i - 1) % ExcelStreamWriter.alphabetLength))).ToString() + text;
			}
			return string.Format(CultureInfo.InvariantCulture, "A{0}:{1}", 3.ToString(), text);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002990 File Offset: 0x00000B90
		private List<string> GenerateOrderedAndUniqueColumnNames(DataTable schemaTable, IList<int> ordering)
		{
			List<string> list = new List<string>();
			foreach (int num in this.m_ordering)
			{
				string text = SecurityElement.Escape((string)schemaTable.Rows[num]["ColumnName"]);
				list.Add(text);
			}
			int num2 = 1;
			List<string> list2 = new List<string>();
			HashSet<string> hashSet = new HashSet<string>(list, StringComparer.OrdinalIgnoreCase);
			foreach (string text2 in list)
			{
				string text3 = text2;
				if (list2.Contains(text3))
				{
					while (hashSet.Contains(text3))
					{
						string text4 = text2;
						int num3;
						num2 = (num3 = num2 + 1);
						text3 = text4 + num3.ToString();
					}
				}
				list2.Add(text3);
				hashSet.Add(text3);
			}
			return list2;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A9C File Offset: 0x00000C9C
		private async Task WriteTableSchema(DataTable schemaTable)
		{
			try
			{
				using (this.m_schemaTableStreamWriter)
				{
					long num = ((base.RowsProcessed > 0L) ? (base.RowsProcessed + 1L) : 2L);
					int columnsCount = this.m_ordering.Count;
					string tableRange = ExcelStreamWriter.GetTableRange(num, columnsCount);
					await this.m_schemaTableStreamWriter.WriteAsync("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
					await this.m_schemaTableStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<table {0} id=\"1\" name=\"{1}\" displayName=\"{2}\" ref=\"{3}\">", new object[] { "xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"", "Table1", "Table1", tableRange }));
					await this.m_schemaTableStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<autoFilter ref=\"{0}\"/>", tableRange));
					await this.m_schemaTableStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<tableColumns count=\"{0}\">", columnsCount));
					int columnId = 1;
					List<string> list = this.GenerateOrderedAndUniqueColumnNames(schemaTable, this.m_ordering);
					foreach (string text in list)
					{
						await this.m_schemaTableStreamWriter.WriteAsync(string.Format(CultureInfo.InvariantCulture, "<tableColumn id=\"{0}\" name=\"{1}\"/>", columnId, text));
						columnId++;
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					await this.m_schemaTableStreamWriter.WriteAsync("</tableColumns>");
					await this.m_schemaTableStreamWriter.WriteAsync("<tableStyleInfo name=\"TableStyleMedium2\" showFirstColumn=\"0\" showLastColumn=\"0\" showRowStripes=\"1\" showColumnStripes=\"0\"/>");
					await this.m_schemaTableStreamWriter.WriteAsync("</table>");
					await this.m_schemaTableStreamWriter.FlushAsync();
					tableRange = null;
				}
				StreamWriter streamWriter = null;
			}
			catch
			{
				DataStreamWriter.CleanupIncompleteOutputStream(this.m_schemaTableStreamWriter);
				throw;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002AE8 File Offset: 0x00000CE8
		private static ExcelStreamWriter.ExcelDataType EscapeString(object value, out string valueStr)
		{
			if (value == null || value == DBNull.Value)
			{
				valueStr = null;
				return ExcelStreamWriter.ExcelDataType.None;
			}
			value = DataStreamWriter.ParseOleDbType(value);
			if (value is bool)
			{
				valueStr = (((bool)value) ? "1" : "0");
				return ExcelStreamWriter.ExcelDataType.Boolean;
			}
			if (value is DateTime)
			{
				valueStr = Convert.ToString(((DateTime)value).ToOADate(), CultureInfo.InvariantCulture);
				return ExcelStreamWriter.ExcelDataType.Date;
			}
			if (ExcelStreamWriter.IsNumericValue(value))
			{
				valueStr = Convert.ToString(value, CultureInfo.InvariantCulture);
				return ExcelStreamWriter.ExcelDataType.Number;
			}
			valueStr = SecurityElement.Escape(value.ToString());
			return ExcelStreamWriter.ExcelDataType.String;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B78 File Offset: 0x00000D78
		private static bool IsNumericValue(object value)
		{
			return value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BE0 File Offset: 0x00000DE0
		protected override void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (this.m_styleStream != null && this.m_stylesDoc != null)
			{
				this.m_styleStream.Position = 0L;
				this.m_styleStream.SetLength(0L);
				this.m_stylesDoc.Save(this.m_styleStream);
			}
			this.m_disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C40 File Offset: 0x00000E40
		private Tuple<string, string> BuildExcelCellElement(int columnIndex, ExcelStreamWriter.ExcelDataType excelDataType)
		{
			if (this.m_excelCellFormattingSchema[columnIndex] == null || this.m_excelCellFormattingSchema[columnIndex] == ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[0])
			{
				Tuple<string, string> tuple = ExcelStreamWriter.ExcelCellDataTypeSchemaMapping[(int)excelDataType];
				if (excelDataType == ExcelStreamWriter.ExcelDataType.Date || excelDataType == ExcelStreamWriter.ExcelDataType.Number)
				{
					int num = this.GenerateStyleIndex(columnIndex, excelDataType);
					if (num != -1)
					{
						tuple = Tuple.Create<string, string>(string.Format(CultureInfo.InvariantCulture, "<c s=\"{0}\"><v>", num), "</v></c>");
					}
				}
				this.m_excelCellFormattingSchema[columnIndex] = tuple;
			}
			return this.m_excelCellFormattingSchema[columnIndex];
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002CB8 File Offset: 0x00000EB8
		private int GenerateStyleIndex(int columnIndex, ExcelStreamWriter.ExcelDataType excelDataType)
		{
			string text = this.m_columnsFormatting[columnIndex];
			if (text != null)
			{
				if (excelDataType != ExcelStreamWriter.ExcelDataType.Number)
				{
					if (excelDataType == ExcelStreamWriter.ExcelDataType.Date)
					{
						if (!Regex.IsMatch(text, "^[a-z]$", RegexOptions.IgnoreCase))
						{
							return this.AddNewFormatToStylesDoc(Regex.Replace(Regex.Replace(text, "%", string.Empty), "tt", "AM/PM"));
						}
						if (text == "d")
						{
							return 1;
						}
						if (text == "G")
						{
							return 2;
						}
					}
				}
				else if (!Regex.IsMatch(text, "^[a-z][0-9]{0,2}$", RegexOptions.IgnoreCase))
				{
					return this.AddNewFormatToStylesDoc(text);
				}
			}
			return -1;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D48 File Offset: 0x00000F48
		private int AddNewFormatToStylesDoc(string formatString)
		{
			int lastNewNumFmtId = this.m_lastNewNumFmtId;
			this.StylesNumFmtsElement.Add(new XElement(this.StylesNamespace + "numFmt", new object[]
			{
				new XAttribute("numFmtId", lastNewNumFmtId),
				new XAttribute("formatCode", formatString)
			}));
			int num = int.Parse(this.StylesNumFmtsElement.Attribute("count").Value);
			this.StylesNumFmtsElement.SetAttributeValue("count", num + 1);
			this.StylesCellXfsElement.Add(new XElement(this.StylesNamespace + "xf", new object[]
			{
				new XAttribute("numFmtId", lastNewNumFmtId),
				new XAttribute("fontId", 0),
				new XAttribute("fillId", 0),
				new XAttribute("borderId", 0),
				new XAttribute("xfId", 0),
				new XAttribute("applyNumberFormat", 1)
			}));
			int num2 = int.Parse(this.StylesCellXfsElement.Attribute("count").Value);
			this.StylesCellXfsElement.SetAttributeValue("count", num2 + 1);
			this.m_lastNewNumFmtId++;
			return num2;
		}

		// Token: 0x0400001A RID: 26
		private const string xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";

		// Token: 0x0400001B RID: 27
		private const string openXmlSchemaAttribute = "xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"";

		// Token: 0x0400001C RID: 28
		private const string tableName = "Table1";

		// Token: 0x0400001D RID: 29
		private const string rowTagOpenFormat = "<row r=\"{0}\">";

		// Token: 0x0400001E RID: 30
		private const string rowTagClose = "</row>";

		// Token: 0x0400001F RID: 31
		private const string cellTagOpenStyleIndexFormat = "<c s=\"{0}\"><v>";

		// Token: 0x04000020 RID: 32
		private const string cellTagClose = "</v></c>";

		// Token: 0x04000021 RID: 33
		private const int dateFormatStyleIndex_d = 1;

		// Token: 0x04000022 RID: 34
		private const int dateFormatStyleIndex_G = 2;

		// Token: 0x04000023 RID: 35
		private const int firstTableRow = 3;

		// Token: 0x04000024 RID: 36
		private static readonly int alphabetLength = 26;

		// Token: 0x04000025 RID: 37
		private static readonly Tuple<string, string>[] ExcelCellDataTypeSchemaMapping = new Tuple<string, string>[]
		{
			Tuple.Create<string, string>("<c>", "</c>"),
			Tuple.Create<string, string>("<c><v>", "</v></c>"),
			Tuple.Create<string, string>("<c t=\"b\"><v>", "</v></c>"),
			Tuple.Create<string, string>("<c t=\"str\"><v>", "</v></c>"),
			Tuple.Create<string, string>("<c s=\"1\"><v>", "</v></c>")
		};

		// Token: 0x04000026 RID: 38
		private readonly StreamWriter m_schemaTableStreamWriter;

		// Token: 0x04000027 RID: 39
		private readonly Stream m_styleStream;

		// Token: 0x04000028 RID: 40
		private readonly IList<string> m_columnsFormatting;

		// Token: 0x04000029 RID: 41
		private readonly XDocument m_stylesDoc;

		// Token: 0x0400002A RID: 42
		private readonly IList<int> m_ordering;

		// Token: 0x0400002B RID: 43
		private readonly string m_tableDescription;

		// Token: 0x0400002C RID: 44
		private Tuple<string, string>[] m_excelCellFormattingSchema;

		// Token: 0x0400002D RID: 45
		private XElement m_stylesStyleSheetElement;

		// Token: 0x0400002E RID: 46
		private XElement m_stylesNumFmtsElement;

		// Token: 0x0400002F RID: 47
		private XElement m_stylesCellXfsElement;

		// Token: 0x04000030 RID: 48
		private int m_lastNewNumFmtId = 180;

		// Token: 0x04000031 RID: 49
		private bool m_disposed;

		// Token: 0x04000032 RID: 50
		private int m_currentRowIndex = 1;

		// Token: 0x02000028 RID: 40
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum ExcelDataType
		{
			// Token: 0x0400009D RID: 157
			None,
			// Token: 0x0400009E RID: 158
			Number,
			// Token: 0x0400009F RID: 159
			Boolean,
			// Token: 0x040000A0 RID: 160
			String,
			// Token: 0x040000A1 RID: 161
			Date
		}
	}
}
