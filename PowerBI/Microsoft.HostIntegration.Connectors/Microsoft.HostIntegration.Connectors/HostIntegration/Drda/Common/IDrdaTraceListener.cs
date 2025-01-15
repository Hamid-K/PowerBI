using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000812 RID: 2066
	public interface IDrdaTraceListener
	{
		// Token: 0x06004135 RID: 16693
		void TraceEvent(TraceEventType type, int id, string msg);

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004137 RID: 16695
		// (set) Token: 0x06004136 RID: 16694
		int TraceLevel { get; set; }

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004139 RID: 16697
		// (set) Token: 0x06004138 RID: 16696
		long MaxTraceEntries { get; set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x0600413B RID: 16699
		// (set) Token: 0x0600413A RID: 16698
		int MaxTraceFiles { get; set; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600413C RID: 16700
		// (set) Token: 0x0600413D RID: 16701
		string Settings { get; set; }

		// Token: 0x0600413E RID: 16702
		void Close();

		// Token: 0x0600413F RID: 16703
		void Flush();

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004140 RID: 16704
		bool AutoFlush { get; }
	}
}
