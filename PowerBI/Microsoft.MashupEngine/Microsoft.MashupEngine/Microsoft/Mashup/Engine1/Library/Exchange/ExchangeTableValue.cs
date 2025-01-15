using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BFA RID: 3066
	internal sealed class ExchangeTableValue : TableValue
	{
		// Token: 0x06005364 RID: 21348 RVA: 0x0011A9BC File Offset: 0x00118BBC
		public ExchangeTableValue(ExchangeVersion exchangeVersion, IExchangeService exchangeService, IEngineHost host, IResource resource, ExchangeCatalog catalog, string mailbox)
		{
			this.exchangeVersion = exchangeVersion;
			this.service = exchangeService;
			this.host = host;
			this.resource = resource;
			this.columnInfos = catalog.GetAllColumnInfos(true);
			this.catalog = catalog;
			this.folderPathFilter = new FolderFilter();
			this.itemSearchFilter = catalog.BaseItemSearchFilter;
			this.folderSearchFilter = catalog.BaseFolderSearchFilter;
			this.mailbox = mailbox;
			this.additionalProperties = new HashSet<PropertyDefinitionBase>();
			this.rowRange = RowRange.All;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0011AA48 File Offset: 0x00118C48
		private ExchangeTableValue(IExchangeService exchangeService, IEngineHost host, IResource resource, string mailbox, ExchangeColumnInfo[] columnInfos, ExchangeCatalog catalog, FolderFilter folderPathFilter, SearchFilter mailSearchFilter, SearchFilter baseFolderSearchFilter, RowRange rowRange, HashSet<PropertyDefinitionBase> additionalProperties = null)
		{
			this.mailbox = mailbox;
			this.service = exchangeService;
			this.host = host;
			this.resource = resource;
			this.columnInfos = columnInfos;
			this.catalog = catalog;
			this.folderPathFilter = folderPathFilter;
			this.itemSearchFilter = mailSearchFilter;
			this.folderSearchFilter = baseFolderSearchFilter;
			this.rowRange = rowRange;
			this.additionalProperties = additionalProperties ?? new HashSet<PropertyDefinitionBase>();
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06005366 RID: 21350 RVA: 0x0011AABC File Offset: 0x00118CBC
		private long SkipCount
		{
			get
			{
				if (this.rowRange.SkipCount.IsInfinite)
				{
					return long.MaxValue;
				}
				return this.rowRange.SkipCount.Value;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06005367 RID: 21351 RVA: 0x0011AB04 File Offset: 0x00118D04
		private long TakeCount
		{
			get
			{
				if (this.rowRange.TakeCount.IsInfinite)
				{
					return long.MaxValue;
				}
				return this.rowRange.TakeCount.Value;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06005368 RID: 21352 RVA: 0x0011AB4C File Offset: 0x00118D4C
		public override long LargeCount
		{
			get
			{
				long num = -this.SkipCount;
				foreach (ExchangeSearchResult exchangeSearchResult in this.GetTargetFolders())
				{
					num += (long)this.service.FindItemCount(exchangeSearchResult.Id, this.itemSearchFilter);
					if (num > this.TakeCount)
					{
						return this.TakeCount;
					}
				}
				return Math.Max(num, 0L);
			}
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0011ABD8 File Offset: 0x00118DD8
		public override void TestConnection()
		{
			try
			{
				Folder.Bind(this.service.InnerService, new FolderId(WellKnownFolderName.Root));
			}
			catch (ServiceRequestException ex)
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, ex.Message, null, null);
			}
			catch (ServiceResponseException ex2)
			{
				throw ExchangeExceptions.NewExchangeServiceResponseException(this.host, ex2, this.resource);
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0011AC4C File Offset: 0x00118E4C
		public override TypeValue Type
		{
			get
			{
				if (this.typeValue == null)
				{
					this.typeValue = this.columnInfos.CreateTableTypeValue();
				}
				return this.typeValue;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x0011AC6D File Offset: 0x00118E6D
		public override IQueryDomain QueryDomain
		{
			get
			{
				return ExchangeTableValue.ExchangeQueryDomain.Instance;
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0011AC74 File Offset: 0x00118E74
		private static bool TryGetSelectedColumnInfo(ExchangeColumnInfo[] columnInfos, Keys keys, Keys newColumns, out ExchangeColumnInfo[] list)
		{
			list = new ExchangeColumnInfo[keys.Length];
			for (int i = 0; i < keys.Count<string>(); i++)
			{
				for (int j = 0; j < columnInfos.Length; j++)
				{
					if (columnInfos[j].DisplayName == keys[i])
					{
						list[i] = ExchangeColumnInfo.New(columnInfos[j], newColumns[i]);
					}
				}
			}
			for (int k = 0; k < list.Length; k++)
			{
				if (list[k] == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0011ACF0 File Offset: 0x00118EF0
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			ExchangeColumnInfo exchangeColumnInfo = this.columnInfos[columnIndex];
			if (exchangeColumnInfo.ColumnCategory == ColumnCategory.ListColumn || exchangeColumnInfo.ColumnCategory == ColumnCategory.TableColumn)
			{
				ExchangeColumnInfo[] array = new ExchangeColumnInfo[this.columnInfos.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (i == columnIndex)
					{
						array[i] = exchangeColumnInfo.SubColumns[0];
					}
					else
					{
						array[i] = this.columnInfos[i];
					}
				}
				return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, array, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange, null);
			}
			return base.ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0011AD98 File Offset: 0x00118F98
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			ExchangeColumnInfo exchangeColumnInfo = this.columnInfos[columnToExpand];
			ExchangeColumnInfo[] array;
			if (exchangeColumnInfo.SubColumns != null && !exchangeColumnInfo.IsExpandedFromList && ExchangeTableValue.TryGetSelectedColumnInfo(exchangeColumnInfo.SubColumns, fieldsToProject, newColumns, out array))
			{
				int num = this.columnInfos.Count<ExchangeColumnInfo>();
				int num2 = array.Count<ExchangeColumnInfo>();
				int num3 = num + num2 - 1;
				ExchangeColumnInfo[] array2 = new ExchangeColumnInfo[num3];
				for (int i = 0; i < columnToExpand; i++)
				{
					array2[i] = this.columnInfos[i];
				}
				for (int j = columnToExpand; j < columnToExpand + num2; j++)
				{
					array2[j] = array[j - columnToExpand];
				}
				for (int k = columnToExpand + num2; k < num3; k++)
				{
					array2[k] = this.columnInfos[k - num2 + 1];
				}
				return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, array2, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange, null);
			}
			return base.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0011AEA0 File Offset: 0x001190A0
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue value)
		{
			Keys keys = columnSelection.Keys;
			ExchangeColumnInfo[] array = new ExchangeColumnInfo[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				array[i] = ExchangeColumnInfo.New(this.columnInfos[column], keys[i]);
			}
			value = new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, array, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange, null);
			return true;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0011AF30 File Offset: 0x00119130
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (this.rowRange.IsAll && SortQuery.TryGetSelectors(sortOrder, this.Type.AsTableType.ItemType, out array, out array2) && array.Length == 1)
			{
				QueryExpression queryExpression = array[0];
				if (array[0].Kind == QueryExpressionKind.ColumnAccess)
				{
					ColumnAccessQueryExpression columnAccessQueryExpression = (ColumnAccessQueryExpression)array[0];
					if (this.columnInfos[columnAccessQueryExpression.Column].ColumnCategory == ColumnCategory.PrimitiveColumn && this.columnInfos[columnAccessQueryExpression.Column].IsFoldable && !this.folderPathFilter.IsEmpty && this.folderPathFilter.Values.Length == 1)
					{
						ExchangeColumnInfo[] array3 = new ExchangeColumnInfo[this.columnInfos.Length];
						for (int i = 0; i < array3.Length; i++)
						{
							array3[i] = this.columnInfos[i];
							if (columnAccessQueryExpression.Column == i)
							{
								array3[i] = ExchangeColumnInfo.New(this.columnInfos[i], new SortDirection?(array2[0] ? SortDirection.Ascending : SortDirection.Descending));
							}
						}
						return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, array3, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange, null);
					}
					if (this.columnInfos[columnAccessQueryExpression.Column].ColumnCategory == ColumnCategory.FolderPath)
					{
						FolderFilter folderFilter = new FolderFilter(this.folderPathFilter, array2[0] ? SortDirection.Ascending : SortDirection.Descending);
						return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, this.columnInfos, this.catalog, folderFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange, null);
					}
				}
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0011B0F0 File Offset: 0x001192F0
		public override TableValue Skip(RowCount count)
		{
			return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, this.columnInfos, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange.Skip(count), null);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0011B148 File Offset: 0x00119348
		public override TableValue Take(RowCount count)
		{
			return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, this.columnInfos, this.catalog, this.folderPathFilter, this.itemSearchFilter, this.folderSearchFilter, this.rowRange.Take(count), null);
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0011B1A0 File Offset: 0x001193A0
		public override TableValue SelectRows(FunctionValue condition)
		{
			if (this.rowRange.IsAll)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				if (queryExpression != null)
				{
					FolderFilter folderFilter;
					SearchFilter searchFilter;
					if (new ExchangeSearchFilterBuilder(this.columnInfos, queryExpression).TryGetSearchFilter(this.folderPathFilter, this.itemSearchFilter, out folderFilter, out searchFilter))
					{
						return new ExchangeTableValue(this.service, this.host, this.resource, this.mailbox, this.columnInfos, this.catalog, folderFilter, searchFilter, this.folderSearchFilter, this.rowRange, null);
					}
					if (this.IsIdSelection(queryExpression))
					{
						return base.SelectRows(condition).Take(NumberValue.New(1));
					}
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0011B25C File Offset: 0x0011945C
		private bool IsIdSelection(QueryExpression query)
		{
			BinaryQueryExpression binaryQueryExpression = query as BinaryQueryExpression;
			if (binaryQueryExpression != null)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = binaryQueryExpression.Left as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null && binaryQueryExpression.Operator == BinaryOperator2.Equals && this.columnInfos[columnAccessQueryExpression.Column].UniqueName == "Id")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0011B2AC File Offset: 0x001194AC
		private bool TryGetFolderPath(string filterText, out string folderPath)
		{
			if (filterText.Length > 0 && filterText[0] == "\\"[0] && filterText[filterText.Length - 1] == "\\"[0])
			{
				folderPath = filterText.Substring(1, filterText.Length - 2);
				return true;
			}
			folderPath = null;
			return false;
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0011B308 File Offset: 0x00119508
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			if (this.rowRange.IsAll && this.columnInfos.Length == 1 && this.columnInfos[0].ColumnCategory == ColumnCategory.FolderPath)
			{
				return new ExchangeTableValue.DistinctFolderTableValue(this.columnInfos, this.GetTargetFolders().ToArray<ExchangeSearchResult>()).Distinct(distinctCriteria);
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0011B364 File Offset: 0x00119564
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new ExchangeTableValue.ExchangeEnumerator(this.exchangeVersion, this).GetAllItems(this.GetTargetFolders()).GetEnumerator();
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0011B384 File Offset: 0x00119584
		private IEnumerable<ExchangeSearchResult> GetTargetFolders()
		{
			FolderId folderId = new FolderId(WellKnownFolderName.MsgFolderRoot, new Mailbox(this.mailbox));
			if (!this.folderPathFilter.IsEmpty)
			{
				return this.SortFolders(this.GetTargetFoldersByFolderPath(folderId));
			}
			IEnumerable<ExchangeSearchResult> subFolders = this.GetSubFolders(folderId);
			if (this.folderPathFilter.HasSortDirection)
			{
				return this.SortFolders(subFolders);
			}
			return this.ReorderToShowInbox(subFolders);
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0011B3E4 File Offset: 0x001195E4
		private IEnumerable<ExchangeSearchResult> SortFolders(IEnumerable<ExchangeSearchResult> results)
		{
			if (!this.folderPathFilter.HasSortDirection)
			{
				return results;
			}
			if (this.folderPathFilter.SortDirection == SortDirection.Ascending)
			{
				return results.OrderBy((ExchangeSearchResult x) => x.FolderPath);
			}
			return results.OrderByDescending((ExchangeSearchResult x) => x.FolderPath);
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0011B458 File Offset: 0x00119658
		private IEnumerable<ExchangeSearchResult> ReorderToShowInbox(IEnumerable<ExchangeSearchResult> input)
		{
			List<ExchangeSearchResult> list = new List<ExchangeSearchResult>();
			List<ExchangeSearchResult> list2 = new List<ExchangeSearchResult>();
			foreach (ExchangeSearchResult exchangeSearchResult in input)
			{
				if (exchangeSearchResult.FolderPath.StartsWith("\\Inbox", StringComparison.Ordinal))
				{
					list.Add(exchangeSearchResult);
				}
				else
				{
					list2.Add(exchangeSearchResult);
				}
			}
			return list.OrderBy((ExchangeSearchResult x) => x.FolderPath).Concat(list2);
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0011B4F4 File Offset: 0x001196F4
		private IEnumerable<ExchangeSearchResult> GetTargetFoldersByFolderPath(FolderId rootFolderId)
		{
			foreach (string text in this.folderPathFilter.Values)
			{
				ExchangeSearchResult exchangeSearchResult;
				if (this.TryGetFolderByFolderPath(text, rootFolderId, out exchangeSearchResult) && exchangeSearchResult.FolderPath == text)
				{
					yield return exchangeSearchResult;
				}
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0011B50C File Offset: 0x0011970C
		private bool TryGetFolderByFolderPath(string folderPath, FolderId rootFolderId, out ExchangeSearchResult result)
		{
			ExchangeSearchResult exchangeSearchResult = null;
			string[] array;
			if (this.TryGetFolderPath(folderPath, out folderPath) && ExchangeHelper.TryGetSplitFolderPath(folderPath, out array))
			{
				for (int i = 0; i < array.Length; i++)
				{
					bool flag;
					int? num;
					IEnumerable<ExchangeSearchResult> enumerable = this.service.FindFolders(rootFolderId, new FolderView(1)
					{
						Traversal = FolderTraversal.Shallow
					}, ExchangeSearchFilterHelper.GetSearchFilter(FilterOptions.Equals, FolderSchema.DisplayName, array[i]), out flag, out num);
					if (enumerable.Count<ExchangeSearchResult>() == 0)
					{
						result = null;
						return false;
					}
					rootFolderId = new FolderId(enumerable.Single<ExchangeSearchResult>().Id);
					exchangeSearchResult = enumerable.Single<ExchangeSearchResult>();
				}
			}
			result = exchangeSearchResult;
			return result != null;
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0011B59C File Offset: 0x0011979C
		private IEnumerable<ExchangeSearchResult> GetSubFolders(FolderId folderId)
		{
			FolderView folderView = new FolderView(1000)
			{
				Traversal = FolderTraversal.Deep
			};
			folderView.Offset = 0;
			bool moreFolderAvailable = true;
			int? nextPageOffset = new int?(0);
			do
			{
				folderView.Offset = nextPageOffset.Value;
				IEnumerable<ExchangeSearchResult> enumerable = this.service.FindFolders(folderId, folderView, this.folderSearchFilter, out moreFolderAvailable, out nextPageOffset);
				foreach (ExchangeSearchResult exchangeSearchResult in enumerable)
				{
					yield return exchangeSearchResult;
				}
				IEnumerator<ExchangeSearchResult> enumerator = null;
			}
			while (moreFolderAvailable);
			yield break;
			yield break;
		}

		// Token: 0x04002E19 RID: 11801
		private const int itemPageSize = 1000;

		// Token: 0x04002E1A RID: 11802
		private const int folderPageSize = 1000;

		// Token: 0x04002E1B RID: 11803
		private readonly RowRange rowRange;

		// Token: 0x04002E1C RID: 11804
		private readonly string mailbox;

		// Token: 0x04002E1D RID: 11805
		private readonly IExchangeService service;

		// Token: 0x04002E1E RID: 11806
		private readonly IEngineHost host;

		// Token: 0x04002E1F RID: 11807
		private readonly IResource resource;

		// Token: 0x04002E20 RID: 11808
		private readonly ExchangeColumnInfo[] columnInfos;

		// Token: 0x04002E21 RID: 11809
		private readonly ExchangeCatalog catalog;

		// Token: 0x04002E22 RID: 11810
		private readonly FolderFilter folderPathFilter;

		// Token: 0x04002E23 RID: 11811
		private readonly SearchFilter itemSearchFilter;

		// Token: 0x04002E24 RID: 11812
		private readonly SearchFilter folderSearchFilter;

		// Token: 0x04002E25 RID: 11813
		private readonly HashSet<PropertyDefinitionBase> additionalProperties;

		// Token: 0x04002E26 RID: 11814
		private readonly ExchangeVersion exchangeVersion;

		// Token: 0x04002E27 RID: 11815
		private TypeValue typeValue;

		// Token: 0x02000BFB RID: 3067
		private class ExchangeQueryDomain : IQueryDomain
		{
			// Token: 0x170019A4 RID: 6564
			// (get) Token: 0x0600537E RID: 21374 RVA: 0x00002139 File Offset: 0x00000339
			public bool CanIndex
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600537F RID: 21375 RVA: 0x0011B5B3 File Offset: 0x001197B3
			public bool IsCompatibleWith(IQueryDomain domain)
			{
				return domain == ExchangeTableValue.ExchangeQueryDomain.Instance;
			}

			// Token: 0x06005380 RID: 21376 RVA: 0x0011B5BD File Offset: 0x001197BD
			public Query Optimize(Query query)
			{
				return new ExchangeTableValue.ExchangeQueryVisitor().VisitQuery(ExpandColumnsVisitor.ExpandColumns(query));
			}

			// Token: 0x04002E28 RID: 11816
			public static readonly IQueryDomain Instance = new ExchangeTableValue.ExchangeQueryDomain();
		}

		// Token: 0x02000BFC RID: 3068
		private class ExchangeQueryVisitor : QueryVisitor
		{
			// Token: 0x06005383 RID: 21379 RVA: 0x0011B5DC File Offset: 0x001197DC
			protected override Query VisitDataSource(DataSourceQuery dataQuery)
			{
				TableQuery tableQuery = (TableQuery)dataQuery;
				ExchangeTableValue exchangeTableValue = tableQuery.Table as ExchangeTableValue;
				if (exchangeTableValue != null && tableQuery.ExpandColumnns != null)
				{
					HashSet<PropertyDefinitionBase> hashSet = new HashSet<PropertyDefinitionBase>();
					for (int i = 0; i < tableQuery.ExpandColumnns.Length; i++)
					{
						int num = tableQuery.ExpandColumnns[i];
						if (num < exchangeTableValue.columnInfos.Length)
						{
							ExchangeColumnInfo exchangeColumnInfo = exchangeTableValue.columnInfos[num];
							if (exchangeColumnInfo.Property != null)
							{
								hashSet.Add(exchangeColumnInfo.Property);
							}
						}
					}
					if (this.fieldsToProjectInAttributesColumn.Count > 0)
					{
						foreach (ExchangeColumnInfo exchangeColumnInfo2 in exchangeTableValue.columnInfos)
						{
							if (exchangeColumnInfo2.ColumnCategory == ColumnCategory.RecordColumn && exchangeColumnInfo2.Property == null)
							{
								foreach (Keys keys in this.fieldsToProjectInAttributesColumn)
								{
									foreach (string text in keys)
									{
										foreach (ExchangeColumnInfo exchangeColumnInfo3 in exchangeColumnInfo2.SubColumns)
										{
											if (exchangeColumnInfo3.DisplayName == text && exchangeColumnInfo3.Property != null)
											{
												hashSet.Add(exchangeColumnInfo3.Property);
											}
										}
									}
								}
							}
						}
					}
					return new TableQuery(new ExchangeTableValue(exchangeTableValue.service, exchangeTableValue.host, exchangeTableValue.resource, exchangeTableValue.mailbox, exchangeTableValue.columnInfos, exchangeTableValue.catalog, exchangeTableValue.folderPathFilter, exchangeTableValue.itemSearchFilter, exchangeTableValue.folderSearchFilter, exchangeTableValue.rowRange, hashSet), tableQuery.ExpandColumnns, exchangeTableValue.service.EngineHost);
				}
				return base.VisitDataSource(tableQuery);
			}

			// Token: 0x06005384 RID: 21380 RVA: 0x0011B7D4 File Offset: 0x001199D4
			protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
			{
				if (query.InnerQuery.Columns[query.ColumnToExpand] == "Attributes" || query.InnerQuery.Columns[query.ColumnToExpand] == "Body")
				{
					this.fieldsToProjectInAttributesColumn.Add(query.FieldsToProject);
				}
				return base.VisitExpandRecordColumn(query);
			}

			// Token: 0x04002E29 RID: 11817
			private List<Keys> fieldsToProjectInAttributesColumn = new List<Keys>();
		}

		// Token: 0x02000BFD RID: 3069
		private class DistinctFolderTableValue : TableValue
		{
			// Token: 0x06005386 RID: 21382 RVA: 0x0011B850 File Offset: 0x00119A50
			public DistinctFolderTableValue(ExchangeColumnInfo[] columnInfos, ExchangeSearchResult[] results)
			{
				this.columnInfos = columnInfos;
				this.results = results;
			}

			// Token: 0x170019A5 RID: 6565
			// (get) Token: 0x06005387 RID: 21383 RVA: 0x0011B866 File Offset: 0x00119A66
			public override TypeValue Type
			{
				get
				{
					if (this.typeValue == null)
					{
						this.typeValue = this.columnInfos.CreateTableTypeValue();
					}
					return this.typeValue;
				}
			}

			// Token: 0x06005388 RID: 21384 RVA: 0x0011B887 File Offset: 0x00119A87
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (ExchangeSearchResult exchangeSearchResult in this.results)
				{
					Value value = Value.Null;
					if (this.columnInfos[0].ColumnCategory == ColumnCategory.FolderPath)
					{
						value = TextValue.New(exchangeSearchResult.FolderPath);
					}
					yield return RecordValue.New(this.columnInfos.GetKeys(), new Value[] { value });
				}
				ExchangeSearchResult[] array = null;
				yield break;
			}

			// Token: 0x04002E2A RID: 11818
			private ExchangeColumnInfo[] columnInfos;

			// Token: 0x04002E2B RID: 11819
			private ExchangeSearchResult[] results;

			// Token: 0x04002E2C RID: 11820
			private TypeValue typeValue;
		}

		// Token: 0x02000BFF RID: 3071
		private class ExchangeEnumerator
		{
			// Token: 0x0600538F RID: 21391 RVA: 0x0011B976 File Offset: 0x00119B76
			public ExchangeEnumerator(ExchangeVersion exchangeVersion, ExchangeTableValue table)
			{
				this.exchangeVersion = exchangeVersion;
				this.table = table;
			}

			// Token: 0x06005390 RID: 21392 RVA: 0x0011B98C File Offset: 0x00119B8C
			public IEnumerable<IValueReference> GetAllItems(IEnumerable<ExchangeSearchResult> folderResults)
			{
				foreach (ExchangeSearchResult exchangeSearchResult in folderResults)
				{
					if ((long)this.taken >= this.table.TakeCount || this.table.rowRange.IsNone)
					{
						yield break;
					}
					foreach (IValueReference valueReference in this.GetFolderItems(exchangeSearchResult.Id, exchangeSearchResult.FolderPath))
					{
						yield return valueReference;
					}
					IEnumerator<IValueReference> enumerator2 = null;
				}
				IEnumerator<ExchangeSearchResult> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06005391 RID: 21393 RVA: 0x0011B9A3 File Offset: 0x00119BA3
			private IEnumerable<IValueReference> GetFolderItems(FolderId folderId, string folderPath)
			{
				int? nextPageOffset = new int?(0);
				if (this.table.SkipCount > (long)this.skipped)
				{
					int num = this.table.service.FindItemCount(folderId, this.table.itemSearchFilter);
					if ((long)num <= this.table.SkipCount)
					{
						this.skipped += num;
						yield break;
					}
					nextPageOffset = new int?((int)this.table.SkipCount - this.skipped);
					this.skipped = (int)this.table.SkipCount;
				}
				bool moreMailAvailable = true;
				while (moreMailAvailable && (long)this.taken < this.table.TakeCount)
				{
					ItemView itemView = new ItemView((int)Math.Min(this.table.TakeCount - (long)this.taken, 1000L));
					itemView.Offset = nextPageOffset.Value;
					IEnumerable<ExchangeSearchResult> enumerable = this.table.service.FindItems(folderId, itemView, this.table.itemSearchFilter, folderPath, this.table.columnInfos, this.table.additionalProperties, out moreMailAvailable, out nextPageOffset);
					foreach (ExchangeSearchResult exchangeSearchResult in enumerable)
					{
						this.taken++;
						foreach (Value value in ExchangeValueBuilder.CreateRowValue(this.exchangeVersion, this.table.service, exchangeSearchResult, this.table.columnInfos, this.table.additionalProperties))
						{
							yield return value;
						}
						Value[] array = null;
					}
					IEnumerator<ExchangeSearchResult> enumerator = null;
				}
				yield break;
				yield break;
			}

			// Token: 0x04002E32 RID: 11826
			private int skipped;

			// Token: 0x04002E33 RID: 11827
			private int taken;

			// Token: 0x04002E34 RID: 11828
			private readonly ExchangeVersion exchangeVersion;

			// Token: 0x04002E35 RID: 11829
			private readonly ExchangeTableValue table;
		}
	}
}
