using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x02000020 RID: 32
	internal sealed class ResultTableLookup
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000049EC File Offset: 0x00002BEC
		internal ResultTableLookup()
		{
			this._dataSets = new List<DataSet>();
			this._resultTableInfos = new List<ResultTableLookupInfo>();
			this._dataTransforms = new List<DataTransform>();
			this._tableIds = new List<string>();
			this._fieldIdGenerator = new UniqueStringGenerator(StringComparer.Ordinal);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004A3B File Offset: 0x00002C3B
		internal List<ResultTableLookupInfo> ResultTableInfos
		{
			get
			{
				return this._resultTableInfos;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004A43 File Offset: 0x00002C43
		internal ResultTableMetadata ResultTableMetadata
		{
			get
			{
				return this._resultTableMetadata;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004A4C File Offset: 0x00002C4C
		internal void AddDataSet(DataSet dataSet)
		{
			this._dataSets.Add(dataSet);
			for (int i = 0; i < dataSet.ResultTables.Count; i++)
			{
				this._resultTableInfos.Add(ResultTableLookupInfo.ForDataSet(this._dataSets.Count - 1, i));
				this._tableIds.Add(dataSet.ResultTables[i].Id);
				foreach (Field field in dataSet.ResultTables[i].Fields)
				{
					this._fieldIdGenerator.RegisterString(field.Id);
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B14 File Offset: 0x00002D14
		internal void AddDataTransform(DataTransform dataTransform)
		{
			this._dataTransforms.Add(dataTransform);
			this._resultTableInfos.Add(ResultTableLookupInfo.ForDataTransform(this._dataTransforms.Count - 1));
			this._tableIds.Add(dataTransform.Output.Table.Id);
			foreach (Field field in dataTransform.Output.Table.Fields)
			{
				this._fieldIdGenerator.RegisterString(field.Id);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004BBC File Offset: 0x00002DBC
		internal int GetResultTableIndex(string tableId)
		{
			int num;
			this.GetResultTableInfo(tableId, out num);
			return num;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004BD4 File Offset: 0x00002DD4
		internal ResultTableLookupInfo GetResultTableInfo(string tableId, out int globalTableIndex)
		{
			for (int i = 0; i < this._resultTableInfos.Count; i++)
			{
				if (this._tableIds[i] == tableId)
				{
					globalTableIndex = i;
					return this._resultTableInfos[i];
				}
			}
			Contract.RetailFail("Could not resolve ResultTable {0}", tableId);
			globalTableIndex = -1;
			return null;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004C2C File Offset: 0x00002E2C
		internal ResultTable GetResultTable(string tableId)
		{
			int num;
			ResultTableLookupInfo resultTableInfo = this.GetResultTableInfo(tableId, out num);
			return this.GetResultTable(resultTableInfo);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004C4C File Offset: 0x00002E4C
		private ResultTable GetResultTable(ResultTableLookupInfo tableInfo)
		{
			if (tableInfo.IsDataSetTable)
			{
				return this._dataSets[tableInfo.DataSetIndex].ResultTables[tableInfo.LocalTableIndex];
			}
			return this._dataTransforms[tableInfo.DataTransformIndex].Output.Table;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal int GetFieldIndex(string fieldId, int tableIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[tableIndex];
			ResultTable resultTable;
			if (resultTableLookupInfo.IsDataSetTable)
			{
				resultTable = this._dataSets[resultTableLookupInfo.DataSetIndex].ResultTables[resultTableLookupInfo.LocalTableIndex];
			}
			else
			{
				resultTable = this._dataTransforms[resultTableLookupInfo.DataTransformIndex].Output.Table;
			}
			for (int i = 0; i < resultTable.Fields.Count; i++)
			{
				if (resultTable.Fields[i].Id == fieldId)
				{
					return i;
				}
			}
			Contract.RetailFail("Could not resolve Field {0} from table {1}", fieldId, tableIndex);
			return -1;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004D48 File Offset: 0x00002F48
		internal void GenerateResultTableMetadata()
		{
			FieldValueExpressionNode[] array = new FieldValueExpressionNode[this._resultTableInfos.Count];
			string text = this._fieldIdGenerator.MakeUniqueString("RowIndexField");
			for (int i = 0; i < this._resultTableInfos.Count; i++)
			{
				ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[i];
				ResultTable resultTable = this.GetResultTable(resultTableLookupInfo);
				FieldValueExpressionNode fieldValueExpressionNode;
				if (resultTableLookupInfo.IsDataSetTable)
				{
					fieldValueExpressionNode = ResultTableLookup.CreateRowIndexField(resultTable, i, text);
				}
				else
				{
					fieldValueExpressionNode = this.CreateRowIndexFieldForTransform(text, i, resultTable, resultTableLookupInfo.DataTransformIndex);
				}
				array[i] = fieldValueExpressionNode;
			}
			this._resultTableMetadata = new ResultTableMetadata(array);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004DDC File Offset: 0x00002FDC
		private FieldValueExpressionNode CreateRowIndexFieldForTransform(string rowIndexFieldId, int tableIndex, ResultTable transformOutputTable, int transformIndex)
		{
			this._dataTransforms[transformIndex].Input.Schema.Columns.Insert(0, new DataTransformColumn(rowIndexFieldId, null));
			int num = 0;
			Field field = new Field(rowIndexFieldId, rowIndexFieldId, null, false, null);
			transformOutputTable.Fields.Insert(num, field);
			return new FieldValueExpressionNode(rowIndexFieldId, num, transformOutputTable.Id, tableIndex);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004E3C File Offset: 0x0000303C
		internal static FieldValueExpressionNode CreateRowIndexField(ResultTable resultTable, int tableIndex, string rowIndexFieldId)
		{
			int num = 0;
			Field field = new Field(rowIndexFieldId, null, null, true, null);
			resultTable.Fields.Insert(num, field);
			return new FieldValueExpressionNode(rowIndexFieldId, num, resultTable.Id, tableIndex);
		}

		// Token: 0x0400009A RID: 154
		private const string RowIndexFieldId = "RowIndexField";

		// Token: 0x0400009B RID: 155
		private readonly List<DataSet> _dataSets;

		// Token: 0x0400009C RID: 156
		private readonly List<DataTransform> _dataTransforms;

		// Token: 0x0400009D RID: 157
		private readonly List<ResultTableLookupInfo> _resultTableInfos;

		// Token: 0x0400009E RID: 158
		private readonly List<string> _tableIds;

		// Token: 0x0400009F RID: 159
		private readonly UniqueStringGenerator _fieldIdGenerator;

		// Token: 0x040000A0 RID: 160
		private ResultTableMetadata _resultTableMetadata;
	}
}
