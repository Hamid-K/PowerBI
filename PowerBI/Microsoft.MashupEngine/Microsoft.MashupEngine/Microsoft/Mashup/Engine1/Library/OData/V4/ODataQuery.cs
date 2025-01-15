using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.OData.V4.Write;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000878 RID: 2168
	internal class ODataQuery : DataSourceQuery
	{
		// Token: 0x06003E5B RID: 15963 RVA: 0x000CBA40 File Offset: 0x000C9C40
		public ODataQuery(ODataEnvironment environment, Microsoft.OData.Edm.IEdmEntitySet entitySet, RecordTypeValue recordTypeValue)
			: this(environment, entitySet, new ODataPath(new ODataPathSegment[]
			{
				new EntitySetSegment(entitySet)
			}), entitySet, entitySet.EntityType(), recordTypeValue, true, true, environment.ServiceUri)
		{
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000CBA7C File Offset: 0x000C9C7C
		public ODataQuery(ODataEnvironment environment, Microsoft.OData.Edm.IEdmSingleton singleton, RecordTypeValue recordTypeValue)
			: this(environment, singleton, new ODataPath(new ODataPathSegment[]
			{
				new SingletonSegment(singleton)
			}), singleton, singleton.EntityType(), recordTypeValue, true, true, environment.ServiceUri)
		{
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000CBAB8 File Offset: 0x000C9CB8
		public ODataQuery(Uri entryUri, ODataEnvironment environment, Microsoft.OData.Edm.IEdmFunction function, Microsoft.OData.Edm.IEdmEntitySetBase functionEntitySetResult, Microsoft.OData.Edm.IEdmEntityType type, IEnumerable<OperationSegmentParameter> parameters, RecordTypeValue recordTypeValue)
			: this(environment, functionEntitySetResult, new ODataPath(new ODataPathSegment[]
			{
				new OperationSegment(new Microsoft.OData.Edm.IEdmOperation[] { function }, parameters, functionEntitySetResult)
			}), function, type, recordTypeValue, function.IsComposable, true, entryUri)
		{
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000CBAFC File Offset: 0x000C9CFC
		public ODataQuery(ODataEnvironment environment, Microsoft.OData.Edm.IEdmFunctionImport functionImport, Microsoft.OData.Edm.IEdmEntitySet functionImportEntitySetResult, Microsoft.OData.Edm.IEdmEntityType type, IEnumerable<OperationSegmentParameter> parameters, RecordTypeValue recordTypeValue)
			: this(environment, functionImportEntitySetResult, new ODataPath(new ODataPathSegment[] { ODataQueryBuilderWrapper.CreateOperationImportSegment(functionImport, functionImportEntitySetResult, parameters) }), functionImport, functionImport.Function.ReturnType.AsEntity().EntityDefinition(), recordTypeValue, functionImport.Function.IsComposable, true, environment.ServiceUri)
		{
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000CBB54 File Offset: 0x000C9D54
		public ODataQuery(ODataEnvironment environment, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, RecordTypeValue recordTypeValue, Uri baseUri)
			: this(environment, navigationSource, new ODataPath(new ODataPathSegment[]
			{
				new NavigationPropertySegment(navigationProperty, navigationSource)
			}), navigationSource, navigationSource.EntityType(), recordTypeValue, true, true, baseUri)
		{
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000CBB8B File Offset: 0x000C9D8B
		public ODataQuery(ODataQuery baseQuery, ODataColumns columns)
			: this(baseQuery, columns, baseQuery.range, baseQuery.whereCondition, baseQuery.sortOrder, baseQuery.allColumnsSelected, null)
		{
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000CBBB0 File Offset: 0x000C9DB0
		private ODataQuery(ODataEnvironment environment, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, ODataPath path, Microsoft.OData.Edm.IEdmNamedElement namedElement, Microsoft.OData.Edm.IEdmEntityType entityType, RecordTypeValue recordTypeValue, bool isComposable, bool allColumnsSelected, Uri baseUri)
		{
			this.engineHost = environment.Host;
			this.path = path;
			this.capabilities = environment.Annotations.GetElementCapability(namedElement);
			this.columns = new ODataColumns(recordTypeValue, this.capabilities, entityType, environment);
			this.sortOrder = ODataSortOrder.None;
			this.range = RowRange.All;
			this.keyColumns = Keys.New((from k in entityType.Key()
				select k.Name).ToArray<string>());
			this.metadata = new ODataQueryMetadata(environment, navigationSource, entityType, this.columns, this.keyColumns);
			this.isComposable = isComposable;
			this.allColumnsSelected = allColumnsSelected;
			this.baseUri = baseUri;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000CBC84 File Offset: 0x000C9E84
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

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x000CBD5B File Offset: 0x000C9F5B
		public override Keys Columns
		{
			get
			{
				return this.columns.Names;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000CBD68 File Offset: 0x000C9F68
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.metadata.QueryDomain;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06003E65 RID: 15973 RVA: 0x000CBD78 File Offset: 0x000C9F78
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

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000CBDE1 File Offset: 0x000C9FE1
		public ODataColumns QueryColumn
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000CBDE9 File Offset: 0x000C9FE9
		public override IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000CBDF1 File Offset: 0x000C9FF1
		public override TypeValue GetColumnType(int index)
		{
			return this.columns.Types[index].Value.AsType;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000CBE0C File Offset: 0x000CA00C
		public override Query Skip(RowCount count)
		{
			if (!count.IsInfinite && this.isComposable && this.capabilities.SupportsSkip && !this.metadata.IsSingleton)
			{
				return new ODataQuery(this, this.columns, this.range.Skip(count), this.whereCondition, this.sortOrder, this.allColumnsSelected, null);
			}
			return base.Skip(count);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000CBE7C File Offset: 0x000CA07C
		public override Query Take(RowCount count)
		{
			if (!count.IsZero && this.isComposable && this.capabilities.SupportsTop && !this.metadata.IsSingleton)
			{
				return new ODataQuery(this, this.columns, this.range.Take(count), this.whereCondition, this.sortOrder, this.allColumnsSelected, null);
			}
			return base.Take(count);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000CBEEC File Offset: 0x000CA0EC
		public override Query Sort(TableSortOrder tableSortOrder)
		{
			ODataSortOrder odataSortOrder;
			if (this.range.IsAll && this.isComposable && this.capabilities.SupportsSort && !this.metadata.IsSingleton && this.TryGetSortOrder(tableSortOrder, out odataSortOrder))
			{
				return new ODataQuery(this, this.columns, this.range, this.whereCondition, odataSortOrder, this.allColumnsSelected, null);
			}
			return base.Sort(tableSortOrder);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000CBF60 File Offset: 0x000CA160
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
				IEdmVocabularyAnnotatable entityType = this.metadata.EntityType;
				Capabilities capabilities = AnnotationProcessor.ProcessCapabilities(string.Empty, this.metadata.Environment.EdmModel, entityType, this.metadata.Environment.Annotations, this.metadata.Environment.UserSettings);
				flag = capabilities.ApplySupported;
				flag2 = capabilities.PropertyRestrictions;
			}
			this.capabilities.PropertyRestrictions = flag2;
			this.capabilities.ApplySupported = flag;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000CC00C File Offset: 0x000CA20C
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
					EntityRangeVariableReferenceNode entityRangeVariableReferenceNode = this.path.ImplicitEntityRangeVariableReferenceNode();
					for (int i = 0; i < grouping.Constructors.Length; i++)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[i];
						array[i] = columnConstructor.Type;
						keysBuilder.Add(columnConstructor.Name);
						AggregateExpression aggregateExpression = new ODataExpression(this.capabilities, grouping.GroupTableType.AsTableType.ItemType.AsRecordType, columnConstructor.Type.Value.AsType, QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), columnConstructor.Function), this.metadata.EntityType, this.metadata.Environment, entityRangeVariableReferenceNode, null).BuildAggregateExpression(columnConstructor.Name, grouping.KeyKeys);
						list.Add(aggregateExpression);
					}
					foreach (string text in grouping.KeyKeys)
					{
						Microsoft.OData.Edm.IEdmProperty edmProperty = entityRangeVariableReferenceNode.TypeReference.AsStructured().FindProperty(text);
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
						GroupByPropertyNode groupByPropertyNode = new GroupByPropertyNode(text, entityRangeVariableReferenceNode.PropertyAccessNode(text));
						list2.Add(groupByPropertyNode);
					}
					List<TransformationNode> list3 = new List<TransformationNode>();
					if (this.whereCondition != null)
					{
						FilterTransformationNode filterTransformationNode = new FilterTransformationNode(this.whereCondition.GetFilterClause());
						list3.Add(filterTransformationNode);
						this.whereCondition = null;
					}
					AggregateTransformationNode aggregateTransformationNode = null;
					if (list.Count > 0)
					{
						aggregateTransformationNode = new AggregateTransformationNode(list);
					}
					GroupByTransformationNode groupByTransformationNode = new GroupByTransformationNode(list2, aggregateTransformationNode, entityRangeVariableReferenceNode.RangeVariable.EntityCollectionNode);
					list3.Add(groupByTransformationNode);
					this.applyClause = new ApplyClause(list3);
					RecordTypeValue recordTypeValue = ODataQuery.CreateRecordType(keysBuilder.ToKeys(), array);
					RecordValue recordValue = RecordValue.New(Keys.New("aggregate"), new Value[] { LogicalValue.New(true) });
					recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, recordValue).AsType.AsRecordType;
					ODataColumns odataColumns = new ODataColumns(recordTypeValue, this.capabilities, this.metadata.EntityType, this.metadata.Environment);
					ODataColumns columns = this.columns.SelectColumns(grouping.KeyKeys).Add(odataColumns, this.capabilities);
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

		// Token: 0x06003E6E RID: 15982 RVA: 0x000CC3E4 File Offset: 0x000CA5E4
		public override Query Group(Grouping grouping)
		{
			ODataQuery odataQuery;
			if (this.TryGrouping(grouping, out odataQuery))
			{
				return odataQuery;
			}
			return base.Group(grouping);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x000CC408 File Offset: 0x000CA608
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

		// Token: 0x06003E70 RID: 15984 RVA: 0x000CC46C File Offset: 0x000CA66C
		public override Query SelectRows(FunctionValue condition)
		{
			if (this.range.IsAll && this.isComposable && this.capabilities.SupportsFilter && !this.metadata.IsSingleton)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition);
				queryExpression = ColumnAccessRewriter.Rewrite(queryExpression, this.columns, this.capabilities);
				if (this.whereCondition != null)
				{
					queryExpression = new BinaryQueryExpression(BinaryOperator2.And, this.whereCondition.Expression, queryExpression);
				}
				RecordTypeValue recordTypeValue = ((this.applyClause == null) ? this.metadata.Columns.RecordTypeValue : this.columns.RecordTypeValue);
				ODataExpression odataExpression = new ODataExpression(this.capabilities, recordTypeValue, TypeValue.Record, queryExpression, this.metadata.EntityType, this.metadata.Environment, this.path.ImplicitEntityRangeVariableReferenceNode(), null);
				if (odataExpression.Type.TypeKind == ValueKind.Logical)
				{
					return new ODataQuery(this, this.columns, this.range, odataExpression, this.sortOrder, this.allColumnsSelected, null);
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000CC584 File Offset: 0x000CA784
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			if (this.isComposable && this.capabilities.SupportsSelect && !columnSelection.Keys.Equals(this.metadata.ColumnNames) && this.applyClause == null)
			{
				return new ODataQuery(this, this.columns.SelectColumns(columnSelection), this.range, this.whereCondition, this.sortOrder, false, null);
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			int[] array;
			if (this.applyClause == null && this.sortOrder == ODataSortOrder.None && distinctCriteria.TryGetColumns(QueryTableValue.NewRowType(this), out array) && this.Columns.Length == array.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
				Grouping grouping = new Grouping(false, this.Columns, this.Columns, array, EmptyArray<ColumnConstructor>.Instance, true, null, TableTypeValue.New(ODataQuery.CreateRecordType(this.Columns, this.columns.Types)));
				return this.Group(grouping);
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000CC690 File Offset: 0x000CA890
		public override IEnumerable<IValueReference> GetRows()
		{
			Uri uri;
			try
			{
				uri = this.GetUri();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				uri = null;
			}
			return new ODataReaderEnumerable(this.metadata, this.columns.RecordTypeValue, uri);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000CC6DC File Offset: 0x000CA8DC
		private Func<Value> GetWriteFunction(Func<List<ODataWriteRequest>> createWriteRequests)
		{
			return delegate
			{
				List<ODataWriteRequest> list = createWriteRequests();
				return ListValue.New(ODataWriteRequestExecuter.New(this.metadata.Environment).ExecuteODataWriteRequests(list).ToArray()).ToTable();
			};
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000CC6FC File Offset: 0x000CA8FC
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

		// Token: 0x06003E76 RID: 15990 RVA: 0x000CC774 File Offset: 0x000CA974
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

		// Token: 0x06003E77 RID: 15991 RVA: 0x000CC7EC File Offset: 0x000CA9EC
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

		// Token: 0x06003E78 RID: 15992 RVA: 0x000CC8B8 File Offset: 0x000CAAB8
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

		// Token: 0x06003E79 RID: 15993 RVA: 0x000CC948 File Offset: 0x000CAB48
		public override bool TryGetExpression(out IExpression expression)
		{
			Uri uri;
			FunctionValue functionValue;
			if (this.TryGetUri(out uri) && LibraryHelper.TryGetFunctionValue("OData.Feed", out functionValue))
			{
				RecordValue recordValue = this.metadata.Environment.UserSettings.GetOptionsValue();
				recordValue = recordValue.Concatenate(ODataQuery.V4Options).AsRecord;
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

		// Token: 0x06003E7A RID: 15994 RVA: 0x000CC9E8 File Offset: 0x000CABE8
		public Uri GetUri()
		{
			EntityRangeVariableReferenceNode entityRangeVariableReferenceNode = this.path.ImplicitEntityRangeVariableReferenceNode();
			OrderByClause orderByClause = this.GetOrderByClause(entityRangeVariableReferenceNode);
			ODataQueryClauses queryClause = this.columns.GetQueryClause(this.allColumnsSelected, this.metadata, entityRangeVariableReferenceNode);
			FilterClause filterClause = ((this.whereCondition != null) ? this.whereCondition.GetFilterClause() : null);
			if (!this.metadata.IsSingleton)
			{
				filterClause = ODataColumns.AppendFilter(filterClause, queryClause.OuterFilterClause, entityRangeVariableReferenceNode);
			}
			return ODataQueryBuilderWrapper.BuildUri(this.baseUri, this.path, this.range, orderByClause, filterClause, queryClause.SelectExpandClause, this.applyClause, null);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000CCA84 File Offset: 0x000CAC84
		private bool TryGetUri(out Uri uri)
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

		// Token: 0x06003E7C RID: 15996 RVA: 0x000CCAC0 File Offset: 0x000CACC0
		private OrderByClause GetOrderByClause(EntityRangeVariableReferenceNode implicitVariableRefNode)
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

		// Token: 0x06003E7D RID: 15997 RVA: 0x000CCB30 File Offset: 0x000CAD30
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
				array3[i] = this.Columns[((ColumnAccessQueryExpression)array[i]).Column];
				if (!this.capabilities.CanSort(array3[i], array2[i]))
				{
					odataSortOrder = null;
					return false;
				}
			}
			odataSortOrder = new ODataSortOrder(array3, array2);
			return true;
		}

		// Token: 0x040020C7 RID: 8391
		private static readonly RecordValue V4Options = RecordValue.New(new NamedValue[]
		{
			new NamedValue("ODataVersion", NumberValue.New(4)),
			new NamedValue("Implementation", Value.Null)
		});

		// Token: 0x040020C8 RID: 8392
		private readonly IEngineHost engineHost;

		// Token: 0x040020C9 RID: 8393
		private readonly RowRange range;

		// Token: 0x040020CA RID: 8394
		private readonly Keys keyColumns;

		// Token: 0x040020CB RID: 8395
		private readonly ODataColumns columns;

		// Token: 0x040020CC RID: 8396
		private readonly Capabilities capabilities;

		// Token: 0x040020CD RID: 8397
		private readonly bool isComposable;

		// Token: 0x040020CE RID: 8398
		private readonly bool allColumnsSelected;

		// Token: 0x040020CF RID: 8399
		private readonly ODataPath path;

		// Token: 0x040020D0 RID: 8400
		private readonly ODataQueryMetadata metadata;

		// Token: 0x040020D1 RID: 8401
		private readonly Uri baseUri;

		// Token: 0x040020D2 RID: 8402
		private ApplyClause applyClause;

		// Token: 0x040020D3 RID: 8403
		private ODataExpression whereCondition;

		// Token: 0x040020D4 RID: 8404
		private ODataSortOrder sortOrder;

		// Token: 0x040020D5 RID: 8405
		private IList<TableKey> tableKeys;
	}
}
