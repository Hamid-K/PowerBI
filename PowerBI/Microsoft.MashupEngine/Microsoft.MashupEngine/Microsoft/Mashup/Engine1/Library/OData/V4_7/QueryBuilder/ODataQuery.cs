using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Write;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007E1 RID: 2017
	internal class ODataQuery : DataSourceQuery
	{
		// Token: 0x06003A6D RID: 14957 RVA: 0x000BC909 File Offset: 0x000BAB09
		public ODataQuery(ODataQuery baseQuery, ODataColumns columns)
			: this(baseQuery, columns, baseQuery.range, baseQuery.whereCondition, baseQuery.sortOrder, baseQuery.allColumnsSelected, null)
		{
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000BC92C File Offset: 0x000BAB2C
		public ODataQuery(ODataEnvironment environment, ODataPath path, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, Microsoft.OData.Edm.IEdmEntityType entityType, RecordTypeValue itemTypeValue, Capabilities capabilities, bool isComposable)
		{
			this.engineHost = environment.Host;
			this.path = path;
			this.capabilities = capabilities;
			this.columns = ODataColumns.New(itemTypeValue);
			this.sortOrder = ODataSortOrder.None;
			this.range = RowRange.All;
			this.keyColumns = Keys.New((from k in entityType.Key()
				select k.Name).ToArray<string>());
			this.metadata = new ODataQueryMetadata(environment, navigationSource, entityType, this.columns, this.keyColumns);
			this.isComposable = isComposable;
			this.allColumnsSelected = true;
			this.baseUri = environment.ServiceUri;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000BC9F0 File Offset: 0x000BABF0
		private ODataQuery(ODataQuery baseQuery, ODataColumns columns, RowRange range, ODataExpression whereCondition, ODataSortOrder sortOrder, bool allColumnsSelected, ApplyClause applyClause = null)
		{
			this.engineHost = baseQuery.EngineHost;
			this.capabilities = baseQuery.capabilities;
			this.isComposable = baseQuery.isComposable;
			if (baseQuery.keyColumns.All((string key) => columns.Names.Contains(key)))
			{
				this.keyColumns = baseQuery.keyColumns;
			}
			else
			{
				this.keyColumns = Keys.Empty;
			}
			this.path = baseQuery.path;
			this.metadata = baseQuery.metadata;
			this.baseUri = baseQuery.baseUri;
			this.columns = columns;
			this.range = range;
			this.whereCondition = whereCondition;
			this.sortOrder = sortOrder;
			this.allColumnsSelected = allColumnsSelected;
			this.applyClause = applyClause ?? baseQuery.applyClause;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000BCAC7 File Offset: 0x000BACC7
		public override Keys Columns
		{
			get
			{
				return this.columns.Names;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000BCAD4 File Offset: 0x000BACD4
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.metadata.QueryDomain;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000BCAE4 File Offset: 0x000BACE4
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					if (this.keyColumns.Length > 0)
					{
						int[] array = this.keyColumns.Select((string key) => this.Columns.IndexOfKey(key)).ToArray<int>();
						this.tableKeys = new TableKey[]
						{
							new TableKey(array, true)
						};
					}
					else
					{
						this.tableKeys = new List<TableKey>();
					}
				}
				return this.tableKeys;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000BCB4D File Offset: 0x000BAD4D
		public ODataColumns QueryColumn
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000BCB55 File Offset: 0x000BAD55
		public ResourceRangeVariable ResourceRangeVariable
		{
			get
			{
				if (this.resourceRangeVariable == null)
				{
					this.resourceRangeVariable = new ResourceRangeVariable("$it", new EdmEntityTypeReference(this.metadata.EntityType, true), this.metadata.NavigationSource);
				}
				return this.resourceRangeVariable;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000BCB91 File Offset: 0x000BAD91
		public ResourceRangeVariableReferenceNode ResourceRangeVariableReferenceNode
		{
			get
			{
				return new ResourceRangeVariableReferenceNode(this.ResourceRangeVariable.Name, this.ResourceRangeVariable);
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000BCBA9 File Offset: 0x000BADA9
		public override IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000BCBB1 File Offset: 0x000BADB1
		public override TypeValue GetColumnType(int index)
		{
			return this.columns.GetColumnType(index);
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000BCBC0 File Offset: 0x000BADC0
		public override Query Skip(RowCount count)
		{
			Query query = base.Skip(count);
			if (!count.IsInfinite && this.isComposable && this.capabilities.SupportsSkip && !this.metadata.IsSingleton)
			{
				ODataQuery odataQuery = new ODataQuery(this, this.columns, this.range.Skip(count), this.whereCondition, this.sortOrder, this.allColumnsSelected, null);
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.AllOperations, odataQuery, query);
			}
			return query;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000BCC3C File Offset: 0x000BAE3C
		public override Query Take(RowCount count)
		{
			Query query = base.Take(count);
			if (!count.IsZero && this.isComposable && this.capabilities.SupportsTop && !this.metadata.IsSingleton)
			{
				ODataQuery odataQuery = new ODataQuery(this, this.columns, this.range.Take(count), this.whereCondition, this.sortOrder, this.allColumnsSelected, null);
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.Top, odataQuery, query);
			}
			return query;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000BCCB8 File Offset: 0x000BAEB8
		public override Query Sort(TableSortOrder tableSortOrder)
		{
			Query query = base.Sort(tableSortOrder);
			ODataSortOrder odataSortOrder;
			if (this.range.IsAll && this.isComposable && this.capabilities.SupportsSort && !this.metadata.IsSingleton && this.TryGetSortOrder(tableSortOrder, out odataSortOrder))
			{
				ODataQuery odataQuery = new ODataQuery(this, this.columns, this.range, this.whereCondition, odataSortOrder, this.allColumnsSelected, null);
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.AllOperations, odataQuery, query);
			}
			return query;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000BCD38 File Offset: 0x000BAF38
		private void ProcessAggregateAnnotations()
		{
			bool flag;
			bool flag2;
			if (this.metadata.Environment.Annotations.ApplySupported)
			{
				flag = true;
				flag2 = this.metadata.Environment.Annotations.PropertyRestrictions;
			}
			else
			{
				Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable entityType = this.metadata.EntityType;
				Capabilities capabilities = AnnotationProcessor.ProcessCapabilities(string.Empty, this.metadata.Environment.EdmModel, entityType, this.metadata.Environment.Annotations, this.metadata.Environment.UserSettings);
				flag = capabilities.ApplySupported;
				flag2 = capabilities.PropertyRestrictions;
			}
			this.capabilities.PropertyRestrictions = flag2;
			this.capabilities.ApplySupported = flag;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000BCDE4 File Offset: 0x000BAFE4
		public bool TryGrouping(Grouping grouping, out ODataQuery query)
		{
			if (!grouping.Adjacent)
			{
				this.ProcessAggregateAnnotations();
				if (this.capabilities.ApplySupported)
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					IValueReference[] array = new IValueReference[grouping.Constructors.Length];
					List<AggregateExpression> list = new List<AggregateExpression>();
					List<GroupByPropertyNode> list2 = new List<GroupByPropertyNode>();
					ResourceRangeVariableReferenceNode resourceRangeVariableReferenceNode = this.ResourceRangeVariableReferenceNode;
					for (int i = 0; i < grouping.Constructors.Length; i++)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[i];
						array[i] = columnConstructor.Type;
						keysBuilder.Add(columnConstructor.Name);
						AggregateExpression aggregateExpression = this.BuildAggregateExpression(columnConstructor);
						list.Add(aggregateExpression);
					}
					foreach (string text in grouping.KeyKeys)
					{
						Microsoft.OData.Edm.IEdmProperty edmProperty = resourceRangeVariableReferenceNode.TypeReference.AsStructured().FindProperty(text);
						IEnumerable<IEdmVocabularyAnnotation> enumerable = edmProperty.VocabularyAnnotations(this.metadata.Environment.EdmModel);
						bool flag;
						if (this.capabilities.PropertyRestrictions)
						{
							flag = enumerable.Any((IEdmVocabularyAnnotation annotation) => annotation.Term.FullName().Contains("Org.OData.Aggregation.V1.Groupable"));
						}
						else
						{
							flag = true;
						}
						if (!flag || edmProperty.Type.TypeKind() != Microsoft.OData.Edm.EdmTypeKind.Primitive)
						{
							query = null;
							return false;
						}
						GroupByPropertyNode groupByPropertyNode = new GroupByPropertyNode(text, resourceRangeVariableReferenceNode.PropertyAccessNode(text));
						list2.Add(groupByPropertyNode);
					}
					List<TransformationNode> list3 = new List<TransformationNode>();
					if (this.whereCondition != null)
					{
						FilterTransformationNode filterTransformationNode = new FilterTransformationNode(new FilterClause(this.whereCondition.Node, this.ResourceRangeVariable));
						list3.Add(filterTransformationNode);
						this.whereCondition = null;
					}
					AggregateTransformationNode aggregateTransformationNode = null;
					if (list.Count > 0)
					{
						aggregateTransformationNode = new AggregateTransformationNode(list);
					}
					GroupByTransformationNode groupByTransformationNode = new GroupByTransformationNode(list2, aggregateTransformationNode, resourceRangeVariableReferenceNode.RangeVariable.CollectionResourceNode);
					list3.Add(groupByTransformationNode);
					this.applyClause = new ApplyClause(list3);
					RecordTypeValue recordTypeValue = ODataQuery.CreateRecordType(keysBuilder.ToKeys(), array);
					RecordValue recordValue = RecordValue.New(Keys.New("aggregate"), new Value[] { LogicalValue.New(true) });
					recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, recordValue).AsType.AsRecordType;
					ODataColumns odataColumns = ODataColumns.New(recordTypeValue);
					ODataColumns columns = this.columns.SelectColumns(grouping.KeyKeys).Add(odataColumns);
					if (this.sortOrder != null)
					{
						IEnumerable<bool> enumerable2 = this.sortOrder.Names.Select((string name) => !columns.Names.Contains(name));
						if (enumerable2.Count<bool>() > 0)
						{
							List<string> list4 = this.sortOrder.Names.ToList<string>();
							List<bool> list5 = this.sortOrder.Ascendings.ToList<bool>();
							for (int j = 0; j < enumerable2.Count<bool>(); j++)
							{
								if (enumerable2.ElementAt(j))
								{
									list4.RemoveAt(j);
									list5.RemoveAt(j);
								}
							}
							this.sortOrder = new ODataSortOrder(list4.ToArray(), list5.ToArray());
						}
					}
					query = new ODataQuery(this, columns, this.range, this.whereCondition, this.sortOrder, true, this.applyClause);
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000BD134 File Offset: 0x000BB334
		public override Query Group(Grouping grouping)
		{
			Query query = base.Group(grouping);
			ODataQuery odataQuery;
			if (this.TryGrouping(grouping, out odataQuery))
			{
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.AllOperations, odataQuery, query);
			}
			return query;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000BD160 File Offset: 0x000BB360
		public static RecordTypeValue CreateRecordType(Keys names, IValueReference[] types)
		{
			Value[] array = new Value[types.Count<IValueReference>()];
			for (int i = 0; i < names.Count<string>(); i++)
			{
				TypeValue asType = types[i].Value.AsType;
				array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					asType,
					LogicalValue.False
				});
			}
			return RecordTypeValue.New(RecordValue.New(names, array));
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000BD1C4 File Offset: 0x000BB3C4
		public override Query SelectRows(FunctionValue condition)
		{
			RecordTypeValue recordTypeValue = QueryTableValue.NewRowType(this);
			if (this.range.IsAll && this.isComposable && this.capabilities.SupportsFilter && !this.metadata.IsSingleton)
			{
				FunctionValue functionValue;
				FunctionValue functionValue2;
				if (SelectRowsQuery.TryGetAndAdjustConditions(recordTypeValue, this.Columns, condition, new Func<QueryExpression, bool>(this.CanApplyFilter), (QueryExpression e) => e, out functionValue, out functionValue2) && functionValue != null)
				{
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(recordTypeValue, functionValue);
					ODataExpression odataExpression = new ODataQueryExpressionVisitor(this.metadata.Environment, this.ResourceRangeVariableReferenceNode, this.columns.RecordTypeValue).Compile(queryExpression);
					if (odataExpression.Type.TypeKind == ValueKind.Logical)
					{
						if (this.whereCondition != null)
						{
							odataExpression = new ODataExpression(new BinaryOperatorNode(BinaryOperatorKind.And, this.whereCondition.Node, odataExpression.Node), TypeValue.Logical);
						}
						ODataQuery odataQuery = new ODataQuery(this, this.columns, this.range, odataExpression, this.sortOrder, this.allColumnsSelected, null);
						Query query = ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.Filter, odataQuery, base.SelectRows(functionValue));
						if (functionValue2 != null)
						{
							return query.SelectRows(functionValue2);
						}
						return query;
					}
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000BD318 File Offset: 0x000BB518
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			if (!SelectColumnsQuery.NewQueryRequired(columnSelection, this))
			{
				query = this;
			}
			else
			{
				bool flag = false;
				Value value;
				if (this.columns.RecordTypeValue.TryGetMetaField("MoreColumns", out value))
				{
					int num = this.Columns.IndexOfKey(value.AsString);
					if (columnSelection.CreateSelectMap(this.Columns).MapColumn(num) != -1)
					{
						flag = true;
					}
				}
				if (flag)
				{
					query = base.SelectColumns(columnSelection);
				}
				else
				{
					query = new ODataQuery(this, this.columns.SelectColumns(columnSelection), this.range, this.whereCondition, this.sortOrder, true, this.applyClause);
				}
			}
			if (this.isComposable && this.capabilities.SupportsSelect && !columnSelection.Keys.Equals(this.metadata.ColumnNames) && this.applyClause == null)
			{
				ODataQuery odataQuery = new ODataQuery(this, this.columns.SelectColumns(columnSelection), this.range, this.whereCondition, this.sortOrder, false, null);
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.SelectColumns, odataQuery, query);
			}
			return query;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000BD418 File Offset: 0x000BB618
		public Query ExpandColumns(IEnumerable<ODataExpandedColumn> expandedColumns)
		{
			if (this.isComposable && this.capabilities.SupportsExpand && expandedColumns.Any<ODataExpandedColumn>())
			{
				List<ODataExpandedColumn> list = expandedColumns.ToList<ODataExpandedColumn>();
				List<ODataExpandedColumn> list2 = list.Select(new Func<ODataExpandedColumn, ODataExpandedColumn>(ODataQuery.RemoveFilters)).ToList<ODataExpandedColumn>();
				List<ODataExpandedColumn> list3 = list2.Select(new Func<ODataExpandedColumn, ODataExpandedColumn>(ODataQuery.RemoveProjections)).ToList<ODataExpandedColumn>();
				ODataQuery odataQuery = new ODataQuery(this, this.columns.ExpandColumn(list));
				ODataQuery odataQuery2 = new ODataQuery(this, this.columns.ExpandColumn(list2));
				ODataQuery odataQuery3 = new ODataQuery(this, this.columns.ExpandColumn(list3));
				return ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.AnyNode, odataQuery, ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.SelectColumns, odataQuery2, ODataRetryQuery.New(ODataRetryQuery.FallibleOperationKind.ExpandColumns, odataQuery3, this)));
			}
			return this;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000BD4D8 File Offset: 0x000BB6D8
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			int[] array;
			if (this.applyClause == null && this.sortOrder == ODataSortOrder.None && distinctCriteria.TryGetColumns(QueryTableValue.NewRowType(this), out array) && this.Columns.Length == array.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
				Grouping grouping = new Grouping(false, this.Columns, this.Columns, array, EmptyArray<ColumnConstructor>.Instance, true, null, TableTypeValue.New(this.columns.RecordTypeValue));
				return this.Group(grouping);
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000BD566 File Offset: 0x000BB766
		public override IEnumerable<IValueReference> GetRows()
		{
			return new ODataReaderEnumerable(this.metadata.Environment, this.GetUri(), this.columns.RecordTypeValue, !this.metadata.IsSingleton, this.metadata.NavigationSource, null);
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000BD5A3 File Offset: 0x000BB7A3
		private Func<Value> GetWriteFunction(Func<List<ODataWriteRequest>> createWriteRequests)
		{
			return delegate
			{
				List<ODataWriteRequest> list = createWriteRequests();
				return ListValue.New(ODataWriteRequestExecuter.New(this.metadata.Environment).ExecuteODataWriteRequests(list).ToArray()).ToTable();
			};
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000BD5C4 File Offset: 0x000BB7C4
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			this.metadata.Environment.VerifyActionPermitted();
			if (!this.capabilities.IsInsertable)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataInsertRestriction, TextValue.New(this.metadata.NavigationSource.Name), null);
			}
			Func<List<ODataWriteRequest>> func = delegate
			{
				rowsToInsert = rowsToInsert.QueryDomain.Optimize(rowsToInsert);
				Uri uri = new ODataUri
				{
					ServiceRoot = this.baseUri,
					Path = this.path
				}.GetUri();
				List<ODataWriteRequest> list = new List<ODataWriteRequest>();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("Prefer", "return=representation");
				foreach (IValueReference valueReference in rowsToInsert.GetRows())
				{
					ODataWriteRequest odataWriteRequest = new InsertRequest(uri, valueReference, dictionary, this.metadata.NavigationSource, this.metadata.Environment);
					list.Add(odataWriteRequest);
				}
				return list;
			};
			return ActionValue.New(this.GetWriteFunction(func));
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000BD63C File Offset: 0x000BB83C
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			this.metadata.Environment.VerifyActionPermitted();
			if (!this.capabilities.IsUpdatable)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataUpdateRestriction, TextValue.New(this.metadata.NavigationSource.Name), null);
			}
			Func<List<ODataWriteRequest>> func = delegate
			{
				List<ODataWriteRequest> list = new List<ODataWriteRequest>();
				foreach (IValueReference valueReference in this.GetRows())
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					Dictionary<string, string> etagHeader = WriteHelper.GetETagHeader(asRecord);
					etagHeader.Add("Prefer", "return=representation");
					UpdateRequest updateRequest = new UpdateRequest(WriteHelper.CreateCanonicalUri(asRecord, this.metadata, this.baseUri), this.CreateUpdateBody(asRecord, columnUpdates), etagHeader, this.metadata.NavigationSource, this.metadata.Environment);
					list.Add(updateRequest);
				}
				return list;
			};
			return ActionValue.New(this.GetWriteFunction(func));
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000BD6B4 File Offset: 0x000BB8B4
		private Value CreateUpdateBody(RecordValue row, ColumnUpdates columnUpdates)
		{
			RecordBuilder recordBuilder = new RecordBuilder(columnUpdates.Updates.Count);
			foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
			{
				int key = keyValuePair.Key;
				FunctionValue value = keyValuePair.Value;
				string text = row.Keys[key];
				TypeValue type = row.Type.AsRecordType.Fields[text].Type;
				Value value2 = value.Invoke(row[key]);
				recordBuilder.Add(text, value2, type);
			}
			RecordValue recordValue = recordBuilder.ToRecord();
			return BinaryOperator.AddMeta.Invoke(recordValue, row.MetaValue);
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000BD780 File Offset: 0x000BB980
		public override ActionValue DeleteRows()
		{
			this.metadata.Environment.VerifyActionPermitted();
			if (!this.capabilities.IsDeletable)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataDeleteRestriction, TextValue.New(this.metadata.NavigationSource.Name), null);
			}
			if (this.metadata.NavigationSource.NavigationSourceKind() == Microsoft.OData.Edm.EdmNavigationSourceKind.Singleton)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.ODataCannotDeleteSingleton, ListValue.New(this.GetRows()), null);
			}
			Func<List<ODataWriteRequest>> func = delegate
			{
				List<ODataWriteRequest> list = new List<ODataWriteRequest>();
				foreach (IValueReference valueReference in this.GetRows())
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					list.Add(new DeleteRequest(WriteHelper.CreateCanonicalUri(asRecord, this.metadata, this.baseUri), valueReference, WriteHelper.GetETagHeader(asRecord), this.metadata.NavigationSource, this.metadata.Environment));
				}
				return list;
			};
			return ActionValue.New(this.GetWriteFunction(func));
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000BD810 File Offset: 0x000BBA10
		public override bool TryGetExpression(out IExpression expression)
		{
			Uri uri;
			FunctionValue functionValue;
			if (this.TryGetUri(out uri) && LibraryHelper.TryGetFunctionValue("OData.Feed", out functionValue))
			{
				RecordValue recordValue = this.metadata.Environment.UserSettings.GetOptionsValue();
				recordValue = recordValue.Concatenate(ODataQuery.V47Options).AsRecord;
				expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(functionValue, TokenRange.Null), new IExpression[]
				{
					new ConstantExpressionSyntaxNode(TextValue.New(uri.OriginalString)),
					new ConstantExpressionSyntaxNode(Value.Null),
					new ConcatenateBinaryExpressionSyntaxNode(new ConstantExpressionSyntaxNode(recordValue), new NotImplementedExpressionSyntaxNode())
				});
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000BD8B0 File Offset: 0x000BBAB0
		public Uri GetUri()
		{
			if (this.uriException != null)
			{
				throw this.uriException;
			}
			if (this.uri == null)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.metadata.Environment.Host, "Engine/IO/OData/V4_7/ODataQuery/GetUri", TraceEventType.Information, this.metadata.Environment.Resource))
				{
					try
					{
						OrderByClause orderByClause = this.GetOrderByClause(this.ResourceRangeVariableReferenceNode);
						ODataQueryClauses queryClause = this.GetQueryClause();
						FilterClause filterClause = ((this.whereCondition != null) ? new FilterClause(this.whereCondition.Node, this.ResourceRangeVariable) : null);
						if (!this.metadata.IsSingleton)
						{
							filterClause = ODataQuery.AppendFilter(filterClause, queryClause.OuterFilterClause, this.ResourceRangeVariableReferenceNode);
						}
						this.uri = ODataQueryBuilderWrapper.BuildUri(this.baseUri, this.path, this.range, orderByClause, filterClause, queryClause.SelectExpandClause, this.applyClause, null);
						hostTrace.Add("Uri", this.uri.AbsoluteUri, true);
					}
					catch (UriFormatException ex)
					{
						this.uriException = ValueException.NewDataFormatError(ODataCommonErrors.CreateDataSourceExceptionMessage(ex.Message), Value.Null, null);
						hostTrace.Add(this.uriException, TraceEventType.Information, true);
						throw this.uriException;
					}
				}
			}
			return this.uri;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000BDA10 File Offset: 0x000BBC10
		public bool TryGetUri(out Uri uri)
		{
			bool flag;
			try
			{
				uri = this.GetUri();
				flag = true;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				uri = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000BDA4C File Offset: 0x000BBC4C
		public bool Equals(Query query)
		{
			if (this == query)
			{
				return true;
			}
			ODataQuery odataQuery = query as ODataQuery;
			Uri uri;
			Uri uri2;
			return odataQuery != null && this.Columns.Equals(odataQuery.Columns) && (this.TryGetUri(out uri) && odataQuery.TryGetUri(out uri2)) && uri == uri2;
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000BDAA0 File Offset: 0x000BBCA0
		private ODataQueryClauses GetQueryClause()
		{
			EdmEntityTypeReference edmEntityTypeReference = new EdmEntityTypeReference(this.metadata.EntityType, false);
			ResourceRangeVariable resourceRangeVariable = new ResourceRangeVariable("$it", edmEntityTypeReference, this.metadata.NavigationSource);
			ResourceRangeVariableReferenceNode resourceRangeVariableReferenceNode = new ResourceRangeVariableReferenceNode("$it", resourceRangeVariable);
			IterationRangeVariable iterationRangeVariable = new IterationRangeVariable();
			ExpandedColumnClause expandedColumnClause = new SelectExpandClauseBuilder(this.metadata, resourceRangeVariableReferenceNode, this.columns.RecordTypeValue, this.metadata.NavigationSource, iterationRangeVariable).Build(this.allColumnsSelected ? null : this.columns.Names, this.columns.ExpandedColumns);
			return new ODataQueryClauses(expandedColumnClause.SelectExpandClause, (expandedColumnClause.Filter == null) ? null : expandedColumnClause.Filter.OuterFilterClause);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000BDB58 File Offset: 0x000BBD58
		private AggregateExpression BuildAggregateExpression(ColumnConstructor constructor)
		{
			InvocationQueryExpression invocationQueryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), constructor.Function) as InvocationQueryExpression;
			Value value;
			AggregationMethod aggregationMethod;
			if (invocationQueryExpression != null && invocationQueryExpression.Function.TryGetConstant(out value) && ODataBuiltInFunctions.TryFindAggregateFunction(value, out aggregationMethod) && (this.capabilities.AggregateTransformations.Count == 0 || this.capabilities.AggregateTransformations.Contains(aggregationMethod.ToString())))
			{
				SingleValuePropertyAccessNode singleValuePropertyAccessNode = new ODataQueryExpressionVisitor(this.metadata.Environment, this.ResourceRangeVariableReferenceNode, this.columns.RecordTypeValue).Compile(invocationQueryExpression.Arguments[0]).Node as SingleValuePropertyAccessNode;
				if (singleValuePropertyAccessNode != null)
				{
					IEnumerable<IEdmVocabularyAnnotation> enumerable = singleValuePropertyAccessNode.Property.VocabularyAnnotations(this.metadata.Environment.EdmModel);
					bool flag;
					if (this.capabilities.ApplySupported)
					{
						if (this.capabilities.PropertyRestrictions)
						{
							flag = enumerable.Any((IEdmVocabularyAnnotation annotation) => annotation.Term.FullName().Contains("Org.OData.Aggregation.V1.Aggregatable"));
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						return new AggregateExpression(singleValuePropertyAccessNode, aggregationMethod, constructor.Name, singleValuePropertyAccessNode.TypeReference);
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000BDC98 File Offset: 0x000BBE98
		private OrderByClause GetOrderByClause(ResourceRangeVariableReferenceNode implicitVariableRefNode)
		{
			if (this.sortOrder != ODataSortOrder.None)
			{
				OrderByClause orderByClause = null;
				for (int i = this.sortOrder.Names.Length - 1; i >= 0; i--)
				{
					string text = this.sortOrder.Names[i];
					OrderByDirection orderByDirection = (this.sortOrder.Ascendings[i] ? OrderByDirection.Ascending : OrderByDirection.Descending);
					orderByClause = new OrderByClause(orderByClause, implicitVariableRefNode.PropertyAccessNode(text), orderByDirection, implicitVariableRefNode.RangeVariable);
				}
				return orderByClause;
			}
			return null;
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000BDD08 File Offset: 0x000BBF08
		private bool CanApplyFilter(QueryExpression conditionExpresion)
		{
			return conditionExpresion.AllAccess(ArgumentAccess.Deny, (int column) => this.capabilities.CanFilter(EdmNameEncoder.Encode(this.Columns[column])));
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000BDD24 File Offset: 0x000BBF24
		private bool TryGetSortOrder(TableSortOrder sortOrder, out ODataSortOrder odataSortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (!SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				odataSortOrder = null;
				return false;
			}
			string[] array3 = new string[array.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				if (array[i].Kind != QueryExpressionKind.ColumnAccess)
				{
					odataSortOrder = null;
					return false;
				}
				int column = ((ColumnAccessQueryExpression)array[i]).Column;
				array3[i] = this.Columns[column];
				TypeValue columnType = this.GetColumnType(column);
				if (!this.capabilities.CanSort(array3[i], array2[i]) || columnType.IsListType || columnType.IsTableType)
				{
					odataSortOrder = null;
					return false;
				}
			}
			odataSortOrder = new ODataSortOrder(array3, array2);
			return true;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000BDDCC File Offset: 0x000BBFCC
		private static FilterClause AppendFilter(FilterClause currentFilter, FilterClause newFilter, ResourceRangeVariableReferenceNode resourceRange)
		{
			if (currentFilter == null)
			{
				return newFilter;
			}
			if (newFilter == null)
			{
				return currentFilter;
			}
			return new FilterClause(new BinaryOperatorNode(BinaryOperatorKind.And, currentFilter.Expression, newFilter.Expression), resourceRange.RangeVariable);
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000BDDF5 File Offset: 0x000BBFF5
		private static ODataExpandedColumn RemoveFilters(ODataExpandedColumn expandedColumn)
		{
			return new ODataExpandedColumn(expandedColumn.ColumnToExpandName, expandedColumn.FieldsToProject, (expandedColumn.InnerExpandedColumns != null) ? expandedColumn.InnerExpandedColumns.Select(new Func<ODataExpandedColumn, ODataExpandedColumn>(ODataQuery.RemoveFilters)) : null, null);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000BDE2B File Offset: 0x000BC02B
		private static ODataExpandedColumn RemoveProjections(ODataExpandedColumn expandedColumn)
		{
			return new ODataExpandedColumn(expandedColumn.ColumnToExpandName, null, (expandedColumn.InnerExpandedColumns != null) ? expandedColumn.InnerExpandedColumns.Select(new Func<ODataExpandedColumn, ODataExpandedColumn>(ODataQuery.RemoveProjections)) : null, expandedColumn.SelectRowsCondition);
		}

		// Token: 0x04001E50 RID: 7760
		private static readonly RecordValue V47Options = RecordValue.New(new Microsoft.Mashup.Engine1.Runtime.NamedValue[]
		{
			new Microsoft.Mashup.Engine1.Runtime.NamedValue("ODataVersion", NumberValue.New(4)),
			new Microsoft.Mashup.Engine1.Runtime.NamedValue("Implementation", TextValue.New(ODataUserSettings.Implementation20))
		});

		// Token: 0x04001E51 RID: 7761
		private readonly IEngineHost engineHost;

		// Token: 0x04001E52 RID: 7762
		private readonly RowRange range;

		// Token: 0x04001E53 RID: 7763
		private readonly Keys keyColumns;

		// Token: 0x04001E54 RID: 7764
		private readonly ODataColumns columns;

		// Token: 0x04001E55 RID: 7765
		private readonly Capabilities capabilities;

		// Token: 0x04001E56 RID: 7766
		private readonly bool isComposable;

		// Token: 0x04001E57 RID: 7767
		private readonly bool allColumnsSelected;

		// Token: 0x04001E58 RID: 7768
		private readonly ODataPath path;

		// Token: 0x04001E59 RID: 7769
		private readonly ODataQueryMetadata metadata;

		// Token: 0x04001E5A RID: 7770
		private readonly Uri baseUri;

		// Token: 0x04001E5B RID: 7771
		private ApplyClause applyClause;

		// Token: 0x04001E5C RID: 7772
		private ODataExpression whereCondition;

		// Token: 0x04001E5D RID: 7773
		private ODataSortOrder sortOrder;

		// Token: 0x04001E5E RID: 7774
		private IList<TableKey> tableKeys;

		// Token: 0x04001E5F RID: 7775
		private ResourceRangeVariable resourceRangeVariable;

		// Token: 0x04001E60 RID: 7776
		private Uri uri;

		// Token: 0x04001E61 RID: 7777
		private Exception uriException;
	}
}
