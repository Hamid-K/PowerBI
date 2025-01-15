using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B36 RID: 2870
	internal sealed class FuzzyDataTableCreator
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x0010AAC4 File Offset: 0x00108CC4
		public static DataTable CreateFromRecord(string dataTableName, RecordValue record, int[] keys, out List<RecordValue> records)
		{
			DataTable dataTable = FuzzyDataTableCreator.PopulateColumnMetadata(dataTableName, keys);
			records = new List<RecordValue>();
			records.Add(record);
			DataRow dataRow = FuzzyDataTableCreator.PopulateRow(0, record, dataTable, keys);
			dataTable.Rows.Add(dataRow);
			return dataTable;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0010AB00 File Offset: 0x00108D00
		public static DataTable CreateFromRecords(string dataTableName, IEnumerable<IValueReference> rows, int[] keys, out List<RecordValue> records)
		{
			DataTable dataTable = FuzzyDataTableCreator.PopulateColumnMetadata(dataTableName, keys);
			records = new List<RecordValue>();
			int num = 0;
			foreach (IValueReference valueReference in rows)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				records.Add(asRecord);
				DataRow dataRow = FuzzyDataTableCreator.PopulateRow(num++, asRecord, dataTable, keys);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0010AB84 File Offset: 0x00108D84
		public static DataTable CreateTransformationDataTableFromTableValue(TableValue transformationTable)
		{
			DataTable dataTable = new DataTable("Transformation")
			{
				Locale = CultureInfo.InvariantCulture
			};
			if (transformationTable != null)
			{
				Keys columns = transformationTable.Columns;
				for (int i = 0; i < columns.Length; i++)
				{
					dataTable.Columns.Add(columns[i], typeof(string));
				}
				new List<RecordValue>();
				foreach (IValueReference valueReference in transformationTable.Query.GetRows())
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					DataRow dataRow = dataTable.NewRow();
					for (int j = 0; j < columns.Length; j++)
					{
						dataRow[j] = FuzzyDataTableCreator.GetTextFrom(asRecord[columns[j]]);
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			if (dataTable.Columns.Count == 0)
			{
				dataTable.Columns.Add("From", typeof(string));
				dataTable.Columns.Add("To", typeof(string));
			}
			return dataTable;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0010ACBC File Offset: 0x00108EBC
		public static IEnumerator<IValueReference> ToRecordsEnumerator(DataTable outputTable, FuzzyJoinParameters parameters, List<RecordValue> inputRecords, List<RecordValue> referenceRecords)
		{
			int columnCount = parameters.LeftKeyColumns.Length;
			int resultCount = outputTable.Rows.Count;
			int num3;
			for (int i = 0; i < resultCount; i = num3)
			{
				RecordValue recordValue = null;
				if (outputTable.Rows[i][0] != DBNull.Value)
				{
					int num = (int)outputTable.Rows[i][0];
					recordValue = inputRecords[num];
				}
				RecordValue recordValue2 = null;
				if (outputTable.Rows[i][columnCount + 1] != DBNull.Value)
				{
					int num2 = (int)outputTable.Rows[i][columnCount + 1];
					recordValue2 = referenceRecords[num2];
				}
				RecordValue recordValue3 = JoinAlgorithm.GetRow(recordValue, recordValue2, parameters.JoinKeys, parameters.JoinColumns);
				if (parameters.JoinOptions.SimilarityColumnName != null)
				{
					object obj = outputTable.Rows[i][(columnCount + 1) * 2];
					Value value = ((obj != DBNull.Value) ? NumberValue.New(FuzzyUtils.TruncateSimilarity((double)obj)) : Value.Null);
					recordValue3 = Library.Record.AddField.Invoke(recordValue3, TextValue.New(parameters.JoinOptions.SimilarityColumnName), value).AsRecord;
				}
				yield return recordValue3;
				num3 = i + 1;
			}
			yield break;
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0010ACE0 File Offset: 0x00108EE0
		private static string GetTextFrom(IValue cellValue)
		{
			if (cellValue.IsText)
			{
				return cellValue.AsString;
			}
			return null;
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0010ACF4 File Offset: 0x00108EF4
		private static DataTable PopulateColumnMetadata(string dataTableName, int[] keys)
		{
			DataTable dataTable = new DataTable(dataTableName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			dataTable.Columns.Add("RecordId", typeof(int));
			foreach (int num in keys)
			{
				dataTable.Columns.Add(dataTableName + num.ToString(), typeof(string));
			}
			return dataTable;
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x0010AD6C File Offset: 0x00108F6C
		private static DataRow PopulateRow(int recordId, RecordValue record, DataTable table, int[] keys)
		{
			DataRow dataRow = table.NewRow();
			dataRow[0] = recordId;
			for (int i = 0; i < keys.Length; i++)
			{
				string textFrom = FuzzyDataTableCreator.GetTextFrom(record[keys[i]]);
				dataRow[i + 1] = textFrom;
			}
			return dataRow;
		}
	}
}
