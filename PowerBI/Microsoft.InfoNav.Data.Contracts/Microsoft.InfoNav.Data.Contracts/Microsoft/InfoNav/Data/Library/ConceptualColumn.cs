using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000073 RID: 115
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	public sealed class ConceptualColumn : ConceptualProperty, IConceptualColumn, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>, IBuiltConceptualType
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00007008 File Offset: 0x00005208
		private ConceptualColumn(ConceptualProperty.ConceptualPropertyInfo propertyInfo, bool isRowNumber)
			: base(propertyInfo)
		{
			ConceptualColumn[] array = this.ArrayWrap<ConceptualColumn>();
			this._grouping = new ConceptualColumnGrouping(GroupingIdentity.Value, array, array, Util.EmptyReadOnlyCollection<IConceptualColumn>(), false);
			this._isRowNumber = isRowNumber;
			this.ConceptualTypeColumn = ConceptualPrimitiveResultType.FromPrimitive(propertyInfo.ConceptualDataType).Column(propertyInfo.Name, propertyInfo.EdmName);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007060 File Offset: 0x00005260
		public static ConceptualColumn.Builder Create(string name, int ordinal, string edmName, string displayName, string description, DataType type, bool isHidden, bool isRowNumber, string formatString, ConceptualPrimitiveType conceptualDataType, ConceptualDataCategory conceptualDataCategory, string stableName = null, bool isStable = true, bool isPrivate = false)
		{
			return new ConceptualColumn.Builder(new ConceptualColumn(new ConceptualProperty.ConceptualPropertyInfo(name, edmName, displayName, description, type, isHidden, isPrivate, formatString, conceptualDataType, conceptualDataCategory, ordinal, stableName, isStable), isRowNumber));
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007094 File Offset: 0x00005294
		public ConceptualColumnGrouping Grouping
		{
			get
			{
				return this._grouping;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000709C File Offset: 0x0000529C
		public bool IsRowNumber
		{
			get
			{
				return this._isRowNumber;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000070A4 File Offset: 0x000052A4
		public IReadOnlyList<IConceptualColumn> OrderByColumns
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000070AB File Offset: 0x000052AB
		public ConceptualDefaultAggregate ConceptualDefaultAggregate
		{
			get
			{
				return ConceptualDefaultAggregate.Default;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000070AE File Offset: 0x000052AE
		public PrimitiveValue DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000070B1 File Offset: 0x000052B1
		public AggregateBehavior AggregateBehavior
		{
			get
			{
				return AggregateBehavior.Default;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000070B4 File Offset: 0x000052B4
		public IReadOnlyList<IConceptualVariationSource> VariationSources
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualVariationSource>();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000070BB File Offset: 0x000052BB
		public ConceptualGroupingMetadata GroupingMetadata
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000070BE File Offset: 0x000052BE
		public ConceptualParameterMetadata ParameterMetadata
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000070C1 File Offset: 0x000052C1
		public bool Nullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000070C4 File Offset: 0x000052C4
		public ConceptualTypeColumn ConceptualTypeColumn { get; }

		// Token: 0x06000259 RID: 601 RVA: 0x000070CC File Offset: 0x000052CC
		public bool TryGetVariationSource(string referenceName, out IConceptualVariationSource variationSource)
		{
			variationSource = null;
			return false;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000070D2 File Offset: 0x000052D2
		public bool Equals(IConceptualColumn other)
		{
			return this == other;
		}

		// Token: 0x0400017C RID: 380
		private readonly ConceptualColumnGrouping _grouping;

		// Token: 0x0400017D RID: 381
		private readonly bool _isRowNumber;

		// Token: 0x020002F9 RID: 761
		public sealed class Builder : ConceptualProperty.Builder<ConceptualColumn>
		{
			// Token: 0x0600192C RID: 6444 RVA: 0x0002D341 File Offset: 0x0002B541
			internal Builder(ConceptualColumn conceptualColumn)
				: base(conceptualColumn)
			{
			}

			// Token: 0x0600192D RID: 6445 RVA: 0x0002D34A File Offset: 0x0002B54A
			public IConceptualColumn CompleteInitialization(IConceptualEntity entity)
			{
				return base.CompletePropertyInitialization(entity);
			}
		}
	}
}
