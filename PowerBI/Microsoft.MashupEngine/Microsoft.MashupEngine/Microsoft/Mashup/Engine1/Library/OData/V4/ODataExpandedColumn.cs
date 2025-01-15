using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200085F RID: 2143
	internal class ODataExpandedColumn
	{
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000C9161 File Offset: 0x000C7361
		public ODataExpandedColumn(int columnToExpand, string columnToExpandName, Keys newColumns, Keys fieldsToProject, IEnumerable<ODataExpandedColumn> innerExpandedColumns, Dictionary<SelectRowsQuery, List<QueryExpression>> selectQueryExpressions)
		{
			this.columnToExpand = columnToExpand;
			this.columnToExpandName = columnToExpandName;
			this.newColumns = newColumns;
			this.fieldsToProject = fieldsToProject;
			this.innerExpandedColumns = innerExpandedColumns;
			this.allSelectRowsExpressions = selectQueryExpressions;
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000C9196 File Offset: 0x000C7396
		public int ColumnToExpand
		{
			get
			{
				return this.columnToExpand;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000C919E File Offset: 0x000C739E
		public string ColumnToExpandName
		{
			get
			{
				return this.columnToExpandName;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000C91A6 File Offset: 0x000C73A6
		public Keys NewColumns
		{
			get
			{
				return this.newColumns;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000C91AE File Offset: 0x000C73AE
		public Keys FieldsToProject
		{
			get
			{
				return this.fieldsToProject;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000C91B6 File Offset: 0x000C73B6
		public IEnumerable<ODataExpandedColumn> InnerExpandedColumns
		{
			get
			{
				return this.innerExpandedColumns;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000C91BE File Offset: 0x000C73BE
		public Dictionary<SelectRowsQuery, List<QueryExpression>> SelectQueryExpressions
		{
			get
			{
				return this.allSelectRowsExpressions;
			}
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000C91C8 File Offset: 0x000C73C8
		public ExpandedColumnClause GetInnerSelectExpandClause(ODataQueryMetadata metadata, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty, ExpandedColumnFilter parentFilter, IterationRangeVariable iterator)
		{
			Capabilities elementCapability = metadata.Environment.Annotations.GetElementCapability(navigationSource.Name);
			IEnumerable<Microsoft.OData.Edm.IEdmStructuralProperty> enumerable = navigationSource.EntityType().Key();
			List<SelectItem> list = new List<SelectItem>(this.fieldsToProject.Length);
			if (elementCapability.SupportsSelect)
			{
				foreach (Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty in enumerable)
				{
					if (!this.fieldsToProject.Contains(edmStructuralProperty.Name))
					{
						list.AddRange(ODataColumns.GetSelectItems(metadata.Environment.EdmModel, navigationSource.EntityType(), new string[] { edmStructuralProperty.Name }));
					}
				}
				list.AddRange(ODataColumns.GetSelectItems(metadata.Environment.EdmModel, navigationSource.EntityType(), this.fieldsToProject));
			}
			ExpandedColumnFilter expandedColumnFilter = this.GetFilter(metadata, elementCapability, navigationProperty, navigationSource, parentFilter, iterator);
			if (elementCapability.SupportsExpand)
			{
				foreach (ODataExpandedColumn odataExpandedColumn in this.innerExpandedColumns)
				{
					string text = this.fieldsToProject[odataExpandedColumn.ColumnToExpand];
					if (elementCapability.CanExpand(text))
					{
						ODataExpandedColumn.NavigationPropertyMetadata navigationProperty2 = this.GetNavigationProperty(navigationSource, text);
						if (navigationProperty2 != null)
						{
							ExpandedColumnClause innerSelectExpandClause = odataExpandedColumn.GetInnerSelectExpandClause(metadata, navigationProperty2.NavigationSource, navigationProperty2.Property, expandedColumnFilter, iterator);
							ODataExpandPath odataExpandPath = new ODataExpandPath(new ODataPathSegment[]
							{
								new NavigationPropertySegment(navigationProperty2.Property, navigationProperty2.NavigationSource)
							});
							list.Add(new ExpandedNavigationSelectItem(odataExpandPath, navigationProperty2.NavigationSource, innerSelectExpandClause.SelectExpandClause, innerSelectExpandClause.Filter.InnerFilterClause, null, null, null, null, null, null));
							expandedColumnFilter = expandedColumnFilter.AppendOuterFilter(innerSelectExpandClause.Filter);
						}
					}
				}
			}
			return new ExpandedColumnClause(new SelectExpandClause(list, false), expandedColumnFilter);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000C93E0 File Offset: 0x000C75E0
		private ODataExpandedColumn.NavigationPropertyMetadata GetNavigationProperty(Microsoft.OData.Edm.IEdmNavigationSource navigationSource, string propertyName)
		{
			Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = navigationSource.EntityType().FindProperty(propertyName) as Microsoft.OData.Edm.IEdmNavigationProperty;
			if (edmNavigationProperty != null)
			{
				return new ODataExpandedColumn.NavigationPropertyMetadata(edmNavigationProperty, navigationSource.FindNavigationTarget(edmNavigationProperty));
			}
			return null;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000C9414 File Offset: 0x000C7614
		private ExpandedColumnFilter GetFilter(ODataQueryMetadata metadata, Capabilities capabilities, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, ExpandedColumnFilter parentFilter, IterationRangeVariable iterator)
		{
			FilterClause filterClause = null;
			SingleValueNode singleValueNode = null;
			CollectionNavigationNode collectionNavigationNode = null;
			EntityRangeVariable entityRangeVariable = null;
			EntityRangeVariableReferenceNode entityRangeVariableReferenceNode = null;
			SingleNavigationNode singleNavigationNode = null;
			Microsoft.OData.Edm.Library.EdmEntityTypeReference edmEntityTypeReference = new Microsoft.OData.Edm.Library.EdmEntityTypeReference(metadata.EntityType, false);
			EntityRangeVariable entityRangeVariable2 = new EntityRangeVariable("$it", edmEntityTypeReference, metadata.NavigationSource);
			EntityRangeVariableReferenceNode entityRangeVariableReferenceNode2 = new EntityRangeVariableReferenceNode("$it", entityRangeVariable2);
			bool flag = navigationProperty.TargetMultiplicity() == Microsoft.OData.Edm.EdmMultiplicity.One || navigationProperty.TargetMultiplicity() == Microsoft.OData.Edm.EdmMultiplicity.ZeroOrOne;
			if (!flag)
			{
				entityRangeVariable = iterator.New(this.GetEdmTypeReference(navigationSource), collectionNavigationNode);
				entityRangeVariableReferenceNode = new EntityRangeVariableReferenceNode(entityRangeVariable.Name, entityRangeVariable);
				if (parentFilter == null)
				{
					collectionNavigationNode = new CollectionNavigationNode(navigationProperty, entityRangeVariableReferenceNode2);
				}
				else
				{
					collectionNavigationNode = parentFilter.ExpandInnerNavigationPropertyNode(flag, navigationProperty) as CollectionNavigationNode;
				}
			}
			else if (parentFilter == null)
			{
				singleNavigationNode = new SingleNavigationNode(navigationProperty, entityRangeVariableReferenceNode2);
			}
			else
			{
				singleNavigationNode = parentFilter.ExpandInnerNavigationPropertyNode(flag, navigationProperty) as SingleNavigationNode;
			}
			if (this.allSelectRowsExpressions != null && this.allSelectRowsExpressions.Count > 0 && capabilities.SupportsFilter)
			{
				Microsoft.OData.Edm.IEdmType edmType = this.GetEdmType(navigationSource);
				TypeValue typeValue;
				if (metadata.Environment.EdmTypeValueLookup.TryGetValue(edmType, out typeValue))
				{
					Microsoft.OData.Edm.IEdmEntityType edmEntityType = navigationSource.EntityType();
					ODataColumns odataColumns = new ODataColumns(typeValue.AsRecordType, capabilities, edmEntityType, metadata.Environment);
					ODataExpandPath odataExpandPath = new ODataExpandPath(new ODataPathSegment[]
					{
						new NavigationPropertySegment(navigationProperty, navigationSource)
					});
					foreach (KeyValuePair<SelectRowsQuery, List<QueryExpression>> keyValuePair in this.allSelectRowsExpressions)
					{
						ODataExpression[] array = ODataExpandedColumn.ExpandedColumnAccessRewriter.Rewrite(this, keyValuePair.Key, keyValuePair.Value, odataColumns, capabilities, typeValue.AsRecordType, edmEntityType, metadata.Environment, odataExpandPath.ImplicitEntityRangeVariableReferenceNode(), entityRangeVariableReferenceNode, singleNavigationNode);
						if (flag)
						{
							if (array[0] != null)
							{
								SingleValueNode expression = array[0].GetFilterClause().Expression;
								if (singleValueNode == null)
								{
									singleValueNode = expression;
								}
								else
								{
									singleValueNode = new BinaryOperatorNode(BinaryOperatorKind.And, singleValueNode, expression);
								}
							}
						}
						else
						{
							if (array[0] != null)
							{
								FilterClause filterClause2 = array[0].GetFilterClause();
								if (filterClause == null)
								{
									filterClause = filterClause2;
								}
								else
								{
									filterClause = new FilterClause(new BinaryOperatorNode(BinaryOperatorKind.And, filterClause.Expression, filterClause2.Expression), filterClause.RangeVariable);
								}
							}
							if (array[1] != null)
							{
								SingleValueNode singleValueNode2 = array[1].GetFilterClause().Expression;
								if (singleValueNode != null)
								{
									singleValueNode2 = new BinaryOperatorNode(BinaryOperatorKind.And, ((AnyNode)singleValueNode).Body, singleValueNode2);
								}
								singleValueNode = new AnyNode(new Collection<RangeVariable> { entityRangeVariable }, entityRangeVariable)
								{
									Body = singleValueNode2,
									Source = collectionNavigationNode
								};
							}
						}
					}
				}
			}
			if (flag)
			{
				singleValueNode = ODataExpression.AppendNullRowsExprToExpandedRecordFilter(singleValueNode, singleNavigationNode);
				return new ExpandedRecordColumnFilter(entityRangeVariable2, singleValueNode, singleNavigationNode);
			}
			singleValueNode = ODataExpression.AppendNullRowsExprToExpandedTableFilter(singleValueNode, collectionNavigationNode);
			return new ExpandedTableColumnFilter(entityRangeVariable2, entityRangeVariable, entityRangeVariableReferenceNode, filterClause, singleValueNode, collectionNavigationNode);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000C96D4 File Offset: 0x000C78D4
		private Microsoft.OData.Edm.IEdmType GetEdmType(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			if (navigationSource.Type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				return ((Microsoft.OData.Edm.IEdmCollectionType)navigationSource.Type).ElementType.Definition;
			}
			return navigationSource.Type;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000C9700 File Offset: 0x000C7900
		private Microsoft.OData.Edm.IEdmEntityTypeReference GetEdmTypeReference(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			if (navigationSource.Type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				return ((Microsoft.OData.Edm.IEdmCollectionType)navigationSource.Type).ElementType.AsEntity();
			}
			return new Microsoft.OData.Edm.Library.EdmEntityTypeReference(navigationSource.EntityType(), false);
		}

		// Token: 0x0400206D RID: 8301
		private readonly int columnToExpand;

		// Token: 0x0400206E RID: 8302
		private readonly string columnToExpandName;

		// Token: 0x0400206F RID: 8303
		private readonly Keys newColumns;

		// Token: 0x04002070 RID: 8304
		private readonly Keys fieldsToProject;

		// Token: 0x04002071 RID: 8305
		private readonly IEnumerable<ODataExpandedColumn> innerExpandedColumns;

		// Token: 0x04002072 RID: 8306
		private readonly Dictionary<SelectRowsQuery, List<QueryExpression>> allSelectRowsExpressions;

		// Token: 0x02000860 RID: 2144
		private class NavigationPropertyMetadata
		{
			// Token: 0x06003DC4 RID: 15812 RVA: 0x000C9732 File Offset: 0x000C7932
			public NavigationPropertyMetadata(Microsoft.OData.Edm.IEdmNavigationProperty property, Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
			{
				this.property = property;
				this.navigationSource = navigationSource;
			}

			// Token: 0x17001458 RID: 5208
			// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000C9748 File Offset: 0x000C7948
			public Microsoft.OData.Edm.IEdmNavigationProperty Property
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x17001459 RID: 5209
			// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000C9750 File Offset: 0x000C7950
			public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource
			{
				get
				{
					return this.navigationSource;
				}
			}

			// Token: 0x04002073 RID: 8307
			private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

			// Token: 0x04002074 RID: 8308
			private readonly Microsoft.OData.Edm.IEdmNavigationProperty property;
		}

		// Token: 0x02000861 RID: 2145
		private class ExpandedColumnAccessRewriter : QueryExpressionVisitor
		{
			// Token: 0x06003DC7 RID: 15815 RVA: 0x000C9758 File Offset: 0x000C7958
			private ExpandedColumnAccessRewriter(ODataExpandedColumn expandedColumn, ODataColumns columns, Capabilities capabilities, RecordTypeValue recordType, Microsoft.OData.Edm.IEdmEntityType entityType, ODataEnvironment environment, EntityRangeVariableReferenceNode source, EntityRangeVariableReferenceNode outerSource, SingleNavigationNode singleNavNode, SelectRowsQuery selectRowsQueryItem, List<QueryExpression> selectQueryExpressions)
			{
				this.expandedColumn = expandedColumn;
				this.columns = columns;
				this.capabilities = capabilities;
				this.recordType = recordType;
				this.entityType = entityType;
				this.environment = environment;
				this.source = source;
				this.outerSource = outerSource;
				this.singleNavNode = singleNavNode;
				this.selectRowsQueryItem = selectRowsQueryItem;
				this.selectQueryExpressions = selectQueryExpressions;
			}

			// Token: 0x06003DC8 RID: 15816 RVA: 0x000C97C0 File Offset: 0x000C79C0
			public static ODataExpression[] Rewrite(ODataExpandedColumn expandedColumn, SelectRowsQuery selectRowsQueryItem, List<QueryExpression> selectQueryExpressions, ODataColumns columns, Capabilities capabilities, RecordTypeValue recordType, Microsoft.OData.Edm.IEdmEntityType entityType, ODataEnvironment environment, EntityRangeVariableReferenceNode source, EntityRangeVariableReferenceNode outerSource, SingleNavigationNode singleNavNode)
			{
				return new ODataExpandedColumn.ExpandedColumnAccessRewriter(expandedColumn, columns, capabilities, recordType, entityType, environment, source, outerSource, singleNavNode, selectRowsQueryItem, selectQueryExpressions).TryVisit(selectQueryExpressions);
			}

			// Token: 0x06003DC9 RID: 15817 RVA: 0x000C97EC File Offset: 0x000C79EC
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				int num = this.expandedColumn.NewColumns.IndexOfKey(this.selectRowsQueryItem.Columns[columnAccess.Column]);
				if (num != -1)
				{
					string text = this.expandedColumn.FieldsToProject[num];
					if (this.capabilities.CanFilter(text))
					{
						num = this.columns.Names.IndexOfKey(text);
						return this.columns.Expressions[num].Expression;
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x06003DCA RID: 15818 RVA: 0x000C9874 File Offset: 0x000C7A74
			private ODataExpression[] TryVisit(List<QueryExpression> expressions)
			{
				List<QueryExpression> list = new List<QueryExpression>();
				ODataExpression[] array = new ODataExpression[2];
				try
				{
					foreach (QueryExpression queryExpression in expressions)
					{
						try
						{
							list.Add(base.Visit(queryExpression));
						}
						catch (NotSupportedException)
						{
						}
					}
					if (list.Count > 0)
					{
						QueryExpression queryExpression2 = SelectRowsQuery.CreateConjunctiveNF(list);
						array[0] = new ODataExpression(this.capabilities, this.recordType, TypeValue.Record, queryExpression2, this.entityType, this.environment, this.source, this.singleNavNode);
						if (this.singleNavNode == null)
						{
							array[1] = new ODataExpression(this.capabilities, this.recordType, TypeValue.Record, queryExpression2, this.entityType, this.environment, this.outerSource, null);
						}
						else
						{
							array[1] = null;
						}
					}
				}
				catch (NotSupportedException)
				{
				}
				return array;
			}

			// Token: 0x04002075 RID: 8309
			private readonly ODataExpandedColumn expandedColumn;

			// Token: 0x04002076 RID: 8310
			private readonly ODataColumns columns;

			// Token: 0x04002077 RID: 8311
			private readonly Capabilities capabilities;

			// Token: 0x04002078 RID: 8312
			private readonly RecordTypeValue recordType;

			// Token: 0x04002079 RID: 8313
			private readonly Microsoft.OData.Edm.IEdmEntityType entityType;

			// Token: 0x0400207A RID: 8314
			private readonly ODataEnvironment environment;

			// Token: 0x0400207B RID: 8315
			private readonly EntityRangeVariableReferenceNode source;

			// Token: 0x0400207C RID: 8316
			private readonly EntityRangeVariableReferenceNode outerSource;

			// Token: 0x0400207D RID: 8317
			private readonly SingleNavigationNode singleNavNode;

			// Token: 0x0400207E RID: 8318
			private List<QueryExpression> selectQueryExpressions;

			// Token: 0x0400207F RID: 8319
			private SelectRowsQuery selectRowsQueryItem;
		}
	}
}
