using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x02000205 RID: 517
	internal class SalesforceReportsTableValue : TableValue
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0001774B File Offset: 0x0001594B
		public SalesforceReportsTableValue(IEngineHost host, SalesforceDataLoader dataLoader)
		{
			this.host = host;
			this.reportsMetadata = SalesforceCatalog.LoadReportsMetadata(host, dataLoader);
			this.dataLoader = dataLoader;
			this.values = new Dictionary<string, TableValue>();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00017779 File Offset: 0x00015979
		public override TypeValue Type
		{
			get
			{
				return SalesforceReportsTableValue.NavigationTableTypeValue;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00017780 File Offset: 0x00015980
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			Query query = new SoqlQuery(this.dataLoader, this.reportsMetadata);
			ColumnSelection columnSelection = SalesforceReportsTableValue.MakeSelection(query.Columns, Keys.New("Id", "Name", "IsDeleted"));
			query = query.SelectColumns(columnSelection);
			query = query.Sort(query.Columns, TextValue.New("Name"));
			foreach (IValueReference valueReference in query.GetRows())
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				if (!asRecord[2].AsBoolean)
				{
					yield return this.CreateTableRecord(asRecord[0], asRecord[1]);
				}
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00017790 File Offset: 0x00015990
		public override TableValue SelectRows(FunctionValue condition)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
			if (queryExpression != null && queryExpression.Kind == QueryExpressionKind.Binary)
			{
				BinaryQueryExpression binaryQueryExpression = (BinaryQueryExpression)queryExpression;
				if (binaryQueryExpression.Operator == BinaryOperator2.Equals)
				{
					ColumnAccessQueryExpression columnAccessQueryExpression = binaryQueryExpression.Left as ColumnAccessQueryExpression;
					ConstantQueryExpression constantQueryExpression = binaryQueryExpression.Right as ConstantQueryExpression;
					if (columnAccessQueryExpression == null)
					{
						columnAccessQueryExpression = binaryQueryExpression.Right as ColumnAccessQueryExpression;
						constantQueryExpression = binaryQueryExpression.Left as ConstantQueryExpression;
					}
					if (columnAccessQueryExpression != null && columnAccessQueryExpression.Column == 0 && constantQueryExpression != null && constantQueryExpression.Value.IsText)
					{
						return new ListTableValue(ListValue.New(new Value[] { this.CreateTableRecord(constantQueryExpression.Value.AsText, Value.Null) }), SalesforceReportsTableValue.NavigationTableTypeValue);
					}
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001785C File Offset: 0x00015A5C
		private Value CreateTableRecord(Value id, Value name)
		{
			return RecordValue.New(SalesforceReportsTableValue.RowKeys, delegate(int j)
			{
				switch (j)
				{
				case 0:
					return id;
				case 1:
					return TypeServices.ConvertToLimitedPreview(this.CreateTableValue(id.AsText.String));
				case 2:
					return SalesforceReportsTableValue.DataKind;
				default:
					return name;
				}
			});
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001789C File Offset: 0x00015A9C
		private static ColumnSelection MakeSelection(Keys originalKeys, Keys newKeys)
		{
			int[] array = new int[newKeys.Length];
			for (int i = 0; i < newKeys.Length; i++)
			{
				array[i] = originalKeys.IndexOfKey(newKeys[i]);
			}
			return new ColumnSelection(newKeys, array);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000178DD File Offset: 0x00015ADD
		private TableValue CreateTableValue(string reportId)
		{
			return new SalesforceReportTableValue(this.host, this.dataLoader, reportId);
		}

		// Token: 0x0400062F RID: 1583
		private const string LabelColumn = "Label";

		// Token: 0x04000630 RID: 1584
		private readonly IEngineHost host;

		// Token: 0x04000631 RID: 1585
		private readonly SalesforceObjectDetail reportsMetadata;

		// Token: 0x04000632 RID: 1586
		private readonly SalesforceDataLoader dataLoader;

		// Token: 0x04000633 RID: 1587
		private readonly Dictionary<string, TableValue> values;

		// Token: 0x04000634 RID: 1588
		private static readonly Keys RowKeys = Keys.New("Name", "Data", "Kind", "Label");

		// Token: 0x04000635 RID: 1589
		private static readonly RecordTypeValue NavigationTableRowType = RecordTypeValue.New(RecordValue.New(SalesforceReportsTableValue.RowKeys, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Table", true), false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		}));

		// Token: 0x04000636 RID: 1590
		private static readonly TableTypeValue NavigationTableTypeValue = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(SalesforceReportsTableValue.NavigationTableRowType, new TableKey[]
		{
			new TableKey(new int[1], true)
		}), TextValue.New("Label"), NavigationTableServices.DataColumnValue, NavigationTableServices.KindColumnValue);

		// Token: 0x04000637 RID: 1591
		private static readonly TextValue DataKind = TextValue.New("Table");
	}
}
