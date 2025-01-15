using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000075 RID: 117
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	public sealed class ConceptualMeasure : ConceptualProperty, IConceptualMeasure, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>, IBuiltConceptualType
	{
		// Token: 0x0600027A RID: 634 RVA: 0x000072F0 File Offset: 0x000054F0
		private ConceptualMeasure(ConceptualProperty.ConceptualPropertyInfo propertyInfo)
			: base(propertyInfo)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000072FC File Offset: 0x000054FC
		public static ConceptualMeasure.Builder Create(string name, int ordinal, DataType type, ConceptualPrimitiveType conceptualDataType, ConceptualDataCategory conceptualDataCategory = ConceptualDataCategory.None, string edmName = null, string displayName = null, string description = null, bool isHidden = false, bool isPrivate = false, string formatString = null, bool isVariant = false, string stableName = null, bool isStable = true)
		{
			return new ConceptualMeasure.Builder(new ConceptualMeasure(new ConceptualProperty.ConceptualPropertyInfo(name, edmName, displayName, description, type, isHidden, isPrivate, formatString, conceptualDataType, conceptualDataCategory, ordinal, stableName, isStable)));
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000732E File Offset: 0x0000552E
		public IConceptualKpi Kpi
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00007331 File Offset: 0x00005531
		public IConceptualMeasure DynamicFormatString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00007334 File Offset: 0x00005534
		public IConceptualMeasure DynamicFormatCulture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007337 File Offset: 0x00005537
		public ConceptualMeasureTemplate Template
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000733A File Offset: 0x0000553A
		public ConceptualDataChangeDetectionMetadata ChangeDetectionMetadata
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00007340 File Offset: 0x00005540
		public ConceptualDistributiveAggregateKind? DistributiveAggegate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00007356 File Offset: 0x00005556
		public IReadOnlyList<IConceptualEntity> DistributiveBy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00007359 File Offset: 0x00005559
		public bool IsVariant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000735C File Offset: 0x0000555C
		public bool Equals(IConceptualMeasure other)
		{
			return this == other;
		}

		// Token: 0x020002FB RID: 763
		public sealed class Builder : ConceptualProperty.Builder<ConceptualMeasure>
		{
			// Token: 0x06001934 RID: 6452 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
			internal Builder(ConceptualMeasure conceptualMeasure)
				: base(conceptualMeasure)
			{
			}

			// Token: 0x06001935 RID: 6453 RVA: 0x0002D5F9 File Offset: 0x0002B7F9
			public IConceptualMeasure CompleteInitialization(IConceptualEntity entity)
			{
				return base.CompletePropertyInitialization(entity);
			}
		}
	}
}
