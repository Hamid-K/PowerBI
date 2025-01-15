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
		// Token: 0x060002C3 RID: 707 RVA: 0x0000EBB9 File Offset: 0x0000CDB9
		public RowsetFormatter()
			: this(new Dictionary<string, IDictionary<string, string>>())
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EBC6 File Offset: 0x0000CDC6
		public RowsetFormatter(IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
		{
			this.columnNameLookupTable = columnNameLookupTable;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000EBD5 File Offset: 0x0000CDD5
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		internal DataSet RowsetDataset
		{
			get
			{
				return this.rowsetDataset;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EC18 File Offset: 0x0000CE18
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

		// Token: 0x060002C8 RID: 712 RVA: 0x0000EC64 File Offset: 0x0000CE64
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

		// Token: 0x060002C9 RID: 713 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		private static DataRow CreateNewRow(DataTable datatable, DataColumn parentColumn, int parentRowIndex)
		{
			DataRow dataRow = datatable.NewRow();
			if (parentColumn != null)
			{
				dataRow[parentColumn] = parentRowIndex;
			}
			return dataRow;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000ED54 File Offset: 0x0000CF54
		private static DataTable FindNestedTable(DataColumn column)
		{
			string text = column.Table.TableName + column.ColumnName;
			if (!column.Table.ChildRelations.Contains(text))
			{
				return null;
			}
			return column.Table.ChildRelations[text].ChildTable;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000EDA3 File Offset: 0x0000CFA3
		private static bool IsNestedColumn(DataColumn column)
		{
			return column.AutoIncrement;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000EDAC File Offset: 0x0000CFAC
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

		// Token: 0x060002CD RID: 717 RVA: 0x0000EE12 File Offset: 0x0000D012
		private void PopulateDataset(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable)
		{
			this.PopulateMultiRow(reader, FormattersHelpers.RowElement, FormattersHelpers.RowElementNamespace, inTable, inlineErrorHandling, -1);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000EE28 File Offset: 0x0000D028
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

		// Token: 0x060002CF RID: 719 RVA: 0x0000EEBC File Offset: 0x0000D0BC
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

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F028 File Offset: 0x0000D228
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

		// Token: 0x0400021F RID: 543
		internal const string rowsetTable = "rowsetTable";

		// Token: 0x04000220 RID: 544
		private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;

		// Token: 0x04000221 RID: 545
		private DataSet rowsetDataset;

		// Token: 0x0200018B RID: 395
		internal class DataColumnCreator
		{
			// Token: 0x06001220 RID: 4640 RVA: 0x0003F54A File Offset: 0x0003D74A
			public DataColumnCreator(bool setColumnTypes, IDictionary<string, bool> columnsToConvertTimeFor, IDictionary<string, IDictionary<string, string>> columnNameLookupTable)
			{
				this.setColumnTypes = setColumnTypes;
				this.columnsToConvertTimeFor = columnsToConvertTimeFor;
				this.columnNameLookupTable = columnNameLookupTable;
			}

			// Token: 0x06001221 RID: 4641 RVA: 0x0003F568 File Offset: 0x0003D768
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

			// Token: 0x06001222 RID: 4642 RVA: 0x0003F61C File Offset: 0x0003D81C
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

			// Token: 0x04000C53 RID: 3155
			private readonly bool setColumnTypes;

			// Token: 0x04000C54 RID: 3156
			private readonly IDictionary<string, bool> columnsToConvertTimeFor;

			// Token: 0x04000C55 RID: 3157
			private readonly IDictionary<string, IDictionary<string, string>> columnNameLookupTable;
		}

		// Token: 0x0200018C RID: 396
		internal class DataSetCreator
		{
			// Token: 0x06001223 RID: 4643 RVA: 0x0003F654 File Offset: 0x0003D854
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

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x06001224 RID: 4644 RVA: 0x0003F6D0 File Offset: 0x0003D8D0
			public DataSet DataSet
			{
				get
				{
					return this.dataset;
				}
			}

			// Token: 0x06001225 RID: 4645 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
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

			// Token: 0x04000C56 RID: 3158
			private DataSet dataset;

			// Token: 0x04000C57 RID: 3159
			private const string MainTableName = "rowsetTable";

			// Token: 0x04000C58 RID: 3160
			private readonly RowsetFormatter.DataColumnCreator dataColumnCreator;
		}

		// Token: 0x0200018D RID: 397
		private class DataTableCreator
		{
			// Token: 0x06001226 RID: 4646 RVA: 0x0003F814 File Offset: 0x0003DA14
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

			// Token: 0x06001227 RID: 4647 RVA: 0x0003F84C File Offset: 0x0003DA4C
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

			// Token: 0x04000C59 RID: 3161
			private DataTable datatable;

			// Token: 0x04000C5A RID: 3162
			private readonly RowsetFormatter.DataColumnCreator dataColumnCreator;
		}
	}
}
