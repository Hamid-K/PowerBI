using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003EB RID: 1003
	public sealed class LineItem : ReportItem
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0007EE3C File Offset: 0x0007D03C
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x0007EE44 File Offset: 0x0007D044
		public new Unit Width
		{
			get
			{
				return base.RawWidth;
			}
			set
			{
				Utils.ValidateValueRange("Width", value, new Unit(-Constants.MaxUnits.FPixels), Constants.MaxUnits);
				base.RawWidth = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0007EE85 File Offset: 0x0007D085
		// (set) Token: 0x06001FDD RID: 8157 RVA: 0x0007EE90 File Offset: 0x0007D090
		public new Unit Height
		{
			get
			{
				return base.RawHeight;
			}
			set
			{
				Utils.ValidateValueRange("Height", value, new Unit(-Constants.MaxUnits.FPixels), Constants.MaxUnits);
				base.RawHeight = value;
			}
		}
	}
}
