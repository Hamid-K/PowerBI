using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000033 RID: 51
	public abstract class Report
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007D RID: 125
		public abstract string Name { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007E RID: 126
		public abstract string URL { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007F RID: 127
		public abstract DateTime Date { get; }

		// Token: 0x06000080 RID: 128
		public abstract RenderedOutputFile[] Render(string renderFormat, string deviceInfo);
	}
}
