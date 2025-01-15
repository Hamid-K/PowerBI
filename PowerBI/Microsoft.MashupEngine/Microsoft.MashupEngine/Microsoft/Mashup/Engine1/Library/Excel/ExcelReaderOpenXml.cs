using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C34 RID: 3124
	internal sealed class ExcelReaderOpenXml
	{
		// Token: 0x060054DF RID: 21727 RVA: 0x001241DC File Offset: 0x001223DC
		public ExcelReaderOpenXml(IEngineHost host, ExcelFile content, bool useFirstRowAsHeader, bool inferTypes, bool inferSheetDimensions, bool loadFormattingInfo = false)
		{
			this.engineHost = host;
			this.content = content;
			IPersistentCache persistentCache = this.engineHost.GetPersistentCache();
			this.cache = new ExcelReaderOpenXml.TimestampedCache(this, persistentCache);
			this.useFirstRowAsHeader = useFirstRowAsHeader;
			this.inferTypes = inferTypes;
			this.inferSheetDimensions = inferSheetDimensions;
			this.loadFormattingInfo = loadFormattingInfo;
			this.nameTable = new NameTable();
			this.cellName = this.nameTable.Add("c");
			this.cellXfsName = this.nameTable.Add("cellXfs");
			this.definedNameName = this.nameTable.Add("definedName");
			this.definedNamesName = this.nameTable.Add("definedNames");
			this.dimensionName = this.nameTable.Add("dimension");
			this.formatCodeName = this.nameTable.Add("formatCode");
			this.idName = this.nameTable.Add("id");
			this.inlineStringName = this.nameTable.Add("is");
			this.mainNamespaceName = this.nameTable.Add("http://schemas.openxmlformats.org/spreadsheetml/2006/main");
			this.nameName = this.nameTable.Add("name");
			this.numFmtIdName = this.nameTable.Add("numFmtId");
			this.numFmtName = this.nameTable.Add("numFmt");
			this.numFmtsName = this.nameTable.Add("numFmts");
			this.rName = this.nameTable.Add("r");
			this.refName = this.nameTable.Add("ref");
			this.relNamespaceName = this.nameTable.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			this.rowName = this.nameTable.Add("row");
			this.sName = this.nameTable.Add("s");
			this.sharedStringItemName = this.nameTable.Add("si");
			this.sheetDataName = this.nameTable.Add("sheetData");
			this.sheetName = this.nameTable.Add("sheet");
			this.sheetsName = this.nameTable.Add("sheets");
			this.stateName = this.nameTable.Add("state");
			this.tName = this.nameTable.Add("t");
			this.valueName = this.nameTable.Add("v");
			this.xfName = this.nameTable.Add("xf");
			this.localSheetId = this.nameTable.Add("localSheetId");
			this.hidden = this.nameTable.Add("hidden");
			this.options = string.Empty;
			if (this.useFirstRowAsHeader)
			{
				this.options += "+UseFirstRowAsHeader";
			}
			if (this.inferTypes)
			{
				this.options += "+InferType";
			}
			if (this.inferSheetDimensions)
			{
				this.options += "+InferSheetDimensions";
			}
			if (this.loadFormattingInfo)
			{
				this.options += "+LoadFormattingInfo";
			}
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0012452C File Offset: 0x0012272C
		public TableValue ReadTables()
		{
			TableValue tableValue;
			try
			{
				tableValue = this.ReadTablesUnguarded();
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (OpenXmlPackageException)
			{
				throw;
			}
			catch (FileFormatException)
			{
				throw;
			}
			catch (RuntimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				this.ExceptionHandler(ex);
				throw;
			}
			return tableValue;
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0012459C File Offset: 0x0012279C
		private string CreateCacheKey(string kind, params string[] keys)
		{
			string[] array = new string[keys.Length + 4];
			array[0] = this.content.CacheKey;
			array[1] = this.workbookStreamId;
			array[2] = this.options;
			array[3] = kind;
			keys.CopyTo(array, 4);
			return PersistentCacheKey.ExcelWorkbook.Qualify(array);
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x001245F0 File Offset: 0x001227F0
		private XmlReader CreateReader(Uri partUri)
		{
			Stream stream = this.content.Open();
			XmlReader xmlReader;
			try
			{
				ZipArchive archive = new ZipArchive(stream);
				ZipArchiveEntry entry = archive.GetEntry(partUri.ToString().TrimStart(new char[] { '/' }));
				if (entry == null)
				{
					archive.Dispose();
					throw ValueException.NewDataFormatError<Message1>(Strings.ExcelCantFindPart(partUri), this.content.Content, null);
				}
				XmlReaderSettings xmlReaderSettings = XmlModuleHelper.DefaultXmlReaderSettings.Clone();
				xmlReaderSettings.NameTable = this.nameTable;
				Stream entryStream = entry.Open();
				xmlReader = XmlHelperUtility.XmlReaderCreate(entryStream.OnDispose(delegate
				{
					entryStream.Dispose();
					archive.Dispose();
				}), xmlReaderSettings);
			}
			catch
			{
				stream.Dispose();
				throw;
			}
			return xmlReader;
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x001246C4 File Offset: 0x001228C4
		private static string ToString(uint i)
		{
			return i.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x001246D4 File Offset: 0x001228D4
		private static void GetCellLocation(string reference, out uint row, out ushort col)
		{
			row = 0U;
			col = 0;
			uint num = 1U;
			ushort num2 = 1;
			bool flag = false;
			for (int i = reference.Length - 1; i >= 0; i--)
			{
				char c = reference[i];
				if (c >= '0' && c <= '9')
				{
					row += (uint)((long)(c - '0') * (long)((ulong)num));
					num *= 10U;
					if (flag)
					{
						throw ValueException.NewDataSourceError<Message1>(Strings.ExcelCellReferenceInvalid(reference), TextValue.New(reference), null);
					}
				}
				else
				{
					if (c < 'A' || c > 'Z')
					{
						throw ValueException.NewDataSourceError<Message1>(Strings.ExcelCellReferenceInvalid(reference), TextValue.New(reference), null);
					}
					flag = true;
					col += (ushort)((c - 'A' + '\u0001') * (char)num2);
					num2 *= 26;
				}
			}
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x00124778 File Offset: 0x00122978
		private static ExcelReaderOpenXml.ExcelRange GetDimension(string dimension)
		{
			string[] array = dimension.Split(new char[] { ':' });
			uint num;
			ushort num2;
			ExcelReaderOpenXml.GetCellLocation(array[0], out num, out num2);
			uint num3;
			ushort num4;
			if (array.Length == 2)
			{
				ExcelReaderOpenXml.GetCellLocation(array[1], out num3, out num4);
			}
			else
			{
				num3 = num;
				num4 = num2;
			}
			return new ExcelReaderOpenXml.ExcelRange(Math.Max(1U, num), Math.Max(1, num2), (num3 > 0U) ? num3 : uint.MaxValue, (num4 > 0) ? num4 : ushort.MaxValue);
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x001247E8 File Offset: 0x001229E8
		private void ReadSharedStringTable(XmlReader reader, Dictionary<string, TextValue> sharedStringsTable)
		{
			reader.ReadStartElement();
			uint num = 0U;
			for (;;)
			{
				if (reader.IsStartElement(this.sharedStringItemName, this.mainNamespaceName))
				{
					this.ReadSharedStringItem(reader, sharedStringsTable, ref num);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x00124830 File Offset: 0x00122A30
		private void ReadSharedStringItem(XmlReader reader, Dictionary<string, TextValue> sharedStringsTable, ref uint index)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			bool flag = false;
			StringBuilder stringBuilder = null;
			for (;;)
			{
				if (!flag && reader.IsStartElement(this.tName, this.mainNamespaceName))
				{
					string text = ExcelReaderOpenXml.ExcelXmlConvert.DecodeName(reader.ReadElementContentAsString());
					uint num = index;
					index = num + 1U;
					sharedStringsTable.Add(ExcelReaderOpenXml.ToString(num), TextValue.New(text));
					flag = true;
				}
				else if (!flag && reader.IsStartElement(this.rName, this.mainNamespaceName))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					this.ReadRun(reader, stringBuilder);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
			if (!flag)
			{
				string text2 = ((stringBuilder != null) ? stringBuilder.ToString() : string.Empty);
				uint num = index;
				index = num + 1U;
				sharedStringsTable.Add(ExcelReaderOpenXml.ToString(num), TextValue.New(text2));
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00124908 File Offset: 0x00122B08
		private void ReadRun(XmlReader reader, StringBuilder text)
		{
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.IsStartElement(this.tName, this.mainNamespaceName))
				{
					text.Append(ExcelReaderOpenXml.ExcelXmlConvert.DecodeName(reader.ReadElementContentAsString()));
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0012495C File Offset: 0x00122B5C
		private void ReadWorkbookHeader()
		{
			if (this.sharedStringsTable == null)
			{
				this.workbookDimensions = new ExcelReaderOpenXml.WorkbookDimensions(this);
				this.sharedStringsTable = new ExcelReaderOpenXml.SharedStringTable(this, this.sharedStringsTablePartUri);
				this.cellFormatsTable = new Dictionary<string, ExcelReaderOpenXml.CellType>();
				this.numberFormatsTable = new Dictionary<string, ExcelReaderOpenXml.CellType>();
				this.ReadWorkbookStyles(this.workbookStylesPartUri);
			}
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x001249B4 File Offset: 0x00122BB4
		private void ReadWorkbookStyles(Uri workbookStylesUri)
		{
			if (workbookStylesUri != null)
			{
				using (XmlReader xmlReader = this.CreateReader(workbookStylesUri))
				{
					xmlReader.ReadStartElement();
					for (;;)
					{
						if (xmlReader.IsStartElement(this.cellXfsName, this.mainNamespaceName))
						{
							this.ReadCellXfs(xmlReader);
						}
						else if (xmlReader.IsStartElement(this.numFmtsName, this.mainNamespaceName))
						{
							this.ReadNumFmts(xmlReader);
						}
						else if (!this.loadFormattingInfo || !this.formattingInfo.TryReadWorkbookStylesSection(xmlReader, this.mainNamespaceName, this.nameName))
						{
							if (!xmlReader.IsStartElement())
							{
								break;
							}
							xmlReader.Skip();
						}
					}
				}
			}
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x00124A64 File Offset: 0x00122C64
		private void ReadCellXfs(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			uint num = 0U;
			for (;;)
			{
				if (reader.IsStartElement(this.xfName, this.mainNamespaceName))
				{
					this.ReadXf(reader, ref num);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x00124AC0 File Offset: 0x00122CC0
		private void ReadXf(XmlReader reader, ref uint index)
		{
			string attribute = reader.GetAttribute(this.numFmtIdName, string.Empty);
			ExcelReaderOpenXml.CellType cellType;
			if (attribute == null || (!ExcelReaderOpenXml.DefaultNumberFormats.TryGetValue(attribute, out cellType) && !this.numberFormatsTable.TryGetValue(attribute, out cellType)))
			{
				cellType = ExcelReaderOpenXml.CellType.General;
			}
			uint num = index;
			index = num + 1U;
			string text = ExcelReaderOpenXml.ToString(num);
			this.cellFormatsTable.Add(text, cellType);
			if (this.loadFormattingInfo)
			{
				this.formattingInfo.ReadXf(reader, text, this.mainNamespaceName, attribute);
				return;
			}
			reader.Skip();
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x00124B44 File Offset: 0x00122D44
		private void ReadNumFmts(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.IsStartElement(this.numFmtName, this.mainNamespaceName))
				{
					this.ReadNumFmt(reader);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00124B9C File Offset: 0x00122D9C
		private void ReadNumFmt(XmlReader reader)
		{
			string attribute = reader.GetAttribute(this.numFmtIdName, string.Empty);
			string attribute2 = reader.GetAttribute(this.formatCodeName, string.Empty);
			if (attribute != null && attribute2 != null)
			{
				ExcelReaderOpenXml.CellType cellType = this.ExtractNumberFormatCodes(attribute2);
				try
				{
					this.numberFormatsTable.Add(attribute, cellType);
				}
				catch (ArgumentException)
				{
					ExcelReaderOpenXml.CellType cellType2;
					if (!this.numberFormatsTable.TryGetValue(attribute, out cellType2) || cellType2 != cellType)
					{
						throw;
					}
				}
				if (this.loadFormattingInfo)
				{
					this.formattingInfo.RegisterNumberFormat(attribute, attribute2);
				}
			}
			reader.Skip();
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00124C30 File Offset: 0x00122E30
		private ExcelReaderOpenXml.CellType ExtractNumberFormatCodes(string formatCode)
		{
			HashSet<char> hashSet = new HashSet<char>();
			char c = '\0';
			for (int i = 0; i < formatCode.Length; i++)
			{
				if (c != '\0' && formatCode[i] == c)
				{
					c = '\0';
				}
				else if (c == '\0')
				{
					if (formatCode[i] == '[')
					{
						c = ']';
					}
					else if (formatCode[i] == '"')
					{
						c = '"';
					}
					else if (formatCode[i] == '\\')
					{
						i++;
					}
					else
					{
						hashSet.Add(formatCode[i]);
					}
				}
			}
			bool flag = hashSet.Contains('m') && (hashSet.Contains('d') || hashSet.Contains('y'));
			bool flag2 = hashSet.Contains('m') && (hashSet.Contains('h') || hashSet.Contains('s'));
			if (flag && flag2)
			{
				return ExcelReaderOpenXml.CellType.DateTime;
			}
			if (flag)
			{
				return ExcelReaderOpenXml.CellType.Date;
			}
			if (flag2)
			{
				return ExcelReaderOpenXml.CellType.Time;
			}
			return ExcelReaderOpenXml.CellType.General;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00124D0C File Offset: 0x00122F0C
		private void ReadPartUris(WorkbookPart workbookPart)
		{
			WorkbookStylesPart workbookStylesPart = workbookPart.WorkbookStylesPart;
			if (workbookStylesPart != null)
			{
				this.workbookStylesPartUri = workbookStylesPart.Uri;
			}
			SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
			if (sharedStringTablePart != null)
			{
				this.sharedStringsTablePartUri = sharedStringTablePart.Uri;
			}
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00124D48 File Offset: 0x00122F48
		private void ReadWorkbookProperties(WorkbookPart workbookPart)
		{
			if (workbookPart.Workbook != null && workbookPart.Workbook.WorkbookProperties != null)
			{
				BooleanValue date = workbookPart.Workbook.WorkbookProperties.Date1904;
				if (date != null && date.HasValue)
				{
					this.uses1904BasedDates = date.Value;
				}
			}
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00124D94 File Offset: 0x00122F94
		private void ReadLastModified(SpreadsheetDocument document)
		{
			DateTime dateTime = new DateTime(1980, 1, 1, 0, 0, 0);
			this.lastModified = document.Package.PackageProperties.Modified;
			using (Stream stream = this.content.Open())
			{
				using (ZipArchive zipArchive = new ZipArchive(stream))
				{
					foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
					{
						DateTime dateTime2 = zipArchiveEntry.LastWriteTime.DateTime;
						if (dateTime2 != dateTime && (this.lastModified == null || dateTime2 > this.lastModified.Value))
						{
							this.lastModified = new DateTime?(dateTime2);
						}
					}
				}
			}
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00124E88 File Offset: 0x00123088
		private TableValue ReadTablesUnguarded()
		{
			if (this.loadFormattingInfo)
			{
				this.formattingInfo = new ExcelReaderOpenXml.FormattingInfo(this.nameTable);
			}
			TableValue tableValue;
			using (Stream stream = this.content.Open(out this.workbookStreamId))
			{
				using (Package package = new ExcelReaderOpenXml.RelationshipHidingPackage(this.engineHost, stream))
				{
					using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(package, new OpenSettings()))
					{
						using (XmlReader xmlReader = this.CreateReader(spreadsheetDocument.WorkbookPart.Uri))
						{
							this.ReadPartUris(spreadsheetDocument.WorkbookPart);
							this.ReadLastModified(spreadsheetDocument);
							this.ReadWorkbookProperties(spreadsheetDocument.WorkbookPart);
							int num = 1;
							List<ExcelReaderOpenXml.WorkbookItem> itemsInWorkbook = new List<ExcelReaderOpenXml.WorkbookItem>();
							new List<ExcelReaderOpenXml.WorkbookItem>();
							HashSet<string> hashSet = new HashSet<string>();
							Dictionary<string, ExcelReaderOpenXml.ExcelWorksheet> dictionary = new Dictionary<string, ExcelReaderOpenXml.ExcelWorksheet>();
							List<ExcelReaderOpenXml.DefinedName> list = new List<ExcelReaderOpenXml.DefinedName>();
							xmlReader.ReadStartElement();
							for (;;)
							{
								if (xmlReader.IsStartElement(this.sheetsName, this.mainNamespaceName))
								{
									this.ReadSheets(spreadsheetDocument.WorkbookPart, xmlReader, itemsInWorkbook, hashSet, dictionary, ref num);
								}
								else if (xmlReader.IsStartElement(this.definedNamesName, this.mainNamespaceName))
								{
									this.ReadDefinedNames(xmlReader, ref num, hashSet, list);
								}
								else
								{
									if (!xmlReader.IsStartElement())
									{
										break;
									}
									xmlReader.Skip();
								}
							}
							if (list.Count > 0)
							{
								foreach (ExcelReaderOpenXml.DefinedName definedName in list)
								{
									string text;
									string text2;
									ExcelReaderOpenXml.ExcelWorksheet excelWorksheet;
									if (ExcelReaderOpenXml.TryParseDefinedName(definedName.Reference, out text, out text2) && dictionary.TryGetValue(text, out excelWorksheet))
									{
										hashSet.Add(definedName.UniqueName);
										string text3 = (definedName.InSheet ? string.Format(CultureInfo.InvariantCulture, "{0}!{1}", text, definedName.Name) : definedName.Name);
										ExcelReaderOpenXml.WorkbookItem workbookItem = new ExcelReaderOpenXml.WorkbookItem(text3, definedName.UniqueName, "DefinedName", new ExcelReaderOpenXml.WorksheetTableValueReference(this, excelWorksheet, text2, this.useFirstRowAsHeader), definedName.Hidden ? ExcelReaderOpenXml.WorkbookItemVisibility.Hidden : ExcelReaderOpenXml.WorkbookItemVisibility.Visible);
										itemsInWorkbook.Add(workbookItem);
									}
								}
							}
							tableValue = new ExcelTableValue(ListValue.New(itemsInWorkbook.Count, (int i) => this.CreateItemRecord(itemsInWorkbook[i])).ToTable(NavigationPropertiesHelper.ExcelSheetTableTypeValue));
						}
					}
				}
			}
			return tableValue;
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x00125164 File Offset: 0x00123364
		private RecordValue CreateItemRecord(ExcelReaderOpenXml.WorkbookItem workbookItem)
		{
			return RecordValue.New(NavigationPropertiesHelper.ExcelSheetNavigationKeys, new IValueReference[]
			{
				TextValue.New(workbookItem.UniqueName),
				workbookItem.Table,
				TextValue.New(workbookItem.Name),
				TextValue.New(workbookItem.Kind),
				LogicalValue.New(workbookItem.Hidden)
			});
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x001251C8 File Offset: 0x001233C8
		private void ReadSheets(WorkbookPart workbookPart, XmlReader reader, List<ExcelReaderOpenXml.WorkbookItem> itemsInWorkbook, HashSet<string> usedNames, Dictionary<string, ExcelReaderOpenXml.ExcelWorksheet> worksheets, ref int nameQualifier)
		{
			List<ExcelReaderOpenXml.ExcelTable> list = new List<ExcelReaderOpenXml.ExcelTable>();
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				for (;;)
				{
					if (reader.IsStartElement(this.sheetName, this.mainNamespaceName))
					{
						this.ReadSheet(workbookPart, reader, itemsInWorkbook, usedNames, worksheets, list);
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			foreach (ExcelReaderOpenXml.ExcelTable excelTable in list)
			{
				string uniqueName = ExcelReaderOpenXml.GetUniqueName(excelTable.Name, usedNames, ref nameQualifier);
				usedNames.Add(uniqueName);
				itemsInWorkbook.Add(new ExcelReaderOpenXml.WorkbookItem(excelTable.Name, uniqueName, "Table", new ExcelReaderOpenXml.WorksheetTableValueReference(this, excelTable.Sheet, excelTable.Reference, true), excelTable.Visibility));
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x001252B4 File Offset: 0x001234B4
		private bool IsPowerViewSheet(WorksheetPart worksheetPart)
		{
			if (worksheetPart == null)
			{
				return false;
			}
			OpenXmlReader openXmlReader = OpenXmlReader.Create(worksheetPart);
			while (openXmlReader.Read())
			{
				if (openXmlReader.ElementType == typeof(Row))
				{
					int num = 0;
					for (;;)
					{
						num++;
						if (num > 5)
						{
							break;
						}
						if (!openXmlReader.ReadNextSibling())
						{
							goto Block_4;
						}
					}
					return false;
					Block_4:
					IL_0091:
					while (openXmlReader.ReadNextSibling())
					{
						if (openXmlReader.ElementType == typeof(CustomProperties) && openXmlReader.ReadFirstChild())
						{
							while (!(((CustomProperty)openXmlReader.LoadCurrentElement()).Name == "Microsoft.ReportingServices.InteractiveReport.Excel.Id"))
							{
								if (!openXmlReader.ReadNextSibling())
								{
									return false;
								}
							}
							return true;
						}
					}
					return false;
				}
			}
			goto IL_0091;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0012535C File Offset: 0x0012355C
		private void ReadSheet(WorkbookPart workbookPart, XmlReader reader, List<ExcelReaderOpenXml.WorkbookItem> itemsInWorkbook, HashSet<string> usedNames, Dictionary<string, ExcelReaderOpenXml.ExcelWorksheet> worksheets, List<ExcelReaderOpenXml.ExcelTable> tablesInSheets)
		{
			string attribute = reader.GetAttribute(this.stateName, string.Empty);
			string attribute2 = reader.GetAttribute(this.nameName, string.Empty);
			string attribute3 = reader.GetAttribute(this.idName, this.relNamespaceName);
			ExcelReaderOpenXml.WorkbookItemVisibility workbookItemVisibility;
			if (!(attribute == "hidden"))
			{
				if (!(attribute == "veryHidden"))
				{
					workbookItemVisibility = ExcelReaderOpenXml.WorkbookItemVisibility.Visible;
				}
				else
				{
					workbookItemVisibility = ExcelReaderOpenXml.WorkbookItemVisibility.VeryHidden;
				}
			}
			else
			{
				workbookItemVisibility = ExcelReaderOpenXml.WorkbookItemVisibility.Hidden;
			}
			WorksheetPart worksheetPart;
			try
			{
				worksheetPart = workbookPart.GetPartById(attribute3) as WorksheetPart;
			}
			catch (ArgumentOutOfRangeException)
			{
				worksheetPart = null;
				reader.Skip();
				return;
			}
			if (this.IsPowerViewSheet(worksheetPart))
			{
				reader.Skip();
				return;
			}
			usedNames.Add(attribute2);
			if (worksheetPart != null)
			{
				ExcelReaderOpenXml.ExcelWorksheet excelWorksheet = this.CreateWorksheet(worksheetPart);
				worksheets.Add(attribute2, excelWorksheet);
				if (this.loadFormattingInfo)
				{
					this.formattingInfo.RegisterWorksheetName(excelWorksheet, attribute2);
				}
				itemsInWorkbook.Add(new ExcelReaderOpenXml.WorkbookItem(attribute2, attribute2, "Sheet", new ExcelReaderOpenXml.WorksheetTableValueReference(this, excelWorksheet, null, this.useFirstRowAsHeader), workbookItemVisibility));
				using (IEnumerator<TableDefinitionPart> enumerator = worksheetPart.TableDefinitionParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TableDefinitionPart tableDefinitionPart = enumerator.Current;
						StringValue stringValue;
						if ((stringValue = tableDefinitionPart.Table.Name) == null)
						{
							stringValue = tableDefinitionPart.Table.DisplayName ?? string.Empty;
						}
						tablesInSheets.Add(new ExcelReaderOpenXml.ExcelTable(stringValue, tableDefinitionPart.Table.Reference, excelWorksheet, workbookItemVisibility));
					}
					goto IL_0181;
				}
			}
			itemsInWorkbook.Add(new ExcelReaderOpenXml.WorkbookItem(attribute2, attribute2, "Sheet", TableValue.Empty, workbookItemVisibility));
			IL_0181:
			reader.Skip();
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0012550C File Offset: 0x0012370C
		private void ReadDefinedNames(XmlReader reader, ref int nameQualifier, HashSet<string> usedNames, List<ExcelReaderOpenXml.DefinedName> definedNames)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.IsStartElement(this.definedNameName, this.mainNamespaceName))
				{
					this.ReadDefinedName(reader, ref nameQualifier, usedNames, definedNames);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x00125568 File Offset: 0x00123768
		private void ReadDefinedName(XmlReader reader, ref int nameQualifier, HashSet<string> usedNames, List<ExcelReaderOpenXml.DefinedName> definedNames)
		{
			string attribute = reader.GetAttribute(this.nameName, string.Empty);
			string attribute2 = reader.GetAttribute(this.localSheetId, string.Empty);
			string attribute3 = reader.GetAttribute(this.hidden, string.Empty);
			string text = reader.ReadElementContentAsString();
			definedNames.Add(new ExcelReaderOpenXml.DefinedName(attribute, ExcelReaderOpenXml.GetUniqueName(attribute, usedNames, ref nameQualifier), text, attribute2 != null, attribute3 == "1"));
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x001255D8 File Offset: 0x001237D8
		private static bool TryParseDefinedName(string reference, out string sheet, out string dimension)
		{
			sheet = null;
			dimension = null;
			if (reference.Length == 0)
			{
				return false;
			}
			if (reference[0] == '\'')
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < reference.Length; i++)
				{
					char c = reference[i];
					if (c == '\'')
					{
						if (!flag)
						{
							flag = true;
						}
						else
						{
							if (i >= reference.Length - 1)
							{
								return false;
							}
							char c2 = reference[i + 1];
							if (c2 == '\'')
							{
								stringBuilder.Append('\'');
							}
							else
							{
								if (c2 != '!')
								{
									return false;
								}
								sheet = stringBuilder.ToString();
								i += 2;
								if (i >= reference.Length)
								{
									return false;
								}
								dimension = reference.Substring(i);
								return ExcelReaderOpenXml.ValidateAndFormatDefinedNameDimension(dimension, out dimension);
							}
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				return false;
			}
			int num = reference.IndexOf('!');
			if (num <= 0 || num >= reference.Length - 1)
			{
				return false;
			}
			sheet = reference.Substring(0, num);
			dimension = reference.Substring(num + 1);
			return ExcelReaderOpenXml.ValidateAndFormatDefinedNameDimension(dimension, out dimension);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x001256D0 File Offset: 0x001238D0
		private static string GetUniqueName(string name, HashSet<string> usedNames, ref int nameQualifier)
		{
			string text = name;
			while (!usedNames.Add(text))
			{
				int num = nameQualifier;
				nameQualifier = num + 1;
				text = name + num.ToString();
			}
			return text;
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00125701 File Offset: 0x00123901
		private static bool ValidateAndFormatDefinedNameDimension(string dimension, out string newDimension)
		{
			if (dimension.IndexOfAny(ExcelReaderOpenXml.CellRefDelims) >= 0)
			{
				newDimension = null;
				return false;
			}
			newDimension = dimension.Replace("$", string.Empty);
			return true;
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x00125729 File Offset: 0x00123929
		private ExcelReaderOpenXml.ExcelWorksheet CreateWorksheet(WorksheetPart sheetPart)
		{
			return new ExcelReaderOpenXml.ExcelWorksheet(this, sheetPart.Uri, this.inferSheetDimensions);
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00125740 File Offset: 0x00123940
		private string ReadDimensionReference(ExcelReaderOpenXml.ExcelWorksheet worksheet)
		{
			try
			{
				using (XmlReader xmlReader = this.CreateReader(worksheet.PartUri))
				{
					xmlReader.ReadStartElement();
					while (!xmlReader.IsStartElement(this.dimensionName, this.mainNamespaceName))
					{
						if (xmlReader.IsStartElement(this.sheetDataName, this.mainNamespaceName))
						{
							goto IL_0063;
						}
						xmlReader.Skip();
					}
					return xmlReader.GetAttribute(this.refName, string.Empty);
				}
				IL_0063:;
			}
			catch (Exception ex)
			{
				this.ExceptionHandler(ex);
				throw;
			}
			return null;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x001257DC File Offset: 0x001239DC
		private IEnumerable<ExcelReaderOpenXml.ExcelRow> ReadWorksheetRows(ExcelReaderOpenXml.ExcelWorksheet worksheet)
		{
			IEnumerable<ExcelReaderOpenXml.ExcelRow> enumerable;
			try
			{
				this.ReadWorkbookHeader();
				enumerable = new ExcelReaderOpenXml.HandleExceptionsEnumerable<ExcelReaderOpenXml.ExcelRow>(new Action<Exception>(this.ExceptionHandler), this.ReadWorksheetRowsWithMergedCellInfoUnguarded(worksheet));
			}
			catch (Exception ex)
			{
				this.ExceptionHandler(ex);
				throw;
			}
			return enumerable;
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00125828 File Offset: 0x00123A28
		private IEnumerable<ExcelReaderOpenXml.ExcelRow> ReadWorksheetRowsWithMergedCellInfoUnguarded(ExcelReaderOpenXml.ExcelWorksheet worksheet)
		{
			IEnumerable<ExcelReaderOpenXml.ExcelRow> enumerable = this.ReadWorksheetRowsUnguarded(worksheet);
			if (this.loadFormattingInfo)
			{
				return this.formattingInfo.ReadWorksheetRowsWithMergedCellInfoUnguarded(worksheet, enumerable, new Func<Uri, XmlReader>(this.CreateReader), this.hidden, this.refName, this.stateName, this.mainNamespaceName);
			}
			return enumerable;
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x00125878 File Offset: 0x00123A78
		private IEnumerable<ExcelReaderOpenXml.ExcelRow> ReadWorksheetRowsUnguarded(ExcelReaderOpenXml.ExcelWorksheet worksheet)
		{
			using (XmlReader reader = this.CreateReader(worksheet.PartUri))
			{
				reader.ReadStartElement();
				for (;;)
				{
					if (reader.IsStartElement(this.sheetDataName, this.mainNamespaceName))
					{
						foreach (ExcelReaderOpenXml.ExcelRow excelRow in this.ReadSheetData(reader))
						{
							yield return excelRow;
						}
						IEnumerator<ExcelReaderOpenXml.ExcelRow> enumerator = null;
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
			}
			XmlReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0012588F File Offset: 0x00123A8F
		private IEnumerable<ExcelReaderOpenXml.ExcelRow> ReadSheetData(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				uint lastRow = 0U;
				for (;;)
				{
					if (reader.IsStartElement(this.rowName, this.mainNamespaceName))
					{
						yield return this.ReadRow(reader, ref lastRow);
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			yield break;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x001258A8 File Offset: 0x00123AA8
		private ExcelReaderOpenXml.ExcelRow ReadRow(XmlReader reader, ref uint lastRow)
		{
			string attribute = reader.GetAttribute(this.rName, string.Empty);
			uint num = ((attribute == null) ? (lastRow + 1U) : uint.Parse(attribute, CultureInfo.InvariantCulture));
			lastRow = num;
			bool flag = this.loadFormattingInfo && reader.GetAttribute(this.hidden, string.Empty) == "1";
			List<Microsoft.Mashup.Engine1.Runtime.Value> list = new List<Microsoft.Mashup.Engine1.Runtime.Value>();
			List<ValueException> list2 = new List<ValueException>();
			List<ushort> list3 = new List<ushort>();
			bool flag2 = true;
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				ushort num2 = 0;
				int num3 = 0;
				for (;;)
				{
					if (reader.IsStartElement(this.cellName, this.mainNamespaceName))
					{
						Microsoft.Mashup.Engine1.Runtime.Value value;
						ValueException ex;
						if (this.TryReadCellValue(reader, num, ref num2, out value, out ex))
						{
							list2.Add(null);
							list.Add(value);
						}
						else
						{
							list2.Add(ex);
							list.Add(null);
						}
						list3.Add(num2);
						if (flag2 && (int)num2 != num3)
						{
							if (list3.Count == 1)
							{
								num3 = (int)num2;
							}
							else
							{
								flag2 = false;
							}
						}
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
					num3++;
				}
				reader.ReadEndElement();
			}
			ExcelReaderOpenXml.ExcelRow excelRow = new ExcelReaderOpenXml.ExcelRow(list, list2, list3, num, flag2);
			if (this.loadFormattingInfo && flag)
			{
				this.formattingInfo.RegisterHiddenRow(excelRow);
			}
			return excelRow;
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x001259F4 File Offset: 0x00123BF4
		private bool TryReadCellValue(XmlReader reader, uint rowIndex, ref ushort col, out Microsoft.Mashup.Engine1.Runtime.Value value, out ValueException error)
		{
			string attribute = reader.GetAttribute(this.rName, string.Empty);
			if (attribute != null)
			{
				uint num;
				ExcelReaderOpenXml.GetCellLocation(attribute, out num, out col);
			}
			else
			{
				uint num = rowIndex;
				col += 1;
			}
			string attribute2 = reader.GetAttribute(this.tName, string.Empty);
			ExcelReaderOpenXml.CellType cellType = ExcelReaderOpenXml.CellType.General;
			string attribute3 = reader.GetAttribute(this.sName, string.Empty);
			ExcelReaderOpenXml.CellType cellType2;
			if (attribute3 != null && this.cellFormatsTable.TryGetValue(attribute3, out cellType2))
			{
				cellType = cellType2;
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				for (;;)
				{
					if (reader.IsStartElement(this.valueName, this.mainNamespaceName))
					{
						text = ExcelReaderOpenXml.ExcelXmlConvert.DecodeName(reader.ReadElementContentAsString());
					}
					else if (reader.IsStartElement(this.inlineStringName, this.mainNamespaceName))
					{
						text = this.ReadInlineString(reader);
					}
					else if (this.loadFormattingInfo && reader.IsStartElement(this.formattingInfo.formulaName, this.mainNamespaceName))
					{
						text3 = reader.GetAttribute(this.formattingInfo.sharedGroupIndexName);
						text2 = reader.ReadElementContentAsString();
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			bool flag;
			cellType = this.ParseDataType(attribute2, cellType, text, out flag);
			bool flag2 = this.TryGetCellValue(text, cellType, flag, out value, out error);
			if (this.loadFormattingInfo)
			{
				value = this.formattingInfo.AddCellMeta(value ?? Microsoft.Mashup.Engine1.Runtime.Value.Null, attribute3, text2, text3);
				if (error != null)
				{
					value = value.NewMeta(value.MetaValue.Concatenate(RecordValue.New(ExcelReaderOpenXml.ErrorMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { error.Value })).AsRecord);
				}
				return true;
			}
			return flag2;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00125BAC File Offset: 0x00123DAC
		private string ReadInlineString(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return null;
			}
			reader.ReadStartElement();
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				if (reader.IsStartElement(this.tName, this.mainNamespaceName))
				{
					stringBuilder.Append(ExcelReaderOpenXml.ExcelXmlConvert.DecodeName(reader.ReadElementContentAsString()));
				}
				else if (reader.IsStartElement(this.rName, this.mainNamespaceName))
				{
					this.ReadRun(reader, stringBuilder);
				}
				else
				{
					if (!reader.IsStartElement())
					{
						break;
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
			return stringBuilder.ToString();
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00125C3C File Offset: 0x00123E3C
		private ExcelReaderOpenXml.CellType ParseDataType(string dataType, ExcelReaderOpenXml.CellType format, string cellText, out bool isSharedString)
		{
			isSharedString = false;
			if (dataType == null)
			{
				return format;
			}
			if (dataType != null)
			{
				int length = dataType.Length;
				if (length != 1)
				{
					if (length != 3)
					{
						if (length == 9)
						{
							if (dataType == "inlineStr")
							{
								return ExcelReaderOpenXml.CellType.Text;
							}
						}
					}
					else if (dataType == "str")
					{
						return ExcelReaderOpenXml.CellType.Text;
					}
				}
				else
				{
					char c = dataType[0];
					switch (c)
					{
					case 'b':
						return ExcelReaderOpenXml.CellType.Boolean;
					case 'c':
						break;
					case 'd':
						if (format != ExcelReaderOpenXml.CellType.Date && format != ExcelReaderOpenXml.CellType.Time)
						{
							return ExcelReaderOpenXml.CellType.DateTime;
						}
						return format;
					case 'e':
						return ExcelReaderOpenXml.CellType.Error;
					default:
						if (c == 'n')
						{
							return ExcelReaderOpenXml.CellType.Number;
						}
						if (c == 's')
						{
							isSharedString = true;
							return ExcelReaderOpenXml.CellType.Text;
						}
						break;
					}
				}
			}
			throw new InvalidOperationException("Unhanded CellValues type: " + dataType);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00125CE4 File Offset: 0x00123EE4
		private bool TryGetCellValue(string cellText, ExcelReaderOpenXml.CellType cellType, bool isSharedString, out Microsoft.Mashup.Engine1.Runtime.Value value, out ValueException error)
		{
			value = null;
			error = null;
			if (cellText == null)
			{
				value = Microsoft.Mashup.Engine1.Runtime.Value.Null;
				return true;
			}
			if (cellText.Length == 0)
			{
				value = TextValue.Empty;
				return true;
			}
			TextValue textValue = null;
			if (isSharedString)
			{
				textValue = this.sharedStringsTable[cellText];
				cellText = textValue.AsString;
			}
			switch (cellType)
			{
			case ExcelReaderOpenXml.CellType.General:
				value = this.GetGeneralValue(cellText);
				return true;
			case ExcelReaderOpenXml.CellType.Boolean:
				value = this.GetLogicalValue(cellText);
				return true;
			case ExcelReaderOpenXml.CellType.Number:
				value = this.GetNumberValue(cellText);
				return true;
			case ExcelReaderOpenXml.CellType.Date:
			case ExcelReaderOpenXml.CellType.Time:
			case ExcelReaderOpenXml.CellType.DateTime:
				value = this.GetDateTimeValue(cellText, cellType);
				return true;
			case ExcelReaderOpenXml.CellType.Text:
				value = textValue ?? TextValue.New(cellText);
				return true;
			case ExcelReaderOpenXml.CellType.Error:
				error = ExcelModule.GetExcelError(textValue ?? TextValue.New(cellText));
				return false;
			default:
				throw new InvalidOperationException("Unhanded CellType type: " + cellType.ToString());
			}
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00125DCC File Offset: 0x00123FCC
		private Microsoft.Mashup.Engine1.Runtime.Value GetLogicalValue(string cellText)
		{
			LogicalValue logicalValue;
			if (ExcelReaderOpenXml.BooleanCache.TryGetValue(cellText, out logicalValue))
			{
				return logicalValue;
			}
			return TextValue.New(cellText);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00125DF0 File Offset: 0x00123FF0
		private Microsoft.Mashup.Engine1.Runtime.Value GetNumberValue(string cellText)
		{
			NumberValue numberValue;
			if (!NumberValue.TryParse(cellText, NumberStyles.Float, CultureInfo.InvariantCulture, out numberValue))
			{
				return TextValue.New(cellText);
			}
			return numberValue;
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00125E1C File Offset: 0x0012401C
		private Microsoft.Mashup.Engine1.Runtime.Value GetDateTimeValue(string cellText, ExcelReaderOpenXml.CellType cellType)
		{
			double num;
			if (!double.TryParse(cellText, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				return TextValue.New(cellText);
			}
			Microsoft.Mashup.Engine1.Runtime.Value value;
			try
			{
				if (this.uses1904BasedDates)
				{
					num += 1462.0;
				}
				else if (num >= 0.0 && num < 61.0)
				{
					num += 1.0;
				}
				value = ExcelReaderOpenXml.GetDateTime(DateTime.FromOADate(num), cellType);
			}
			catch (ArgumentException)
			{
				value = NumberValue.New(num);
			}
			return value;
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00125EAC File Offset: 0x001240AC
		private static Microsoft.Mashup.Engine1.Runtime.Value GetDateTime(DateTime dateTime, ExcelReaderOpenXml.CellType cellType)
		{
			switch (cellType)
			{
			case ExcelReaderOpenXml.CellType.Date:
				if (dateTime.TimeOfDay != TimeSpan.Zero)
				{
					return Microsoft.Mashup.Engine1.Runtime.DateTimeValue.New(dateTime);
				}
				return DateValue.New(dateTime);
			case ExcelReaderOpenXml.CellType.Time:
				if (dateTime.DayOfYear != 0)
				{
					return Microsoft.Mashup.Engine1.Runtime.DateTimeValue.New(dateTime);
				}
				return TimeValue.New(dateTime.TimeOfDay);
			case ExcelReaderOpenXml.CellType.DateTime:
				return Microsoft.Mashup.Engine1.Runtime.DateTimeValue.New(dateTime);
			default:
				throw new InvalidOperationException("Invalid CellType passed in for DateTime: " + cellType.ToString());
			}
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x00125F30 File Offset: 0x00124130
		private Microsoft.Mashup.Engine1.Runtime.Value GetGeneralValue(string cellText)
		{
			NumberValue numberValue;
			if (NumberValue.TryParse(cellText, NumberStyles.Float, CultureInfo.InvariantCulture, out numberValue))
			{
				return numberValue;
			}
			LogicalValue logicalValue;
			if (ExcelReaderOpenXml.BooleanCache.TryGetValue(cellText, out logicalValue))
			{
				return logicalValue;
			}
			return TextValue.New(cellText);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x00125F6C File Offset: 0x0012416C
		private TableValue CreateTableValue(ExcelReaderOpenXml.ExcelWorksheet sheet, string dimension, bool useFirstRowAsHeader)
		{
			ExcelReaderOpenXml.ExcelRange dimensions = sheet.GetDimensions();
			ExcelReaderOpenXml.ExcelRange range = dimensions;
			if (dimension != null)
			{
				range = ExcelReaderOpenXml.GetDimension(dimension).AdjustForSheet(dimensions);
			}
			IEnumerable<ExcelReaderOpenXml.ExcelRow> enumerable = sheet.GetRows(range);
			bool flag = false;
			string[] array = null;
			if (useFirstRowAsHeader)
			{
				ExcelReaderOpenXml.ExcelRow excelRow = enumerable.FirstOrDefault<ExcelReaderOpenXml.ExcelRow>();
				if (excelRow != null)
				{
					array = ExcelReaderOpenXml.CreateHeader(excelRow, range.StartColumn, range.ColumnCount);
					enumerable = enumerable.Skip(1);
				}
			}
			if (array == null)
			{
				array = new string[range.ColumnCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = "Column" + (i + 1).ToString();
				}
				flag = true;
			}
			Keys header = Keys.New(array);
			ValueKind[] columnKinds = new ValueKind[header.Length];
			IEnumerable<IValueReference> enumerable2 = enumerable.Select((ExcelReaderOpenXml.ExcelRow r) => this.CreateRow(r, range.StartColumn, header, columnKinds));
			if (this.inferTypes)
			{
				ExcelReaderOpenXml.ColumnTypes columnTypes = new ExcelReaderOpenXml.ColumnTypes(this, sheet.PartUri.ToString(), dimension);
				ValueKind[] array2;
				if (columnTypes.TryGetTypes(out array2))
				{
					columnKinds = array2;
				}
				else
				{
					enumerable2 = enumerable2.ToArray<IValueReference>();
					columnTypes.SetTypes(columnKinds);
				}
			}
			Keys header2 = header;
			Microsoft.Mashup.Engine1.Runtime.Value[] array3 = columnKinds.Select(new Func<ValueKind, RecordValue>(ExcelReaderOpenXml.GetTypeField)).ToArray<RecordValue>();
			TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(header2, array3)));
			TableValue tableValue = ListValue.New(enumerable2).ToTable(tableTypeValue);
			if (!this.inferTypes)
			{
				tableValue = ValueServices.AddShouldInferTableTypeMeta(tableValue);
			}
			if (flag)
			{
				tableValue = ValueServices.AddFirstRowMayContainHeadersMeta(tableValue);
			}
			if (this.loadFormattingInfo)
			{
				tableValue = this.formattingInfo.AddTableMeta(tableValue, sheet, range, array);
			}
			return tableValue;
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00126140 File Offset: 0x00124340
		private static RecordValue GetTypeField(ValueKind kind)
		{
			return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Microsoft.Mashup.Engine1.Runtime.Value[]
			{
				TypeServices.GetTypeValueForKind(kind).Nullable,
				LogicalValue.False
			});
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00126168 File Offset: 0x00124368
		private static ValueKind AdjustColumnType(ValueKind columnKind, ValueKind cellKind)
		{
			if (columnKind == ValueKind.Any)
			{
				return ValueKind.Any;
			}
			if (columnKind == ValueKind.Null)
			{
				return cellKind;
			}
			if (cellKind != ValueKind.Null && columnKind != cellKind)
			{
				return ValueKind.Any;
			}
			return columnKind;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00126180 File Offset: 0x00124380
		private static string[] CreateHeader(ExcelReaderOpenXml.ExcelRow headerRow, ushort startCol, int colCount)
		{
			int num = 1;
			HashSet<string> hashSet = new HashSet<string>();
			string[] array = new string[colCount];
			int num2 = 0;
			int num3 = (int)startCol;
			for (int i = 0; i < headerRow.cellValues.Count; i++)
			{
				ushort num4 = headerRow.cellColumns[i];
				if (num4 >= startCol)
				{
					if ((int)num4 >= (int)startCol + colCount)
					{
						return array;
					}
					for (int j = num3; j < (int)num4; j++)
					{
						array[num2++] = "Column" + (j - (int)startCol + 1).ToString();
					}
					Microsoft.Mashup.Engine1.Runtime.Value value = headerRow.cellValues[i];
					string text;
					if (value == null || value.IsNull)
					{
						text = "Column" + num4.ToString();
					}
					else if (value.IsText)
					{
						text = value.AsString;
					}
					else
					{
						text = value.ToString();
					}
					array[num2++] = ExcelReaderOpenXml.GetUniqueName(text, hashSet, ref num);
					num3 = (int)(num4 + 1);
				}
			}
			for (int k = num3; k < (int)startCol + colCount; k++)
			{
				array[num2++] = "Column" + (k - (int)startCol + 1).ToString();
			}
			return array;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x001262B0 File Offset: 0x001244B0
		private RecordValue CreateRow(ExcelReaderOpenXml.ExcelRow row, ushort startCol, Keys header, ValueKind[] columnKinds)
		{
			ushort num = 0;
			Microsoft.Mashup.Engine1.Runtime.Value[] cellValues = new Microsoft.Mashup.Engine1.Runtime.Value[header.Length];
			ValueException[] cellErrors = null;
			bool flag = row.contiguous && cellValues.Length == row.cellValues.Count && (row.cellColumns.Count == 0 || row.cellColumns[0] == startCol);
			ushort num2 = 0;
			while ((int)num2 < cellValues.Length)
			{
				if (!flag)
				{
					if ((int)num < row.cellValues.Count)
					{
						ushort num3 = row.cellColumns[(int)num];
						int num4;
						while ((num4 = ExcelReaderOpenXml.ComparePositionTo((int)num3, (int)num2, (int)startCol)) < 0)
						{
							num += 1;
							if ((int)num == row.cellColumns.Count)
							{
								break;
							}
							num3 = row.cellColumns[(int)num];
						}
						if (num4 == 0)
						{
							Microsoft.Mashup.Engine1.Runtime.Value cellValueOrStoreError = ExcelReaderOpenXml.GetCellValueOrStoreError(ref cellErrors, row, (int)num2, (int)num, header);
							cellValues[(int)num2] = cellValueOrStoreError;
							columnKinds[(int)num2] = ExcelReaderOpenXml.AdjustColumnType(columnKinds[(int)num2], (cellValueOrStoreError == null) ? ValueKind.Null : cellValueOrStoreError.Kind);
							num += 1;
							goto IL_0154;
						}
					}
					cellValues[(int)num2] = Microsoft.Mashup.Engine1.Runtime.Value.Null;
				}
				else
				{
					Microsoft.Mashup.Engine1.Runtime.Value cellValueOrStoreError2 = ExcelReaderOpenXml.GetCellValueOrStoreError(ref cellErrors, row, (int)num2, (int)num, header);
					cellValues[(int)num2] = cellValueOrStoreError2;
					columnKinds[(int)num2] = ExcelReaderOpenXml.AdjustColumnType(columnKinds[(int)num2], (cellValueOrStoreError2 == null) ? ValueKind.Null : cellValueOrStoreError2.Kind);
					num += 1;
				}
				IL_0154:
				num2 += 1;
			}
			RecordValue recordValue;
			if (cellErrors == null)
			{
				recordValue = RecordValue.New(header, cellValues);
			}
			else
			{
				recordValue = RecordValue.New(header, delegate(int index)
				{
					if (cellErrors[index] != null)
					{
						throw cellErrors[index];
					}
					return cellValues[index];
				});
			}
			if (this.loadFormattingInfo)
			{
				recordValue = this.formattingInfo.AddRowMeta(recordValue, row);
			}
			return recordValue;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00126468 File Offset: 0x00124668
		private static Microsoft.Mashup.Engine1.Runtime.Value GetCellValueOrStoreError(ref ValueException[] cellErrors, ExcelReaderOpenXml.ExcelRow row, int i, int curCellIndex, Keys header)
		{
			if (row.cellValues[curCellIndex] != null)
			{
				return row.cellValues[curCellIndex];
			}
			if (cellErrors == null)
			{
				cellErrors = new ValueException[header.Length];
			}
			cellErrors[i] = row.cellErrors[curCellIndex];
			return null;
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x001264A8 File Offset: 0x001246A8
		private static int ComparePositionTo(int cellPos, int currentCol, int startCol)
		{
			return cellPos - startCol - currentCol;
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001264B0 File Offset: 0x001246B0
		private void ExceptionHandler(Exception e)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, "ExcelReaderOpenXml/ExceptionHandler", TraceEventType.Information, null))
			{
				hostTrace.Add(e, true);
				XmlException ex = e as XmlException;
				if (ex != null)
				{
					throw ValueException.NewDataFormatError(ex.Message, this.content.Content, ex);
				}
				if (SafeExceptions.IsSafeException(e))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.ExcelInvalidInput, this.content.Content, e);
				}
			}
		}

		// Token: 0x04002F20 RID: 12064
		private const string DefaultColumnName = "Column";

		// Token: 0x04002F21 RID: 12065
		private const string HiddenValue = "hidden";

		// Token: 0x04002F22 RID: 12066
		private const string VeryHiddenValue = "veryHidden";

		// Token: 0x04002F23 RID: 12067
		private const int powerViewSheetMaxRowsToCheck = 5;

		// Token: 0x04002F24 RID: 12068
		private const double december31st1899 = 0.0;

		// Token: 0x04002F25 RID: 12069
		private const double march1st1900 = 61.0;

		// Token: 0x04002F26 RID: 12070
		private const int offsetFor1904BasedDates = 1462;

		// Token: 0x04002F27 RID: 12071
		private static readonly Dictionary<string, ExcelReaderOpenXml.CellType> DefaultNumberFormats = new Dictionary<string, ExcelReaderOpenXml.CellType>(30)
		{
			{
				"0",
				ExcelReaderOpenXml.CellType.General
			},
			{
				"1",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"2",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"3",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"4",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"9",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"10",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"11",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"12",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"13",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"14",
				ExcelReaderOpenXml.CellType.Date
			},
			{
				"15",
				ExcelReaderOpenXml.CellType.Date
			},
			{
				"16",
				ExcelReaderOpenXml.CellType.Date
			},
			{
				"17",
				ExcelReaderOpenXml.CellType.Date
			},
			{
				"18",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"19",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"20",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"21",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"22",
				ExcelReaderOpenXml.CellType.DateTime
			},
			{
				"37",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"38",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"39",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"40",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"45",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"46",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"47",
				ExcelReaderOpenXml.CellType.Time
			},
			{
				"48",
				ExcelReaderOpenXml.CellType.Number
			},
			{
				"49",
				ExcelReaderOpenXml.CellType.Text
			}
		};

		// Token: 0x04002F28 RID: 12072
		private readonly string cellName;

		// Token: 0x04002F29 RID: 12073
		private readonly string cellXfsName;

		// Token: 0x04002F2A RID: 12074
		private readonly string definedNameName;

		// Token: 0x04002F2B RID: 12075
		private readonly string definedNamesName;

		// Token: 0x04002F2C RID: 12076
		private readonly string dimensionName;

		// Token: 0x04002F2D RID: 12077
		private readonly string formatCodeName;

		// Token: 0x04002F2E RID: 12078
		private readonly string idName;

		// Token: 0x04002F2F RID: 12079
		private readonly string inlineStringName;

		// Token: 0x04002F30 RID: 12080
		private readonly string mainNamespaceName;

		// Token: 0x04002F31 RID: 12081
		private readonly string nameName;

		// Token: 0x04002F32 RID: 12082
		private readonly string numFmtIdName;

		// Token: 0x04002F33 RID: 12083
		private readonly string numFmtName;

		// Token: 0x04002F34 RID: 12084
		private readonly string numFmtsName;

		// Token: 0x04002F35 RID: 12085
		private readonly string rName;

		// Token: 0x04002F36 RID: 12086
		private readonly string refName;

		// Token: 0x04002F37 RID: 12087
		private readonly string relNamespaceName;

		// Token: 0x04002F38 RID: 12088
		private readonly string rowName;

		// Token: 0x04002F39 RID: 12089
		private readonly string sName;

		// Token: 0x04002F3A RID: 12090
		private readonly string sharedStringItemName;

		// Token: 0x04002F3B RID: 12091
		private readonly string sheetDataName;

		// Token: 0x04002F3C RID: 12092
		private readonly string sheetName;

		// Token: 0x04002F3D RID: 12093
		private readonly string sheetsName;

		// Token: 0x04002F3E RID: 12094
		private readonly string stateName;

		// Token: 0x04002F3F RID: 12095
		private readonly string tName;

		// Token: 0x04002F40 RID: 12096
		private readonly string valueName;

		// Token: 0x04002F41 RID: 12097
		private readonly string xfName;

		// Token: 0x04002F42 RID: 12098
		private readonly string localSheetId;

		// Token: 0x04002F43 RID: 12099
		private readonly string hidden;

		// Token: 0x04002F44 RID: 12100
		private static readonly char[] CellRefDelims = new char[] { ',', '\'', '!' };

		// Token: 0x04002F45 RID: 12101
		private static readonly List<Microsoft.Mashup.Engine1.Runtime.Value> EmptyCellValues = new List<Microsoft.Mashup.Engine1.Runtime.Value>(0);

		// Token: 0x04002F46 RID: 12102
		private static readonly List<ushort> EmptyCellCols = new List<ushort>(0);

		// Token: 0x04002F47 RID: 12103
		private static readonly Dictionary<string, LogicalValue> BooleanCache = new Dictionary<string, LogicalValue>(4, StringComparer.OrdinalIgnoreCase)
		{
			{
				"1",
				LogicalValue.True
			},
			{
				"0",
				LogicalValue.False
			},
			{
				"true",
				LogicalValue.True
			},
			{
				"false",
				LogicalValue.False
			}
		};

		// Token: 0x04002F48 RID: 12104
		private const string RowSpanMetaString = "RowSpan";

		// Token: 0x04002F49 RID: 12105
		private const string ColumnSpanMetaString = "ColumnSpan";

		// Token: 0x04002F4A RID: 12106
		private static readonly Keys SpanMetaKey = Keys.New("RowSpan", "ColumnSpan");

		// Token: 0x04002F4B RID: 12107
		private const string ErrorMetaString = "Error";

		// Token: 0x04002F4C RID: 12108
		private static readonly Keys ErrorMetaKey = Keys.New("Error");

		// Token: 0x04002F4D RID: 12109
		private readonly IEngineHost engineHost;

		// Token: 0x04002F4E RID: 12110
		private readonly ExcelReaderOpenXml.TimestampedCache cache;

		// Token: 0x04002F4F RID: 12111
		private readonly ExcelFile content;

		// Token: 0x04002F50 RID: 12112
		private readonly bool useFirstRowAsHeader;

		// Token: 0x04002F51 RID: 12113
		private readonly bool inferTypes;

		// Token: 0x04002F52 RID: 12114
		private readonly bool inferSheetDimensions;

		// Token: 0x04002F53 RID: 12115
		private readonly bool loadFormattingInfo;

		// Token: 0x04002F54 RID: 12116
		private readonly XmlNameTable nameTable;

		// Token: 0x04002F55 RID: 12117
		private readonly string options;

		// Token: 0x04002F56 RID: 12118
		private string workbookStreamId;

		// Token: 0x04002F57 RID: 12119
		private DateTime? lastModified;

		// Token: 0x04002F58 RID: 12120
		private Uri sharedStringsTablePartUri;

		// Token: 0x04002F59 RID: 12121
		private Uri workbookStylesPartUri;

		// Token: 0x04002F5A RID: 12122
		private ExcelReaderOpenXml.WorkbookDimensions workbookDimensions;

		// Token: 0x04002F5B RID: 12123
		private ExcelReaderOpenXml.SharedStringTable sharedStringsTable;

		// Token: 0x04002F5C RID: 12124
		private Dictionary<string, ExcelReaderOpenXml.CellType> cellFormatsTable;

		// Token: 0x04002F5D RID: 12125
		private Dictionary<string, ExcelReaderOpenXml.CellType> numberFormatsTable;

		// Token: 0x04002F5E RID: 12126
		private ExcelReaderOpenXml.FormattingInfo formattingInfo;

		// Token: 0x04002F5F RID: 12127
		private bool uses1904BasedDates;

		// Token: 0x02000C35 RID: 3125
		private enum CellType
		{
			// Token: 0x04002F61 RID: 12129
			General,
			// Token: 0x04002F62 RID: 12130
			Boolean,
			// Token: 0x04002F63 RID: 12131
			Number,
			// Token: 0x04002F64 RID: 12132
			Date,
			// Token: 0x04002F65 RID: 12133
			Time,
			// Token: 0x04002F66 RID: 12134
			DateTime,
			// Token: 0x04002F67 RID: 12135
			Text,
			// Token: 0x04002F68 RID: 12136
			Error
		}

		// Token: 0x02000C36 RID: 3126
		private sealed class ExcelWorksheet
		{
			// Token: 0x06005516 RID: 21782 RVA: 0x00126740 File Offset: 0x00124940
			public ExcelWorksheet(ExcelReaderOpenXml reader, Uri partUri, bool inferDimensions)
			{
				this.reader = reader;
				this.partUri = partUri;
				this.inferDimensions = inferDimensions;
				this.dimension = ExcelReaderOpenXml.ExcelRange.Empty;
			}

			// Token: 0x170019D3 RID: 6611
			// (get) Token: 0x06005517 RID: 21783 RVA: 0x00126768 File Offset: 0x00124968
			public Uri PartUri
			{
				get
				{
					return this.partUri;
				}
			}

			// Token: 0x170019D4 RID: 6612
			// (get) Token: 0x06005518 RID: 21784 RVA: 0x00126770 File Offset: 0x00124970
			public ExcelReaderOpenXml.ExcelRow FirstRow
			{
				get
				{
					if (!this.checkedFirstRow)
					{
						this.firstRow = this.GetAllRows().FirstOrDefault<ExcelReaderOpenXml.ExcelRow>();
						this.checkedFirstRow = true;
					}
					return this.firstRow;
				}
			}

			// Token: 0x06005519 RID: 21785 RVA: 0x00126798 File Offset: 0x00124998
			public ExcelReaderOpenXml.ExcelRange GetDimensions()
			{
				if (this.dimension.IsEmpty)
				{
					string text = this.reader.ReadDimensionReference(this);
					if (text != null && !this.inferDimensions)
					{
						this.dimension = ExcelReaderOpenXml.GetDimension(text);
					}
					if (this.dimension.IsEmpty || this.dimension.IsSingleCell)
					{
						if (this.FirstRow == null)
						{
							this.dimension = new ExcelReaderOpenXml.ExcelRange(0U, 0, 0U, 0);
						}
						else
						{
							string text2 = this.partUri.ToString();
							if (!this.reader.workbookDimensions.TryGetValue(text2, out this.dimension))
							{
								uint num = uint.MaxValue;
								ushort num2 = ushort.MaxValue;
								uint num3 = 0U;
								ushort num4 = 0;
								foreach (ExcelReaderOpenXml.ExcelRow excelRow in this.GetAllRows())
								{
									num = Math.Min(num, excelRow.index);
									num3 = Math.Max(num3, excelRow.index);
									if (excelRow.cellColumns.Count > 0)
									{
										num2 = Math.Min(num2, excelRow.cellColumns[0]);
										num4 = Math.Max(num4, excelRow.cellColumns[excelRow.cellColumns.Count - 1]);
									}
								}
								this.dimension = new ExcelReaderOpenXml.ExcelRange(num, num2, num3, num4);
								this.reader.workbookDimensions.Add(text2, this.dimension);
							}
						}
					}
				}
				return this.dimension;
			}

			// Token: 0x0600551A RID: 21786 RVA: 0x0012691C File Offset: 0x00124B1C
			public IEnumerable<ExcelReaderOpenXml.ExcelRow> GetRows(ExcelReaderOpenXml.ExcelRange range)
			{
				return this.GetRowsGenerator(range, this.GetAllRows());
			}

			// Token: 0x0600551B RID: 21787 RVA: 0x0012692B File Offset: 0x00124B2B
			private IEnumerable<ExcelReaderOpenXml.ExcelRow> GetRowsGenerator(ExcelReaderOpenXml.ExcelRange range, IEnumerable<ExcelReaderOpenXml.ExcelRow> allRowsEnumerable)
			{
				uint num = range.StartRow;
				foreach (ExcelReaderOpenXml.ExcelRow row in allRowsEnumerable)
				{
					if (row.index >= range.StartRow && row.index <= range.EndRow)
					{
						uint num2;
						for (uint i = num; i < row.index; i = num2 + 1U)
						{
							yield return new ExcelReaderOpenXml.ExcelRow(ExcelReaderOpenXml.EmptyCellValues, null, ExcelReaderOpenXml.EmptyCellCols, i, false);
							num2 = i;
						}
						yield return row;
						num = row.index + 1U;
					}
					row = null;
				}
				IEnumerator<ExcelReaderOpenXml.ExcelRow> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600551C RID: 21788 RVA: 0x00126942 File Offset: 0x00124B42
			private IEnumerable<ExcelReaderOpenXml.ExcelRow> GetAllRows()
			{
				return this.reader.ReadWorksheetRows(this);
			}

			// Token: 0x04002F69 RID: 12137
			private readonly ExcelReaderOpenXml reader;

			// Token: 0x04002F6A RID: 12138
			private readonly Uri partUri;

			// Token: 0x04002F6B RID: 12139
			private readonly bool inferDimensions;

			// Token: 0x04002F6C RID: 12140
			private ExcelReaderOpenXml.ExcelRange dimension;

			// Token: 0x04002F6D RID: 12141
			private ExcelReaderOpenXml.ExcelRow firstRow;

			// Token: 0x04002F6E RID: 12142
			private bool checkedFirstRow;
		}

		// Token: 0x02000C38 RID: 3128
		private sealed class ExcelRow
		{
			// Token: 0x06005526 RID: 21798 RVA: 0x00126BA4 File Offset: 0x00124DA4
			public ExcelRow(List<Microsoft.Mashup.Engine1.Runtime.Value> cellValues, List<ValueException> cellErrors, List<ushort> cellColumns, uint index, bool contiguous)
			{
				if (cellValues.Count != cellColumns.Count || (cellErrors != null && cellErrors.Count != cellColumns.Count))
				{
					throw new ArgumentException("cellValues, and cellColumns must have same count. If cellErrors is not null, it must also have the same count.");
				}
				this.cellValues = cellValues;
				this.cellErrors = cellErrors;
				this.cellColumns = cellColumns;
				this.index = index;
				this.contiguous = contiguous;
			}

			// Token: 0x04002F79 RID: 12153
			public readonly List<Microsoft.Mashup.Engine1.Runtime.Value> cellValues;

			// Token: 0x04002F7A RID: 12154
			public readonly List<ValueException> cellErrors;

			// Token: 0x04002F7B RID: 12155
			public readonly List<ushort> cellColumns;

			// Token: 0x04002F7C RID: 12156
			public readonly uint index;

			// Token: 0x04002F7D RID: 12157
			public readonly bool contiguous;
		}

		// Token: 0x02000C39 RID: 3129
		private struct ExcelRange
		{
			// Token: 0x06005527 RID: 21799 RVA: 0x00126C06 File Offset: 0x00124E06
			public ExcelRange(uint startRowIndex, ushort startColIndex, uint endRowIndex, ushort endColIndex)
			{
				this.startRowIndex = startRowIndex;
				this.startColIndex = startColIndex;
				this.endRowIndex = endRowIndex;
				this.endColIndex = endColIndex;
			}

			// Token: 0x170019D7 RID: 6615
			// (get) Token: 0x06005528 RID: 21800 RVA: 0x00126C25 File Offset: 0x00124E25
			public bool IsEmpty
			{
				get
				{
					return this.startRowIndex > this.endRowIndex || this.startColIndex > this.endColIndex;
				}
			}

			// Token: 0x170019D8 RID: 6616
			// (get) Token: 0x06005529 RID: 21801 RVA: 0x00126C45 File Offset: 0x00124E45
			public bool IsSingleCell
			{
				get
				{
					return this.startRowIndex == this.endRowIndex && this.startColIndex == this.endColIndex;
				}
			}

			// Token: 0x170019D9 RID: 6617
			// (get) Token: 0x0600552A RID: 21802 RVA: 0x00126C65 File Offset: 0x00124E65
			public uint StartRow
			{
				get
				{
					return this.startRowIndex;
				}
			}

			// Token: 0x170019DA RID: 6618
			// (get) Token: 0x0600552B RID: 21803 RVA: 0x00126C6D File Offset: 0x00124E6D
			public uint EndRow
			{
				get
				{
					return this.endRowIndex;
				}
			}

			// Token: 0x170019DB RID: 6619
			// (get) Token: 0x0600552C RID: 21804 RVA: 0x00126C75 File Offset: 0x00124E75
			public ushort StartColumn
			{
				get
				{
					return this.startColIndex;
				}
			}

			// Token: 0x170019DC RID: 6620
			// (get) Token: 0x0600552D RID: 21805 RVA: 0x00126C7D File Offset: 0x00124E7D
			public ushort EndColumn
			{
				get
				{
					return this.endColIndex;
				}
			}

			// Token: 0x170019DD RID: 6621
			// (get) Token: 0x0600552E RID: 21806 RVA: 0x00126C85 File Offset: 0x00124E85
			public long RowCount
			{
				get
				{
					return Math.Max(0L, (long)((ulong)this.endRowIndex - (ulong)this.startRowIndex + 1UL));
				}
			}

			// Token: 0x170019DE RID: 6622
			// (get) Token: 0x0600552F RID: 21807 RVA: 0x00126CA0 File Offset: 0x00124EA0
			public int ColumnCount
			{
				get
				{
					return Math.Max(0, (int)(this.endColIndex - this.startColIndex + 1));
				}
			}

			// Token: 0x06005530 RID: 21808 RVA: 0x00126CB8 File Offset: 0x00124EB8
			public ExcelReaderOpenXml.ExcelRange AdjustForSheet(ExcelReaderOpenXml.ExcelRange sheetRange)
			{
				uint num = this.endRowIndex;
				ushort num2 = this.endColIndex;
				if (num == 4294967295U || num2 == 65535)
				{
					num = ((num == uint.MaxValue) ? sheetRange.endRowIndex : num);
					num2 = ((num2 == ushort.MaxValue) ? sheetRange.endColIndex : num2);
				}
				return new ExcelReaderOpenXml.ExcelRange(this.startRowIndex, this.startColIndex, num, num2);
			}

			// Token: 0x06005531 RID: 21809 RVA: 0x00126D12 File Offset: 0x00124F12
			public static void Serialize(BinaryWriter writer, ExcelReaderOpenXml.ExcelRange range)
			{
				writer.Write(range.startRowIndex);
				writer.Write(range.endRowIndex);
				writer.Write(range.startColIndex);
				writer.Write(range.endColIndex);
			}

			// Token: 0x06005532 RID: 21810 RVA: 0x00126D44 File Offset: 0x00124F44
			public static ExcelReaderOpenXml.ExcelRange Deserialize(BinaryReader reader)
			{
				uint num = reader.ReadUInt32();
				uint num2 = reader.ReadUInt32();
				ushort num3 = reader.ReadUInt16();
				ushort num4 = reader.ReadUInt16();
				return new ExcelReaderOpenXml.ExcelRange(num, num3, num2, num4);
			}

			// Token: 0x04002F7E RID: 12158
			public static readonly ExcelReaderOpenXml.ExcelRange Empty = new ExcelReaderOpenXml.ExcelRange(uint.MaxValue, ushort.MaxValue, 0U, 0);

			// Token: 0x04002F7F RID: 12159
			private readonly uint startRowIndex;

			// Token: 0x04002F80 RID: 12160
			private readonly uint endRowIndex;

			// Token: 0x04002F81 RID: 12161
			private readonly ushort startColIndex;

			// Token: 0x04002F82 RID: 12162
			private readonly ushort endColIndex;
		}

		// Token: 0x02000C3A RID: 3130
		private struct ExcelTable
		{
			// Token: 0x06005534 RID: 21812 RVA: 0x00126D88 File Offset: 0x00124F88
			public ExcelTable(string name, string reference, ExcelReaderOpenXml.ExcelWorksheet sheet, ExcelReaderOpenXml.WorkbookItemVisibility visibility)
			{
				this.name = name;
				this.reference = reference;
				this.sheet = sheet;
				this.visibility = visibility;
			}

			// Token: 0x170019DF RID: 6623
			// (get) Token: 0x06005535 RID: 21813 RVA: 0x00126DA7 File Offset: 0x00124FA7
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170019E0 RID: 6624
			// (get) Token: 0x06005536 RID: 21814 RVA: 0x00126DAF File Offset: 0x00124FAF
			public string Reference
			{
				get
				{
					return this.reference;
				}
			}

			// Token: 0x170019E1 RID: 6625
			// (get) Token: 0x06005537 RID: 21815 RVA: 0x00126DB7 File Offset: 0x00124FB7
			public ExcelReaderOpenXml.ExcelWorksheet Sheet
			{
				get
				{
					return this.sheet;
				}
			}

			// Token: 0x170019E2 RID: 6626
			// (get) Token: 0x06005538 RID: 21816 RVA: 0x00126DBF File Offset: 0x00124FBF
			public ExcelReaderOpenXml.WorkbookItemVisibility Visibility
			{
				get
				{
					return this.visibility;
				}
			}

			// Token: 0x04002F83 RID: 12163
			private string name;

			// Token: 0x04002F84 RID: 12164
			private string reference;

			// Token: 0x04002F85 RID: 12165
			private ExcelReaderOpenXml.ExcelWorksheet sheet;

			// Token: 0x04002F86 RID: 12166
			private ExcelReaderOpenXml.WorkbookItemVisibility visibility;
		}

		// Token: 0x02000C3B RID: 3131
		private enum WorkbookItemVisibility
		{
			// Token: 0x04002F88 RID: 12168
			Visible,
			// Token: 0x04002F89 RID: 12169
			Hidden,
			// Token: 0x04002F8A RID: 12170
			VeryHidden
		}

		// Token: 0x02000C3C RID: 3132
		private struct WorkbookItem
		{
			// Token: 0x06005539 RID: 21817 RVA: 0x00126DC7 File Offset: 0x00124FC7
			public WorkbookItem(string name, string uniqueName, string kind, IValueReference table, ExcelReaderOpenXml.WorkbookItemVisibility visiblity)
			{
				this.name = name;
				this.uniqueName = uniqueName;
				this.kind = kind;
				this.table = table;
				this.visibility = visiblity;
			}

			// Token: 0x170019E3 RID: 6627
			// (get) Token: 0x0600553A RID: 21818 RVA: 0x00126DEE File Offset: 0x00124FEE
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170019E4 RID: 6628
			// (get) Token: 0x0600553B RID: 21819 RVA: 0x00126DF6 File Offset: 0x00124FF6
			public string UniqueName
			{
				get
				{
					return this.uniqueName;
				}
			}

			// Token: 0x170019E5 RID: 6629
			// (get) Token: 0x0600553C RID: 21820 RVA: 0x00126DFE File Offset: 0x00124FFE
			public string Kind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x170019E6 RID: 6630
			// (get) Token: 0x0600553D RID: 21821 RVA: 0x00126E06 File Offset: 0x00125006
			public IValueReference Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x170019E7 RID: 6631
			// (get) Token: 0x0600553E RID: 21822 RVA: 0x00126E0E File Offset: 0x0012500E
			public bool Hidden
			{
				get
				{
					return this.visibility > ExcelReaderOpenXml.WorkbookItemVisibility.Visible;
				}
			}

			// Token: 0x04002F8B RID: 12171
			private string name;

			// Token: 0x04002F8C RID: 12172
			private string uniqueName;

			// Token: 0x04002F8D RID: 12173
			private string kind;

			// Token: 0x04002F8E RID: 12174
			private IValueReference table;

			// Token: 0x04002F8F RID: 12175
			private ExcelReaderOpenXml.WorkbookItemVisibility visibility;
		}

		// Token: 0x02000C3D RID: 3133
		private struct DefinedName
		{
			// Token: 0x0600553F RID: 21823 RVA: 0x00126E19 File Offset: 0x00125019
			public DefinedName(string name, string uniqueName, string reference, bool inSheet, bool isHidden)
			{
				this.name = name;
				this.uniqueName = uniqueName;
				this.reference = reference;
				this.inSheet = inSheet;
				this.hidden = isHidden;
			}

			// Token: 0x170019E8 RID: 6632
			// (get) Token: 0x06005540 RID: 21824 RVA: 0x00126E40 File Offset: 0x00125040
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170019E9 RID: 6633
			// (get) Token: 0x06005541 RID: 21825 RVA: 0x00126E48 File Offset: 0x00125048
			public string UniqueName
			{
				get
				{
					return this.uniqueName;
				}
			}

			// Token: 0x170019EA RID: 6634
			// (get) Token: 0x06005542 RID: 21826 RVA: 0x00126E50 File Offset: 0x00125050
			public string Reference
			{
				get
				{
					return this.reference;
				}
			}

			// Token: 0x170019EB RID: 6635
			// (get) Token: 0x06005543 RID: 21827 RVA: 0x00126E58 File Offset: 0x00125058
			public bool InSheet
			{
				get
				{
					return this.inSheet;
				}
			}

			// Token: 0x170019EC RID: 6636
			// (get) Token: 0x06005544 RID: 21828 RVA: 0x00126E60 File Offset: 0x00125060
			public bool Hidden
			{
				get
				{
					return this.hidden;
				}
			}

			// Token: 0x04002F90 RID: 12176
			private string name;

			// Token: 0x04002F91 RID: 12177
			private string uniqueName;

			// Token: 0x04002F92 RID: 12178
			private string reference;

			// Token: 0x04002F93 RID: 12179
			private bool inSheet;

			// Token: 0x04002F94 RID: 12180
			private bool hidden;
		}

		// Token: 0x02000C3E RID: 3134
		private class WorksheetTableValueReference : IValueReference
		{
			// Token: 0x06005545 RID: 21829 RVA: 0x00126E68 File Offset: 0x00125068
			public WorksheetTableValueReference(ExcelReaderOpenXml reader, ExcelReaderOpenXml.ExcelWorksheet sheet, string dimension, bool useFirstRowAsHeader)
			{
				this.reader = reader;
				this.sheet = sheet;
				this.dimension = dimension;
				this.useFirstRowAsHeader = useFirstRowAsHeader;
			}

			// Token: 0x170019ED RID: 6637
			// (get) Token: 0x06005546 RID: 21830 RVA: 0x00126E8D File Offset: 0x0012508D
			public bool Evaluated
			{
				get
				{
					return this.table != null;
				}
			}

			// Token: 0x170019EE RID: 6638
			// (get) Token: 0x06005547 RID: 21831 RVA: 0x00126E98 File Offset: 0x00125098
			public Microsoft.Mashup.Engine1.Runtime.Value Value
			{
				get
				{
					if (this.table == null)
					{
						this.table = this.reader.CreateTableValue(this.sheet, this.dimension, this.useFirstRowAsHeader);
						this.reader = null;
					}
					return this.table;
				}
			}

			// Token: 0x04002F95 RID: 12181
			private ExcelReaderOpenXml reader;

			// Token: 0x04002F96 RID: 12182
			private ExcelReaderOpenXml.ExcelWorksheet sheet;

			// Token: 0x04002F97 RID: 12183
			private string dimension;

			// Token: 0x04002F98 RID: 12184
			private bool useFirstRowAsHeader;

			// Token: 0x04002F99 RID: 12185
			private TableValue table;
		}

		// Token: 0x02000C3F RID: 3135
		private sealed class ColumnTypes
		{
			// Token: 0x06005548 RID: 21832 RVA: 0x00126ED2 File Offset: 0x001250D2
			public ColumnTypes(ExcelReaderOpenXml excelReader, string sheetId, string dimension)
			{
				this.excelReader = excelReader;
				this.cacheKey = this.excelReader.CreateCacheKey("ColumnTypes", new string[]
				{
					sheetId,
					dimension ?? string.Empty
				});
			}

			// Token: 0x06005549 RID: 21833 RVA: 0x00126F0E File Offset: 0x0012510E
			public bool TryGetTypes(out ValueKind[] columnTypes)
			{
				this.Deserialize();
				columnTypes = this.columnTypes;
				return columnTypes != null;
			}

			// Token: 0x0600554A RID: 21834 RVA: 0x00126F23 File Offset: 0x00125123
			public void SetTypes(ValueKind[] columnTypes)
			{
				this.columnTypes = (ValueKind[])columnTypes.Clone();
				this.Serialize();
			}

			// Token: 0x0600554B RID: 21835 RVA: 0x00126F3C File Offset: 0x0012513C
			private void Deserialize()
			{
				if (this.columnTypes == null)
				{
					this.excelReader.cache.TryDeserialize(this.cacheKey, delegate(BinaryReader reader)
					{
						this.columnTypes = new ValueKind[reader.ReadInt32()];
						for (int i = 0; i < this.columnTypes.Length; i++)
						{
							this.columnTypes[i] = (ValueKind)reader.ReadInt16();
						}
					});
				}
			}

			// Token: 0x0600554C RID: 21836 RVA: 0x00126F69 File Offset: 0x00125169
			private void Serialize()
			{
				this.excelReader.cache.Serialize(this.cacheKey, delegate(BinaryWriter writer)
				{
					writer.Write(this.columnTypes.Length);
					for (int i = 0; i < this.columnTypes.Length; i++)
					{
						writer.Write((ushort)this.columnTypes[i]);
					}
				});
			}

			// Token: 0x04002F9A RID: 12186
			private readonly ExcelReaderOpenXml excelReader;

			// Token: 0x04002F9B RID: 12187
			private readonly string cacheKey;

			// Token: 0x04002F9C RID: 12188
			private ValueKind[] columnTypes;
		}

		// Token: 0x02000C40 RID: 3136
		private sealed class WorkbookDimensions
		{
			// Token: 0x0600554F RID: 21839 RVA: 0x0012700D File Offset: 0x0012520D
			public WorkbookDimensions(ExcelReaderOpenXml excelReader)
			{
				this.excelReader = excelReader;
				this.cacheKey = this.excelReader.CreateCacheKey("WorkbookDimensions", Array.Empty<string>());
			}

			// Token: 0x06005550 RID: 21840 RVA: 0x00127037 File Offset: 0x00125237
			public bool TryGetValue(string id, out ExcelReaderOpenXml.ExcelRange dimension)
			{
				this.Deserialize();
				return this.dimensions.TryGetValue(id, out dimension);
			}

			// Token: 0x06005551 RID: 21841 RVA: 0x0012704C File Offset: 0x0012524C
			public void Add(string id, ExcelReaderOpenXml.ExcelRange dimension)
			{
				this.Deserialize();
				this.dimensions.Add(id, dimension);
				this.Serialize();
			}

			// Token: 0x06005552 RID: 21842 RVA: 0x00127067 File Offset: 0x00125267
			private void Deserialize()
			{
				if (this.dimensions == null)
				{
					this.dimensions = new Dictionary<string, ExcelReaderOpenXml.ExcelRange>();
					this.excelReader.cache.TryDeserialize(this.cacheKey, delegate(BinaryReader reader)
					{
						int num = reader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							string text = reader.ReadString();
							ExcelReaderOpenXml.ExcelRange excelRange = ExcelReaderOpenXml.ExcelRange.Deserialize(reader);
							this.dimensions.Add(text, excelRange);
						}
					});
				}
			}

			// Token: 0x06005553 RID: 21843 RVA: 0x0012709F File Offset: 0x0012529F
			private void Serialize()
			{
				this.excelReader.cache.Serialize(this.cacheKey, delegate(BinaryWriter writer)
				{
					writer.Write(this.dimensions.Count);
					foreach (KeyValuePair<string, ExcelReaderOpenXml.ExcelRange> keyValuePair in this.dimensions)
					{
						writer.Write(keyValuePair.Key);
						ExcelReaderOpenXml.ExcelRange.Serialize(writer, keyValuePair.Value);
					}
				});
			}

			// Token: 0x04002F9D RID: 12189
			private readonly ExcelReaderOpenXml excelReader;

			// Token: 0x04002F9E RID: 12190
			private readonly string cacheKey;

			// Token: 0x04002F9F RID: 12191
			private Dictionary<string, ExcelReaderOpenXml.ExcelRange> dimensions;
		}

		// Token: 0x02000C41 RID: 3137
		private class FormattingInfo
		{
			// Token: 0x06005556 RID: 21846 RVA: 0x00127178 File Offset: 0x00125378
			public FormattingInfo(XmlNameTable nameTable)
			{
				this.alignmentName = nameTable.Add("alignment");
				this.autoName = nameTable.Add("auto");
				this.autoFilterName = nameTable.Add("autoFilter");
				this.boldName = nameTable.Add("b");
				this.bgColorName = nameTable.Add("bgColor");
				this.borderName = nameTable.Add("border");
				this.borderIdName = nameTable.Add("borderId");
				this.bordersName = nameTable.Add("borders");
				this.bottomName = nameTable.Add("bottom");
				this.colName = nameTable.Add("col");
				this.colorName = nameTable.Add("color");
				this.colsName = nameTable.Add("cols");
				this.countName = nameTable.Add("count");
				this.fillName = nameTable.Add("fill");
				this.fillIdName = nameTable.Add("fillId");
				this.fillsName = nameTable.Add("fills");
				this.fgColorName = nameTable.Add("fgColor");
				this.fontName = nameTable.Add("font");
				this.fontIdName = nameTable.Add("fontId");
				this.fontsName = nameTable.Add("fonts");
				this.formulaName = nameTable.Add("f");
				this.horizontalName = nameTable.Add("horizontal");
				this.italicName = nameTable.Add("i");
				this.indexedName = nameTable.Add("indexed");
				this.leftName = nameTable.Add("left");
				this.maxName = nameTable.Add("max");
				this.mergeCellName = nameTable.Add("mergeCell");
				this.mergeCellsName = nameTable.Add("mergeCells");
				this.minName = nameTable.Add("min");
				this.paneName = nameTable.Add("pane");
				this.patternFillName = nameTable.Add("patternFill");
				this.patternTypeName = nameTable.Add("patternType");
				this.rgbName = nameTable.Add("rgb");
				this.rightName = nameTable.Add("right");
				this.sheetViewName = nameTable.Add("sheetView");
				this.sheetViewsName = nameTable.Add("sheetViews");
				this.sharedGroupIndexName = nameTable.Add("si");
				this.sortStateName = nameTable.Add("sortState");
				this.strikethroughName = nameTable.Add("strike");
				this.styleName = nameTable.Add("style");
				this.szName = nameTable.Add("sz");
				this.themeName = nameTable.Add("theme");
				this.tintName = nameTable.Add("tint");
				this.topName = nameTable.Add("top");
				this.topLeftCellName = nameTable.Add("topLeftCell");
				this.underlineName = nameTable.Add("u");
				this.valName = nameTable.Add("val");
				this.verticalName = nameTable.Add("vertical");
				this.xSplitName = nameTable.Add("xSplit");
				this.ySplitName = nameTable.Add("ySplit");
				this.cellXfsTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle>();
				this.numberFormatsTable = new Dictionary<string, string>();
				this.fontsTable = null;
				this.bordersTable = null;
				this.fillsTable = null;
				this.sheetFormattingInfos = null;
			}

			// Token: 0x06005557 RID: 21847 RVA: 0x00127510 File Offset: 0x00125710
			private ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo GetWorksheetFormattingInfo(ExcelReaderOpenXml.ExcelWorksheet sheet)
			{
				if (this.sheetFormattingInfos == null)
				{
					this.sheetFormattingInfos = new Dictionary<ExcelReaderOpenXml.ExcelWorksheet, ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo>();
				}
				ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo excelWorksheetFormattingInfo;
				if (!this.sheetFormattingInfos.TryGetValue(sheet, out excelWorksheetFormattingInfo))
				{
					excelWorksheetFormattingInfo = new ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo();
					this.sheetFormattingInfos[sheet] = excelWorksheetFormattingInfo;
				}
				return excelWorksheetFormattingInfo;
			}

			// Token: 0x06005558 RID: 21848 RVA: 0x00127554 File Offset: 0x00125754
			public void RegisterHiddenRow(ExcelReaderOpenXml.ExcelRow row)
			{
				if (this.hiddenRows == null)
				{
					this.hiddenRows = new HashSet<ExcelReaderOpenXml.ExcelRow>();
				}
				this.hiddenRows.Add(row);
			}

			// Token: 0x06005559 RID: 21849 RVA: 0x00127576 File Offset: 0x00125776
			public void RegisterNumberFormat(string formatId, string formatCode)
			{
				this.numberFormatsTable[formatId] = formatCode;
			}

			// Token: 0x0600555A RID: 21850 RVA: 0x00127585 File Offset: 0x00125785
			public void RegisterWorksheetName(ExcelReaderOpenXml.ExcelWorksheet sheet, string name)
			{
				this.GetWorksheetFormattingInfo(sheet).name = name;
			}

			// Token: 0x0600555B RID: 21851 RVA: 0x00127594 File Offset: 0x00125794
			public void ReadXf(XmlReader reader, string key, string mainNamespaceName, string numFmtIdStr)
			{
				string attribute = reader.GetAttribute(this.fontIdName, string.Empty);
				string attribute2 = reader.GetAttribute(this.fillIdName, string.Empty);
				string attribute3 = reader.GetAttribute(this.borderIdName, string.Empty);
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Font font;
				this.fontsTable.TryGetValue(attribute, out font);
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill fill;
				this.fillsTable.TryGetValue(attribute2, out fill);
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Border border;
				this.bordersTable.TryGetValue(attribute3, out border);
				string text;
				if (!ExcelReaderOpenXml.FormattingInfo.DefaultNumberFormats.TryGetValue(numFmtIdStr, out text))
				{
					this.numberFormatsTable.TryGetValue(numFmtIdStr, out text);
				}
				string text2 = null;
				string text3 = null;
				if (reader.IsEmptyElement)
				{
					reader.Skip();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(this.alignmentName, mainNamespaceName))
						{
							text2 = reader.GetAttribute(this.horizontalName, string.Empty);
							text3 = reader.GetAttribute(this.verticalName, string.Empty);
							reader.Skip();
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				ExcelReaderOpenXml.FormattingInfo.CellStyle cellStyle = new ExcelReaderOpenXml.FormattingInfo.CellStyle(font, fill, border, text2, text3, text);
				this.cellXfsTable.Add(key, cellStyle);
			}

			// Token: 0x0600555C RID: 21852 RVA: 0x001276B4 File Offset: 0x001258B4
			public bool TryReadWorkbookStylesSection(XmlReader reader, string mainNamespaceName, string nameName)
			{
				if (reader.IsStartElement(this.fontsName, mainNamespaceName))
				{
					this.ReadFonts(reader, mainNamespaceName, nameName);
				}
				else if (reader.IsStartElement(this.bordersName, mainNamespaceName))
				{
					this.ReadBorders(reader, mainNamespaceName);
				}
				else
				{
					if (!reader.IsStartElement(this.fillsName, mainNamespaceName))
					{
						return false;
					}
					this.ReadFills(reader, mainNamespaceName);
				}
				return true;
			}

			// Token: 0x0600555D RID: 21853 RVA: 0x00127710 File Offset: 0x00125910
			private void ReadFonts(XmlReader reader, string mainNamespaceName, string nameName)
			{
				if (reader.IsEmptyElement)
				{
					reader.Read();
					return;
				}
				string attribute = reader.GetAttribute(this.countName, string.Empty);
				reader.ReadStartElement();
				int num;
				if (int.TryParse(attribute, out num))
				{
					this.fontsTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Font>(num);
				}
				else
				{
					this.fontsTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Font>();
				}
				uint num2 = 0U;
				for (;;)
				{
					if (reader.IsStartElement(this.fontName, mainNamespaceName))
					{
						this.fontsTable.Add(ExcelReaderOpenXml.ToString(num2++), this.ReadFont(reader, mainNamespaceName, nameName));
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}

			// Token: 0x0600555E RID: 21854 RVA: 0x001277B0 File Offset: 0x001259B0
			private ExcelReaderOpenXml.FormattingInfo.CellStyle.Font ReadFont(XmlReader reader, string mainNamespaceName, string nameName)
			{
				string text = null;
				string text2 = null;
				bool flag = false;
				bool flag2 = false;
				string text3 = null;
				bool flag3 = false;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color = null;
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(nameName, mainNamespaceName))
						{
							text2 = reader.GetAttribute(this.valName, string.Empty);
							reader.Skip();
						}
						else if (reader.IsStartElement(this.szName, mainNamespaceName))
						{
							text = reader.GetAttribute(this.valName, string.Empty);
							reader.Skip();
						}
						else if (reader.IsStartElement(this.boldName, mainNamespaceName))
						{
							flag = true;
							reader.Skip();
						}
						else if (reader.IsStartElement(this.italicName, mainNamespaceName))
						{
							flag2 = true;
							reader.Skip();
						}
						else if (reader.IsStartElement(this.underlineName, mainNamespaceName))
						{
							text3 = reader.GetAttribute(this.valName, string.Empty) ?? "single";
							reader.Skip();
						}
						else if (reader.IsStartElement(this.strikethroughName, mainNamespaceName))
						{
							flag3 = true;
							reader.Skip();
						}
						else if (reader.IsStartElement(this.colorName, mainNamespaceName))
						{
							color = this.ReadColor(reader);
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				return new ExcelReaderOpenXml.FormattingInfo.CellStyle.Font(text2, text, flag, flag2, text3, flag3, color);
			}

			// Token: 0x0600555F RID: 21855 RVA: 0x00127900 File Offset: 0x00125B00
			private void ReadFills(XmlReader reader, string mainNamespaceName)
			{
				if (reader.IsEmptyElement)
				{
					reader.Read();
					return;
				}
				int num;
				if (int.TryParse(reader.GetAttribute(this.countName, string.Empty), out num))
				{
					this.fillsTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill>(num);
				}
				else
				{
					this.fillsTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill>();
				}
				reader.ReadStartElement();
				uint num2 = 0U;
				for (;;)
				{
					if (reader.IsStartElement(this.fillName, mainNamespaceName))
					{
						this.fillsTable.Add(ExcelReaderOpenXml.ToString(num2++), this.ReadFill(reader, mainNamespaceName));
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}

			// Token: 0x06005560 RID: 21856 RVA: 0x0012799C File Offset: 0x00125B9C
			private ExcelReaderOpenXml.FormattingInfo.CellStyle.Color ReadColor(XmlReader reader)
			{
				bool? flag = null;
				int? num = null;
				int? num2 = null;
				double? num3 = null;
				string attribute = reader.GetAttribute(this.autoName, string.Empty);
				if (attribute != null)
				{
					if (attribute == "true" || attribute == "1")
					{
						flag = new bool?(true);
					}
					else if (attribute == "false" || attribute == "0")
					{
						flag = new bool?(false);
					}
				}
				string attribute2 = reader.GetAttribute(this.indexedName, string.Empty);
				int num4;
				if (attribute2 != null && int.TryParse(attribute2, out num4))
				{
					num = new int?(num4);
				}
				string attribute3 = reader.GetAttribute(this.themeName, string.Empty);
				int num5;
				if (attribute3 != null && int.TryParse(attribute3, out num5))
				{
					num2 = new int?(num5);
				}
				string attribute4 = reader.GetAttribute(this.rgbName, string.Empty);
				string attribute5 = reader.GetAttribute(this.tintName, string.Empty);
				double num6;
				if (attribute5 != null && double.TryParse(attribute5, out num6))
				{
					num3 = new double?(num6);
				}
				reader.Skip();
				return new ExcelReaderOpenXml.FormattingInfo.CellStyle.Color(flag, num, attribute4, num2, num3);
			}

			// Token: 0x06005561 RID: 21857 RVA: 0x00127AD0 File Offset: 0x00125CD0
			private ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill ReadFill(XmlReader reader, string mainNamespaceName)
			{
				string text = null;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color = null;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color2 = null;
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(this.patternFillName, mainNamespaceName))
						{
							text = reader.GetAttribute(this.patternTypeName, string.Empty);
							if (reader.IsEmptyElement)
							{
								reader.Read();
							}
							else
							{
								reader.ReadStartElement();
								for (;;)
								{
									if (reader.IsStartElement(this.fgColorName, mainNamespaceName))
									{
										color = this.ReadColor(reader);
									}
									else if (reader.IsStartElement(this.bgColorName, mainNamespaceName))
									{
										color2 = this.ReadColor(reader);
									}
									else
									{
										if (!reader.IsStartElement())
										{
											break;
										}
										reader.Skip();
									}
								}
								reader.ReadEndElement();
							}
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				return new ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill(text, color, color2);
			}

			// Token: 0x06005562 RID: 21858 RVA: 0x00127BA4 File Offset: 0x00125DA4
			private void ReadBorders(XmlReader reader, string mainNamespaceName)
			{
				if (reader.IsEmptyElement)
				{
					reader.Read();
					return;
				}
				int num;
				if (int.TryParse(reader.GetAttribute(this.countName, string.Empty), out num))
				{
					this.bordersTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border>(num);
				}
				else
				{
					this.bordersTable = new Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border>();
				}
				reader.ReadStartElement();
				uint num2 = 0U;
				for (;;)
				{
					if (reader.IsStartElement(this.borderName, mainNamespaceName))
					{
						this.bordersTable.Add(ExcelReaderOpenXml.ToString(num2++), this.ReadBorder(reader, mainNamespaceName));
					}
					else
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}

			// Token: 0x06005563 RID: 21859 RVA: 0x00127C40 File Offset: 0x00125E40
			private ExcelReaderOpenXml.FormattingInfo.CellStyle.Border ReadBorder(XmlReader reader, string mainNamespaceName)
			{
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide borderSide = null;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide borderSide2 = null;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide borderSide3 = null;
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide borderSide4 = null;
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(this.leftName, mainNamespaceName))
						{
							borderSide = this.ReadBorderSide(reader, mainNamespaceName);
						}
						else if (reader.IsStartElement(this.rightName, mainNamespaceName))
						{
							borderSide2 = this.ReadBorderSide(reader, mainNamespaceName);
						}
						else if (reader.IsStartElement(this.topName, mainNamespaceName))
						{
							borderSide3 = this.ReadBorderSide(reader, mainNamespaceName);
						}
						else if (reader.IsStartElement(this.bottomName, mainNamespaceName))
						{
							borderSide4 = this.ReadBorderSide(reader, mainNamespaceName);
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				if (borderSide == null && borderSide2 == null && borderSide3 == null && borderSide4 == null)
				{
					return null;
				}
				return new ExcelReaderOpenXml.FormattingInfo.CellStyle.Border(borderSide, borderSide2, borderSide3, borderSide4);
			}

			// Token: 0x06005564 RID: 21860 RVA: 0x00127D04 File Offset: 0x00125F04
			private ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide ReadBorderSide(XmlReader reader, string mainNamespaceName)
			{
				string attribute = reader.GetAttribute(this.styleName, string.Empty);
				ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color = null;
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(this.colorName, mainNamespaceName))
						{
							color = this.ReadColor(reader);
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				if (attribute == null)
				{
					return null;
				}
				return new ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide(attribute, color);
			}

			// Token: 0x06005565 RID: 21861 RVA: 0x00127D78 File Offset: 0x00125F78
			private SortedDictionary<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> ReadSheetMergedCells(ExcelReaderOpenXml.ExcelWorksheet worksheet, Func<Uri, XmlReader> createReader, string refName, string mainNamespaceName)
			{
				SortedDictionary<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> sortedDictionary = new SortedDictionary<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>>();
				ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo worksheetFormattingInfo = this.GetWorksheetFormattingInfo(worksheet);
				using (XmlReader xmlReader = createReader(worksheet.PartUri))
				{
					xmlReader.ReadStartElement();
					for (;;)
					{
						if (xmlReader.IsStartElement(this.mergeCellsName, mainNamespaceName))
						{
							using (IEnumerator<ExcelReaderOpenXml.ExcelRange> enumerator = this.ReadMergedCells(xmlReader, refName, mainNamespaceName).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ExcelReaderOpenXml.ExcelRange excelRange = enumerator.Current;
									SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange> sortedDictionary2;
									if (!sortedDictionary.TryGetValue(excelRange.StartRow, out sortedDictionary2))
									{
										sortedDictionary2 = (sortedDictionary[excelRange.StartRow] = new SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>());
									}
									sortedDictionary2[excelRange.StartColumn] = excelRange;
									if (worksheetFormattingInfo.paneStartingColumn != null)
									{
										int startColumn = (int)excelRange.StartColumn;
										ushort? num = worksheetFormattingInfo.paneStartingColumn;
										int? num2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
										if ((startColumn <= num2.GetValueOrDefault()) & (num2 != null))
										{
											num = worksheetFormattingInfo.paneStartingColumn;
											num2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null) - (int)excelRange.StartColumn;
											int columnCount = excelRange.ColumnCount;
											if ((num2.GetValueOrDefault() < columnCount) & (num2 != null))
											{
												uint startRow = excelRange.StartRow;
												uint? num3 = worksheetFormattingInfo.paneStartingRow;
												if ((startRow <= num3.GetValueOrDefault()) & (num3 != null))
												{
													num3 = worksheetFormattingInfo.paneStartingRow;
													uint endRow = excelRange.EndRow;
													if ((num3.GetValueOrDefault() <= endRow) & (num3 != null))
													{
														worksheetFormattingInfo.paneStartingColumn = new ushort?(excelRange.StartColumn);
														worksheetFormattingInfo.paneStartingRow = new uint?(excelRange.StartRow);
													}
												}
											}
										}
									}
								}
								continue;
							}
						}
						if (!xmlReader.IsStartElement())
						{
							break;
						}
						xmlReader.Skip();
					}
				}
				return sortedDictionary;
			}

			// Token: 0x06005566 RID: 21862 RVA: 0x00127FC0 File Offset: 0x001261C0
			private IEnumerable<ExcelReaderOpenXml.ExcelRange> ReadMergedCells(XmlReader reader, string refName, string mainNamespaceName)
			{
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					for (;;)
					{
						if (reader.IsStartElement(this.mergeCellName, mainNamespaceName))
						{
							string attribute = reader.GetAttribute(refName, string.Empty);
							if (attribute != null)
							{
								yield return ExcelReaderOpenXml.GetDimension(attribute);
							}
							reader.Skip();
						}
						else
						{
							if (!reader.IsStartElement())
							{
								break;
							}
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				yield break;
			}

			// Token: 0x06005567 RID: 21863 RVA: 0x00127FE8 File Offset: 0x001261E8
			private void ReadWorksheetMetadataUnguarded(ExcelReaderOpenXml.ExcelWorksheet worksheet, Func<Uri, XmlReader> createReader, string hidden, string refName, string stateName, string mainNamespaceName)
			{
				ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo worksheetFormattingInfo = this.GetWorksheetFormattingInfo(worksheet);
				using (XmlReader xmlReader = createReader(worksheet.PartUri))
				{
					xmlReader.ReadStartElement();
					string text = null;
					string text2 = null;
					for (;;)
					{
						if (xmlReader.IsStartElement(this.colsName, mainNamespaceName))
						{
							if (xmlReader.IsEmptyElement)
							{
								xmlReader.Read();
							}
							else
							{
								xmlReader.ReadStartElement();
								for (;;)
								{
									if (xmlReader.IsStartElement(this.colName, mainNamespaceName))
									{
										if (xmlReader.GetAttribute(hidden, string.Empty) == "1")
										{
											string attribute = xmlReader.GetAttribute(this.minName, string.Empty);
											string attribute2 = xmlReader.GetAttribute(this.maxName, string.Empty);
											int num;
											int num2;
											if (int.TryParse(attribute, out num) && int.TryParse(attribute2, out num2))
											{
												if (worksheetFormattingInfo.hiddenColumns == null)
												{
													worksheetFormattingInfo.hiddenColumns = new List<int>();
												}
												for (int i = num; i <= num2; i++)
												{
													worksheetFormattingInfo.hiddenColumns.Add(i);
												}
											}
										}
										xmlReader.Skip();
									}
									else
									{
										if (!xmlReader.IsStartElement())
										{
											break;
										}
										xmlReader.Skip();
									}
								}
								xmlReader.ReadEndElement();
							}
						}
						else if (xmlReader.IsStartElement(this.sheetViewsName, mainNamespaceName))
						{
							if (xmlReader.IsEmptyElement)
							{
								xmlReader.Read();
							}
							else
							{
								xmlReader.ReadStartElement();
								for (;;)
								{
									if (xmlReader.IsStartElement(this.sheetViewName, mainNamespaceName))
									{
										if (xmlReader.IsEmptyElement)
										{
											xmlReader.Read();
										}
										else
										{
											string attribute3 = xmlReader.GetAttribute(this.topLeftCellName, string.Empty);
											if (attribute3 != null)
											{
												uint num3;
												ushort num4;
												ExcelReaderOpenXml.GetCellLocation(attribute3, out num3, out num4);
												worksheetFormattingInfo.paneStartingRow = new uint?(num3);
												worksheetFormattingInfo.paneStartingColumn = new ushort?(num4);
											}
											xmlReader.ReadStartElement();
											for (;;)
											{
												if (xmlReader.IsStartElement(this.paneName, mainNamespaceName))
												{
													if (xmlReader.GetAttribute(stateName, string.Empty) == "frozen")
													{
														string attribute4 = xmlReader.GetAttribute(this.xSplitName, string.Empty);
														string attribute5 = xmlReader.GetAttribute(this.ySplitName, string.Empty);
														int num5;
														if (int.TryParse(attribute4, out num5))
														{
															worksheetFormattingInfo.numFrozenColumns = new int?(num5);
														}
														int num6;
														if (int.TryParse(attribute5, out num6))
														{
															worksheetFormattingInfo.numFrozenRows = new int?(num6);
														}
													}
													xmlReader.Skip();
												}
												else
												{
													if (!xmlReader.IsStartElement())
													{
														break;
													}
													xmlReader.Skip();
												}
											}
											xmlReader.ReadEndElement();
										}
									}
									else
									{
										if (!xmlReader.IsStartElement())
										{
											break;
										}
										xmlReader.Skip();
									}
								}
								xmlReader.ReadEndElement();
							}
						}
						else if (xmlReader.IsStartElement(this.autoFilterName, mainNamespaceName))
						{
							text = xmlReader.GetAttribute(refName, string.Empty);
							if (xmlReader.IsEmptyElement)
							{
								xmlReader.Read();
							}
							else
							{
								xmlReader.ReadStartElement();
								for (;;)
								{
									if (xmlReader.IsStartElement(this.sortStateName, mainNamespaceName))
									{
										text2 = xmlReader.GetAttribute(refName, string.Empty);
										xmlReader.Skip();
									}
									else
									{
										if (!xmlReader.IsStartElement())
										{
											break;
										}
										xmlReader.Skip();
									}
								}
								xmlReader.ReadEndElement();
							}
						}
						else if (xmlReader.IsStartElement(this.sortStateName, mainNamespaceName))
						{
							text2 = xmlReader.GetAttribute(refName, string.Empty);
							xmlReader.Skip();
						}
						else
						{
							if (!xmlReader.IsStartElement())
							{
								break;
							}
							xmlReader.Skip();
						}
					}
					if (text != null && text2 != null)
					{
						ExcelReaderOpenXml.ExcelRange dimension = ExcelReaderOpenXml.GetDimension(text);
						ExcelReaderOpenXml.ExcelRange dimension2 = ExcelReaderOpenXml.GetDimension(text2);
						ExcelReaderOpenXml.ExcelRange excelRange = new ExcelReaderOpenXml.ExcelRange(Math.Min(dimension.StartRow, dimension2.StartRow), Math.Min(dimension.StartColumn, dimension2.StartColumn), Math.Max(dimension.EndRow, dimension2.EndRow), Math.Max(dimension.EndColumn, dimension2.EndColumn));
						worksheetFormattingInfo.autoFilterRange = new ExcelReaderOpenXml.ExcelRange?(excelRange);
					}
				}
			}

			// Token: 0x06005568 RID: 21864 RVA: 0x001283A4 File Offset: 0x001265A4
			private static RecordValue AddSpanMeta(RecordValue oldMeta, int rowSpan, int columnSpan)
			{
				return oldMeta.Concatenate(RecordValue.New(ExcelReaderOpenXml.SpanMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
				{
					NumberValue.New(rowSpan),
					NumberValue.New(columnSpan)
				})).AsRecord;
			}

			// Token: 0x06005569 RID: 21865 RVA: 0x001283D3 File Offset: 0x001265D3
			private static Microsoft.Mashup.Engine1.Runtime.Value AddSpanMetaToCell(Microsoft.Mashup.Engine1.Runtime.Value cell, int rowSpan, int columnSpan)
			{
				return cell.NewMeta(ExcelReaderOpenXml.FormattingInfo.AddSpanMeta(cell.MetaValue, rowSpan, columnSpan));
			}

			// Token: 0x0600556A RID: 21866 RVA: 0x001283E8 File Offset: 0x001265E8
			private static IEnumerable<ExcelReaderOpenXml.ExcelRow> CombineRowsWithMergedCellsInfo(IEnumerable<ExcelReaderOpenXml.ExcelRow> rows, SortedDictionary<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> mergedCellsInfo)
			{
				IEnumerator<KeyValuePair<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>>> mergedCellsEnumerator = mergedCellsInfo.GetEnumerator();
				uint? num;
				if (!mergedCellsEnumerator.MoveNext())
				{
					num = null;
				}
				else
				{
					KeyValuePair<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> keyValuePair = mergedCellsEnumerator.Current;
					num = new uint?(keyValuePair.Key);
				}
				uint? nextRowIdx = num;
				foreach (ExcelReaderOpenXml.ExcelRow excelRow in rows)
				{
					if (nextRowIdx != null && nextRowIdx.Value == excelRow.index)
					{
						KeyValuePair<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> keyValuePair = mergedCellsEnumerator.Current;
						SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange> value = keyValuePair.Value;
						List<Microsoft.Mashup.Engine1.Runtime.Value> cellValues = excelRow.cellValues;
						List<ushort> cellColumns = excelRow.cellColumns;
						int num2 = -1;
						foreach (KeyValuePair<ushort, ExcelReaderOpenXml.ExcelRange> keyValuePair2 in value)
						{
							num2 = cellColumns.IndexOf(keyValuePair2.Key, num2 + 1);
							ExcelReaderOpenXml.ExcelRange value2 = keyValuePair2.Value;
							Microsoft.Mashup.Engine1.Runtime.Value value3 = cellValues[num2];
							if (value3 != null)
							{
								int num3 = (int)(value2.EndRow - value2.StartRow + 1U);
								cellValues[num2] = ExcelReaderOpenXml.FormattingInfo.AddSpanMetaToCell(value3, num3, value2.ColumnCount);
							}
						}
						uint? num4;
						if (!mergedCellsEnumerator.MoveNext())
						{
							num4 = null;
						}
						else
						{
							keyValuePair = mergedCellsEnumerator.Current;
							num4 = new uint?(keyValuePair.Key);
						}
						nextRowIdx = num4;
					}
					yield return excelRow;
				}
				IEnumerator<ExcelReaderOpenXml.ExcelRow> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600556B RID: 21867 RVA: 0x00128400 File Offset: 0x00126600
			public IEnumerable<ExcelReaderOpenXml.ExcelRow> ReadWorksheetRowsWithMergedCellInfoUnguarded(ExcelReaderOpenXml.ExcelWorksheet worksheet, IEnumerable<ExcelReaderOpenXml.ExcelRow> rows, Func<Uri, XmlReader> createReader, string hidden, string refName, string stateName, string mainNamespaceName)
			{
				this.ReadWorksheetMetadataUnguarded(worksheet, createReader, hidden, refName, stateName, mainNamespaceName);
				SortedDictionary<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>> sortedDictionary = this.ReadSheetMergedCells(worksheet, createReader, refName, mainNamespaceName);
				ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo worksheetFormattingInfo = this.GetWorksheetFormattingInfo(worksheet);
				if (worksheetFormattingInfo.paneStartingColumn != null && worksheetFormattingInfo.numFrozenColumns != null)
				{
					worksheetFormattingInfo.numFrozenColumns += (int)(worksheetFormattingInfo.paneStartingColumn.Value - 1);
				}
				if (worksheetFormattingInfo.paneStartingRow != null && worksheetFormattingInfo.numFrozenRows != null)
				{
					worksheetFormattingInfo.numFrozenRows += (int)(worksheetFormattingInfo.paneStartingRow.Value - 1U);
				}
				if (sortedDictionary.Any<KeyValuePair<uint, SortedDictionary<ushort, ExcelReaderOpenXml.ExcelRange>>>())
				{
					return ExcelReaderOpenXml.FormattingInfo.CombineRowsWithMergedCellsInfo(rows, sortedDictionary);
				}
				return rows;
			}

			// Token: 0x0600556C RID: 21868 RVA: 0x001284F4 File Offset: 0x001266F4
			public Microsoft.Mashup.Engine1.Runtime.Value AddCellMeta(Microsoft.Mashup.Engine1.Runtime.Value value, string styleIndex, string cellFormula, string cellFormulaSharedId)
			{
				ExcelReaderOpenXml.FormattingInfo.CellStyle cellStyle = null;
				this.cellXfsTable.TryGetValue(styleIndex ?? "0", out cellStyle);
				if (cellStyle != null)
				{
					value = cellStyle.AddMetaToValue(value);
				}
				if (cellFormula != null)
				{
					value = value.NewMeta(value.MetaValue.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.FormulaMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { TextValue.New(cellFormula) })).AsRecord);
				}
				if (cellFormulaSharedId != null)
				{
					value = value.NewMeta(value.MetaValue.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.FormulaSharedIdMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { TextValue.New(cellFormulaSharedId) })).AsRecord);
				}
				return value;
			}

			// Token: 0x0600556D RID: 21869 RVA: 0x00128592 File Offset: 0x00126792
			public RecordValue AddRowMeta(RecordValue rowRecord, ExcelReaderOpenXml.ExcelRow row)
			{
				Keys rowMetaKey = ExcelReaderOpenXml.FormattingInfo.RowMetaKey;
				Microsoft.Mashup.Engine1.Runtime.Value[] array = new Microsoft.Mashup.Engine1.Runtime.Value[1];
				int num = 0;
				HashSet<ExcelReaderOpenXml.ExcelRow> hashSet = this.hiddenRows;
				array[num] = LogicalValue.New(hashSet != null && hashSet.Contains(row));
				return rowRecord.NewMeta(RecordValue.New(rowMetaKey, array)).AsRecord;
			}

			// Token: 0x0600556E RID: 21870 RVA: 0x001285CC File Offset: 0x001267CC
			private static Microsoft.Mashup.Engine1.Runtime.Value RangeToMeta(ExcelReaderOpenXml.ExcelRange? rangeOpt)
			{
				if (rangeOpt == null)
				{
					return Microsoft.Mashup.Engine1.Runtime.Value.Null;
				}
				ExcelReaderOpenXml.ExcelRange value = rangeOpt.Value;
				return RecordValue.New(ExcelReaderOpenXml.FormattingInfo.RangeMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
				{
					NumberValue.New((long)((ulong)(value.StartRow - 1U))),
					NumberValue.New((int)(value.StartColumn - 1)),
					NumberValue.New(value.RowCount),
					NumberValue.New(value.ColumnCount)
				});
			}

			// Token: 0x0600556F RID: 21871 RVA: 0x00128644 File Offset: 0x00126844
			public TableValue AddTableMeta(TableValue result, ExcelReaderOpenXml.ExcelWorksheet worksheet, ExcelReaderOpenXml.ExcelRange range, string[] columnNames)
			{
				ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo worksheetFormattingInfo = this.GetWorksheetFormattingInfo(worksheet);
				List<int> hiddenColumns = worksheetFormattingInfo.hiddenColumns;
				ListValue listValue = ListValue.New(((hiddenColumns != null) ? (from colIdx in hiddenColumns
					select colIdx - (int)range.StartColumn into colIdx
					where colIdx < columnNames.Length
					select columnNames[colIdx]).ToArray<string>() : null) ?? new string[0]);
				return result.NewMeta(result.MetaValue.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.TableFormattingMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
				{
					TextValue.New(worksheetFormattingInfo.name),
					ExcelReaderOpenXml.FormattingInfo.RangeToMeta(new ExcelReaderOpenXml.ExcelRange?(range)),
					(worksheetFormattingInfo.numFrozenRows != null) ? NumberValue.New((long)worksheetFormattingInfo.numFrozenRows.Value - (long)((ulong)range.StartRow) + 1L) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
					(worksheetFormattingInfo.numFrozenColumns != null) ? NumberValue.New(worksheetFormattingInfo.numFrozenColumns.Value - (int)range.StartColumn + 1) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
					listValue,
					ExcelReaderOpenXml.FormattingInfo.RangeToMeta(worksheetFormattingInfo.autoFilterRange)
				})).AsRecord).AsTable;
			}

			// Token: 0x04002FA0 RID: 12192
			private const string DefaultStyleIndex = "0";

			// Token: 0x04002FA1 RID: 12193
			public readonly string autoFilterName;

			// Token: 0x04002FA2 RID: 12194
			public readonly string formulaName;

			// Token: 0x04002FA3 RID: 12195
			public readonly string sharedGroupIndexName;

			// Token: 0x04002FA4 RID: 12196
			private readonly string alignmentName;

			// Token: 0x04002FA5 RID: 12197
			private readonly string autoName;

			// Token: 0x04002FA6 RID: 12198
			private readonly string bgColorName;

			// Token: 0x04002FA7 RID: 12199
			private readonly string boldName;

			// Token: 0x04002FA8 RID: 12200
			private readonly string borderName;

			// Token: 0x04002FA9 RID: 12201
			private readonly string borderIdName;

			// Token: 0x04002FAA RID: 12202
			private readonly string bordersName;

			// Token: 0x04002FAB RID: 12203
			private readonly string bottomName;

			// Token: 0x04002FAC RID: 12204
			private readonly string colName;

			// Token: 0x04002FAD RID: 12205
			private readonly string colorName;

			// Token: 0x04002FAE RID: 12206
			private readonly string colsName;

			// Token: 0x04002FAF RID: 12207
			private readonly string countName;

			// Token: 0x04002FB0 RID: 12208
			private readonly string fgColorName;

			// Token: 0x04002FB1 RID: 12209
			private readonly string fillName;

			// Token: 0x04002FB2 RID: 12210
			private readonly string fillIdName;

			// Token: 0x04002FB3 RID: 12211
			private readonly string fillsName;

			// Token: 0x04002FB4 RID: 12212
			private readonly string fontName;

			// Token: 0x04002FB5 RID: 12213
			private readonly string fontIdName;

			// Token: 0x04002FB6 RID: 12214
			private readonly string fontsName;

			// Token: 0x04002FB7 RID: 12215
			private readonly string horizontalName;

			// Token: 0x04002FB8 RID: 12216
			private readonly string indexedName;

			// Token: 0x04002FB9 RID: 12217
			private readonly string italicName;

			// Token: 0x04002FBA RID: 12218
			private readonly string leftName;

			// Token: 0x04002FBB RID: 12219
			private readonly string maxName;

			// Token: 0x04002FBC RID: 12220
			private readonly string mergeCellName;

			// Token: 0x04002FBD RID: 12221
			private readonly string mergeCellsName;

			// Token: 0x04002FBE RID: 12222
			private readonly string minName;

			// Token: 0x04002FBF RID: 12223
			private readonly string paneName;

			// Token: 0x04002FC0 RID: 12224
			private readonly string patternFillName;

			// Token: 0x04002FC1 RID: 12225
			private readonly string patternTypeName;

			// Token: 0x04002FC2 RID: 12226
			private readonly string rgbName;

			// Token: 0x04002FC3 RID: 12227
			private readonly string rightName;

			// Token: 0x04002FC4 RID: 12228
			private readonly string sheetViewName;

			// Token: 0x04002FC5 RID: 12229
			private readonly string sheetViewsName;

			// Token: 0x04002FC6 RID: 12230
			private readonly string sortStateName;

			// Token: 0x04002FC7 RID: 12231
			private readonly string strikethroughName;

			// Token: 0x04002FC8 RID: 12232
			private readonly string styleName;

			// Token: 0x04002FC9 RID: 12233
			private readonly string szName;

			// Token: 0x04002FCA RID: 12234
			private readonly string themeName;

			// Token: 0x04002FCB RID: 12235
			private readonly string tintName;

			// Token: 0x04002FCC RID: 12236
			private readonly string topName;

			// Token: 0x04002FCD RID: 12237
			private readonly string topLeftCellName;

			// Token: 0x04002FCE RID: 12238
			private readonly string underlineName;

			// Token: 0x04002FCF RID: 12239
			private readonly string valName;

			// Token: 0x04002FD0 RID: 12240
			private readonly string verticalName;

			// Token: 0x04002FD1 RID: 12241
			private readonly string xSplitName;

			// Token: 0x04002FD2 RID: 12242
			private readonly string ySplitName;

			// Token: 0x04002FD3 RID: 12243
			private static readonly Keys RowMetaKey = Keys.New("Hidden");

			// Token: 0x04002FD4 RID: 12244
			private static readonly Keys TableFormattingMetaKey = Keys.New(new string[] { "SheetName", "Range", "FrozenRowCount", "FrozenColumnCount", "HiddenColumns", "AutoFilterRange" });

			// Token: 0x04002FD5 RID: 12245
			private static readonly Keys RangeMetaKey = Keys.New("SkippedRowCount", "SkippedColumnCount", "RowCount", "ColumnCount");

			// Token: 0x04002FD6 RID: 12246
			private const string FormulaMetaString = "Formula";

			// Token: 0x04002FD7 RID: 12247
			private static readonly Keys FormulaMetaKey = Keys.New("Formula");

			// Token: 0x04002FD8 RID: 12248
			private const string FormulaSharedIdMetaString = "FormulaSharedId";

			// Token: 0x04002FD9 RID: 12249
			private static readonly Keys FormulaSharedIdMetaKey = Keys.New("FormulaSharedId");

			// Token: 0x04002FDA RID: 12250
			private readonly Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle> cellXfsTable;

			// Token: 0x04002FDB RID: 12251
			private Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Font> fontsTable;

			// Token: 0x04002FDC RID: 12252
			private Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border> bordersTable;

			// Token: 0x04002FDD RID: 12253
			private Dictionary<string, ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill> fillsTable;

			// Token: 0x04002FDE RID: 12254
			private Dictionary<string, string> numberFormatsTable;

			// Token: 0x04002FDF RID: 12255
			private Dictionary<ExcelReaderOpenXml.ExcelWorksheet, ExcelReaderOpenXml.FormattingInfo.ExcelWorksheetFormattingInfo> sheetFormattingInfos;

			// Token: 0x04002FE0 RID: 12256
			private HashSet<ExcelReaderOpenXml.ExcelRow> hiddenRows;

			// Token: 0x04002FE1 RID: 12257
			private static readonly Dictionary<string, string> DefaultNumberFormats = new Dictionary<string, string>(30)
			{
				{ "0", "General" },
				{ "1", "0" },
				{ "2", "0.00" },
				{ "3", "#,##0" },
				{ "4", "#,##0.00" },
				{ "9", "0%" },
				{ "10", "0.00%" },
				{ "11", "0.00E+00" },
				{ "12", "# ?/?" },
				{ "13", "# ??/??" },
				{ "14", "mm-dd-yy" },
				{ "15", "d-mmm-yy" },
				{ "16", "d-mmm" },
				{ "17", "mmm-yy" },
				{ "18", "h:mm AM/PM" },
				{ "19", "h:mm:ss AM/PM" },
				{ "20", "h:mm" },
				{ "21", "h:mm:ss" },
				{ "22", "m/d/yy h:mm" },
				{ "37", "#,##0 ;(#,##0)" },
				{ "38", "#,##0 ;[Red](#,##0)" },
				{ "39", "#,##0.00;(#,##0.00)" },
				{ "40", "#,##0.00;[Red](#,##0.00)" },
				{ "45", "mm:ss" },
				{ "46", "[h]:mm:ss" },
				{ "47", "mmss.0" },
				{ "48", "##0.0E+0" },
				{ "49", "@" }
			};

			// Token: 0x02000C42 RID: 3138
			private sealed class CellStyle
			{
				// Token: 0x170019EF RID: 6639
				// (get) Token: 0x06005571 RID: 21873 RVA: 0x001289F4 File Offset: 0x00126BF4
				public RecordValue Meta { get; }

				// Token: 0x06005572 RID: 21874 RVA: 0x001289FC File Offset: 0x00126BFC
				public CellStyle(ExcelReaderOpenXml.FormattingInfo.CellStyle.Font font, ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill fill, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border border, string horizontalAlignment, string verticalAlignment, string numberFormat)
				{
					this.font = font;
					this.fill = fill;
					this.border = border;
					this.horizontalAlignment = horizontalAlignment;
					this.verticalAlignment = verticalAlignment;
					this.numberFormat = numberFormat;
					this.Meta = RecordValue.Empty;
					if (font != null)
					{
						this.Meta = this.Meta.Concatenate(font.Meta).AsRecord;
					}
					if (fill != null)
					{
						this.Meta = this.Meta.Concatenate(fill.Meta).AsRecord;
					}
					if (border != null)
					{
						this.Meta = this.Meta.Concatenate(border.Meta).AsRecord;
					}
					if (horizontalAlignment != null)
					{
						this.Meta = this.Meta.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.HorizontalAlignmentMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { TextValue.New(horizontalAlignment) })).AsRecord;
					}
					if (verticalAlignment != null)
					{
						this.Meta = this.Meta.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.VerticalAlignmentMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { TextValue.New(verticalAlignment) })).AsRecord;
					}
					if (numberFormat != null)
					{
						this.Meta = this.Meta.Concatenate(RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.NumberFormatMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { TextValue.New(numberFormat) })).AsRecord;
					}
				}

				// Token: 0x06005573 RID: 21875 RVA: 0x00128B40 File Offset: 0x00126D40
				public Microsoft.Mashup.Engine1.Runtime.Value AddMetaToValue(Microsoft.Mashup.Engine1.Runtime.Value value)
				{
					return value.NewMeta(value.MetaValue.Concatenate(this.Meta).AsRecord);
				}

				// Token: 0x04002FE2 RID: 12258
				private readonly ExcelReaderOpenXml.FormattingInfo.CellStyle.Font font;

				// Token: 0x04002FE3 RID: 12259
				private readonly ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill fill;

				// Token: 0x04002FE4 RID: 12260
				private readonly ExcelReaderOpenXml.FormattingInfo.CellStyle.Border border;

				// Token: 0x04002FE5 RID: 12261
				private readonly string horizontalAlignment;

				// Token: 0x04002FE6 RID: 12262
				private readonly string verticalAlignment;

				// Token: 0x04002FE7 RID: 12263
				private readonly string numberFormat;

				// Token: 0x04002FE9 RID: 12265
				private const string HorizontalAlignmentMetaString = "HorizontalAlignment";

				// Token: 0x04002FEA RID: 12266
				private static readonly Keys HorizontalAlignmentMetaKey = Keys.New("HorizontalAlignment");

				// Token: 0x04002FEB RID: 12267
				private const string VerticalAlignmentMetaString = "VerticalAlignment";

				// Token: 0x04002FEC RID: 12268
				private static readonly Keys VerticalAlignmentMetaKey = Keys.New("VerticalAlignment");

				// Token: 0x04002FED RID: 12269
				private const string NumberFormatMetaString = "NumberFormat";

				// Token: 0x04002FEE RID: 12270
				private static readonly Keys NumberFormatMetaKey = Keys.New("NumberFormat");

				// Token: 0x02000C43 RID: 3139
				public sealed class Font
				{
					// Token: 0x170019F0 RID: 6640
					// (get) Token: 0x06005575 RID: 21877 RVA: 0x00128B8D File Offset: 0x00126D8D
					public string Name { get; }

					// Token: 0x170019F1 RID: 6641
					// (get) Token: 0x06005576 RID: 21878 RVA: 0x00128B95 File Offset: 0x00126D95
					public int? Size { get; }

					// Token: 0x170019F2 RID: 6642
					// (get) Token: 0x06005577 RID: 21879 RVA: 0x00128B9D File Offset: 0x00126D9D
					public bool Bold { get; }

					// Token: 0x170019F3 RID: 6643
					// (get) Token: 0x06005578 RID: 21880 RVA: 0x00128BA5 File Offset: 0x00126DA5
					public bool Italic { get; }

					// Token: 0x170019F4 RID: 6644
					// (get) Token: 0x06005579 RID: 21881 RVA: 0x00128BAD File Offset: 0x00126DAD
					public string Underline { get; }

					// Token: 0x170019F5 RID: 6645
					// (get) Token: 0x0600557A RID: 21882 RVA: 0x00128BB5 File Offset: 0x00126DB5
					public bool Strikethrough { get; }

					// Token: 0x170019F6 RID: 6646
					// (get) Token: 0x0600557B RID: 21883 RVA: 0x00128BBD File Offset: 0x00126DBD
					public ExcelReaderOpenXml.FormattingInfo.CellStyle.Color Color { get; }

					// Token: 0x170019F7 RID: 6647
					// (get) Token: 0x0600557C RID: 21884 RVA: 0x00128BC5 File Offset: 0x00126DC5
					public RecordValue Meta { get; }

					// Token: 0x0600557D RID: 21885 RVA: 0x00128BD0 File Offset: 0x00126DD0
					public Font(string name, string sizeStr, bool bold, bool italic, string underline, bool strikethrough, ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color)
					{
						this.Name = name;
						int num;
						if (int.TryParse(sizeStr, out num))
						{
							this.Size = new int?(num);
						}
						else
						{
							this.Size = null;
						}
						this.Bold = bold;
						this.Italic = italic;
						this.Underline = underline;
						this.Strikethrough = strikethrough;
						this.Color = color;
						Microsoft.Mashup.Engine1.Runtime.Value value = ((this.Size != null) ? NumberValue.New(this.Size.Value) : Microsoft.Mashup.Engine1.Runtime.Value.Null);
						Keys fontInfoMetaKey = ExcelReaderOpenXml.FormattingInfo.CellStyle.Font.FontInfoMetaKey;
						Microsoft.Mashup.Engine1.Runtime.Value[] array = new Microsoft.Mashup.Engine1.Runtime.Value[7];
						array[0] = TextValue.NewOrNull(this.Name);
						array[1] = value;
						array[2] = LogicalValue.New(this.Bold);
						array[3] = LogicalValue.New(this.Italic);
						array[4] = TextValue.NewOrNull(this.Underline);
						array[5] = LogicalValue.New(this.Strikethrough);
						int num2 = 6;
						ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color2 = this.Color;
						array[num2] = ((color2 != null) ? color2.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null;
						RecordValue recordValue = RecordValue.New(fontInfoMetaKey, array);
						this.Meta = RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.Font.FontMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { recordValue });
					}

					// Token: 0x04002FEF RID: 12271
					public const string DefaultUnderline = "single";

					// Token: 0x04002FF0 RID: 12272
					private const string FontMetaString = "Font";

					// Token: 0x04002FF1 RID: 12273
					private const string NameMetaString = "Name";

					// Token: 0x04002FF2 RID: 12274
					private const string SizeMetaString = "Size";

					// Token: 0x04002FF3 RID: 12275
					private const string BoldMetaString = "Bold";

					// Token: 0x04002FF4 RID: 12276
					private const string ItalicMetaString = "Italic";

					// Token: 0x04002FF5 RID: 12277
					private const string UnderlineMetaString = "Underline";

					// Token: 0x04002FF6 RID: 12278
					private const string StrikethroughMetaString = "Strikethrough";

					// Token: 0x04002FF7 RID: 12279
					private const string ColorMetaString = "Color";

					// Token: 0x04002FF8 RID: 12280
					private static readonly Keys FontMetaKey = Keys.New("Font");

					// Token: 0x04002FF9 RID: 12281
					private static readonly Keys FontInfoMetaKey = Keys.New(new string[] { "Name", "Size", "Bold", "Italic", "Underline", "Strikethrough", "Color" });
				}

				// Token: 0x02000C44 RID: 3140
				public sealed class Border
				{
					// Token: 0x170019F8 RID: 6648
					// (get) Token: 0x0600557F RID: 21887 RVA: 0x00128D58 File Offset: 0x00126F58
					public ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide Left { get; }

					// Token: 0x170019F9 RID: 6649
					// (get) Token: 0x06005580 RID: 21888 RVA: 0x00128D60 File Offset: 0x00126F60
					public ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide Right { get; }

					// Token: 0x170019FA RID: 6650
					// (get) Token: 0x06005581 RID: 21889 RVA: 0x00128D68 File Offset: 0x00126F68
					public ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide Top { get; }

					// Token: 0x170019FB RID: 6651
					// (get) Token: 0x06005582 RID: 21890 RVA: 0x00128D70 File Offset: 0x00126F70
					public ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide Bottom { get; }

					// Token: 0x170019FC RID: 6652
					// (get) Token: 0x06005583 RID: 21891 RVA: 0x00128D78 File Offset: 0x00126F78
					public RecordValue Meta { get; }

					// Token: 0x06005584 RID: 21892 RVA: 0x00128D80 File Offset: 0x00126F80
					public Border(ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide left, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide right, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide top, ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide bottom)
					{
						this.Left = left;
						this.Right = right;
						this.Top = top;
						this.Bottom = bottom;
						Keys borderMetaKey = ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderMetaKey;
						Microsoft.Mashup.Engine1.Runtime.Value[] array = new Microsoft.Mashup.Engine1.Runtime.Value[1];
						int num = 0;
						Keys borderInfoMetaKey = ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderInfoMetaKey;
						Microsoft.Mashup.Engine1.Runtime.Value[] array2 = new Microsoft.Mashup.Engine1.Runtime.Value[4];
						int num2 = 0;
						ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide left2 = this.Left;
						array2[num2] = ((left2 != null) ? left2.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null;
						int num3 = 1;
						ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide right2 = this.Right;
						array2[num3] = ((right2 != null) ? right2.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null;
						int num4 = 2;
						ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide top2 = this.Top;
						array2[num4] = ((top2 != null) ? top2.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null;
						int num5 = 3;
						ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide bottom2 = this.Bottom;
						array2[num5] = ((bottom2 != null) ? bottom2.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null;
						array[num] = RecordValue.New(borderInfoMetaKey, array2);
						this.Meta = RecordValue.New(borderMetaKey, array);
					}

					// Token: 0x04003002 RID: 12290
					private const string BorderMetaString = "Border";

					// Token: 0x04003003 RID: 12291
					private const string LeftMetaString = "Left";

					// Token: 0x04003004 RID: 12292
					private const string RightMetaString = "Right";

					// Token: 0x04003005 RID: 12293
					private const string TopMetaString = "Top";

					// Token: 0x04003006 RID: 12294
					private const string BottomMetaString = "Bottom";

					// Token: 0x04003007 RID: 12295
					private static readonly Keys BorderMetaKey = Keys.New("Border");

					// Token: 0x04003008 RID: 12296
					private static readonly Keys BorderInfoMetaKey = Keys.New("Left", "Right", "Top", "Bottom");

					// Token: 0x02000C45 RID: 3141
					public sealed class BorderSide
					{
						// Token: 0x170019FD RID: 6653
						// (get) Token: 0x06005586 RID: 21894 RVA: 0x00128E80 File Offset: 0x00127080
						public string Style { get; }

						// Token: 0x170019FE RID: 6654
						// (get) Token: 0x06005587 RID: 21895 RVA: 0x00128E88 File Offset: 0x00127088
						public RecordValue Meta { get; }

						// Token: 0x06005588 RID: 21896 RVA: 0x00128E90 File Offset: 0x00127090
						public BorderSide(string style, ExcelReaderOpenXml.FormattingInfo.CellStyle.Color color)
						{
							this.Style = style;
							this.Meta = RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.Border.BorderSide.BorderSideMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
							{
								TextValue.New(this.Style),
								((color != null) ? color.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null
							});
						}

						// Token: 0x0400300E RID: 12302
						private const string StyleMetaString = "Style";

						// Token: 0x0400300F RID: 12303
						private const string ColorMetaString = "Color";

						// Token: 0x04003010 RID: 12304
						private static readonly Keys BorderSideMetaKey = Keys.New("Style", "Color");
					}
				}

				// Token: 0x02000C46 RID: 3142
				public sealed class Color
				{
					// Token: 0x170019FF RID: 6655
					// (get) Token: 0x0600558A RID: 21898 RVA: 0x00128EFC File Offset: 0x001270FC
					public RecordValue Meta { get; }

					// Token: 0x0600558B RID: 21899 RVA: 0x00128F04 File Offset: 0x00127104
					public Color(bool? auto, int? indexed, string rgb, int? theme, double? tint)
					{
						this.Meta = RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.Color.ColorMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
						{
							(auto != null) ? LogicalValue.New(auto.Value) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
							(indexed != null) ? NumberValue.New(indexed.Value) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
							(rgb != null) ? TextValue.New(rgb) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
							(theme != null) ? NumberValue.New(theme.Value) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
							(tint != null) ? NumberValue.New(tint.Value) : Microsoft.Mashup.Engine1.Runtime.Value.Null
						});
					}

					// Token: 0x04003013 RID: 12307
					private static readonly Keys ColorMetaKey = Keys.New(new string[] { "Auto", "Indexed", "RGB", "Theme", "Tint" });
				}

				// Token: 0x02000C47 RID: 3143
				public sealed class Fill
				{
					// Token: 0x17001A00 RID: 6656
					// (get) Token: 0x0600558D RID: 21901 RVA: 0x00128FF6 File Offset: 0x001271F6
					public string PatternType { get; }

					// Token: 0x17001A01 RID: 6657
					// (get) Token: 0x0600558E RID: 21902 RVA: 0x00128FFE File Offset: 0x001271FE
					public RecordValue Meta { get; }

					// Token: 0x0600558F RID: 21903 RVA: 0x00129008 File Offset: 0x00127208
					public Fill(string patternType, ExcelReaderOpenXml.FormattingInfo.CellStyle.Color fgColor, ExcelReaderOpenXml.FormattingInfo.CellStyle.Color bgColor)
					{
						this.PatternType = patternType;
						this.Meta = RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill.FillMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[] { RecordValue.New(ExcelReaderOpenXml.FormattingInfo.CellStyle.Fill.FillInfoMetaKey, new Microsoft.Mashup.Engine1.Runtime.Value[]
						{
							TextValue.New(this.PatternType),
							((fgColor != null) ? fgColor.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null,
							((bgColor != null) ? bgColor.Meta : null) ?? Microsoft.Mashup.Engine1.Runtime.Value.Null
						}) });
					}

					// Token: 0x04003015 RID: 12309
					private const string FillMetaString = "Fill";

					// Token: 0x04003016 RID: 12310
					private const string PatternTypeMetaString = "PatternType";

					// Token: 0x04003017 RID: 12311
					private const string FgColorMetaString = "FgColor";

					// Token: 0x04003018 RID: 12312
					private const string BgColorMetaString = "BgColor";

					// Token: 0x04003019 RID: 12313
					private static readonly Keys FillMetaKey = Keys.New("Fill");

					// Token: 0x0400301A RID: 12314
					private static readonly Keys FillInfoMetaKey = Keys.New("PatternType", "FgColor", "BgColor");
				}
			}

			// Token: 0x02000C48 RID: 3144
			private class ExcelWorksheetFormattingInfo
			{
				// Token: 0x0400301D RID: 12317
				public string name;

				// Token: 0x0400301E RID: 12318
				public List<int> hiddenColumns;

				// Token: 0x0400301F RID: 12319
				public int? numFrozenRows;

				// Token: 0x04003020 RID: 12320
				public int? numFrozenColumns;

				// Token: 0x04003021 RID: 12321
				public uint? paneStartingRow;

				// Token: 0x04003022 RID: 12322
				public ushort? paneStartingColumn;

				// Token: 0x04003023 RID: 12323
				public ExcelReaderOpenXml.ExcelRange? autoFilterRange;
			}
		}

		// Token: 0x02000C4C RID: 3148
		private sealed class SharedStringTable : ExcelReaderOpenXml.PagedTable<TextValue>
		{
			// Token: 0x060055A7 RID: 21927 RVA: 0x00129543 File Offset: 0x00127743
			public SharedStringTable(ExcelReaderOpenXml excelReader, Uri partUri)
				: base(excelReader, partUri)
			{
			}

			// Token: 0x060055A8 RID: 21928 RVA: 0x0012954D File Offset: 0x0012774D
			protected override void ReadTable(XmlReader reader, Dictionary<string, TextValue> table)
			{
				this.excelReader.ReadSharedStringTable(reader, table);
			}

			// Token: 0x060055A9 RID: 21929 RVA: 0x0012955C File Offset: 0x0012775C
			protected override TextValue DeserializeEntry(BinaryReader reader)
			{
				return TextValue.New(reader.ReadString());
			}

			// Token: 0x060055AA RID: 21930 RVA: 0x00129569 File Offset: 0x00127769
			protected override void SerializeEntry(BinaryWriter writer, TextValue entry)
			{
				writer.Write(entry.AsString);
			}
		}

		// Token: 0x02000C4D RID: 3149
		private abstract class PagedTable<T>
		{
			// Token: 0x060055AB RID: 21931 RVA: 0x00129577 File Offset: 0x00127777
			protected PagedTable(ExcelReaderOpenXml excelReader, Uri partUri)
			{
				this.excelReader = excelReader;
				this.partUri = partUri;
				this.table = new Dictionary<string, T>();
			}

			// Token: 0x17001A06 RID: 6662
			public T this[string index]
			{
				get
				{
					T t;
					if (!this.table.TryGetValue(index, out t))
					{
						this.ReadPage(ExcelReaderOpenXml.PagedTable<T>.PageFromIndex(uint.Parse(index, CultureInfo.InvariantCulture)));
						t = this.table[index];
					}
					return t;
				}
			}

			// Token: 0x060055AD RID: 21933
			protected abstract void ReadTable(XmlReader reader, Dictionary<string, T> table);

			// Token: 0x060055AE RID: 21934
			protected abstract T DeserializeEntry(BinaryReader reader);

			// Token: 0x060055AF RID: 21935
			protected abstract void SerializeEntry(BinaryWriter writer, T entry);

			// Token: 0x060055B0 RID: 21936 RVA: 0x001295D9 File Offset: 0x001277D9
			private string PageKey(uint page)
			{
				return this.excelReader.CreateCacheKey(this.partUri.ToString(), new string[] { ExcelReaderOpenXml.ToString(page) });
			}

			// Token: 0x060055B1 RID: 21937 RVA: 0x00129600 File Offset: 0x00127800
			private void ReadPage(uint page)
			{
				if (!this.excelReader.cache.TryDeserialize(this.PageKey(page), delegate(BinaryReader reader)
				{
					uint num = page * 16384U;
					uint num2 = reader.ReadUInt32();
					for (uint num3 = 0U; num3 < num2; num3 += 1U)
					{
						T t = this.DeserializeEntry(reader);
						this.table.Add(ExcelReaderOpenXml.ToString(num + num3), t);
					}
				}))
				{
					this.PopulateCache();
				}
			}

			// Token: 0x060055B2 RID: 21938 RVA: 0x00129654 File Offset: 0x00127854
			private void WritePage(uint page)
			{
				this.excelReader.cache.Serialize(this.PageKey(page), delegate(BinaryWriter writer)
				{
					uint num = page * 16384U;
					uint num2 = Math.Min((uint)(this.table.Count - (int)num), 16384U);
					writer.Write(num2);
					for (uint num3 = 0U; num3 < num2; num3 += 1U)
					{
						T t = this.table[ExcelReaderOpenXml.ToString(num + num3)];
						this.SerializeEntry(writer, t);
					}
				});
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x001296A0 File Offset: 0x001278A0
			private void PopulateCache()
			{
				if (!this.populated)
				{
					this.table.Clear();
					if (this.partUri != null)
					{
						using (XmlReader xmlReader = this.excelReader.CreateReader(this.partUri))
						{
							this.ReadTable(xmlReader, this.table);
						}
						if (this.excelReader.lastModified != null)
						{
							uint num = (uint)(this.table.Count / 16384 + 1);
							for (uint num2 = 0U; num2 < num; num2 += 1U)
							{
								this.WritePage(num2);
							}
						}
					}
					this.populated = true;
				}
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x0012974C File Offset: 0x0012794C
			private static uint PageFromIndex(uint index)
			{
				return index / 16384U;
			}

			// Token: 0x0400303A RID: 12346
			private const uint pageSize = 16384U;

			// Token: 0x0400303B RID: 12347
			protected readonly ExcelReaderOpenXml excelReader;

			// Token: 0x0400303C RID: 12348
			private readonly Uri partUri;

			// Token: 0x0400303D RID: 12349
			private readonly Dictionary<string, T> table;

			// Token: 0x0400303E RID: 12350
			private bool populated;
		}

		// Token: 0x02000C50 RID: 3152
		private sealed class TimestampedCache
		{
			// Token: 0x060055B9 RID: 21945 RVA: 0x0012981C File Offset: 0x00127A1C
			public TimestampedCache(ExcelReaderOpenXml excelReader, IPersistentCache cache)
			{
				this.excelReader = excelReader;
				this.cache = cache;
			}

			// Token: 0x060055BA RID: 21946 RVA: 0x00129834 File Offset: 0x00127A34
			public bool TryDeserialize(string cacheKey, Action<BinaryReader> deserialize)
			{
				Stream stream;
				if (this.excelReader.lastModified != null && this.cache.TryGetValue(cacheKey, out stream))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
					{
						if (binaryReader.ReadDateTime() == this.excelReader.lastModified)
						{
							deserialize(binaryReader);
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x060055BB RID: 21947 RVA: 0x001298D0 File Offset: 0x00127AD0
			public void Serialize(string cacheKey, Action<BinaryWriter> serialize)
			{
				if (this.excelReader.lastModified != null)
				{
					Stream stream = this.cache.BeginAdd();
					try
					{
						BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8);
						binaryWriter.WriteDateTime(this.excelReader.lastModified.Value);
						serialize(binaryWriter);
						binaryWriter.Flush();
						stream = this.cache.EndAdd(cacheKey, stream);
					}
					finally
					{
						stream.Dispose();
					}
				}
			}

			// Token: 0x04003043 RID: 12355
			private readonly ExcelReaderOpenXml excelReader;

			// Token: 0x04003044 RID: 12356
			private readonly IPersistentCache cache;
		}

		// Token: 0x02000C51 RID: 3153
		private sealed class HandleExceptionsEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060055BC RID: 21948 RVA: 0x00129958 File Offset: 0x00127B58
			public HandleExceptionsEnumerable(Action<Exception> handler, IEnumerable<T> enumerable)
			{
				this.handler = handler;
				this.enumerable = enumerable;
			}

			// Token: 0x060055BD RID: 21949 RVA: 0x0012996E File Offset: 0x00127B6E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060055BE RID: 21950 RVA: 0x00129978 File Offset: 0x00127B78
			public IEnumerator<T> GetEnumerator()
			{
				IEnumerator<T> enumerator;
				try
				{
					enumerator = new ExcelReaderOpenXml.HandleExceptionsEnumerable<T>.Enumerator(this, this.enumerable.GetEnumerator());
				}
				catch (Exception ex)
				{
					this.handler(ex);
					throw;
				}
				return enumerator;
			}

			// Token: 0x04003045 RID: 12357
			private readonly Action<Exception> handler;

			// Token: 0x04003046 RID: 12358
			private readonly IEnumerable<T> enumerable;

			// Token: 0x02000C52 RID: 3154
			private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
			{
				// Token: 0x060055BF RID: 21951 RVA: 0x001299BC File Offset: 0x00127BBC
				public Enumerator(ExcelReaderOpenXml.HandleExceptionsEnumerable<T> enumerable, IEnumerator<T> enumerator)
				{
					this.enumerable = enumerable;
					this.enumerator = enumerator;
				}

				// Token: 0x17001A07 RID: 6663
				// (get) Token: 0x060055C0 RID: 21952 RVA: 0x001299D2 File Offset: 0x00127BD2
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17001A08 RID: 6664
				// (get) Token: 0x060055C1 RID: 21953 RVA: 0x001299E0 File Offset: 0x00127BE0
				public T Current
				{
					get
					{
						T t;
						try
						{
							t = this.enumerator.Current;
						}
						catch (Exception ex)
						{
							this.enumerable.handler(ex);
							throw;
						}
						return t;
					}
				}

				// Token: 0x060055C2 RID: 21954 RVA: 0x00129A20 File Offset: 0x00127C20
				public bool MoveNext()
				{
					bool flag;
					try
					{
						flag = this.enumerator.MoveNext();
					}
					catch (Exception ex)
					{
						this.enumerable.handler(ex);
						throw;
					}
					return flag;
				}

				// Token: 0x060055C3 RID: 21955 RVA: 0x00129A60 File Offset: 0x00127C60
				public void Reset()
				{
					this.enumerator.Reset();
				}

				// Token: 0x060055C4 RID: 21956 RVA: 0x00129A6D File Offset: 0x00127C6D
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x04003047 RID: 12359
				private readonly ExcelReaderOpenXml.HandleExceptionsEnumerable<T> enumerable;

				// Token: 0x04003048 RID: 12360
				private readonly IEnumerator<T> enumerator;
			}
		}

		// Token: 0x02000C53 RID: 3155
		private sealed class RelationshipHidingPackage : Package
		{
			// Token: 0x060055C5 RID: 21957 RVA: 0x00129A7A File Offset: 0x00127C7A
			public RelationshipHidingPackage(IEngineHost engineHost, Stream stream)
				: base(FileAccess.Read)
			{
				this.engineHost = engineHost;
				this.package = Package.Open(stream, FileMode.Open, FileAccess.Read);
			}

			// Token: 0x060055C6 RID: 21958 RVA: 0x00129A98 File Offset: 0x00127C98
			protected override PackagePart CreatePartCore(Uri partUri, string contentType, CompressionOption compressionOption)
			{
				return this.package.CreatePart(partUri, contentType, compressionOption);
			}

			// Token: 0x060055C7 RID: 21959 RVA: 0x00129AA8 File Offset: 0x00127CA8
			protected override void DeletePartCore(Uri partUri)
			{
				this.package.DeletePart(partUri);
			}

			// Token: 0x060055C8 RID: 21960 RVA: 0x00129AB6 File Offset: 0x00127CB6
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.package != null)
				{
					((IDisposable)this.package).Dispose();
				}
			}

			// Token: 0x060055C9 RID: 21961 RVA: 0x00129ACE File Offset: 0x00127CCE
			public override bool Equals(object obj)
			{
				return obj is ExcelReaderOpenXml.RelationshipHidingPackage && this.package.Equals(((ExcelReaderOpenXml.RelationshipHidingPackage)obj).package);
			}

			// Token: 0x060055CA RID: 21962 RVA: 0x00129AF0 File Offset: 0x00127CF0
			protected override void FlushCore()
			{
				this.package.Flush();
			}

			// Token: 0x060055CB RID: 21963 RVA: 0x00129AFD File Offset: 0x00127CFD
			public override int GetHashCode()
			{
				return this.package.GetHashCode();
			}

			// Token: 0x060055CC RID: 21964 RVA: 0x00129B0A File Offset: 0x00127D0A
			protected override PackagePart GetPartCore(Uri partUri)
			{
				return new ExcelReaderOpenXml.RelationshipHidingPackage.Part(this.engineHost, this, this.package.GetPart(partUri));
			}

			// Token: 0x060055CD RID: 21965 RVA: 0x00129B24 File Offset: 0x00127D24
			protected override PackagePart[] GetPartsCore()
			{
				return (from part in this.package.GetParts()
					select new ExcelReaderOpenXml.RelationshipHidingPackage.Part(this.engineHost, this, part)).ToArray<ExcelReaderOpenXml.RelationshipHidingPackage.Part>();
			}

			// Token: 0x060055CE RID: 21966 RVA: 0x00129B54 File Offset: 0x00127D54
			public override bool PartExists(Uri partUri)
			{
				return this.package.PartExists(partUri);
			}

			// Token: 0x04003049 RID: 12361
			private const string sheetRelationshipSuffix = ".rels";

			// Token: 0x0400304A RID: 12362
			private const string relationshipsType = "http://schemas.openxmlformats.org/package/2006/relationships";

			// Token: 0x0400304B RID: 12363
			private const string hyperlinkType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";

			// Token: 0x0400304C RID: 12364
			private const string externalLinkPathType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLinkPath";

			// Token: 0x0400304D RID: 12365
			private static readonly string[] sheetRelationshipPrefixes = new string[] { "/xl/worksheets/_rels/", "/xl/externalLinks/_rels" };

			// Token: 0x0400304E RID: 12366
			private readonly IEngineHost engineHost;

			// Token: 0x0400304F RID: 12367
			private Package package;

			// Token: 0x02000C54 RID: 3156
			private class Part : PackagePart
			{
				// Token: 0x060055D1 RID: 21969 RVA: 0x00129B8E File Offset: 0x00127D8E
				public Part(IEngineHost engineHost, Package package, PackagePart part)
					: base(package, part.Uri, part.ContentType, part.CompressionOption)
				{
					this.engineHost = engineHost;
					this.part = part;
				}

				// Token: 0x060055D2 RID: 21970 RVA: 0x00129BB7 File Offset: 0x00127DB7
				public override bool Equals(object obj)
				{
					if (obj is ExcelReaderOpenXml.RelationshipHidingPackage.Part)
					{
						return this.part.Equals(((ExcelReaderOpenXml.RelationshipHidingPackage.Part)obj).part);
					}
					return base.Equals(obj);
				}

				// Token: 0x060055D3 RID: 21971 RVA: 0x00129BDF File Offset: 0x00127DDF
				protected override string GetContentTypeCore()
				{
					return this.part.ContentType;
				}

				// Token: 0x060055D4 RID: 21972 RVA: 0x00129BEC File Offset: 0x00127DEC
				public override int GetHashCode()
				{
					return this.part.GetHashCode();
				}

				// Token: 0x060055D5 RID: 21973 RVA: 0x00129BFC File Offset: 0x00127DFC
				protected override Stream GetStreamCore(FileMode mode, FileAccess access)
				{
					if (mode == FileMode.Open && access == FileAccess.Read && ExcelReaderOpenXml.RelationshipHidingPackage.sheetRelationshipPrefixes.Any((string sheetRelationshipPrefix) => base.Uri.OriginalString.StartsWith(sheetRelationshipPrefix, StringComparison.OrdinalIgnoreCase)) && base.Uri.OriginalString.EndsWith(".rels", StringComparison.OrdinalIgnoreCase))
					{
						return new ExcelReaderOpenXml.RelationshipHidingPackage.XmlTransformStream(this.engineHost, this.part.GetStream(mode, access), XmlModuleHelper.DefaultXmlReaderSettings, new XmlWriterSettings());
					}
					return this.part.GetStream(mode, access);
				}

				// Token: 0x04003050 RID: 12368
				private readonly IEngineHost engineHost;

				// Token: 0x04003051 RID: 12369
				private readonly PackagePart part;
			}

			// Token: 0x02000C55 RID: 3157
			private sealed class XmlTransformStream : Stream
			{
				// Token: 0x060055D7 RID: 21975 RVA: 0x00129C88 File Offset: 0x00127E88
				public XmlTransformStream(IEngineHost engineHost, Stream inputStream, XmlReaderSettings readerSettings, XmlWriterSettings writerSettings)
				{
					this.engineHost = engineHost;
					this.inputBuffer = new byte[4096];
					this.input = inputStream;
					this.outputBuffer = new MemoryStream();
					this.reader = XmlHelperUtility.XmlReaderCreate(inputStream, readerSettings);
					this.writer = XmlWriter.Create(this.outputBuffer, writerSettings);
				}

				// Token: 0x17001A09 RID: 6665
				// (get) Token: 0x060055D8 RID: 21976 RVA: 0x00002139 File Offset: 0x00000339
				public override bool CanRead
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17001A0A RID: 6666
				// (get) Token: 0x060055D9 RID: 21977 RVA: 0x00002105 File Offset: 0x00000305
				public override bool CanSeek
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17001A0B RID: 6667
				// (get) Token: 0x060055DA RID: 21978 RVA: 0x00002105 File Offset: 0x00000305
				public override bool CanWrite
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17001A0C RID: 6668
				// (get) Token: 0x060055DB RID: 21979 RVA: 0x000091AE File Offset: 0x000073AE
				public override long Length
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				// Token: 0x17001A0D RID: 6669
				// (get) Token: 0x060055DC RID: 21980 RVA: 0x000091AE File Offset: 0x000073AE
				// (set) Token: 0x060055DD RID: 21981 RVA: 0x000091AE File Offset: 0x000073AE
				public override long Position
				{
					get
					{
						throw new NotImplementedException();
					}
					set
					{
						throw new NotImplementedException();
					}
				}

				// Token: 0x17001A0E RID: 6670
				// (get) Token: 0x060055DE RID: 21982 RVA: 0x00129CE4 File Offset: 0x00127EE4
				private long Available
				{
					get
					{
						return this.outputBuffer.Length - this.outputBuffer.Position;
					}
				}

				// Token: 0x060055DF RID: 21983 RVA: 0x0000336E File Offset: 0x0000156E
				public override void Flush()
				{
				}

				// Token: 0x060055E0 RID: 21984 RVA: 0x00129CFD File Offset: 0x00127EFD
				public override int ReadByte()
				{
					if (this.Available == 0L)
					{
						this.FillBuffer();
					}
					return this.outputBuffer.ReadByte();
				}

				// Token: 0x060055E1 RID: 21985 RVA: 0x00129D18 File Offset: 0x00127F18
				public override int Read(byte[] buffer, int offset, int count)
				{
					if (this.Available == 0L)
					{
						this.FillBuffer();
					}
					return this.outputBuffer.Read(buffer, offset, count);
				}

				// Token: 0x060055E2 RID: 21986 RVA: 0x000091AE File Offset: 0x000073AE
				public override long Seek(long offset, SeekOrigin origin)
				{
					throw new NotImplementedException();
				}

				// Token: 0x060055E3 RID: 21987 RVA: 0x000091AE File Offset: 0x000073AE
				public override void SetLength(long value)
				{
					throw new NotImplementedException();
				}

				// Token: 0x060055E4 RID: 21988 RVA: 0x000091AE File Offset: 0x000073AE
				public override void Write(byte[] buffer, int offset, int count)
				{
					throw new NotImplementedException();
				}

				// Token: 0x060055E5 RID: 21989 RVA: 0x00129D38 File Offset: 0x00127F38
				protected override void Dispose(bool disposing)
				{
					if (disposing && this.input != null)
					{
						((IDisposable)this.reader).Dispose();
						((IDisposable)this.writer).Dispose();
						this.input.Dispose();
						this.outputBuffer.Dispose();
						this.reader = null;
						this.writer = null;
						this.input = null;
						this.outputBuffer = null;
					}
					base.Dispose(disposing);
				}

				// Token: 0x060055E6 RID: 21990 RVA: 0x00129DA0 File Offset: 0x00127FA0
				private void FillBuffer()
				{
					this.outputBuffer.Position = 0L;
					this.outputBuffer.SetLength(0L);
					bool flag = true;
					while (this.outputBuffer.Length == 0L && this.reader != null)
					{
						if (flag && !this.reader.Read())
						{
							this.writer.Flush();
							break;
						}
						Uri uri;
						if (this.reader.NodeType == XmlNodeType.Element && this.reader.LocalName == "Relationship" && (this.reader.GetAttribute("Type") == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink" || this.reader.GetAttribute("Type") == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLinkPath") && !Uri.TryCreate(this.reader.GetAttribute("Target"), UriKind.RelativeOrAbsolute, out uri))
						{
							this.reader.Skip();
							flag = false;
						}
						else
						{
							ExcelReaderOpenXml.RelationshipHidingPackage.XmlTransformStream.ShallowCopyNode(this.engineHost, this.reader, this.writer);
							flag = true;
						}
					}
					this.outputBuffer.Position = 0L;
				}

				// Token: 0x060055E7 RID: 21991 RVA: 0x00129EB4 File Offset: 0x001280B4
				private static void ShallowCopyNode(IEngineHost engineHost, XmlReader reader, XmlWriter writer)
				{
					switch (reader.NodeType)
					{
					case XmlNodeType.None:
						return;
					case XmlNodeType.Element:
						writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						writer.WriteAttributes(reader, true);
						if (reader.IsEmptyElement)
						{
							writer.WriteEndElement();
							return;
						}
						return;
					case XmlNodeType.Text:
						writer.WriteString(reader.Value);
						return;
					case XmlNodeType.CDATA:
						writer.WriteCData(reader.Value);
						return;
					case XmlNodeType.EntityReference:
						writer.WriteEntityRef(reader.Name);
						return;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.XmlDeclaration:
						writer.WriteProcessingInstruction(reader.Name, reader.Value);
						return;
					case XmlNodeType.Comment:
						writer.WriteComment(reader.Value);
						return;
					case XmlNodeType.DocumentType:
						writer.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
						return;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						writer.WriteWhitespace(reader.Value);
						return;
					case XmlNodeType.EndElement:
						writer.WriteFullEndElement();
						return;
					}
					using (IHostTrace hostTrace = TracingService.CreateTrace(engineHost, "ExcelReaderOpenXml/XmlTransportStream/ShallowCopyNode", TraceEventType.Information, null))
					{
						hostTrace.Add("NodeType", reader.NodeType.ToString(), false);
					}
				}

				// Token: 0x04003052 RID: 12370
				private const int inputChunkSize = 4096;

				// Token: 0x04003053 RID: 12371
				private readonly IEngineHost engineHost;

				// Token: 0x04003054 RID: 12372
				private byte[] inputBuffer;

				// Token: 0x04003055 RID: 12373
				private Stream input;

				// Token: 0x04003056 RID: 12374
				private MemoryStream outputBuffer;

				// Token: 0x04003057 RID: 12375
				private XmlReader reader;

				// Token: 0x04003058 RID: 12376
				private XmlWriter writer;
			}
		}

		// Token: 0x02000C56 RID: 3158
		private class ExcelXmlConvert
		{
			// Token: 0x060055E8 RID: 21992 RVA: 0x0012A01C File Offset: 0x0012821C
			public static string DecodeName(string name)
			{
				if (name == null || name.Length == 0)
				{
					return name;
				}
				StringBuilder stringBuilder = null;
				int length = name.Length;
				int num = 0;
				int num2 = name.IndexOf('_');
				if (num2 < 0)
				{
					return name;
				}
				if (ExcelReaderOpenXml.ExcelXmlConvert.c_DecodeCharPattern == null)
				{
					ExcelReaderOpenXml.ExcelXmlConvert.c_DecodeCharPattern = new Regex("_[x]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
				}
				IEnumerator enumerator = ExcelReaderOpenXml.ExcelXmlConvert.c_DecodeCharPattern.Matches(name, num2).GetEnumerator();
				int num3 = -1;
				if (enumerator != null && enumerator.MoveNext())
				{
					num3 = ((Match)enumerator.Current).Index;
				}
				for (int i = 0; i < length - 7 + 1; i++)
				{
					if (i == num3)
					{
						if (enumerator.MoveNext())
						{
							num3 = ((Match)enumerator.Current).Index;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length + 20);
						}
						stringBuilder.Append(name, num, i - num);
						if (name[i + 6] != '_')
						{
							int num4 = ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 2]) * 268435456 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 3]) * 16777216 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 4]) * 1048576 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 5]) * 65536 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 6]) * 4096 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 7]) * 256 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 8]) * 16 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 9]);
							if (num4 >= 65536)
							{
								if (num4 <= 1114111)
								{
									num = i + 7 + 4;
									char c;
									char c2;
									ExcelReaderOpenXml.XmlCharType.SplitSurrogateChar(num4, out c, out c2);
									stringBuilder.Append(c2);
									stringBuilder.Append(c);
								}
							}
							else
							{
								num = i + 7 + 4;
								stringBuilder.Append((char)num4);
							}
							i += 10;
						}
						else
						{
							num = i + 7;
							stringBuilder.Append((char)(ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 2]) * 4096 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 3]) * 256 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 4]) * 16 + ExcelReaderOpenXml.ExcelXmlConvert.FromHex(name[i + 5])));
							i += 6;
						}
					}
				}
				if (num == 0)
				{
					return name;
				}
				if (num < length)
				{
					stringBuilder.Append(name, num, length - num);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060055E9 RID: 21993 RVA: 0x0012A282 File Offset: 0x00128482
			private static int FromHex(char digit)
			{
				if (digit > '9')
				{
					return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
				}
				return (int)(digit - '0');
			}

			// Token: 0x04003059 RID: 12377
			private const int c_EncodedCharLength = 7;

			// Token: 0x0400305A RID: 12378
			private static Regex c_DecodeCharPattern;
		}

		// Token: 0x02000C57 RID: 3159
		private class XmlCharType
		{
			// Token: 0x060055EB RID: 21995 RVA: 0x0012A2A0 File Offset: 0x001284A0
			public static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
			{
				int num = combinedChar - 65536;
				lowChar = (char)(56320 + num % 1024);
				highChar = (char)(55296 + num / 1024);
			}

			// Token: 0x0400305B RID: 12379
			private const int SurHighStart = 55296;

			// Token: 0x0400305C RID: 12380
			private const int SurLowStart = 56320;
		}
	}
}
