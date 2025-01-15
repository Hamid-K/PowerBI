using System;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200032D RID: 813
	public class ReportHistorySnapshot
	{
		// Token: 0x06001B64 RID: 7012 RVA: 0x0006FBA0 File Offset: 0x0006DDA0
		public ReportHistorySnapshot()
		{
			this.HistoryID = null;
			this.CreationDate = DateTime.MinValue;
			this.Size = 0;
		}

		// Token: 0x04000AF9 RID: 2809
		public string HistoryID;

		// Token: 0x04000AFA RID: 2810
		public DateTime CreationDate;

		// Token: 0x04000AFB RID: 2811
		public int Size;
	}
}
