using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BB RID: 955
	public class CategoryAxis
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x0007DC22 File Offset: 0x0007BE22
		public CategoryAxis()
		{
			this.Axis = new Axis();
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0007DC35 File Offset: 0x0007BE35
		public CategoryAxis(Axis axis)
		{
			this.Axis = axis;
		}

		// Token: 0x04000D61 RID: 3425
		public Axis Axis;
	}
}
