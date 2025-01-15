using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000091 RID: 145
	internal sealed class ChartSeries
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000CD33 File Offset: 0x0000AF33
		internal ChartSeries(string type, string subtype, List<ChartDataPoint> dataPoints, bool isLabelsVisible, ChartDataLabelPositions labelsPosition, MapBackdropType mapBackdrop)
		{
			this._type = type;
			this._subtype = subtype;
			this._dataPoints = dataPoints;
			this._isLabelsVisible = isLabelsVisible;
			this._labelsPosition = labelsPosition;
			this._mapBackdrop = mapBackdrop;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000CD68 File Offset: 0x0000AF68
		internal string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000CD70 File Offset: 0x0000AF70
		internal string Subtype
		{
			get
			{
				return this._subtype;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000CD78 File Offset: 0x0000AF78
		internal MapBackdropType MapBackdrop
		{
			get
			{
				return this._mapBackdrop;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000CD80 File Offset: 0x0000AF80
		internal List<ChartDataPoint> DataPoints
		{
			get
			{
				return this._dataPoints;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000CD88 File Offset: 0x0000AF88
		internal bool IsLabelsVisible
		{
			get
			{
				return this._isLabelsVisible;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000CD90 File Offset: 0x0000AF90
		internal ChartDataLabelPositions LabelsPosition
		{
			get
			{
				return this._labelsPosition;
			}
		}

		// Token: 0x040001E7 RID: 487
		private readonly string _type;

		// Token: 0x040001E8 RID: 488
		private readonly string _subtype;

		// Token: 0x040001E9 RID: 489
		private readonly List<ChartDataPoint> _dataPoints;

		// Token: 0x040001EA RID: 490
		private readonly bool _isLabelsVisible;

		// Token: 0x040001EB RID: 491
		private readonly ChartDataLabelPositions _labelsPosition;

		// Token: 0x040001EC RID: 492
		private readonly MapBackdropType _mapBackdrop;
	}
}
