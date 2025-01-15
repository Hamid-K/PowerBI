using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E1 RID: 481
	internal sealed class EdmConceptualColumn : EdmConceptualProperty, IConceptualColumn, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x060016CB RID: 5835 RVA: 0x0003EBCC File Offset: 0x0003CDCC
		internal EdmConceptualColumn(EdmProperty property)
			: base(property)
		{
			EdmField edmField = property as EdmField;
			this._defaultAggregate = EdmConceptualColumn.ConvertDefaultAggregate(edmField);
			this._defaultValue = EdmConceptualColumn.ConvertScalarValue(property.DefaultMember);
			this._aggregateBehavior = (edmField.AggregateBehavior ? AggregateBehavior.Default : AggregateBehavior.DiscourageAcrossGroups);
			this._isRowNumber = edmField.IsRowNumber();
			this.ConceptualTypeColumn = edmField.Column;
			this._nullable = edmField.Nullable;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0003EC3A File Offset: 0x0003CE3A
		public ConceptualColumnGrouping Grouping
		{
			get
			{
				return this._grouping;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0003EC42 File Offset: 0x0003CE42
		public IReadOnlyList<IConceptualColumn> OrderByColumns
		{
			get
			{
				return this._orderBy;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0003EC4A File Offset: 0x0003CE4A
		public EdmFieldRelationship Relationship
		{
			get
			{
				return ((EdmField)base.Property).Relationship;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0003EC5C File Offset: 0x0003CE5C
		public ConceptualDefaultAggregate ConceptualDefaultAggregate
		{
			get
			{
				return this._defaultAggregate;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0003EC64 File Offset: 0x0003CE64
		public PrimitiveValue DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0003EC6C File Offset: 0x0003CE6C
		public AggregateBehavior AggregateBehavior
		{
			get
			{
				return this._aggregateBehavior;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0003EC74 File Offset: 0x0003CE74
		public bool IsRowNumber
		{
			get
			{
				return this._isRowNumber;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0003EC7C File Offset: 0x0003CE7C
		public IReadOnlyList<IConceptualVariationSource> VariationSources
		{
			get
			{
				return this._variationSources;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public ConceptualGroupingMetadata GroupingMetadata
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0003EC8B File Offset: 0x0003CE8B
		public ConceptualParameterMetadata ParameterMetadata
		{
			get
			{
				return this._parameterMetadata;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0003EC93 File Offset: 0x0003CE93
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0003EC9B File Offset: 0x0003CE9B
		public ConceptualTypeColumn ConceptualTypeColumn { get; }

		// Token: 0x060016D8 RID: 5848 RVA: 0x0003ECA4 File Offset: 0x0003CEA4
		public bool TryGetVariationSource(string name, out IConceptualVariationSource conceptualVariationSource)
		{
			foreach (IConceptualVariationSource conceptualVariationSource2 in this.VariationSources)
			{
				if (EdmItem.ReferenceNameComparer.Equals(conceptualVariationSource2.Name, name))
				{
					conceptualVariationSource = conceptualVariationSource2;
					return true;
				}
			}
			conceptualVariationSource = null;
			return false;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0003ED0C File Offset: 0x0003CF0C
		internal override void CompleteInitialization(EdmConceptualPropertyInitContext context)
		{
			base.CompleteInitialization(context);
			this._orderBy = this.CreateOrderBy(context);
			this._grouping = this.CreateGrouping(context);
			this._variationSources = this.CreateVariations(context);
			this._parameterMetadata = this.CreateParameterMetadata(context);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0003ED4C File Offset: 0x0003CF4C
		private ConceptualParameterMetadata CreateParameterMetadata(EdmConceptualPropertyInitContext context)
		{
			if (context.MappedMParameters == null)
			{
				return null;
			}
			List<ConceptualMParameter> list = null;
			Dictionary<string, List<ConceptualMParameter>> dictionary;
			if (context.MappedMParameters.TryGetValue(context.Entity.EdmName, out dictionary))
			{
				dictionary.TryGetValue(base.EdmName, out list);
			}
			if (list != null && list.Count > 0)
			{
				EdmField edmField = (EdmField)base.Property;
				bool? flag;
				if (edmField == null)
				{
					flag = null;
				}
				else
				{
					ParameterMetadata parameterMetadata = edmField.ParameterMetadata;
					flag = ((parameterMetadata != null) ? new bool?(parameterMetadata.SupportsMultipleValues) : null);
				}
				bool? flag2 = flag;
				bool valueOrDefault = flag2.GetValueOrDefault();
				string text;
				if (edmField == null)
				{
					text = null;
				}
				else
				{
					ParameterMetadata parameterMetadata2 = edmField.ParameterMetadata;
					text = ((parameterMetadata2 != null) ? parameterMetadata2.SelectAllValue : null);
				}
				string text2 = text;
				return new ConceptualParameterMetadata(list, valueOrDefault, text2);
			}
			return null;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0003EE04 File Offset: 0x0003D004
		private IReadOnlyList<IConceptualVariationSource> CreateVariations(EdmConceptualPropertyInitContext context)
		{
			EdmField edmField = base.Property as EdmField;
			if (edmField.Variations.Count == 0)
			{
				return Util.EmptyReadOnlyCollection<IConceptualVariationSource>();
			}
			List<IConceptualVariationSource> list = new List<IConceptualVariationSource>(edmField.Variations.Count);
			foreach (EdmVariationSource edmVariationSource in edmField.Variations)
			{
				IConceptualEntity conceptualEntity = context.Entity;
				IConceptualNavigationProperty conceptualNavigationProperty = null;
				IConceptualHierarchy conceptualHierarchy = null;
				IConceptualProperty conceptualProperty = null;
				if (edmVariationSource.NavigationPropertyName != null && context.Entity.TryGetNavigationPropertyByEdmName(edmVariationSource.NavigationPropertyName, out conceptualNavigationProperty))
				{
					conceptualEntity = conceptualNavigationProperty.TargetEntity;
				}
				if (edmVariationSource.DefaultHierarchyName != null)
				{
					conceptualEntity.TryGetHierarchyByEdmName(edmVariationSource.DefaultHierarchyName, out conceptualHierarchy);
				}
				if (edmVariationSource.DefaultPropertyName != null)
				{
					conceptualEntity.TryGetPropertyByEdmName(edmVariationSource.DefaultPropertyName, out conceptualProperty);
				}
				list.Add(new EdmConceptualVariationSource(edmVariationSource.ReferenceName, edmVariationSource.IsDefault, conceptualNavigationProperty, conceptualHierarchy, conceptualProperty));
			}
			return list;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0003EF08 File Offset: 0x0003D108
		private static ConceptualDefaultAggregate ConvertDefaultAggregate(EdmField edmField)
		{
			if (edmField != null)
			{
				AggregateFunction? defaultAggregateFunction = edmField.DefaultAggregateFunction;
				if (defaultAggregateFunction == null)
				{
					return ConceptualDefaultAggregate.None;
				}
				switch (defaultAggregateFunction.Value)
				{
				case AggregateFunction.Sum:
					return ConceptualDefaultAggregate.Sum;
				case AggregateFunction.Average:
					return ConceptualDefaultAggregate.Average;
				case AggregateFunction.Count:
					return ConceptualDefaultAggregate.Count;
				case AggregateFunction.DistinctCount:
					return ConceptualDefaultAggregate.DistinctCount;
				case AggregateFunction.Min:
					return ConceptualDefaultAggregate.Min;
				case AggregateFunction.Max:
					return ConceptualDefaultAggregate.Max;
				}
			}
			return ConceptualDefaultAggregate.Default;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0003EF60 File Offset: 0x0003D160
		private IReadOnlyList<IConceptualColumn> CreateOrderBy(EdmConceptualPropertyInitContext context)
		{
			EdmField edmField = base.Property as EdmField;
			if (edmField == null)
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			EdmFieldInstance edmFieldInstance = context.EdmEntitySet.FieldInstance(edmField);
			List<IConceptualColumn> list = new List<IConceptualColumn>();
			foreach (EdmFieldInstance edmFieldInstance2 in edmFieldInstance.GetOrderByFieldInstances())
			{
				IConceptualColumn property = context.GetProperty<IConceptualColumn>(edmFieldInstance2.Field);
				if (property != null)
				{
					list.Add(property);
				}
			}
			return list;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0003EFF0 File Offset: 0x0003D1F0
		private ConceptualColumnGrouping CreateGrouping(EdmConceptualPropertyInitContext context)
		{
			EdmField edmField = base.Property as EdmField;
			if (edmField == null)
			{
				return null;
			}
			EdmFieldGrouping grouping = edmField.Grouping;
			return new ConceptualColumnGrouping(this.ConvertIdentity(grouping.Identity), this.GetColumns(context, grouping.IdentityFields), this.GetColumns(context, grouping.QueryGroupFields), this.GetColumns(context, grouping.GroupByFields), grouping.IsQueryGroupOnEntityKey);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0003F053 File Offset: 0x0003D253
		private GroupingIdentity ConvertIdentity(GroupingIdentity groupingIdentity)
		{
			switch (groupingIdentity)
			{
			case GroupingIdentity.EntityKey:
				return GroupingIdentity.EntityKey;
			case GroupingIdentity.Fields:
				return GroupingIdentity.Properties;
			}
			return GroupingIdentity.Value;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0003F070 File Offset: 0x0003D270
		private IReadOnlyList<IConceptualColumn> GetColumns(EdmConceptualPropertyInitContext context, IReadOnlyList<EdmField> edmFields)
		{
			if (edmFields == null || edmFields.Count == 0)
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			List<IConceptualColumn> list = new List<IConceptualColumn>(edmFields.Count);
			foreach (EdmField edmField in edmFields)
			{
				list.Add(context.GetProperty<IConceptualColumn>(edmField));
			}
			return list;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0003F0DC File Offset: 0x0003D2DC
		private static PrimitiveValue ConvertScalarValue(ScalarValue? defaultMember)
		{
			if (defaultMember == null)
			{
				return null;
			}
			ScalarValue value = defaultMember.Value;
			if (value == ScalarValue.Null)
			{
				return PrimitiveValue.Null;
			}
			if (value.IsOfType<string>())
			{
				return value.CastValue<string>();
			}
			if (value.IsOfType<decimal>())
			{
				return value.CastValue<decimal>();
			}
			if (value.IsOfType<double>())
			{
				return value.CastValue<double>();
			}
			if (value.IsOfType<long>())
			{
				return value.CastValue<long>();
			}
			if (value.IsOfType<bool>())
			{
				return value.CastValue<bool>();
			}
			if (value.IsOfType<DateTime>())
			{
				return value.CastValue<DateTime>();
			}
			return null;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0003F194 File Offset: 0x0003D394
		public override string ToString()
		{
			IConceptualEntity entity = base.Entity;
			string text;
			if (entity == null)
			{
				text = null;
			}
			else
			{
				IConceptualSchema schema = entity.Schema;
				text = ((schema != null) ? schema.SchemaId : null);
			}
			string text2 = text ?? string.Empty;
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = "'" + text2 + "'.";
			}
			string[] array = new string[7];
			array[0] = "EdmConceptualColumn : ";
			array[1] = text2;
			array[2] = "'";
			int num = 3;
			IConceptualEntity entity2 = base.Entity;
			array[num] = ((entity2 != null) ? entity2.Name : null);
			array[4] = "'[";
			array[5] = base.Name;
			array[6] = "]";
			return string.Concat(array);
		}

		// Token: 0x04000C3B RID: 3131
		private readonly ConceptualDefaultAggregate _defaultAggregate;

		// Token: 0x04000C3C RID: 3132
		private readonly AggregateBehavior _aggregateBehavior;

		// Token: 0x04000C3D RID: 3133
		private readonly PrimitiveValue _defaultValue;

		// Token: 0x04000C3E RID: 3134
		private readonly bool _isRowNumber;

		// Token: 0x04000C3F RID: 3135
		private IReadOnlyList<IConceptualColumn> _orderBy;

		// Token: 0x04000C40 RID: 3136
		private ConceptualColumnGrouping _grouping;

		// Token: 0x04000C41 RID: 3137
		private IReadOnlyList<IConceptualVariationSource> _variationSources;

		// Token: 0x04000C42 RID: 3138
		private ConceptualParameterMetadata _parameterMetadata;

		// Token: 0x04000C43 RID: 3139
		private readonly bool _nullable;
	}
}
