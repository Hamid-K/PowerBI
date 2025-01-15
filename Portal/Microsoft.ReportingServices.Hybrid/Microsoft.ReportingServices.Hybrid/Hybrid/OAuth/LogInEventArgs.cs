using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000009 RID: 9
	internal sealed class LogInEventArgs : EventArgs
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public LogInEventArgs(ServiceIdToken userInfo, string error)
		{
			this.UserInfo = userInfo;
			this.Error = error;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002ACA File Offset: 0x00000CCA
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public ServiceIdToken UserInfo { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002ADB File Offset: 0x00000CDB
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public string Error { get; private set; }
	}
}
