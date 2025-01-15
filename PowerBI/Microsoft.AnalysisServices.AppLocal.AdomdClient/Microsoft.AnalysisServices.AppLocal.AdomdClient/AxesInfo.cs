using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006D RID: 109
	public sealed class AxesInfo
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x000242CA File Offset: 0x000224CA
		internal AxesInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000242D9 File Offset: 0x000224D9
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

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000242FA File Offset: 0x000224FA
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

		// Token: 0x04000505 RID: 1285
		private MDDatasetFormatter formatter;

		// Token: 0x04000506 RID: 1286
		private OlapInfoAxisCollection axes;

		// Token: 0x04000507 RID: 1287
		private OlapInfoAxis filterAxis;
	}
}
