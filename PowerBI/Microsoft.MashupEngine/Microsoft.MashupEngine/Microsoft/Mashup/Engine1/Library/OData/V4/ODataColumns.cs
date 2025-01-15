using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200085A RID: 2138
	internal sealed class ODataColumns
	{
		// Token: 0x06003D90 RID: 15760 RVA: 0x000C856C File Offset: 0x000C676C
		public ODataColumns(RecordTypeValue recordTypeValue, Capabilities capability, Microsoft.OData.Edm.IEdmEntityType entityType, ODataEnvironment environment)
		{
			this.entityType = entityType;
			this.odataEnvironment = environment;
			this.recordTypeValue = recordTypeValue;
			this.names = recordTypeValue.Fields.Keys;
			this.expressions = new ODataExpression[this.names.Length];
			this.types = new IValueReference[this.names.Length];
			this.expandedColumns = EmptyArray<ODataExpandedColumn>.Instance;
			Value value;
			if (recordTypeValue.TryGetMetaField("MoreColumns", out value))
			{
				this.moreColumnsColumnName = value.AsString;
			}
			for (int i = 0; i < this.names.Length; i++)
			{
				TypeValue asType = recordTypeValue.Fields[i]["Type"].AsType;
				this.types[i] = asType;
				this.expressions[i] = new ODataExpression(capability, recordTypeValue, asType, new ColumnAccessQueryExpression(i), entityType, environment, null, null);
			}
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000C8654 File Offset: 0x000C6854
		public ODataColumns Add(ODataColumns added, Capabilities capabilities)
		{
			KeysBuilder keysBuilder = new KeysBuilder(this.names.Length + added.names.Length);
			keysBuilder.Union(this.names);
			keysBuilder.Union(added.names);
			Keys keys = keysBuilder.ToKeys();
			IValueReference[] array = this.types.Concat(added.types).ToArray<IValueReference>();
			RecordTypeValue recordTypeValue = ODataQuery.CreateRecordType(keys, array);
			RecordValue recordValue = RecordValue.New(Keys.New("aggregate"), new Value[] { LogicalValue.New(true) });
			recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, recordValue).AsType.AsRecordType;
			return new ODataColumns(recordTypeValue, capabilities, this.entityType, this.odataEnvironment);
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000C8708 File Offset: 0x000C6908
		private ODataColumns(RecordTypeValue recordType, Keys names, IList<ODataExpression> expressions, IValueReference[] types, IList<ODataExpandedColumn> expandedColumns, string moreColumnsColumnName, Microsoft.OData.Edm.IEdmEntityType entityType, ODataEnvironment environment)
		{
			this.recordTypeValue = recordType;
			this.names = names;
			this.expressions = expressions;
			this.types = types;
			this.expandedColumns = expandedColumns;
			this.moreColumnsColumnName = moreColumnsColumnName;
			this.entityType = entityType;
			this.odataEnvironment = environment;
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000C8758 File Offset: 0x000C6958
		public Keys Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x000C8760 File Offset: 0x000C6960
		public IList<ODataExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000C8768 File Offset: 0x000C6968
		public IValueReference[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x000C8770 File Offset: 0x000C6970
		public RecordTypeValue RecordTypeValue
		{
			get
			{
				return this.recordTypeValue;
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000C8778 File Offset: 0x000C6978
		public ODataColumns SelectColumns(Keys keys)
		{
			int[] array = new int[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.names.IndexOfKey(keys[i]);
			}
			return this.SelectColumns(new ColumnSelection(keys, array));
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000C87C4 File Offset: 0x000C69C4
		public ODataColumns SelectColumns(ColumnSelection columnSelection)
		{
			ODataExpression[] array = new ODataExpression[columnSelection.Keys.Length];
			IValueReference[] array2 = new IValueReference[columnSelection.Keys.Length];
			RecordValue[] array3 = new RecordValue[columnSelection.Keys.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				array[i] = this.expressions[column];
				array2[i] = this.types[column];
				array3[i] = this.recordTypeValue.Fields[column].AsRecord;
			}
			Keys keys = columnSelection.Keys;
			Value[] array4 = array3;
			RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(keys, array4));
			if (!this.recordTypeValue.MetaValue.IsEmpty)
			{
				recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, this.recordTypeValue.MetaValue).AsType.AsRecordType;
			}
			return new ODataColumns(recordTypeValue, columnSelection.Keys, array, array2, this.expandedColumns, this.moreColumnsColumnName, this.entityType, this.odataEnvironment);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000C88D0 File Offset: 0x000C6AD0
		public ODataColumns ExpandColumn(IList<ODataExpandedColumn> expandedColumns)
		{
			return new ODataColumns(this.recordTypeValue, this.names, this.expressions, this.types, expandedColumns, this.moreColumnsColumnName, this.entityType, this.odataEnvironment);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000C8904 File Offset: 0x000C6B04
		public ODataQueryClauses GetQueryClause(bool allColumnsSelected, ODataQueryMetadata metadata, EntityRangeVariableReferenceNode entityRange)
		{
			if (allColumnsSelected && this.expandedColumns.Count == 0)
			{
				return new ODataQueryClauses(null, null);
			}
			List<SelectItem> list = new List<SelectItem>(this.names.Length + this.expandedColumns.Count);
			if (!allColumnsSelected)
			{
				if (this.moreColumnsColumnName != null && this.names.Contains(this.moreColumnsColumnName))
				{
					ODataExpandedColumn odataExpandedColumn = this.expandedColumns.FirstOrDefault((ODataExpandedColumn column) => column.ColumnToExpandName == this.moreColumnsColumnName);
					if (odataExpandedColumn != null)
					{
						string[] array = this.names.Where((string name) => name != this.moreColumnsColumnName).Concat(odataExpandedColumn.FieldsToProject).ToArray<string>();
						list.AddRange(ODataColumns.GetSelectItems(metadata.Environment.EdmModel, metadata.EntityType, array));
					}
				}
				else
				{
					list.AddRange(ODataColumns.GetSelectItems(metadata.Environment.EdmModel, metadata.EntityType, this.names));
				}
			}
			FilterClause filterClause = null;
			Capabilities elementCapability = metadata.Environment.Annotations.GetElementCapability(metadata.NavigationSource);
			if (elementCapability.SupportsExpand)
			{
				IterationRangeVariable iterationRangeVariable = new IterationRangeVariable();
				foreach (ODataExpandedColumn odataExpandedColumn2 in this.expandedColumns)
				{
					Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = metadata.EntityType.FindProperty(odataExpandedColumn2.ColumnToExpandName) as Microsoft.OData.Edm.IEdmNavigationProperty;
					if (edmNavigationProperty != null && elementCapability.CanExpand(odataExpandedColumn2.ColumnToExpandName))
					{
						Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource = metadata.NavigationSource.FindNavigationTarget(edmNavigationProperty);
						ExpandedColumnClause innerSelectExpandClause = odataExpandedColumn2.GetInnerSelectExpandClause(metadata, edmNavigationSource, edmNavigationProperty, null, iterationRangeVariable);
						list.Add(new ExpandedNavigationSelectItem(new ODataExpandPath(new ODataPathSegment[]
						{
							new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource)
						}), edmNavigationSource, innerSelectExpandClause.SelectExpandClause, innerSelectExpandClause.Filter.InnerFilterClause, null, null, null, null, null, null));
						filterClause = ODataColumns.AppendFilter(filterClause, innerSelectExpandClause.Filter.OuterFilterClause, entityRange);
					}
				}
				if (this.RecordTypeValue.MetaValue != null && !this.RecordTypeValue.MetaValue.Keys.Contains("aggregate"))
				{
					foreach (Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty in metadata.NavigationSource.EntityType().Key())
					{
						if (!this.names.Contains(edmStructuralProperty.Name))
						{
							list.Add(new PathSelectItem(new ODataSelectPath(new ODataPathSegment[]
							{
								new OpenPropertySegment(edmStructuralProperty.Name)
							})));
						}
					}
				}
			}
			return new ODataQueryClauses(new SelectExpandClause(list, allColumnsSelected), filterClause);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000C8BD4 File Offset: 0x000C6DD4
		internal static List<SelectItem> GetSelectItems(Microsoft.OData.Edm.IEdmModel model, Microsoft.OData.Edm.IEdmType type, IEnumerable<string> selectedColumns)
		{
			List<SelectItem> list = new List<SelectItem>(selectedColumns.Count<string>());
			foreach (string text in selectedColumns)
			{
				if (model.FindBoundOperations(type).FilterByName(false, text).Count<Microsoft.OData.Edm.IEdmOperation>() == 0)
				{
					list.Add(new PathSelectItem(new ODataSelectPath(new ODataPathSegment[]
					{
						new OpenPropertySegment(text)
					})));
				}
			}
			return list;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000C8C58 File Offset: 0x000C6E58
		internal static FilterClause AppendFilter(FilterClause currentFilter, FilterClause newFilter, EntityRangeVariableReferenceNode entityRange)
		{
			if (currentFilter == null)
			{
				return newFilter;
			}
			if (newFilter == null)
			{
				return currentFilter;
			}
			return new FilterClause(new BinaryOperatorNode(BinaryOperatorKind.And, currentFilter.Expression, newFilter.Expression), entityRange.RangeVariable);
		}

		// Token: 0x04002049 RID: 8265
		private readonly RecordTypeValue recordTypeValue;

		// Token: 0x0400204A RID: 8266
		private readonly IList<ODataExpression> expressions;

		// Token: 0x0400204B RID: 8267
		private readonly Keys names;

		// Token: 0x0400204C RID: 8268
		private readonly IValueReference[] types;

		// Token: 0x0400204D RID: 8269
		private readonly IList<ODataExpandedColumn> expandedColumns;

		// Token: 0x0400204E RID: 8270
		private readonly string moreColumnsColumnName;

		// Token: 0x0400204F RID: 8271
		private readonly Microsoft.OData.Edm.IEdmEntityType entityType;

		// Token: 0x04002050 RID: 8272
		private readonly ODataEnvironment odataEnvironment;
	}
}
