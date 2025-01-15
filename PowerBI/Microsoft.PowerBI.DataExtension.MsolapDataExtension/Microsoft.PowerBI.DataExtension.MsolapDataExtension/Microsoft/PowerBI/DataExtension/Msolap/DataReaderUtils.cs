using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.Query.Contracts;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000F RID: 15
	internal static class DataReaderUtils
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002B4C File Offset: 0x00000D4C
		public static DataSet ReadToDataSet(this DataReader reader, string tableName, IPrivateInformationService piiService)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add(tableName);
			DataSet dataSet2;
			using (DataReaderUtils.RowsetContext rowsetContext = new DataReaderUtils.RowsetContext(reader, dataTable))
			{
				if (!reader.MoveNext())
				{
					QueryContract.RetailFail("Could not find a row containing the schema.");
				}
				DataReaderUtils.ReadToDataTable(rowsetContext, piiService);
				reader.NextResult();
				dataSet2 = dataSet;
			}
			return dataSet2;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private static void ReadToDataTable(DataReaderUtils.RowsetContext tableContext, IPrivateInformationService piiService)
		{
			DataReaderUtils.InitializeDataSetSchema(tableContext, piiService);
			DataReaderUtils.ReadValues(tableContext, piiService);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private static void InitializeDataSetSchema(DataReaderUtils.RowsetContext rowsetContext, IPrivateInformationService piiService)
		{
			DataReader reader = rowsetContext.Reader;
			DataTable table = rowsetContext.Table;
			DataSet dataSet = table.DataSet;
			try
			{
				uint columnCount = reader.GetColumnCount();
				for (uint num = 0U; num < columnCount; num += 1U)
				{
					string columnName = reader.GetColumnName(num);
					Type type = reader.GetColumnType(num);
					bool flag = type == typeof(ChapterHandle);
					type = (flag ? typeof(ulong) : type);
					table.Columns.Add(columnName, type);
					if (flag)
					{
						DataTable dataTable = dataSet.Tables.Add(table.TableName + columnName);
						dataTable.Columns.Add(columnName, type);
						dataSet.Relations.Add(new DataRelation(dataTable.TableName, table.Columns[columnName], dataTable.Columns[columnName], true));
						DataReader childReader;
						try
						{
							childReader = reader.GetChildReader(num);
						}
						catch (MsolapWrapperException ex)
						{
							throw DataExtensionErrorUtils.CreateDataExtensionException(ex, piiService, "Failed retrieve child DataReader '{0}'.", new object[] { dataTable.TableName });
						}
						DataReaderUtils.RowsetContext rowsetContext2 = new DataReaderUtils.RowsetContext(childReader, dataTable);
						rowsetContext.ChildRowsets.Add(num, rowsetContext2);
						DataReaderUtils.InitializeDataSetSchema(rowsetContext2, piiService);
					}
				}
			}
			catch (MsolapWrapperException ex2)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex2, piiService, "Failed intialize DataSet schema for table '{0}'.", new object[] { table.TableName });
			}
			catch (DataException)
			{
				throw;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D64 File Offset: 0x00000F64
		private static void ReadValues(DataReaderUtils.RowsetContext rowsetContext, IPrivateInformationService piiService)
		{
			try
			{
				DataReader reader = rowsetContext.Reader;
				uint num = ((rowsetContext.CurrentChapter != null) ? 1U : 0U);
				uint columnCount = reader.GetColumnCount();
				object[] array = new object[columnCount + num];
				if (rowsetContext.CurrentChapter != null)
				{
					array[0] = rowsetContext.CurrentChapter.Key;
				}
				do
				{
					for (uint num2 = 0U; num2 < columnCount; num2 += 1U)
					{
						object obj;
						try
						{
							obj = reader.GetValue(num2);
						}
						catch (MsolapWrapperException ex)
						{
							throw DataExtensionErrorUtils.CreateDataExtensionException(ex, piiService, "Exception getting the value for table '{0}', column '{1}'.", new object[]
							{
								rowsetContext.Table.TableName,
								rowsetContext.Table.Columns[(int)(num2 + num)].ColumnName
							});
						}
						ChapterHandle chapterHandle = obj as ChapterHandle;
						DataReaderUtils.RowsetContext rowsetContext2;
						if (rowsetContext.ChildRowsets.TryGetValue(num2, out rowsetContext2))
						{
							rowsetContext2.CurrentChapter = chapterHandle;
						}
						if (chapterHandle != null)
						{
							obj = chapterHandle.Key;
						}
						array[(int)(num2 + num)] = obj;
					}
					try
					{
						rowsetContext.Table.Rows.Add(array);
					}
					catch (DataException)
					{
						throw;
					}
					foreach (KeyValuePair<uint, DataReaderUtils.RowsetContext> keyValuePair in rowsetContext.ChildRowsets)
					{
						if (array[(int)(keyValuePair.Key + num)] != null && keyValuePair.Value.SynchronizeChapter())
						{
							DataReaderUtils.ReadValues(keyValuePair.Value, piiService);
						}
					}
				}
				while (reader.MoveNext());
			}
			catch (MsolapWrapperException ex2)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex2, piiService, "Exception reading the values for table '{0}'.", new object[] { rowsetContext.Table.TableName });
			}
		}

		// Token: 0x02000012 RID: 18
		private class RowsetContext : IDisposable
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000055 RID: 85 RVA: 0x0000348E File Offset: 0x0000168E
			// (set) Token: 0x06000056 RID: 86 RVA: 0x00003496 File Offset: 0x00001696
			internal ChapterHandle CurrentChapter { get; set; }

			// Token: 0x06000057 RID: 87 RVA: 0x0000349F File Offset: 0x0000169F
			internal RowsetContext(DataReader dataReader, DataTable table)
			{
				this.Table = table;
				this.Reader = dataReader;
				this.ChildRowsets = new Dictionary<uint, DataReaderUtils.RowsetContext>();
				this.CurrentChapter = null;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x000034C7 File Offset: 0x000016C7
			internal bool SynchronizeChapter()
			{
				return this.CurrentChapter != null && this.Reader.SynchronizeChapter(this.CurrentChapter);
			}

			// Token: 0x06000059 RID: 89 RVA: 0x000034E4 File Offset: 0x000016E4
			public void Dispose()
			{
				foreach (DataReaderUtils.RowsetContext rowsetContext in this.ChildRowsets.Values)
				{
					rowsetContext.Dispose();
				}
				this.Reader.Dispose();
			}

			// Token: 0x04000055 RID: 85
			internal readonly DataTable Table;

			// Token: 0x04000056 RID: 86
			internal readonly DataReader Reader;

			// Token: 0x04000057 RID: 87
			internal readonly Dictionary<uint, DataReaderUtils.RowsetContext> ChildRowsets;
		}
	}
}
