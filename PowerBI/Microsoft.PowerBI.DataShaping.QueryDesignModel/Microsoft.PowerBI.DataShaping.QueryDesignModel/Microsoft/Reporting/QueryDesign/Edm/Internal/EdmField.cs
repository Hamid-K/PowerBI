using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.Utils;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F2 RID: 498
	public sealed class EdmField : EdmProperty
	{
		// Token: 0x060017B8 RID: 6072 RVA: 0x00041558 File Offset: 0x0003F758
		internal EdmField(EdmProperty edmProperty, ConceptualPrimitiveResultType conceptualType, StructuralType declaringType, XElement extensionElem)
			: base(edmProperty, conceptualType, declaringType, extensionElem)
		{
			if (base.ConceptualType.ConceptualDataType == ConceptualPrimitiveType.Binary)
			{
				this._contents = new FieldContentType?(FieldContentType.Image);
			}
			else
			{
				string stringAttributeOrDefault = extensionElem.GetStringAttributeOrDefault(Extensions.ContentsAttr, null);
				this._contents = Extensions.GetEnumOrNull<FieldContentType>(stringAttributeOrDefault);
				if (this._contents != null)
				{
					FieldContentType? contents = this._contents;
					FieldContentType fieldContentType = FieldContentType.Color;
					if (!((contents.GetValueOrDefault() == fieldContentType) & (contents != null)))
					{
						goto IL_0076;
					}
				}
				this._customContents = stringAttributeOrDefault;
			}
			IL_0076:
			this._defaultAggFunc = extensionElem.GetEnumAttributeOrNull(Extensions.DefaultAggregateFunctionAttr);
			string stringAttributeOrDefault2 = extensionElem.GetStringAttributeOrDefault(Extensions.GroupingBehaviorAttr, null);
			this._grouping = new EdmFieldGrouping(this, stringAttributeOrDefault2);
			this._aggregateBehavior = extensionElem.GetBooleanAttributeOrDefault(Extensions.AggregateBehaviorAttr, true);
			this._orderBy = Microsoft.Reporting.Util.EmptyReadOnlyCollection<EdmField>();
			this._filterNullsBy = Microsoft.Reporting.Util.EmptyReadOnlyCollection<EdmField>();
			this._relationship = new EdmFieldRelationship(this);
			XElement elementOrNull = extensionElem.GetElementOrNull(Extensions.VariationsElem);
			if (elementOrNull != null)
			{
				this._variations = (from elem in elementOrNull.Elements(Extensions.VariationElem)
					select new EdmVariationSource(elem)).ToReadOnlyCollection<EdmVariationSource>();
			}
			else
			{
				this._variations = Microsoft.Reporting.Util.EmptyReadOnlyCollection<EdmVariationSource>();
			}
			this._conceptualColumn = base.ConceptualType.Column(base.ReferenceName, base.Name);
			this._parameterMetadata = CsdlParserUtil.GetParameterMetadataFromExtendedProperty(extensionElem);
			this._nullable = edmProperty.Nullable;
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000416CA File Offset: 0x0003F8CA
		public override bool Hidden
		{
			get
			{
				return this.IsHidden();
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000416D4 File Offset: 0x0003F8D4
		internal bool IsHidden()
		{
			FieldContentType? contents = this._contents;
			FieldContentType fieldContentType = FieldContentType.RowNumber;
			return ((contents.GetValueOrDefault() == fieldContentType) & (contents != null)) || (this._grouping.IsIdentityOnEntityKey && !this.DeclaringTypeHasStableKey()) || base.Hidden;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0004171D File Offset: 0x0003F91D
		public FieldContentType? Contents
		{
			get
			{
				return this._contents;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00041725 File Offset: 0x0003F925
		public string CustomContents
		{
			get
			{
				return this._customContents;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00041730 File Offset: 0x0003F930
		public AggregateFunction? DefaultAggregateFunction
		{
			get
			{
				EdmField.DefaultAggregateFunctionEnum? defaultAggregateFunctionEnum = this._defaultAggFunc;
				EdmField.DefaultAggregateFunctionEnum defaultAggregateFunctionEnum2 = EdmField.DefaultAggregateFunctionEnum.Count;
				if (!((defaultAggregateFunctionEnum.GetValueOrDefault() == defaultAggregateFunctionEnum2) & (defaultAggregateFunctionEnum != null)) && this._grouping.IsIdentityOnEntityKey)
				{
					return null;
				}
				if (this._defaultAggFunc == null)
				{
					ConceptualPrimitiveType? primitiveTypeKind = base.ConceptualType.GetPrimitiveTypeKind();
					if (primitiveTypeKind != null)
					{
						ConceptualPrimitiveType value = primitiveTypeKind.Value;
						if (value - ConceptualPrimitiveType.Decimal <= 2)
						{
							return new AggregateFunction?(AggregateFunction.Sum);
						}
					}
					return null;
				}
				defaultAggregateFunctionEnum = this._defaultAggFunc;
				defaultAggregateFunctionEnum2 = EdmField.DefaultAggregateFunctionEnum.None;
				if ((defaultAggregateFunctionEnum.GetValueOrDefault() == defaultAggregateFunctionEnum2) & (defaultAggregateFunctionEnum != null))
				{
					return null;
				}
				return new AggregateFunction?((AggregateFunction)this._defaultAggFunc.Value);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000417EF File Offset: 0x0003F9EF
		public EdmFieldGrouping Grouping
		{
			get
			{
				return this._grouping;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000417F7 File Offset: 0x0003F9F7
		public EdmFieldRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000417FF File Offset: 0x0003F9FF
		public bool AggregateBehavior
		{
			get
			{
				return this._aggregateBehavior;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00041807 File Offset: 0x0003FA07
		internal ReadOnlyCollection<EdmField> OrderByFields
		{
			get
			{
				return this._orderBy;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0004180F File Offset: 0x0003FA0F
		internal ReadOnlyCollection<EdmField> FilterNullsByFields
		{
			get
			{
				return this._filterNullsBy;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00041817 File Offset: 0x0003FA17
		internal EdmField ColorByField
		{
			get
			{
				if (this._colorBy == null)
				{
					this._colorBy = new EdmFieldColorBy(this);
				}
				return this._colorBy.ColorByField;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00041838 File Offset: 0x0003FA38
		internal ReadOnlyCollection<EdmVariationSource> Variations
		{
			get
			{
				return this._variations;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00041840 File Offset: 0x0003FA40
		internal ParameterMetadata ParameterMetadata
		{
			get
			{
				return this._parameterMetadata;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00041848 File Offset: 0x0003FA48
		internal bool Nullable
		{
			get
			{
				return this._nullable;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00041850 File Offset: 0x0003FA50
		public override bool IsCompatible(AggregateFunction? agg)
		{
			if (agg != null)
			{
				if (this._grouping.IsIdentityOnEntityKey)
				{
					AggregateFunction? aggregateFunction = agg;
					AggregateFunction aggregateFunction2 = AggregateFunction.Count;
					if (!((aggregateFunction.GetValueOrDefault() == aggregateFunction2) & (aggregateFunction != null)))
					{
						return false;
					}
				}
				return EdmField.CanInvokeFunctionOnType(this.Column.PrimitiveType, agg.Value);
			}
			return true;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000418A8 File Offset: 0x0003FAA8
		private static bool CanInvokeFunctionOnType(ConceptualPrimitiveResultType primitiveType, AggregateFunction function)
		{
			if (primitiveType.IsDateTime() && (function == AggregateFunction.Max || function == AggregateFunction.Min))
			{
				return false;
			}
			string text = CoreFunctionLibrary.FunctionNameFromAggregateFunction(function);
			return CoreFunctionLibrary.Instance.CanInvokeFunction(text, new ConceptualResultType[] { ConceptualCollectionType.FromPrimitive(primitiveType) });
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000418E8 File Offset: 0x0003FAE8
		internal bool CanGroupOnValue()
		{
			FieldContentType? contents = this.Contents;
			FieldContentType fieldContentType = FieldContentType.Image;
			return !((contents.GetValueOrDefault() == fieldContentType) & (contents != null)) && base.ConceptualType.GetPrimitiveTypeKind().Value != ConceptualPrimitiveType.Binary;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0004192E File Offset: 0x0003FB2E
		internal void InitializeDeferredModelProperties(ReadOnlyCollection<EdmField> groupByFields, ReadOnlyCollection<EdmField> orderByFields, ReadOnlyCollection<EdmField> filterNullsByFields, ReadOnlyCollection<EdmField> relatedToFields)
		{
			if (groupByFields != null)
			{
				this._grouping.InitializeModelGroupBy(groupByFields);
			}
			if (orderByFields != null)
			{
				this._orderBy = orderByFields;
			}
			if (filterNullsByFields != null)
			{
				this._filterNullsBy = filterNullsByFields;
			}
			if (relatedToFields != null)
			{
				this._relationship.Initialize(relatedToFields);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00041964 File Offset: 0x0003FB64
		internal void CompleteInitialization()
		{
			this._relationship.CompleteInitialization();
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00041971 File Offset: 0x0003FB71
		internal ConceptualTypeColumn Column
		{
			get
			{
				return this._conceptualColumn;
			}
		}

		// Token: 0x04000CB3 RID: 3251
		private readonly FieldContentType? _contents;

		// Token: 0x04000CB4 RID: 3252
		private readonly string _customContents;

		// Token: 0x04000CB5 RID: 3253
		private readonly EdmField.DefaultAggregateFunctionEnum? _defaultAggFunc;

		// Token: 0x04000CB6 RID: 3254
		private readonly EdmFieldGrouping _grouping;

		// Token: 0x04000CB7 RID: 3255
		private readonly bool _aggregateBehavior;

		// Token: 0x04000CB8 RID: 3256
		private readonly ReadOnlyCollection<EdmVariationSource> _variations;

		// Token: 0x04000CB9 RID: 3257
		private readonly EdmFieldRelationship _relationship;

		// Token: 0x04000CBA RID: 3258
		private ReadOnlyCollection<EdmField> _orderBy;

		// Token: 0x04000CBB RID: 3259
		private ReadOnlyCollection<EdmField> _filterNullsBy;

		// Token: 0x04000CBC RID: 3260
		private EdmFieldColorBy _colorBy;

		// Token: 0x04000CBD RID: 3261
		private ConceptualTypeColumn _conceptualColumn;

		// Token: 0x04000CBE RID: 3262
		private ParameterMetadata _parameterMetadata;

		// Token: 0x04000CBF RID: 3263
		private readonly bool _nullable;

		// Token: 0x020003C2 RID: 962
		private enum DefaultAggregateFunctionEnum
		{
			// Token: 0x0400139D RID: 5021
			None = 65535,
			// Token: 0x0400139E RID: 5022
			Sum = 0,
			// Token: 0x0400139F RID: 5023
			Average,
			// Token: 0x040013A0 RID: 5024
			Count,
			// Token: 0x040013A1 RID: 5025
			DistinctCount,
			// Token: 0x040013A2 RID: 5026
			Min,
			// Token: 0x040013A3 RID: 5027
			Max
		}
	}
}
