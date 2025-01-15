using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001F19 RID: 7961
	internal class NestingExpandRecordColumnQuery : ExpandRecordColumnQuery, INestedOperationQuery
	{
		// Token: 0x06010C29 RID: 68649 RVA: 0x0039BADF File Offset: 0x00399CDF
		private NestingExpandRecordColumnQuery(int columnToExpand, Keys fieldsToProject, Keys newColumns, TypeValue[] projectedTypes, RecordTypeValue expandedColumnType, INestedOperationQuery innerQuery)
			: base(columnToExpand, fieldsToProject, newColumns, projectedTypes, innerQuery.AsQuery)
		{
			this.innerNestedOperationQuery = innerQuery;
			this.expandedColumnType = expandedColumnType;
		}

		// Token: 0x06010C2A RID: 68650 RVA: 0x0039BB04 File Offset: 0x00399D04
		public static ExpandRecordColumnQuery New(int columnToExpand, Keys fieldsToProject, Keys newColumns, INestedOperationQuery innerQuery)
		{
			TypeValue columnType = innerQuery.AsQuery.GetColumnType(columnToExpand);
			if (!columnType.IsRecordType)
			{
				return new ExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, null, innerQuery.AsQuery);
			}
			RecordTypeValue recordTypeValue = columnType.AsRecordType;
			TypeValue[] array = new TypeValue[fieldsToProject.Length];
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			int num = 0;
			foreach (string text in fieldsToProject)
			{
				int num2 = recordTypeValue.FieldKeys.IndexOfKey(text);
				if (num2 == -1)
				{
					return new ExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, null, innerQuery.AsQuery);
				}
				bool flag;
				array[num++] = recordTypeValue.GetFieldType(num2, out flag);
				columnSelectionBuilder.Add(text, num2);
			}
			ColumnSelection columnSelection;
			ColumnSelection columnSelection2;
			columnSelectionBuilder.ToColumnSelection().Split(recordTypeValue.FieldKeys, out columnSelection, out columnSelection2);
			NestedColumnSelection[] array2 = new NestedColumnSelection[innerQuery.AsQuery.Columns.Length];
			array2[columnToExpand] = new NestedColumnSelection(columnSelection, null);
			NestedColumnSelection nestedColumnSelection = new NestedColumnSelection(new ColumnSelection(innerQuery.AsQuery.Columns), array2);
			INestedOperationQuery nestedOperationQuery;
			if (innerQuery.TrySelectColumns(nestedColumnSelection, out nestedOperationQuery))
			{
				innerQuery = nestedOperationQuery;
				recordTypeValue = nestedOperationQuery.AsQuery.GetColumnType(columnToExpand).AsRecordType;
			}
			return new NestingExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, array, recordTypeValue, innerQuery);
		}

		// Token: 0x17002C6F RID: 11375
		// (get) Token: 0x06010C2B RID: 68651 RVA: 0x00004FAE File Offset: 0x000031AE
		public Query AsQuery
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06010C2C RID: 68652 RVA: 0x0039BC60 File Offset: 0x00399E60
		public bool TrySelectColumns(NestedColumnSelection columnSelection, out INestedOperationQuery query)
		{
			NestedColumnSelection nestedColumnSelection;
			Keys keys;
			Keys keys2;
			int innerSelection = NestingExpandRecordColumnQuery.GetInnerSelection(base.InnerQuery.Columns, this.expandedColumnType.FieldKeys, columnSelection, base.ColumnToExpand, base.FieldsToProject, base.NewColumns, out nestedColumnSelection, out keys, out keys2);
			INestedOperationQuery nestedOperationQuery;
			if (innerSelection == -1 || !this.innerNestedOperationQuery.TrySelectColumns(nestedColumnSelection, out nestedOperationQuery))
			{
				query = null;
				return false;
			}
			query = new NestingExpandRecordColumnQuery(innerSelection, keys, keys2, null, nestedOperationQuery.AsQuery.GetColumnType(innerSelection).AsRecordType, nestedOperationQuery);
			return true;
		}

		// Token: 0x06010C2D RID: 68653 RVA: 0x0039BAB0 File Offset: 0x00399CB0
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			query = NestingExpandRecordColumnQuery.New(columnToExpand, fieldsToProject, newColumns, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x06010C2E RID: 68654 RVA: 0x0039BAC9 File Offset: 0x00399CC9
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			query = NestingExpandListColumnQuery.New(columnIndex, singleOrDefault, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x06010C2F RID: 68655 RVA: 0x0039BCDC File Offset: 0x00399EDC
		public static int GetInnerSelection(Keys innerColumns, Keys nestedColumns, NestedColumnSelection columnSelection, int columnToExpand, Keys fieldsToProject, Keys newColumns, out NestedColumnSelection innerSelection, out Keys newFieldsToProject, out Keys newNewColumns)
		{
			NestedColumnSelectionBuilder nestedColumnSelectionBuilder = default(NestedColumnSelectionBuilder);
			ColumnSelection.SelectMap selectMap = columnSelection.ColumnSelection.CreateSelectMap(innerColumns.Length + fieldsToProject.Length - 1);
			for (int i = 0; i < columnToExpand; i++)
			{
				int num = selectMap.MapColumn(i);
				if (num != -1)
				{
					nestedColumnSelectionBuilder.Add(innerColumns[i], i, columnSelection.GetNestedColumnSelection(num));
				}
			}
			int count = nestedColumnSelectionBuilder.Count;
			NestedColumnSelectionBuilder nestedColumnSelectionBuilder2 = default(NestedColumnSelectionBuilder);
			KeysBuilder keysBuilder = new KeysBuilder(fieldsToProject.Length);
			KeysBuilder keysBuilder2 = new KeysBuilder(newColumns.Length);
			for (int j = 0; j < fieldsToProject.Length; j++)
			{
				int num2 = selectMap.MapColumn(columnToExpand + j);
				if (num2 != -1)
				{
					string text = fieldsToProject[j];
					int num3 = nestedColumns.IndexOfKey(text);
					if (num3 == -1)
					{
						innerSelection = NestedColumnSelection.All;
						newFieldsToProject = null;
						newNewColumns = null;
						return -1;
					}
					nestedColumnSelectionBuilder2.Add(text, num3, columnSelection.GetNestedColumnSelection(num2));
					keysBuilder.Add(text);
					keysBuilder2.Add(newColumns[j]);
				}
			}
			nestedColumnSelectionBuilder.Add(innerColumns[columnToExpand], columnToExpand, nestedColumnSelectionBuilder2.ToNestedColumnSelection());
			for (int k = columnToExpand + 1; k < innerColumns.Length; k++)
			{
				int num4 = selectMap.MapColumn(k + fieldsToProject.Length - 1);
				if (num4 != -1)
				{
					nestedColumnSelectionBuilder.Add(innerColumns[k], k, columnSelection.GetNestedColumnSelection(num4));
				}
			}
			innerSelection = nestedColumnSelectionBuilder.ToNestedColumnSelection();
			newFieldsToProject = keysBuilder.ToKeys();
			newNewColumns = keysBuilder2.ToKeys();
			return count;
		}

		// Token: 0x04006464 RID: 25700
		private readonly INestedOperationQuery innerNestedOperationQuery;

		// Token: 0x04006465 RID: 25701
		private readonly RecordTypeValue expandedColumnType;
	}
}
