using System;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200032E RID: 814
	public class HistorySnapshot
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x0006FBC1 File Offset: 0x0006DDC1
		public HistorySnapshot()
		{
			this.Id = Guid.Empty;
			this.Snapshot = new ReportHistorySnapshot();
		}

		// Token: 0x04000AFC RID: 2812
		public Guid Id;

		// Token: 0x04000AFD RID: 2813
		public ReportHistorySnapshot Snapshot;
	}
}
