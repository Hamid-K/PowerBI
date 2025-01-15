using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200099F RID: 2463
	public class InformixDateTimeInfo
	{
		// Token: 0x06004C70 RID: 19568 RVA: 0x00131DEA File Offset: 0x0012FFEA
		public InformixDateTimeInfo(string name, short startPoint, short endPoint, short startSubstr, short endSubstr)
		{
			this.name = name;
			this.startPoint = startPoint;
			this.endPoint = endPoint;
			this.startSubstr = startSubstr;
			this.endSubstr = endSubstr;
			this.baseName = ((startPoint > 13) ? "FRACTION" : name);
		}

		// Token: 0x04003C50 RID: 15440
		public string name;

		// Token: 0x04003C51 RID: 15441
		public string baseName;

		// Token: 0x04003C52 RID: 15442
		public short startPoint;

		// Token: 0x04003C53 RID: 15443
		public short endPoint;

		// Token: 0x04003C54 RID: 15444
		public short startSubstr;

		// Token: 0x04003C55 RID: 15445
		public short endSubstr;
	}
}
