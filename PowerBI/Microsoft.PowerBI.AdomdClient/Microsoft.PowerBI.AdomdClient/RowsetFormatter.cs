using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000033 RID: 51
	internal class RowsetFormatter : ResultsetFormatter
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000E889 File Offset: 0x0000CA89
		public RowsetFormatter()
			: this(new Dictionary<string, IDictionary<string, string>>())
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E896 File Offset: 0x0000CA96
		public RowsetFormatter(IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
		{
			this.columnNameLookupTable = columnNameLookupTable;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000E8A5 File Offset: 0x0000CAA5
		internal DataTable MainRowsetTable
		{
			get
			{
				if (this.rowsetDataset != null && this.rowsetDataset.Tables.Contains("rowsetTable"))
				{
					return this.rowsetDataset.Tables["rowsetTable"];
				}
				return null;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000E8DD File Offset: 0x0000CADD
		internal DataSet RowsetDataset
		{
			get
			{
				return this.rowsetDataset;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		public void ReadRowset(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, bool doCreate, bool schemaOnly, IDictionary<string, bool> columnsToConvertTimeFor)
		{
			if (doCreate)
			{
				this.CreateDataset(reader, inlineErrorHandling == InlineErrorHandlingType.Throw || inlineErrorHandling == InlineErrorHandlingType.StoreInErrorsCollection, inTable, columnsToConvertTimeFor);
			}
			if (!schemaOnly)
			{
				this.PopulateDataset(reader, inlineErrorHandling, (inTable == null) ? this.rowsetDataset.Tables["rowsetTable"] : inTable);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000E934 File Offset: 0x0000CB34
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

		// Token: 0x060002BC RID: 700 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		private static DataRow CreateNewRow(DataTable datatable, DataColumn parentColumn, int parentRowIndex)
		{
			DataRow dataRow = datatable.NewRow();
			if (parentColumn != null)
			{
				dataRow[parentColumn] = parentRowIndex;
			}
			return dataRow;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000EA24 File Offset: 0x0000CC24
		private static DataTable FindNestedTable(DataColumn column)
		{
			string text = column.Table.TableName + column.ColumnName;
			if (!column.Table.ChildRelations.Contains(text))
			{
				return null;
			}
			return column.Table.ChildRelations[text].ChildTable;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EA73 File Offset: 0x0000CC73
		private static bool IsNestedColumn(DataColumn column)
		{
			return column.AutoIncrement;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		private void CreateDataset(XmlReader reader, bool setColumnTypes, DataTable inTable, IDictionary<string, bool> columnsToConvertTimeFor)
		{
			if (inTable != null)
			{
				RowsetFormatter.DataTableCreator dataTableCreator = new RowsetFormatter.DataTableCreator(inTable, setColumnTypes, columnsToConvertTimeFor, this.columnNameLookupTable);
				FormattersHelpers.LoadSchema(reader, new ColumnDefinitionDelegate(dataTableCreator.ConstructDataTableColumnDelegate));
				return;
			}
			RowsetFormatter.DataSetCreator dataSetCreator = new RowsetFormatter.DataSetCreator(this.rowsetDataset, setColumnTypes, columnsToConvertTimeFor, this.columnNameLookupTable);
			FormattersHelpers.LoadSchema(reader, new ColumnDefinitionDelegate(dataSetCreator.ConstructDataSetColumnDelegate));
			this.rowsetDataset = dataSetCreator.DataSet;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000EAE2 File Offset: 0x0000CCE2
		private void PopulateDataset(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable)
		{
			this.PopulateMultiRow(reader, FormattersHelpers.RowElement, FormattersHelpers.RowElementNamespace, inTable, inlineErrorHandling, -1);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
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

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EB8C File Offset: 0x0000CD8C
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
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unknown column {0}", localName));
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

		// Token: 0x060002C3 RID: 707 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
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

		// Token: 0x04000212 RID: 530
		internal const string rowsetTable = "rowsetTable";

		// Token: 0x04000213 RID: 531
		private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;

		// Token: 0x04000214 RID: 532
		private DataSet rowsetDataset;

		// Token: 0x0200018B RID: 395
		internal class DataColumnCreator
		{
			// Token: 0x06001213 RID: 4627 RVA: 0x0003F01A File Offset: 0x0003D21A
			public DataColumnCreator(bool setColumnTypes, IDictionary<string, bool> columnsToConvertTimeFor, IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
			{
				this.setColumnTypes = setColumnTypes;
				this.columnsToConvertTimeFor = columnsToConvertTimeFor;
				this.columnNameLookupTable = columnNameLookupTable;
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x0003F038 File Offset: 0x0003D238
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

			// Token: 0x06001215 RID: 4629 RVA: 0x0003F0EC File Offset: 0x0003D2EC
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

			// Token: 0x04000C42 RID: 3138
			private readonly bool setColumnTypes;

			// Token: 0x04000C43 RID: 3139
			private readonly IDictionary<string, bool> columnsToConvertTimeFor;

			// Token: 0x04000C44 RID: 3140
			private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;
		}

		// Token: 0x0200018C RID: 396
		internal class DataSetCreator
		{
			// Token: 0x06001216 RID: 4630 RVA: 0x0003F124 File Offset: 0x0003D324
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

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06001217 RID: 4631 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
			public DataSet DataSet
			{
				get
				{
					return this.dataset;
				}
			}

			// Token: 0x06001218 RID: 4632 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
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

			// Token: 0x04000C45 RID: 3141
			private DataSet dataset;

			// Token: 0x04000C46 RID: 3142
			private const string MainTableName = "rowsetTable";

			// Token: 0x04000C47 RID: 3143
			private readonly RowsetFormatter.DataColumnCreator dataColumnCreator;
		}

		// Token: 0x0200018D RID: 397
		private class DataTableCreator
		{
			// Token: 0x06001219 RID: 4633 RVA: 0x0003F2E4 File Offset: 0x0003D4E4
			public DataTableCreator(DataTable datatable, bool setColumnTypes, IDictionary<string, bool> columnsToConvertTimeFor, IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
			{
				if (datatable == null)
				{
					datatable = new DataTable("rowsetTable");
					datatable.Locale = CultureInfo.InvariantCulture;
				}
				this.datatable = datatable;
				this.dataColumnCreator = new RowsetFormatter.DataColumnCreator(setColumnTypes, columnsToConvertTimeFor, columnNameLookupTable);
			}

			// Token: 0x0600121A RID: 4634 RVA: 0x0003F31C File Offset: 0x0003D51C
			public object ConstructDataTableColumnDelegate(int ordinal, string name, string theNamespace, string caption, Type type, bool isNested, object parent, string strColumnXsdType)
			{
				object obj = parent;
				if (parent == null)
				{
					if (isNested)
					{
						this.dataColumnCreator.AddToColumnNameLookupTable(this.datatable.TableName, name, caption);
						DataColumn dataColumn = new DataColumn(caption, typeof(int));
						dataColumn.Caption = caption;
						dataColumn.AutoIncrement = true;
						FormattersHelpers.SetColumnNamespace(dataColumn, theNamespace);
						this.datatable.Columns.Add(dataColumn);
						obj = this.datatable;
					}
					else
					{
						this.dataColumnCreator.AddColumn(this.datatable, name, theNamespace, caption, type, strColumnXsdType);
					}
				}
				return obj;
			}

			// Token: 0x04000C48 RID: 3144
			private DataTable datatable;

			// Token: 0x04000C49 RID: 3145
			private readonly RowsetFormatter.DataColumnCreator dataColumnCreator;
		}
	}
}
