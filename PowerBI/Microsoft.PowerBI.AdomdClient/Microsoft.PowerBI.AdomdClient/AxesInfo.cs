using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006D RID: 109
	public sealed class AxesInfo
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x00023F9A File Offset: 0x0002219A
		internal AxesInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00023FA9 File Offset: 0x000221A9
		public OlapInfoAxisCollection Axes
		{
			get
			{
				if (this.axes == null)
				{
					this.axes = new OlapInfoAxisCollection(this.formatter);
				}
				return this.axes;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00023FCA File Offset: 0x000221CA
		public OlapInfoAxis FilterAxis
		{
			get
			{
				if (this.filterAxis == null && this.formatter.FilterAxis != null)
				{
					this.filterAxis = new OlapInfoAxis(this.formatter.FilterAxis);
				}
				return this.filterAxis;
			}
		}

		// Token: 0x040004F8 RID: 1272
		private MDDatasetFormatter formatter;

		// Token: 0x040004F9 RID: 1273
		private OlapInfoAxisCollection axes;

		// Token: 0x040004FA RID: 1274
		private OlapInfoAxis filterAxis;
	}
}
