using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000014 RID: 20
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal class ConceptualMeasure : ConceptualProperty, IConceptualMeasure, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003333 File Offset: 0x00001533
		internal ConceptualMeasure(ParsedEdmStructuralProperty parsedEdmStructuralProperty, DataType dataType, ConceptualPrimitiveType conceptualDataType, int ordinal, ConceptualMeasureTemplate template, ConceptualDataChangeDetectionMetadata changeDetectionMetadata, string description)
			: base(parsedEdmStructuralProperty, dataType, conceptualDataType, ordinal, description)
		{
			this._template = template;
			this._changeDetectionMetadata = changeDetectionMetadata;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003352 File Offset: 0x00001552
		public IConceptualKpi Kpi
		{
			get
			{
				return this._kpi;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000335A File Offset: 0x0000155A
		public IConceptualMeasure DynamicFormatString
		{
			get
			{
				return this._dynamicFormatString;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003362 File Offset: 0x00001562
		public IConceptualMeasure DynamicFormatCulture
		{
			get
			{
				return this._dynamicFormatCulture;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000336A File Offset: 0x0000156A
		public ConceptualDistributiveAggregateKind? DistributiveAggegate
		{
			get
			{
				return this._distributiveAggregate;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003372 File Offset: 0x00001572
		public IReadOnlyList<IConceptualEntity> DistributiveBy
		{
			get
			{
				return this._distributiveBy;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000337A File Offset: 0x0000157A
		public ConceptualMeasureTemplate Template
		{
			get
			{
				return this._template;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003382 File Offset: 0x00001582
		public ConceptualDataChangeDetectionMetadata ChangeDetectionMetadata
		{
			get
			{
				return this._changeDetectionMetadata;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000338A File Offset: 0x0000158A
		public bool IsVariant
		{
			get
			{
				return this._isVariant;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003392 File Offset: 0x00001592
		internal void CompleteInitialization(IConceptualEntity parentEntity, ConceptualKpi kpi, IConceptualMeasure dynamicFormatString, IConceptualMeasure dynamicFormatCulture, ConceptualDistributiveAggregateKind? distributiveAggregate, IReadOnlyList<IConceptualEntity> distributiveBy, bool isVariant)
		{
			base.CompleteInitialization(parentEntity);
			this._kpi = kpi;
			this._dynamicFormatString = dynamicFormatString;
			this._dynamicFormatCulture = dynamicFormatCulture;
			this._distributiveAggregate = distributiveAggregate;
			this._distributiveBy = distributiveBy;
			this._isVariant = isVariant;
		}

		// Token: 0x04000078 RID: 120
		private readonly ConceptualDataChangeDetectionMetadata _changeDetectionMetadata;

		// Token: 0x04000079 RID: 121
		private readonly ConceptualMeasureTemplate _template;

		// Token: 0x0400007A RID: 122
		private ConceptualKpi _kpi;

		// Token: 0x0400007B RID: 123
		private IConceptualMeasure _dynamicFormatString;

		// Token: 0x0400007C RID: 124
		private IConceptualMeasure _dynamicFormatCulture;

		// Token: 0x0400007D RID: 125
		private ConceptualDistributiveAggregateKind? _distributiveAggregate;

		// Token: 0x0400007E RID: 126
		private IReadOnlyList<IConceptualEntity> _distributiveBy;

		// Token: 0x0400007F RID: 127
		private bool _isVariant;
	}
}
