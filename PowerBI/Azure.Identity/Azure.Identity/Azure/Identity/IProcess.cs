using System;
using System.Diagnostics;

namespace Azure.Identity
{
	// Token: 0x02000066 RID: 102
	internal interface IProcess : IDisposable
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600038F RID: 911
		bool HasExited { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000390 RID: 912
		int ExitCode { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000391 RID: 913
		// (set) Token: 0x06000392 RID: 914
		ProcessStartInfo StartInfo { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000393 RID: 915
		// (remove) Token: 0x06000394 RID: 916
		event EventHandler Exited;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000395 RID: 917
		// (remove) Token: 0x06000396 RID: 918
		event DataReceivedEventHandler OutputDataReceived;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000397 RID: 919
		// (remove) Token: 0x06000398 RID: 920
		event DataReceivedEventHandler ErrorDataReceived;

		// Token: 0x06000399 RID: 921
		bool Start();

		// Token: 0x0600039A RID: 922
		void Kill();

		// Token: 0x0600039B RID: 923
		void BeginOutputReadLine();

		// Token: 0x0600039C RID: 924
		void BeginErrorReadLine();
	}
}
