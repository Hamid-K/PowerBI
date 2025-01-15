using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000013 RID: 19
	[ImmutableObject(true)]
	internal sealed class ConceptualKpi : IConceptualKpi
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000032CE File Offset: 0x000014CE
		internal ConceptualKpi(string statusGraphic, string trendGraphic, ConceptualMeasure status, ConceptualMeasure goal, ConceptualMeasure trend, string description)
		{
			this._statusGraphic = statusGraphic;
			this._trendGraphic = trendGraphic;
			this._status = status;
			this._goal = goal;
			this._trend = trend;
			this._description = description;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003303 File Offset: 0x00001503
		public string StatusGraphic
		{
			get
			{
				return this._statusGraphic;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000330B File Offset: 0x0000150B
		public string TrendGraphic
		{
			get
			{
				return this._trendGraphic;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003313 File Offset: 0x00001513
		public IConceptualMeasure Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000331B File Offset: 0x0000151B
		public IConceptualMeasure Goal
		{
			get
			{
				return this._goal;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003323 File Offset: 0x00001523
		public IConceptualMeasure Trend
		{
			get
			{
				return this._trend;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000332B File Offset: 0x0000152B
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000072 RID: 114
		private readonly string _statusGraphic;

		// Token: 0x04000073 RID: 115
		private readonly string _trendGraphic;

		// Token: 0x04000074 RID: 116
		private readonly string _description;

		// Token: 0x04000075 RID: 117
		private readonly IConceptualMeasure _status;

		// Token: 0x04000076 RID: 118
		private readonly IConceptualMeasure _goal;

		// Token: 0x04000077 RID: 119
		private readonly IConceptualMeasure _trend;
	}
}
