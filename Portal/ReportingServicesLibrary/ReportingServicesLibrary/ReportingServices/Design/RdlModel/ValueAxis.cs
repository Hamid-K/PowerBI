using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BA RID: 954
	public class ValueAxis
	{
		// Token: 0x06001ECF RID: 7887 RVA: 0x0007DC00 File Offset: 0x0007BE00
		public ValueAxis()
		{
			this.Axis = new Axis();
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0007DC13 File Offset: 0x0007BE13
		public ValueAxis(Axis axis)
		{
			this.Axis = axis;
		}

		// Token: 0x04000D60 RID: 3424
		public Axis Axis;
	}
}
