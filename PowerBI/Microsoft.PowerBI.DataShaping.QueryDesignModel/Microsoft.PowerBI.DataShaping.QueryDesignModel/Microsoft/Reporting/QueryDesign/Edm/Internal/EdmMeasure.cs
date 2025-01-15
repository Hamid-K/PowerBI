using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020A RID: 522
	public sealed class EdmMeasure : EdmProperty
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x00043034 File Offset: 0x00041234
		internal EdmMeasure(EdmProperty edmProperty, ConceptualPrimitiveResultType conceptualType, StructuralType declaringType, XElement extensionElem)
			: base(edmProperty, conceptualType, declaringType, extensionElem)
		{
			this._isSimpleMeasure = extensionElem.GetBooleanAttributeOrDefault(Extensions.IsSimpleMeasureAttr, false);
			this._actualType = extensionElem.GetEnumAttributeOrNull(Extensions.ActualTypeAttr);
			this._culture = extensionElem.GetCultureInfoAttributeOrDefault(Extensions.CultureAttr, null);
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00043084 File Offset: 0x00041284
		public override bool Hidden
		{
			get
			{
				return base.Hidden || this.IsSimpleMeasure;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00043096 File Offset: 0x00041296
		public bool IsSimpleMeasure
		{
			get
			{
				return this._isSimpleMeasure;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x000430A0 File Offset: 0x000412A0
		public override string Caption
		{
			get
			{
				if (this._kpi != null)
				{
					if (this.IsKpiMeasure(KpiMeasureKinds.Status))
					{
						return SR.KpiStatusCaption(this._kpi.Value.Caption);
					}
					if (this.IsKpiMeasure(KpiMeasureKinds.Goal))
					{
						return SR.KpiGoalCaption(this._kpi.Value.Caption);
					}
					if (this.IsKpiMeasure(KpiMeasureKinds.Trend))
					{
						return SR.KpiTrendCaption(this._kpi.Value.Caption);
					}
				}
				return base.Caption;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x00043118 File Offset: 0x00041318
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x00043120 File Offset: 0x00041320
		internal EdmMeasure FormatBy
		{
			get
			{
				return this._formatBy;
			}
			set
			{
				this._formatBy = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00043129 File Offset: 0x00041329
		internal CultureInfo Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00043131 File Offset: 0x00041331
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x00043139 File Offset: 0x00041339
		internal EdmMeasure ApplyCulture
		{
			get
			{
				return this._applyCulture;
			}
			set
			{
				this._applyCulture = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x00043142 File Offset: 0x00041342
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0004314A File Offset: 0x0004134A
		internal Kpi Kpi
		{
			get
			{
				return this._kpi;
			}
			set
			{
				this._kpi = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00043153 File Offset: 0x00041353
		public override IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				if (this._kpi != null && this.IsKpiMeasure(KpiMeasureKinds.AllExceptValue))
				{
					return this._kpi.Value.DisplayFolderParents;
				}
				return base.DisplayFolderParents;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0004317E File Offset: 0x0004137E
		internal override ActualDataType? ActualType
		{
			get
			{
				return this._actualType;
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00043186 File Offset: 0x00041386
		public override bool IsCompatible(AggregateFunction? agg)
		{
			return agg == null;
		}

		// Token: 0x04000D07 RID: 3335
		private readonly bool _isSimpleMeasure;

		// Token: 0x04000D08 RID: 3336
		private readonly ActualDataType? _actualType;

		// Token: 0x04000D09 RID: 3337
		private readonly CultureInfo _culture;

		// Token: 0x04000D0A RID: 3338
		private Kpi _kpi;

		// Token: 0x04000D0B RID: 3339
		private EdmMeasure _formatBy;

		// Token: 0x04000D0C RID: 3340
		private EdmMeasure _applyCulture;
	}
}
