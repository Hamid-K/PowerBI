using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200004C RID: 76
	internal class RowsetFormatter : ResultsetFormatter
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00011AAD File Offset: 0x0000FCAD
		public RowsetFormatter()
			: this(new Dictionary<string, IDictionary<string, string>>())
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00011ABA File Offset: 0x0000FCBA
		public RowsetFormatter(IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
		{
			this.columnNameLookupTable = columnNameLookupTable;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011ACC File Offset: 0x0000FCCC
		internal void PopulateDataset(XmlReader reader, string rowElementName, string rowNamespace, InlineErrorHandlingType inlineErrorHandling, DataTable inTable)
		{
			DataSet dataSet = inTable.DataSet;
			if (dataSet != null)
			{
				using (IEnumerator enumerator = dataSet.Tables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						((DataTable)obj).BeginLoadData();
					}
					goto IL_004B;
				}
			}
			inTable.BeginLoadData();
			IL_004B:
			this.PopulateMultiRow(reader, rowElementName, rowNamespace, inTable, inlineErrorHandling, -1);
			if (dataSet != null)
			{
				using (IEnumerator enumerator = dataSet.Tables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						((DataTable)obj2).EndLoadData();
					}
					return;
				}
			}
			inTable.EndLoadData();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011B94 File Offset: 0x0000FD94
		private static DataRow CreateNewRow(DataTable datatable, DataColumn parentColumn, int parentRowIndex)
		{
			DataRow dataRow = datatable.NewRow();
			if (parentColumn != null)
			{
				dataRow[parentColumn] = parentRowIndex;
			}
			return dataRow;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011BBC File Offset: 0x0000FDBC
		private static DataTable FindNestedTable(DataColumn column)
		{
			string text = column.Table.TableName + column.ColumnName;
			if (!column.Table.ChildRelations.Contains(text))
			{
				return null;
			}
			return column.Table.ChildRelations[text].ChildTable;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011C0B File Offset: 0x0000FE0B
		private static bool IsNestedColumn(DataColumn column)
		{
			return column.AutoIncrement;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011C14 File Offset: 0x0000FE14
		private void PopulateMultiRow(XmlReader reader, string elementName, string elementNamespace, DataTable datatable, InlineErrorHandlingType inlineErrorHandling, int parentRowIndex)
		{
			DataColumn dataColumn = null;
			if (parentRowIndex >= 0 && datatable.Columns.Contains(datatable.TableName))
			{
				dataColumn = datatable.Columns[datatable.TableName];
			}
			while (reader.IsStartElement(elementName, elementNamespace))
			{
				if (reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					DataRow dataRow = RowsetFormatter.CreateNewRow(datatable, dataColumn, parentRowIndex);
					datatable.Rows.Add(dataRow);
				}
				else
				{
					reader.ReadStartElement();
					this.PopulateRow(reader, datatable, inlineErrorHandling, parentRowIndex, dataColumn);
					reader.ReadEndElement();
				}
			}
			FormattersHelpers.CheckException(reader);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		private void PopulateRow(XmlReader reader, DataTable datatable, InlineErrorHandlingType inlineErrorHandling, int parentRowIndex, DataColumn parentColumn)
		{
			DataRow dataRow = RowsetFormatter.CreateNewRow(datatable, parentColumn, parentRowIndex);
			while (reader.IsStartElement())
			{
				FormattersHelpers.CheckException(reader);
				if (!FormattersHelpers.IsNullContentElement(reader))
				{
					string localName = reader.LocalName;
					string captionFromColumnNameTable = this.GetCaptionFromColumnNameTable(datatable.TableName, localName);
					DataColumn dataColumn = datatable.Columns[captionFromColumnNameTable];
					if (dataColumn == null)
					{
						throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unknown column {0}", localName));
					}
					string columnNamespace = FormattersHelpers.GetColumnNamespace(dataColumn);
					if (reader.NamespaceURI == columnNamespace)
					{
						if (RowsetFormatter.IsNestedColumn(dataColumn))
						{
							DataTable dataTable = RowsetFormatter.FindNestedTable(dataColumn);
							if (dataTable != null)
							{
								this.PopulateMultiRow(reader, localName, columnNamespace, dataTable, inlineErrorHandling, (int)dataRow[dataColumn]);
							}
							else
							{
								reader.Skip();
							}
						}
						else
						{
							Type elementType = FormattersHelpers.GetElementType(reader, "http://www.w3.org/2001/XMLSchema-instance", dataColumn);
							bool flag = inlineErrorHandling == InlineErrorHandlingType.Throw;
							object obj = FormattersHelpers.ReadRowsetProperty(reader, localName, columnNamespace, elementType, flag, elementType.IsArray && FormattersHelpers.GetColumnXsdTypeName(dataColumn) != "base64Binary", FormattersHelpers.GetConvertToLocalTime(dataColumn));
							if (inlineErrorHandling == InlineErrorHandlingType.StoreInErrorsCollection)
							{
								XmlaError xmlaError = obj as XmlaError;
								if (xmlaError == null)
								{
									dataRow[captionFromColumnNameTable] = obj;
								}
								else
								{
									dataRow.SetColumnError(captionFromColumnNameTable, xmlaError.Description);
								}
							}
							else
							{
								dataRow[captionFromColumnNameTable] = obj;
							}
						}
					}
					else
					{
						reader.Skip();
					}
				}
				else
				{
					reader.Skip();
				}
			}
			datatable.Rows.Add(dataRow);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011E14 File Offset: 0x00010014
		private string GetCaptionFromColumnNameTable(string tableName, string columnName)
		{
			IDictionary<string, string> dictionary;
			string text;
			if (this.columnNameLookupTable.TryGetValue(tableName, out dictionary) && dictionary.TryGetValue(columnName, out text))
			{
				return text;
			}
			return columnName;
		}

		// Token: 0x04000264 RID: 612
		internal const string rowsetTable = "rowsetTable";

		// Token: 0x04000265 RID: 613
		private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;

		// Token: 0x02000187 RID: 391
		internal class DataColumnCreator
		{
			// Token: 0x060012BE RID: 4798 RVA: 0x00041B86 File Offset: 0x0003FD86
			public DataColumnCreator(bool setColumnTypes, IDictionary<string, bool> columnsToConvertTimeFor, IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
			{
				this.setColumnTypes = setColumnTypes;
				this.columnsToConvertTimeFor = columnsToConvertTimeFor;
				this.columnNameLookupTable = columnNameLookupTable;
			}

			// Token: 0x060012BF RID: 4799 RVA: 0x00041BA4 File Offset: 0x0003FDA4
			public void AddColumn(DataTable table, string colName, string colNamespace, string caption, Type colType, string strColumnXsdType)
			{
				this.AddToColumnNameLookupTable(table.TableName, colName, caption);
				DataColumn dataColumn;
				if (this.setColumnTypes)
				{
					dataColumn = new DataColumn(caption, colType);
				}
				else
				{
					dataColumn = new DataColumn(caption, typeof(object));
				}
				FormattersHelpers.SetColumnType(dataColumn, colType);
				FormattersHelpers.SetColumnNamespace(dataColumn, colNamespace);
				FormattersHelpers.SetColumnXsdTypeName(dataColumn, strColumnXsdType);
				if (this.columnsToConvertTimeFor != null && this.columnsToConvertTimeFor.ContainsKey(caption))
				{
					FormattersHelpers.SetConvertToLocalTime(dataColumn, true);
					if (this.setColumnTypes)
					{
						dataColumn.DateTimeMode = DataSetDateTime.Local;
					}
				}
				dataColumn.Caption = caption;
				try
				{
					table.Columns.Add(dataColumn);
				}
				catch (DuplicateNameException)
				{
				}
			}

			// Token: 0x060012C0 RID: 4800 RVA: 0x00041C58 File Offset: 0x0003FE58
			public void AddToColumnNameLookupTable(string tableName, string colName, string caption)
			{
				IDictionary<string, string> dictionary;
				if (!this.columnNameLookupTable.TryGetValue(tableName, out dictionary))
				{
					dictionary = new Dictionary<string, string>();
					this.columnNameLookupTable.Add(tableName, dictionary);
				}
				dictionary.Add(colName, caption);
			}

			// Token: 0x04000C0D RID: 3085
			private readonly bool setColumnTypes;

			// Token: 0x04000C0E RID: 3086
			private readonly IDictionary<string, bool> columnsToConvertTimeFor;

			// Token: 0x04000C0F RID: 3087
			private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;
		}

		// Token: 0x02000188 RID: 392
		internal class DataSetCreator
		{
			// Token: 0x060012C1 RID: 4801 RVA: 0x00041C90 File Offset: 0x0003FE90
			public DataSetCreator(DataSet dataset, bool setColumnTypes, IDictionary<string, bool> columnsToConvertTimeFor, IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
			{
				if (dataset == null)
				{
					dataset = new DataSet();
					dataset.Locale = CultureInfo.InvariantCulture;
				}
				this.dataset = dataset;
				if (!this.dataset.Tables.Contains("rowsetTable"))
				{
					DataTable dataTable = new DataTable("rowsetTable");
					dataTable.Locale = CultureInfo.InvariantCulture;
					this.dataset.Tables.Add(dataTable);
				}
				this.dataColumnCreator = new RowsetFormatter.DataColumnCreator(setColumnTypes, columnsToConvertTimeFor, columnNameLookupTable);
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00041D0C File Offset: 0x0003FF0C
			public DataSet DataSet
			{
				get
				{
					return this.dataset;
				}
			}

			// Token: 0x060012C3 RID: 4803 RVA: 0x00041D14 File Offset: 0x0003FF14
			public object ConstructDataSetColumnDelegate(int ordinal, string name, string theNamespace, string caption, Type type, bool isNested, object parent, string strColumnXsdType)
			{
				object obj = parent;
				DataTable dataTable;
				if (parent == null)
				{
					dataTable = this.dataset.Tables["rowsetTable"];
				}
				else
				{
					dataTable = parent as DataTable;
				}
				if (isNested)
				{
					this.dataColumnCreator.AddToColumnNameLookupTable(dataTable.TableName, name, caption);
					DataColumn dataColumn = new DataColumn(caption, typeof(int));
					dataColumn.Caption = caption;
					dataColumn.AutoIncrement = true;
					FormattersHelpers.SetColumnNamespace(dataColumn, theNamespace);
					dataTable.Columns.Add(dataColumn);
					string text = dataTable.TableName + caption;
					DataTable dataTable2;
					if (!this.dataset.Tables.Contains(text))
					{
						dataTable2 = new DataTable(text);
						dataTable2.Locale = CultureInfo.InvariantCulture;
						this.dataset.Tables.Add(dataTable2);
					}
					else
					{
						dataTable2 = this.dataset.Tables[text];
					}
					DataColumn dataColumn2 = new DataColumn(text, typeof(int));
					dataColumn2.Caption = text;
					dataTable2.Columns.Add(dataColumn2);
					this.dataset.Relations.Add(text, dataColumn, dataColumn2, false);
					obj = dataTable2;
				}
				else
				{
					this.dataColumnCreator.AddColumn(dataTable, name, theNamespace, caption, type, strColumnXsdType);
				}
				return obj;
			}

			// Token: 0x04000C10 RID: 3088
			private DataSet dataset;

			// Token: 0x04000C11 RID: 3089
			private const string MainTableName = "rowsetTable";

			// Token: 0x04000C12 RID: 3090
			private readonly RowsetFormatter.DataColumnCreator dataColumnCreator;
		}
	}
}
