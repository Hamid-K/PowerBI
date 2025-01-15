using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001DB RID: 475
	internal class SalesforceCatalogTableValue : TableValue
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x00012F3B File Offset: 0x0001113B
		public SalesforceCatalogTableValue(IEngineHost host, SalesforceDataLoader dataLoader, OptionsRecord optionsMap)
		{
			this.host = host;
			this.dataLoader = dataLoader;
			this.values = new Dictionary<string, TableValue>();
			this.optionsMap = optionsMap;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00012F63 File Offset: 0x00011163
		public override TypeValue Type
		{
			get
			{
				return SalesforceCatalogTableValue.NavigationTableTypeValue;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00012F6A File Offset: 0x0001116A
		private SalesforceCatalog Catalog
		{
			get
			{
				if (this.catalog == null)
				{
					this.catalog = SalesforceCatalog.LoadCatalog(this.host, this.dataLoader);
				}
				return this.catalog;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00012F91 File Offset: 0x00011191
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			foreach (SalesforceObjectHeader salesforceObjectHeader in this.Catalog.GetEnumerator())
			{
				if (!salesforceObjectHeader.Deprecated)
				{
					yield return this.CreateTableRecord(salesforceObjectHeader);
				}
			}
			IEnumerator<SalesforceObjectHeader> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00012FA0 File Offset: 0x000111A0
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
						string @string = constantQueryExpression.Value.AsText.String;
						SalesforceObjectHeader salesforceObjectHeader = new SalesforceObjectHeader(this.dataLoader, @string).LoadDetail(this.host, this.dataLoader);
						return new ListTableValue(ListValue.New(new Value[] { this.CreateTableRecord(salesforceObjectHeader) }), SalesforceCatalogTableValue.NavigationTableTypeValue);
					}
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00013090 File Offset: 0x00011290
		private Value CreateTableRecord(SalesforceObjectHeader header)
		{
			return RecordValue.New(SalesforceCatalogTableValue.RowKeys, delegate(int j)
			{
				switch (j)
				{
				case 0:
					return TextValue.New(header.Name);
				case 1:
					return TypeServices.ConvertToLimitedPreview(this.CreateTableValue(header));
				case 2:
					return SalesforceCatalogTableValue.DataKind;
				case 3:
					return TextValue.New(header.Label);
				default:
					return LogicalValue.New(!header.Queryable);
				}
			});
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000130C8 File Offset: 0x000112C8
		private TableValue CreateTableValue(SalesforceObjectHeader header)
		{
			TableValue tableValue;
			if (!this.values.TryGetValue(header.Name, out tableValue))
			{
				SalesforceObjectDetail salesforceObjectDetail = header.LoadDetail(this.host, this.dataLoader);
				SoqlQuery soqlQuery = new SoqlQuery(this.dataLoader, header.Detail);
				tableValue = (this.optionsMap.GetBool("CreateNavigationProperties", false) ? salesforceObjectDetail.AddNavigationProperties(this.host, soqlQuery, this.Catalog, this.dataLoader, new Func<SalesforceObjectHeader, TableValue>(this.CreateTableValue)) : new QueryTableValue(new OptimizableQuery(soqlQuery)));
				this.values[header.Name] = tableValue;
			}
			string text = this.dataLoader.CreateCacheKey(new string[] { "Catalog", header.Name });
			tableValue = tableValue.ReplaceRelationshipIdentity(text);
			return tableValue;
		}

		// Token: 0x04000560 RID: 1376
		private const string LabelColumn = "Label";

		// Token: 0x04000561 RID: 1377
		private readonly IEngineHost host;

		// Token: 0x04000562 RID: 1378
		private readonly SalesforceDataLoader dataLoader;

		// Token: 0x04000563 RID: 1379
		private readonly Dictionary<string, TableValue> values;

		// Token: 0x04000564 RID: 1380
		private readonly OptionsRecord optionsMap;

		// Token: 0x04000565 RID: 1381
		private SalesforceCatalog catalog;

		// Token: 0x04000566 RID: 1382
		private static readonly Keys RowKeys = Keys.New(new string[] { "Name", "Data", "Kind", "Label", "Hidden" });

		// Token: 0x04000567 RID: 1383
		private static readonly RecordTypeValue NavigationTableRowType = RecordTypeValue.New(RecordValue.New(SalesforceCatalogTableValue.RowKeys, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Table", true), false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Logical, false)
		}));

		// Token: 0x04000568 RID: 1384
		private static readonly TableTypeValue NavigationTableTypeValue = NavigationTableServices.AddNavigationTableMetadataWithKindHidden(TableTypeValue.New(SalesforceCatalogTableValue.NavigationTableRowType, new TableKey[]
		{
			new TableKey(new int[1], true)
		}), TextValue.New("Label"), NavigationTableServices.DataColumnValue, NavigationTableServices.KindColumnValue, NavigationTableServices.HiddenColumnValue);

		// Token: 0x04000569 RID: 1385
		private static readonly TextValue DataKind = TextValue.New("Table");
	}
}
