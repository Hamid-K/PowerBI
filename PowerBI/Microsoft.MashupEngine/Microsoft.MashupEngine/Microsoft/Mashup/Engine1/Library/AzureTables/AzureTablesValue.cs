using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Web;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Library.OData.V3;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureTables
{
	// Token: 0x02000EBD RID: 3773
	internal static class AzureTablesValue
	{
		// Token: 0x02000EBE RID: 3774
		public sealed class AzureTablesCatalogTableValue : TableValue
		{
			// Token: 0x06006433 RID: 25651 RVA: 0x00156C53 File Offset: 0x00154E53
			public AzureTablesCatalogTableValue(IEngineHost host, TextValue accountUrl, HttpResource resource, TimeSpan? timeoutValue = null)
			{
				this.host = host;
				this.accountUrl = accountUrl;
				this.resource = resource;
				this.timeoutValue = timeoutValue;
			}

			// Token: 0x17001D1C RID: 7452
			// (get) Token: 0x06006434 RID: 25652 RVA: 0x00156C78 File Offset: 0x00154E78
			public override TypeValue Type
			{
				get
				{
					return AzureTablesValue.AzureTablesCatalogTableValue.type;
				}
			}

			// Token: 0x06006435 RID: 25653 RVA: 0x00156C7F File Offset: 0x00154E7F
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				if (this.navigationListValue == null)
				{
					this.navigationListValue = this.GetCatalogListValue();
				}
				return this.navigationListValue.GetEnumerator();
			}

			// Token: 0x06006436 RID: 25654 RVA: 0x00156CA0 File Offset: 0x00154EA0
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
							return new ListTableValue(ListValue.New(new Value[] { this.CreateTableRecord(constantQueryExpression.Value.AsText) }), this.Type.AsTableType);
						}
					}
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06006437 RID: 25655 RVA: 0x00156D6C File Offset: 0x00154F6C
			private ListValue GetCatalogListValue()
			{
				Uri uri = new Uri(AzureTablesService.GetHttpUri(this.accountUrl, TextValue.New("Tables")).String);
				ResourceCredentialCollection resourceCredentialCollection;
				HttpServices.VerifyPermissionAndGetCredentials(this.host, this.resource.Resource, out resourceCredentialCollection);
				List<IValueReference> list = new List<IValueReference>();
				try
				{
					Uri uri2 = new Uri(this.accountUrl.String);
					ODataUserSettings odataUserSettings = new ODataUserSettings();
					if (this.timeoutValue != null)
					{
						odataUserSettings.Timeout = this.timeoutValue.Value;
					}
					ODataEnvironment odataEnvironment = ODataEnvironment.Create(new ODataServiceDocument(new ODataWorkspace(), uri2, null), new Dictionary<ODataSchemaItem, TypeValue>(), AzureTablesHelper.CreateHeaders(), this.resource, this.host, null, RecordValue.Empty, new ODataSettings(this.host, this.resource, uri2)
					{
						ConcurrentRequestsLimit = 0
					}, odataUserSettings);
					do
					{
						List<IValueReference> list2 = ODataMessageReaderValueConverters.CreateSerialValues(odataEnvironment, TypeValue.List, false, uri, out uri, new Func<RequestInfo, Uri>(this.GetCatalogNextPageUri));
						if (list2 != null)
						{
							list.AddRange(list2);
						}
					}
					while (uri != null);
				}
				catch (WebException ex)
				{
					HttpServices.ThrowIfAuthorizationError(this.host, ex, this.resource.Resource);
				}
				catch (FormatException ex2)
				{
					throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource.Resource, ex2.Message, null, null);
				}
				catch (FoldingFailureException ex3)
				{
					throw ex3.InnerException;
				}
				int count = list.Count;
				Value[] array = new Value[count];
				for (int i = 0; i < count; i++)
				{
					TextValue textValue = TextValue.New(list[i].Value["TableName"].AsString);
					RecordValue recordValue = this.CreateTableRecord(textValue);
					array[i] = recordValue;
				}
				return ListValue.New(array);
			}

			// Token: 0x06006438 RID: 25656 RVA: 0x00156F38 File Offset: 0x00155138
			private RecordValue CreateTableRecord(TextValue tableName)
			{
				return RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
				{
					tableName,
					new QueryTableValue(new OptimizableQuery(new AzureTablesValue.AzureTablesQuery(this.host, this.accountUrl, this.resource, tableName, null, null, null, null, this.timeoutValue)))
				});
			}

			// Token: 0x06006439 RID: 25657 RVA: 0x00156F90 File Offset: 0x00155190
			private Uri GetCatalogNextPageUri(RequestInfo requestInfo)
			{
				if (requestInfo.Result == null)
				{
					return null;
				}
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(new UriBuilder(requestInfo.Uri).Query);
				string header = requestInfo.Response.GetHeader("x-ms-continuation-NextTableName");
				if (string.IsNullOrEmpty(header))
				{
					return null;
				}
				if (nameValueCollection["NextTableName"] == null)
				{
					nameValueCollection.Add("NextTableName", header);
				}
				else
				{
					nameValueCollection["NextTableName"] = header;
				}
				return UriHelper.AddQueryRecord(new Uri(requestInfo.Uri.GetLeftPart(UriPartial.Path)), nameValueCollection);
			}

			// Token: 0x040036B6 RID: 14006
			private static readonly TypeValue type = NavigationTableServices.AddDataColumnIsLeafMetadata(NavigationTableServices.DefaultTypeValue);

			// Token: 0x040036B7 RID: 14007
			private readonly IEngineHost host;

			// Token: 0x040036B8 RID: 14008
			private readonly TextValue accountUrl;

			// Token: 0x040036B9 RID: 14009
			private readonly HttpResource resource;

			// Token: 0x040036BA RID: 14010
			private readonly TimeSpan? timeoutValue;

			// Token: 0x040036BB RID: 14011
			private ListValue navigationListValue;
		}

		// Token: 0x02000EBF RID: 3775
		private sealed class AzureTablesQuery : DataSourceQuery
		{
			// Token: 0x0600643B RID: 25659 RVA: 0x00157028 File Offset: 0x00155228
			public AzureTablesQuery(IEngineHost host, TextValue accountUrl, HttpResource resource, TextValue tableName, AzureTablesValue.ExpandCondition expandCondition, long? top, QueryToken filters, ColumnSelection columnSelection, TimeSpan? timeoutValue)
			{
				this.host = host;
				this.accountUrl = accountUrl;
				this.resource = resource;
				this.tableName = tableName;
				this.expandCondition = expandCondition;
				this.top = top;
				this.filters = filters;
				this.columnSelection = columnSelection ?? new ColumnSelection(AzureTablesConstants.ColumnList.Keys);
				this.timeoutValue = timeoutValue;
			}

			// Token: 0x17001D1D RID: 7453
			// (get) Token: 0x0600643C RID: 25660 RVA: 0x00157093 File Offset: 0x00155293
			public override Keys Columns
			{
				get
				{
					return this.CachedTableValue.Columns;
				}
			}

			// Token: 0x17001D1E RID: 7454
			// (get) Token: 0x0600643D RID: 25661 RVA: 0x001570A0 File Offset: 0x001552A0
			public override IEngineHost EngineHost
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x17001D1F RID: 7455
			// (get) Token: 0x0600643E RID: 25662 RVA: 0x001570A8 File Offset: 0x001552A8
			private TableValue CachedTableValue
			{
				get
				{
					if (this.cachedTableValue == null)
					{
						this.cachedTableValue = this.GetTableValue();
					}
					return this.cachedTableValue;
				}
			}

			// Token: 0x0600643F RID: 25663 RVA: 0x001570C4 File Offset: 0x001552C4
			public override IEnumerable<IValueReference> GetRows()
			{
				return this.CachedTableValue;
			}

			// Token: 0x17001D20 RID: 7456
			// (get) Token: 0x06006440 RID: 25664 RVA: 0x001570CC File Offset: 0x001552CC
			public override IQueryDomain QueryDomain
			{
				get
				{
					return AzureTablesValue.AzureTablesQuery.AzureTablesQueryDomain.Instance;
				}
			}

			// Token: 0x06006441 RID: 25665 RVA: 0x001570D3 File Offset: 0x001552D3
			public override TypeValue GetColumnType(int column)
			{
				return this.CachedTableValue.Type.AsTableType.ItemType.Fields[column]["Type"].AsType;
			}

			// Token: 0x06006442 RID: 25666 RVA: 0x00157104 File Offset: 0x00155304
			private TableValue GetTableValue()
			{
				TableValue tableValue = new AzureTablesValue.AzureTablesContentTableValue(this.accountUrl, this.resource, this.host, this.tableName, this.top, this.filters, this.columnSelection, this.expandCondition, this.timeoutValue);
				if (this.expandCondition == null)
				{
					tableValue = tableValue.SelectColumns(this.columnSelection);
				}
				else
				{
					tableValue = tableValue.ExpandRecordColumn(3, this.expandCondition.fieldsToProject, this.expandCondition.newColumns).SelectColumns(this.columnSelection);
				}
				return tableValue;
			}

			// Token: 0x06006443 RID: 25667 RVA: 0x00157190 File Offset: 0x00155390
			public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
			{
				if (this.expandCondition != null)
				{
					return base.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query);
				}
				AzureTablesValue.ExpandCondition expandCondition = new AzureTablesValue.ExpandCondition(columnToExpand, fieldsToProject, newColumns);
				ColumnSelection columnSelection = this.UpdateColumnSelection(expandCondition);
				query = new AzureTablesValue.AzureTablesQuery(this.host, this.accountUrl, this.resource, this.tableName, expandCondition, this.top, this.filters, columnSelection, this.timeoutValue);
				return true;
			}

			// Token: 0x06006444 RID: 25668 RVA: 0x001571F8 File Offset: 0x001553F8
			private ColumnSelection UpdateColumnSelection(AzureTablesValue.ExpandCondition newExpandCondition)
			{
				int length = this.columnSelection.Keys.Length;
				int length2 = newExpandCondition.newColumns.Length;
				int num = length + length2 - 1;
				string[] array = new string[num];
				int[] array2 = new int[num];
				int num2 = 0;
				if (num2 < num)
				{
					int num3 = 0;
					for (int i = 0; i < length; i++)
					{
						if (this.columnSelection.GetColumn(i) != 3)
						{
							array[num2] = this.columnSelection.Keys[i];
							array2[num2] = this.columnSelection.GetColumn(i);
							num2++;
						}
						else
						{
							num3 = i;
							num2 += length2;
						}
					}
					num2 = num3;
					for (int j = 0; j < newExpandCondition.fieldsToProject.Length; j++)
					{
						array[num2] = newExpandCondition.newColumns[j];
						array2[num2] = j + 3;
						num2++;
					}
				}
				return new ColumnSelection(Keys.New(array), array2);
			}

			// Token: 0x06006445 RID: 25669 RVA: 0x001572F4 File Offset: 0x001554F4
			public override Query Take(RowCount count)
			{
				long num = count.Value;
				if (this.top != null)
				{
					num = Math.Min(this.top.Value, count.Value);
				}
				return new AzureTablesValue.AzureTablesQuery(this.host, this.accountUrl, this.resource, this.tableName, this.expandCondition, new long?(num), this.filters, this.columnSelection, this.timeoutValue);
			}

			// Token: 0x06006446 RID: 25670 RVA: 0x0015736C File Offset: 0x0015556C
			public override Query SelectColumns(ColumnSelection columnSelection)
			{
				ColumnSelection columnSelection2 = this.columnSelection.SelectColumns(columnSelection);
				return new AzureTablesValue.AzureTablesQuery(this.host, this.accountUrl, this.resource, this.tableName, this.expandCondition, this.top, this.filters, columnSelection2, this.timeoutValue);
			}

			// Token: 0x06006447 RID: 25671 RVA: 0x001573BC File Offset: 0x001555BC
			public override Query SelectRows(FunctionValue condition)
			{
				Query query;
				if (this.top == null && this.TrySelectRows(condition, out query))
				{
					return query;
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06006448 RID: 25672 RVA: 0x001573EC File Offset: 0x001555EC
			private bool TrySelectRows(FunctionValue condition, out Query newQuery)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition);
				QueryToken queryToken;
				if (this.TryGetQueryToken(queryExpression, out queryToken) && (queryToken is BinaryOperatorQueryToken || (queryToken is LiteralQueryToken && (queryToken as LiteralQueryToken).Value is bool)))
				{
					if (this.filters != null)
					{
						queryToken = new BinaryOperatorQueryToken(BinaryOperatorKind.And, this.filters, queryToken);
					}
					AzureTablesValue.AzureTablesQuery azureTablesQuery = new AzureTablesValue.AzureTablesQuery(this.host, this.accountUrl, this.resource, this.tableName, this.expandCondition, this.top, queryToken, this.columnSelection, this.timeoutValue);
					try
					{
						azureTablesQuery.CachedTableValue.GetEnumerator().Dispose();
						newQuery = azureTablesQuery;
						return true;
					}
					catch (FoldingFailureException)
					{
						newQuery = null;
						return false;
					}
				}
				newQuery = null;
				return false;
			}

			// Token: 0x06006449 RID: 25673 RVA: 0x001574B8 File Offset: 0x001556B8
			private bool TryGetQueryToken(QueryExpression query, out QueryToken queryToken)
			{
				if (query is ConstantQueryExpression)
				{
					return this.TryGetQueryTokenFromConstantQueryExpression(query as ConstantQueryExpression, out queryToken);
				}
				if (query is BinaryQueryExpression)
				{
					return this.TryGetQueryTokenFromBinaryQueryExpression(query as BinaryQueryExpression, out queryToken);
				}
				if (query is ColumnAccessQueryExpression)
				{
					return this.TryGetQueryTokenFromColumnAccessQueryExpression(query as ColumnAccessQueryExpression, out queryToken);
				}
				queryToken = null;
				return false;
			}

			// Token: 0x0600644A RID: 25674 RVA: 0x0015750C File Offset: 0x0015570C
			private bool TryGetQueryTokenFromBinaryQueryExpression(BinaryQueryExpression binary, out QueryToken queryToken)
			{
				queryToken = null;
				if (!AzureTablesHelper.IsSupportedOperator(binary.Operator))
				{
					return false;
				}
				QueryToken queryToken2;
				QueryToken queryToken3;
				if (this.TryGetQueryToken(binary.Left, out queryToken2) && this.TryGetQueryToken(binary.Right, out queryToken3))
				{
					if (queryToken2 is BinaryOperatorQueryToken && queryToken3 is BinaryOperatorQueryToken)
					{
						queryToken = new BinaryOperatorQueryToken(AzureTablesHelper.GetBinaryOperatorKind(binary.Operator), queryToken2, queryToken3);
						return true;
					}
					if (queryToken2 is PropertyAccessQueryToken)
					{
						if (queryToken3 is LiteralQueryToken)
						{
							BinaryOperatorKind binaryOperatorKind = AzureTablesHelper.GetBinaryOperatorKind(binary.Operator);
							return this.TryGetQueryTokens(queryToken2 as PropertyAccessQueryToken, queryToken3 as LiteralQueryToken, binaryOperatorKind, out queryToken);
						}
					}
					else if (queryToken2 is LiteralQueryToken && queryToken3 is PropertyAccessQueryToken)
					{
						BinaryOperatorKind binaryOperatorKind2 = AzureTablesHelper.GetBinaryOperatorKind(AzureTablesHelper.ReverseOperator(binary.Operator));
						return this.TryGetQueryTokens(queryToken3 as PropertyAccessQueryToken, queryToken2 as LiteralQueryToken, binaryOperatorKind2, out queryToken);
					}
				}
				return false;
			}

			// Token: 0x0600644B RID: 25675 RVA: 0x001575E0 File Offset: 0x001557E0
			private bool TryGetQueryTokens(PropertyAccessQueryToken propertyAccessQueryToken, LiteralQueryToken literalQueryToken, BinaryOperatorKind operatorKind, out QueryToken queryToken)
			{
				if (((propertyAccessQueryToken.Name == "PartitionKey" || propertyAccessQueryToken.Name == "RowKey") && literalQueryToken.Value is string) || (propertyAccessQueryToken.Name == "Timestamp" && literalQueryToken.Value is DateTime))
				{
					queryToken = new BinaryOperatorQueryToken(operatorKind, propertyAccessQueryToken, literalQueryToken);
					return true;
				}
				queryToken = null;
				return false;
			}

			// Token: 0x0600644C RID: 25676 RVA: 0x00157650 File Offset: 0x00155850
			private bool TryGetQueryTokenFromConstantQueryExpression(ConstantQueryExpression constant, out QueryToken queryToken)
			{
				if (constant.Value is LogicalValue)
				{
					queryToken = new LiteralQueryToken(constant.Value.AsBoolean);
					return true;
				}
				if (constant.Value.IsText)
				{
					queryToken = new LiteralQueryToken(constant.Value.AsString);
					return true;
				}
				if (constant.Value.IsDateTime)
				{
					queryToken = new LiteralQueryToken(constant.Value.AsDateTime.AsClrDateTime);
					return true;
				}
				queryToken = null;
				return false;
			}

			// Token: 0x0600644D RID: 25677 RVA: 0x001576D4 File Offset: 0x001558D4
			private bool TryGetQueryTokenFromColumnAccessQueryExpression(ColumnAccessQueryExpression column, out QueryToken queryToken)
			{
				int column2 = this.columnSelection.GetColumn(column.Column);
				if (column2 < 3)
				{
					queryToken = new PropertyAccessQueryToken(AzureTablesConstants.ColumnList.Keys[column2], null);
					return true;
				}
				queryToken = null;
				return false;
			}

			// Token: 0x040036BC RID: 14012
			private readonly IEngineHost host;

			// Token: 0x040036BD RID: 14013
			private readonly TextValue accountUrl;

			// Token: 0x040036BE RID: 14014
			private readonly HttpResource resource;

			// Token: 0x040036BF RID: 14015
			private readonly TextValue tableName;

			// Token: 0x040036C0 RID: 14016
			private TableValue cachedTableValue;

			// Token: 0x040036C1 RID: 14017
			private readonly long? top;

			// Token: 0x040036C2 RID: 14018
			private readonly QueryToken filters;

			// Token: 0x040036C3 RID: 14019
			private readonly ColumnSelection columnSelection;

			// Token: 0x040036C4 RID: 14020
			private readonly AzureTablesValue.ExpandCondition expandCondition;

			// Token: 0x040036C5 RID: 14021
			private readonly TimeSpan? timeoutValue;

			// Token: 0x02000EC0 RID: 3776
			private class AzureTablesQueryDomain : IQueryDomain
			{
				// Token: 0x0600644E RID: 25678 RVA: 0x00157715 File Offset: 0x00155915
				public bool IsCompatibleWith(IQueryDomain domain)
				{
					return domain == AzureTablesValue.AzureTablesQuery.AzureTablesQueryDomain.Instance;
				}

				// Token: 0x17001D21 RID: 7457
				// (get) Token: 0x0600644F RID: 25679 RVA: 0x00002105 File Offset: 0x00000305
				public bool CanIndex
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06006450 RID: 25680 RVA: 0x0015771F File Offset: 0x0015591F
				public Query Optimize(Query query)
				{
					return new OptimizingQueryVisitor().Optimize(query);
				}

				// Token: 0x040036C6 RID: 14022
				public static readonly IQueryDomain Instance = new AzureTablesValue.AzureTablesQuery.AzureTablesQueryDomain();
			}
		}

		// Token: 0x02000EC1 RID: 3777
		private sealed class AzureTablesContentTableValue : TableValue
		{
			// Token: 0x06006453 RID: 25683 RVA: 0x00157738 File Offset: 0x00155938
			public AzureTablesContentTableValue(TextValue accountUrl, HttpResource resource, IEngineHost host, TextValue tableName, long? top, QueryToken filters, ColumnSelection columnSelection, AzureTablesValue.ExpandCondition expandCondition, TimeSpan? timeoutValue)
			{
				this.accountUrl = accountUrl;
				this.resource = resource;
				this.host = host;
				this.tableName = tableName;
				this.top = top;
				this.filters = filters;
				this.columnSelection = columnSelection;
				this.expandCondition = expandCondition;
				this.timeoutValue = timeoutValue;
			}

			// Token: 0x17001D22 RID: 7458
			// (get) Token: 0x06006454 RID: 25684 RVA: 0x00157790 File Offset: 0x00155990
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = TableTypeValue.New(RecordTypeValue.New(AzureTablesConstants.ColumnList));
					}
					return this.type;
				}
			}

			// Token: 0x06006455 RID: 25685 RVA: 0x001577B8 File Offset: 0x001559B8
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new AzureTablesValue.AzureTablesContentTableValue.AzureTablesListEnumerator(this.accountUrl, this.resource, this.host, this.tableName, this.top, this.filters, this.columnSelection, this.expandCondition, this.timeoutValue);
			}

			// Token: 0x040036C7 RID: 14023
			private readonly TextValue accountUrl;

			// Token: 0x040036C8 RID: 14024
			private readonly HttpResource resource;

			// Token: 0x040036C9 RID: 14025
			private readonly IEngineHost host;

			// Token: 0x040036CA RID: 14026
			private readonly TextValue tableName;

			// Token: 0x040036CB RID: 14027
			private readonly long? top;

			// Token: 0x040036CC RID: 14028
			private readonly QueryToken filters;

			// Token: 0x040036CD RID: 14029
			private readonly ColumnSelection columnSelection;

			// Token: 0x040036CE RID: 14030
			private readonly AzureTablesValue.ExpandCondition expandCondition;

			// Token: 0x040036CF RID: 14031
			private TypeValue type;

			// Token: 0x040036D0 RID: 14032
			private readonly TimeSpan? timeoutValue;

			// Token: 0x02000EC2 RID: 3778
			private sealed class AzureTablesListEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06006456 RID: 25686 RVA: 0x00157800 File Offset: 0x00155A00
				public AzureTablesListEnumerator(TextValue accountUrl, HttpResource resource, IEngineHost host, TextValue tableName, long? top, QueryToken filters, ColumnSelection columnSelection, AzureTablesValue.ExpandCondition expandCondition, TimeSpan? timeoutValue)
				{
					this.accountUrl = accountUrl;
					this.resource = resource;
					this.host = host;
					this.tableName = tableName;
					this.top = top;
					this.take = 0L;
					this.filters = filters;
					this.columnSelection = columnSelection;
					this.expandCondition = expandCondition;
					Uri uri = new Uri(this.accountUrl.String);
					ODataUserSettings odataUserSettings = new ODataUserSettings();
					if (timeoutValue != null)
					{
						odataUserSettings.Timeout = timeoutValue.Value;
					}
					this.environment = ODataEnvironment.Create(new ODataServiceDocument(new ODataWorkspace(), uri, null), new Dictionary<ODataSchemaItem, TypeValue>(), AzureTablesHelper.CreateHeaders(), this.resource, this.host, null, RecordValue.Empty, new ODataSettings(this.host, this.resource, uri)
					{
						ConcurrentRequestsLimit = 0
					}, odataUserSettings);
					this.startPageUri = this.GetStartPageUri(out this.expandAndSelectedColumns);
					this.listEnumerator = new Microsoft.Mashup.Engine1.Library.OData.V3.ODataValue.ODataListValue(this.environment, TypeValue.List, this.startPageUri, true, new Func<RequestInfo, Uri>(this.GetNextPageUri), false).GetEnumerator();
				}

				// Token: 0x06006457 RID: 25687 RVA: 0x0015791C File Offset: 0x00155B1C
				private Uri GetStartPageUri(out Keys expandAndSelectedColumns)
				{
					Uri uri = new Uri(AzureTablesService.GetHttpUri(this.accountUrl, this.tableName).String);
					Uri uri2 = new Uri(this.accountUrl.String);
					QueryDescriptorQueryToken queryDescriptorQueryToken = ODataUri.ParseODataUri(this.host, this.resource.Resource, uri, uri2);
					long? num;
					int? num3;
					if (this.top != null)
					{
						num = this.top;
						long num2 = 1000L;
						if ((num.GetValueOrDefault() > num2) & (num != null))
						{
							num3 = new int?(1000);
							goto IL_00B0;
						}
					}
					num = this.top;
					num3 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
					IL_00B0:
					SelectQueryToken selectQueryToken = this.GetSelectQueryToken(out expandAndSelectedColumns);
					QueryDescriptorQueryToken queryDescriptorQueryToken2 = new QueryDescriptorQueryToken(queryDescriptorQueryToken.Path, this.filters, new List<OrderByQueryToken>(), selectQueryToken, null, null, num3, null, null, new List<QueryOptionQueryToken>());
					Uri uri3;
					try
					{
						uri3 = MashupODataUriBuilder.CreateUri(uri, queryDescriptorQueryToken2);
					}
					catch (UriFormatException)
					{
						throw new FoldingFailureException(ODataErrors.QueryTooLong(this.host, uri, this.resource.Kind));
					}
					return uri3;
				}

				// Token: 0x06006458 RID: 25688 RVA: 0x00157A54 File Offset: 0x00155C54
				private SelectQueryToken GetSelectQueryToken(out Keys expandAndSelectedColumns)
				{
					int length = this.columnSelection.Keys.Length;
					QueryToken[] array = new QueryToken[length];
					List<string> list = new List<string>();
					for (int i = 0; i < length; i++)
					{
						int column = this.columnSelection.GetColumn(i);
						if (column < 3)
						{
							array[i] = new PropertyAccessQueryToken(AzureTablesConstants.ColumnList.Keys[column], null);
						}
						else
						{
							if (column == 3 && this.expandCondition == null)
							{
								expandAndSelectedColumns = null;
								return new SelectQueryToken(new QueryToken[0]);
							}
							string text = this.expandCondition.fieldsToProject[column - 3];
							list.Add(text);
							array[i] = new PropertyAccessQueryToken(text, null);
						}
					}
					expandAndSelectedColumns = Keys.New(list.ToArray());
					return new SelectQueryToken(array);
				}

				// Token: 0x17001D23 RID: 7459
				// (get) Token: 0x06006459 RID: 25689 RVA: 0x00157B12 File Offset: 0x00155D12
				public IValueReference Current
				{
					get
					{
						return this.currentValue;
					}
				}

				// Token: 0x17001D24 RID: 7460
				// (get) Token: 0x0600645A RID: 25690 RVA: 0x00157B1A File Offset: 0x00155D1A
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600645B RID: 25691 RVA: 0x00157B22 File Offset: 0x00155D22
				public void Dispose()
				{
					this.currentValue = null;
					if (this.listEnumerator != null)
					{
						this.listEnumerator.Dispose();
						this.listEnumerator = null;
					}
				}

				// Token: 0x17001D25 RID: 7461
				// (get) Token: 0x0600645C RID: 25692 RVA: 0x00157B48 File Offset: 0x00155D48
				private int PredefinedColumnsOccurrence
				{
					get
					{
						if (this.predefinedColumnsOccurrence == -1)
						{
							for (int i = 0; i < this.IndexOfPredefinedColumns.Length; i++)
							{
								if (this.IndexOfPredefinedColumns[i] != -1)
								{
									this.predefinedColumnsOccurrence++;
								}
							}
						}
						return this.predefinedColumnsOccurrence;
					}
				}

				// Token: 0x17001D26 RID: 7462
				// (get) Token: 0x0600645D RID: 25693 RVA: 0x00157B90 File Offset: 0x00155D90
				private int[] IndexOfPredefinedColumns
				{
					get
					{
						if (this.indexOfPredefinedColumns == null)
						{
							this.predefinedColumnsOccurrence = 0;
							RecordValue asRecord = this.listEnumerator.Current.Value.AsRecord;
							this.indexOfPredefinedColumns = new int[3];
							for (int i = 0; i < 3; i++)
							{
								this.indexOfPredefinedColumns[i] = asRecord.Keys.IndexOfKey(AzureTablesConstants.PredefinedColumns[i]);
								if (this.indexOfPredefinedColumns[i] != -1)
								{
									this.predefinedColumnsOccurrence++;
								}
							}
						}
						return this.indexOfPredefinedColumns;
					}
				}

				// Token: 0x0600645E RID: 25694 RVA: 0x00157C14 File Offset: 0x00155E14
				public bool MoveNext()
				{
					if (this.top != null)
					{
						long num = this.take;
						long? num2 = this.top;
						if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
						{
							return false;
						}
					}
					if (this.listEnumerator.MoveNext())
					{
						RecordValue asRecord = this.listEnumerator.Current.Value.AsRecord;
						Value[] array = new Value[4];
						for (int i = 0; i < 3; i++)
						{
							int num3 = this.IndexOfPredefinedColumns[i];
							array[i] = ((num3 >= 0) ? asRecord[num3] : Value.Null);
						}
						if (this.expandAndSelectedColumns == null)
						{
							int length = asRecord.Keys.Length;
							string[] array2 = new string[length - this.PredefinedColumnsOccurrence];
							Value[] array3 = new Value[array2.Length];
							int j = 0;
							int num4 = 0;
							while (j < length)
							{
								if (j != this.IndexOfPredefinedColumns[0] && j != this.IndexOfPredefinedColumns[1] && j != this.IndexOfPredefinedColumns[2])
								{
									array2[num4] = asRecord.Keys[j];
									array3[num4] = asRecord[j];
									num4++;
								}
								j++;
							}
							array[3] = RecordValue.New(Keys.New(array2), array3);
						}
						else if (this.expandAndSelectedColumns.Length == 0)
						{
							array[3] = RecordValue.Empty;
						}
						else
						{
							Value[] array4 = new Value[this.expandAndSelectedColumns.Length];
							for (int k = 0; k < array4.Length; k++)
							{
								array4[k] = asRecord[this.expandAndSelectedColumns[k]];
							}
							array[3] = RecordValue.New(this.expandAndSelectedColumns, array4);
						}
						this.currentValue = RecordValue.New(AzureTablesConstants.ColumnList.Keys, array);
						this.take += 1L;
						return true;
					}
					return false;
				}

				// Token: 0x0600645F RID: 25695 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x06006460 RID: 25696 RVA: 0x00157DDC File Offset: 0x00155FDC
				private Uri GetNextPageUri(RequestInfo requestInfo)
				{
					if (requestInfo.Result == null)
					{
						return null;
					}
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(new UriBuilder(requestInfo.Uri).Query);
					if (nameValueCollection["$top"] != null)
					{
						nameValueCollection["$top"] = Math.Min(this.top.Value, 1000L).ToString(CultureInfo.InvariantCulture);
					}
					string header = requestInfo.Response.GetHeader("x-ms-continuation-NextPartitionKey");
					string header2 = requestInfo.Response.GetHeader("x-ms-continuation-NextRowKey");
					if (string.IsNullOrEmpty(header) && string.IsNullOrEmpty(header2))
					{
						return null;
					}
					if (string.IsNullOrEmpty(header))
					{
						nameValueCollection.Remove("NextPartitionKey");
					}
					else
					{
						nameValueCollection.Set("NextPartitionKey", header);
					}
					if (string.IsNullOrEmpty(header2))
					{
						nameValueCollection.Remove("NextRowKey");
					}
					else
					{
						nameValueCollection.Set("NextRowKey", header2);
					}
					return UriHelper.AddQueryRecord(new Uri(requestInfo.Uri.GetLeftPart(UriPartial.Path)), nameValueCollection);
				}

				// Token: 0x040036D1 RID: 14033
				private RecordValue currentValue;

				// Token: 0x040036D2 RID: 14034
				private IEnumerator<IValueReference> listEnumerator;

				// Token: 0x040036D3 RID: 14035
				private readonly TextValue accountUrl;

				// Token: 0x040036D4 RID: 14036
				private readonly HttpResource resource;

				// Token: 0x040036D5 RID: 14037
				private readonly IEngineHost host;

				// Token: 0x040036D6 RID: 14038
				private readonly TextValue tableName;

				// Token: 0x040036D7 RID: 14039
				private long? top;

				// Token: 0x040036D8 RID: 14040
				private long take;

				// Token: 0x040036D9 RID: 14041
				private readonly QueryToken filters;

				// Token: 0x040036DA RID: 14042
				private readonly ColumnSelection columnSelection;

				// Token: 0x040036DB RID: 14043
				private readonly AzureTablesValue.ExpandCondition expandCondition;

				// Token: 0x040036DC RID: 14044
				private readonly Uri startPageUri;

				// Token: 0x040036DD RID: 14045
				private readonly ODataEnvironment environment;

				// Token: 0x040036DE RID: 14046
				private int predefinedColumnsOccurrence = -1;

				// Token: 0x040036DF RID: 14047
				private int[] indexOfPredefinedColumns;

				// Token: 0x040036E0 RID: 14048
				private Keys expandAndSelectedColumns;
			}
		}

		// Token: 0x02000EC3 RID: 3779
		private sealed class ExpandCondition
		{
			// Token: 0x06006461 RID: 25697 RVA: 0x00157ED1 File Offset: 0x001560D1
			public ExpandCondition(int columnToExpand, Keys fieldsToProject, Keys newColumns)
			{
				this.columnToExpand = columnToExpand;
				this.fieldsToProject = fieldsToProject;
				this.newColumns = newColumns;
			}

			// Token: 0x040036E1 RID: 14049
			public int columnToExpand;

			// Token: 0x040036E2 RID: 14050
			public Keys fieldsToProject;

			// Token: 0x040036E3 RID: 14051
			public Keys newColumns;
		}
	}
}
