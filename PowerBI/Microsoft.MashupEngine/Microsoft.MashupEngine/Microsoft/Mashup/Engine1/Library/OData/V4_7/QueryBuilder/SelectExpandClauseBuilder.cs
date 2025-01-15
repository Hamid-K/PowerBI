using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007FB RID: 2043
	internal class SelectExpandClauseBuilder
	{
		// Token: 0x06003B03 RID: 15107 RVA: 0x000BF528 File Offset: 0x000BD728
		public SelectExpandClauseBuilder(ODataQueryMetadata metadata, SingleResourceNode source, RecordTypeValue sourceTypeValue, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, IterationRangeVariable iterator)
		{
			this.metadata = metadata;
			this.source = source;
			this.sourceTypeValue = sourceTypeValue;
			this.iterator = iterator;
			this.navigationSource = navigationSource;
			this.entityType = navigationSource.EntityType();
			this.capabilities = this.metadata.Environment.Annotations.GetElementCapability(this.navigationSource);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000BF590 File Offset: 0x000BD790
		public ExpandedColumnClause Build(Keys fieldsToProject, IEnumerable<ODataExpandedColumn> expandedColumns)
		{
			bool flag = fieldsToProject == null;
			Dictionary<string, ODataExpandedColumn> dictionary;
			if (expandedColumns == null)
			{
				dictionary = null;
			}
			else
			{
				dictionary = expandedColumns.ToDictionary((ODataExpandedColumn c) => c.ColumnToExpandName);
			}
			Dictionary<string, ODataExpandedColumn> dictionary2 = dictionary ?? new Dictionary<string, ODataExpandedColumn>(0);
			if (flag && dictionary2.Count == 0)
			{
				return new ExpandedColumnClause(null, null);
			}
			List<SelectItem> list = (flag ? new List<SelectItem>() : new List<SelectItem>(fieldsToProject.Length));
			if (!flag && this.capabilities.SupportsSelect)
			{
				string moreColumnsColumnName = null;
				Value value;
				if (this.sourceTypeValue.TryGetMetaField("MoreColumns", out value))
				{
					moreColumnsColumnName = value.AsString;
				}
				if (moreColumnsColumnName != null && fieldsToProject.Contains(moreColumnsColumnName))
				{
					ODataExpandedColumn odataExpandedColumn;
					if (dictionary2.TryGetValue(moreColumnsColumnName, out odataExpandedColumn))
					{
						Keys fieldsToProject2 = odataExpandedColumn.FieldsToProject;
					}
					flag = true;
				}
				if (!flag && this.sourceTypeValue.MetaValue != null && !this.sourceTypeValue.MetaValue.Keys.Contains("aggregate"))
				{
					foreach (Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty in this.navigationSource.EntityType().Key())
					{
						SelectItem selectItem;
						if (!fieldsToProject.Contains(edmStructuralProperty.Name) && SelectExpandClauseBuilder.TryGetSelectItem(this.metadata.Environment.EdmModel, this.entityType, edmStructuralProperty.Name, out selectItem))
						{
							list.Add(selectItem);
						}
					}
				}
			}
			IEnumerable<string> enumerable = fieldsToProject;
			if (flag || !this.capabilities.SupportsSelect)
			{
				enumerable = expandedColumns.Select((ODataExpandedColumn c) => c.ColumnToExpandName);
			}
			if (!this.capabilities.SupportsExpand)
			{
				dictionary2.Clear();
			}
			ExpandedColumnFilter expandedColumnFilter = null;
			foreach (string text in enumerable)
			{
				ODataExpandedColumn odataExpandedColumn2;
				if (dictionary2.TryGetValue(text, out odataExpandedColumn2) && this.capabilities.SupportsExpand && this.capabilities.CanExpand(odataExpandedColumn2.ColumnToExpandName))
				{
					SelectExpandClauseBuilder.NavigationPropertyMetadata navigationProperty = SelectExpandClauseBuilder.GetNavigationProperty(this.navigationSource, odataExpandedColumn2.ColumnToExpandName);
					if (navigationProperty != null)
					{
						Tuple<ExpandedNavigationSelectItem, ExpandedColumnFilter> tuple = new SelectExpandClauseBuilder.ExpandedNavigationSelectItemBuilder(this.metadata, this.iterator, this.source, navigationProperty.Property, navigationProperty.NavigationSource, RecordTypeAlgebra.FieldOrDefault(this.sourceTypeValue, odataExpandedColumn2.ColumnToExpandName, TypeValue.Record)).Build(odataExpandedColumn2);
						list.Add(tuple.Item1);
						if (expandedColumnFilter == null)
						{
							expandedColumnFilter = tuple.Item2;
						}
						else
						{
							expandedColumnFilter = expandedColumnFilter.AppendOuterFilter(tuple.Item2);
						}
						if (tuple.Item1.SelectAndExpand != null && tuple.Item1.SelectAndExpand.SelectedItems.Any<SelectItem>())
						{
							continue;
						}
					}
				}
				SelectItem selectItem2;
				if (!flag && this.capabilities.SupportsSelect && SelectExpandClauseBuilder.TryGetSelectItem(this.metadata.Environment.EdmModel, this.entityType, text, out selectItem2))
				{
					list.Add(selectItem2);
				}
			}
			return new ExpandedColumnClause(new SelectExpandClause(list, flag), expandedColumnFilter);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000BF904 File Offset: 0x000BDB04
		private static bool TryGetSelectItem(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmType type, string selectedColumn, out SelectItem selectItem)
		{
			if (!model.FindBoundOperations(type).FilterByName(false, selectedColumn).Any<Microsoft.OData.Edm.IEdmOperation>())
			{
				selectItem = new PathSelectItem(new ODataSelectPath(new ODataPathSegment[]
				{
					new DynamicPathSegment(selectedColumn)
				}));
				return true;
			}
			selectItem = null;
			return false;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000BF93C File Offset: 0x000BDB3C
		private static SelectExpandClauseBuilder.NavigationPropertyMetadata GetNavigationProperty(Microsoft.OData.Edm.IEdmNavigationSource navigationSource, string propertyName)
		{
			Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = navigationSource.EntityType().FindProperty(propertyName) as Microsoft.OData.Edm.IEdmNavigationProperty;
			if (edmNavigationProperty != null)
			{
				return new SelectExpandClauseBuilder.NavigationPropertyMetadata(edmNavigationProperty, navigationSource.FindNavigationTarget(edmNavigationProperty));
			}
			return null;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000BF96D File Offset: 0x000BDB6D
		private static Microsoft.OData.Edm.IEdmType GetEdmType(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			if (navigationSource.Type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				return ((Microsoft.OData.Edm.IEdmCollectionType)navigationSource.Type).ElementType.Definition;
			}
			return navigationSource.Type;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000BF999 File Offset: 0x000BDB99
		private static Microsoft.OData.Edm.IEdmEntityTypeReference GetEdmTypeReference(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			if (navigationSource.Type.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Collection)
			{
				return ((Microsoft.OData.Edm.IEdmCollectionType)navigationSource.Type).ElementType.AsEntity();
			}
			return new EdmEntityTypeReference(navigationSource.EntityType(), false);
		}

		// Token: 0x04001E9F RID: 7839
		private const bool HandleDerivedTypePropertiesCorrectly = false;

		// Token: 0x04001EA0 RID: 7840
		private readonly ODataQueryMetadata metadata;

		// Token: 0x04001EA1 RID: 7841
		private readonly SingleResourceNode source;

		// Token: 0x04001EA2 RID: 7842
		private readonly RecordTypeValue sourceTypeValue;

		// Token: 0x04001EA3 RID: 7843
		private readonly IterationRangeVariable iterator;

		// Token: 0x04001EA4 RID: 7844
		private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

		// Token: 0x04001EA5 RID: 7845
		private readonly Microsoft.OData.Edm.IEdmEntityType entityType;

		// Token: 0x04001EA6 RID: 7846
		private readonly Capabilities capabilities;

		// Token: 0x020007FC RID: 2044
		private class ExpandedNavigationSelectItemBuilder
		{
			// Token: 0x06003B09 RID: 15113 RVA: 0x000BF9CC File Offset: 0x000BDBCC
			public ExpandedNavigationSelectItemBuilder(ODataQueryMetadata metadata, IterationRangeVariable iterator, SingleResourceNode parentSource, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty, Microsoft.OData.Edm.IEdmNavigationSource navigationTarget, TypeValue targetTypeValue)
			{
				this.metadata = metadata;
				this.iterator = iterator;
				this.parentSource = parentSource;
				this.navigationProperty = navigationProperty;
				this.navigationTarget = navigationTarget;
				this.targetItemTypeValue = (targetTypeValue.IsTableType ? targetTypeValue.AsTableType.ItemType : targetTypeValue.AsRecordType);
				this.capabilities = this.metadata.Environment.Annotations.GetElementCapability(navigationTarget);
			}

			// Token: 0x06003B0A RID: 15114 RVA: 0x000BFA48 File Offset: 0x000BDC48
			public Tuple<ExpandedNavigationSelectItem, ExpandedColumnFilter> Build(ODataExpandedColumn expandedColumn)
			{
				ExpandedColumnFilter expandedColumnFilter = this.BuildFilter(expandedColumn);
				ExpandedColumnClause expandedColumnClause = new SelectExpandClauseBuilder(this.metadata, expandedColumnFilter.Source, this.targetItemTypeValue, this.navigationTarget, this.iterator).Build(expandedColumn.FieldsToProject, expandedColumn.InnerExpandedColumns);
				if (expandedColumnClause.Filter != null)
				{
					expandedColumnFilter = expandedColumnFilter.AppendOuterFilter(expandedColumnClause.Filter);
				}
				return new Tuple<ExpandedNavigationSelectItem, ExpandedColumnFilter>(new ExpandedNavigationSelectItem(new ODataExpandPath(new ODataPathSegment[]
				{
					new NavigationPropertySegment(this.navigationProperty, this.navigationTarget)
				}), this.navigationTarget, expandedColumnClause.SelectExpandClause, expandedColumnFilter.InnerFilterClause, null, null, null, null, null, null), expandedColumnFilter);
			}

			// Token: 0x06003B0B RID: 15115 RVA: 0x000BFB04 File Offset: 0x000BDD04
			private ExpandedColumnFilter BuildFilter(ODataExpandedColumn expandedColumn)
			{
				FilterClause filterClause = null;
				SingleValueNode singleValueNode = null;
				CollectionNavigationNode collectionNavigationNode = null;
				ResourceRangeVariable resourceRangeVariable = null;
				ResourceRangeVariableReferenceNode resourceRangeVariableReferenceNode = null;
				SingleNavigationNode singleNavigationNode = null;
				EdmEntityTypeReference edmEntityTypeReference = new EdmEntityTypeReference(this.metadata.EntityType, false);
				ResourceRangeVariable resourceRangeVariable2 = new ResourceRangeVariable("$it", edmEntityTypeReference, this.metadata.NavigationSource);
				ResourceRangeVariableReferenceNode resourceRangeVariableReferenceNode2 = new ResourceRangeVariableReferenceNode("$it", resourceRangeVariable2);
				bool flag = this.navigationProperty.TargetMultiplicity() == Microsoft.OData.Edm.EdmMultiplicity.One || this.navigationProperty.TargetMultiplicity() == Microsoft.OData.Edm.EdmMultiplicity.ZeroOrOne;
				if (!flag)
				{
					collectionNavigationNode = new CollectionNavigationNode(this.parentSource ?? resourceRangeVariableReferenceNode2, this.navigationProperty, null);
					resourceRangeVariable = this.iterator.New(SelectExpandClauseBuilder.GetEdmTypeReference(this.navigationTarget), collectionNavigationNode);
					resourceRangeVariableReferenceNode = new ResourceRangeVariableReferenceNode(resourceRangeVariable.Name, resourceRangeVariable);
				}
				else
				{
					singleNavigationNode = new SingleNavigationNode(this.parentSource ?? resourceRangeVariableReferenceNode2, this.navigationProperty, null);
				}
				if (expandedColumn.SelectRowsCondition != null && this.capabilities.SupportsFilter)
				{
					Microsoft.OData.Edm.IEdmType edmType = this.navigationTarget.Type.AsElementType();
					TypeValue typeValue = this.metadata.Environment.ConvertType(edmType);
					if (typeValue.IsRecordType)
					{
						RecordTypeValue recordType = typeValue.AsRecordType;
						ODataExpandPath odataExpandPath = new ODataExpandPath(new ODataPathSegment[]
						{
							new NavigationPropertySegment(this.navigationProperty, this.navigationTarget)
						});
						Func<int, bool> <>9__2;
						Func<QueryExpression, bool> func = delegate(QueryExpression node)
						{
							Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
							Func<int, bool> func2;
							if ((func2 = <>9__2) == null)
							{
								func2 = (<>9__2 = (int column) => this.capabilities.CanFilter(EdmNameEncoder.Encode(recordType.Fields.Keys[column])));
							}
							return node.AllAccess(deny, func2);
						};
						FunctionValue functionValue;
						FunctionValue functionValue2;
						if (SelectRowsQuery.TryGetAndAdjustConditions(recordType, recordType.Fields.Keys, expandedColumn.SelectRowsCondition, func, (QueryExpression e) => e, out functionValue, out functionValue2))
						{
							QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(recordType, functionValue);
							if (flag)
							{
								try
								{
									singleValueNode = new ODataQueryExpressionVisitor(this.metadata.Environment, singleNavigationNode, recordType).Compile(queryExpression).Node;
									goto IL_027C;
								}
								catch (NotSupportedException)
								{
									goto IL_027C;
								}
							}
							try
							{
								filterClause = new FilterClause(new ODataQueryExpressionVisitor(this.metadata.Environment, odataExpandPath.ImplicitResourceRangeVariableReferenceNode(), recordType).Compile(queryExpression).Node, odataExpandPath.ImplicitResourceRangeVariable());
							}
							catch (NotSupportedException)
							{
							}
							try
							{
								SingleValueNode node2 = new ODataQueryExpressionVisitor(this.metadata.Environment, resourceRangeVariableReferenceNode, recordType).Compile(queryExpression).Node;
								singleValueNode = new AnyNode(new Collection<RangeVariable> { resourceRangeVariable }, resourceRangeVariable)
								{
									Body = node2,
									Source = collectionNavigationNode
								};
							}
							catch (NotSupportedException)
							{
							}
						}
					}
				}
				IL_027C:
				if (flag)
				{
					singleValueNode = ODataExpression.AppendNullRowsExprToExpandedRecordFilter(singleValueNode, singleNavigationNode);
					return new ExpandedRecordColumnFilter(resourceRangeVariable2, singleValueNode, singleNavigationNode);
				}
				singleValueNode = ODataExpression.AppendNullRowsExprToExpandedTableFilter(singleValueNode, collectionNavigationNode);
				return new ExpandedTableColumnFilter(resourceRangeVariable2, resourceRangeVariable, resourceRangeVariableReferenceNode, filterClause, singleValueNode, collectionNavigationNode);
			}

			// Token: 0x04001EA7 RID: 7847
			private readonly ODataQueryMetadata metadata;

			// Token: 0x04001EA8 RID: 7848
			private readonly SingleResourceNode parentSource;

			// Token: 0x04001EA9 RID: 7849
			private readonly IterationRangeVariable iterator;

			// Token: 0x04001EAA RID: 7850
			private readonly Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty;

			// Token: 0x04001EAB RID: 7851
			private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationTarget;

			// Token: 0x04001EAC RID: 7852
			private readonly RecordTypeValue targetItemTypeValue;

			// Token: 0x04001EAD RID: 7853
			private readonly Capabilities capabilities;
		}

		// Token: 0x020007FF RID: 2047
		private class NavigationPropertyMetadata
		{
			// Token: 0x06003B12 RID: 15122 RVA: 0x000BFE54 File Offset: 0x000BE054
			public NavigationPropertyMetadata(Microsoft.OData.Edm.IEdmNavigationProperty property, Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
			{
				this.property = property;
				this.navigationSource = navigationSource;
			}

			// Token: 0x170013B0 RID: 5040
			// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000BFE6A File Offset: 0x000BE06A
			public Microsoft.OData.Edm.IEdmNavigationProperty Property
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x170013B1 RID: 5041
			// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000BFE72 File Offset: 0x000BE072
			public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource
			{
				get
				{
					return this.navigationSource;
				}
			}

			// Token: 0x04001EB3 RID: 7859
			private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

			// Token: 0x04001EB4 RID: 7860
			private readonly Microsoft.OData.Edm.IEdmNavigationProperty property;
		}
	}
}
