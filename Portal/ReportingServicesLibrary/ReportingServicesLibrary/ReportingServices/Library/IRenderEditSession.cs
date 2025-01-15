using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000191 RID: 401
	internal interface IRenderEditSession
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000EC6 RID: 3782
		string SessionId { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000EC7 RID: 3783
		string UserName { get; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EC8 RID: 3784
		// (set) Token: 0x06000EC9 RID: 3785
		ExternalItemPath ItemPath { get; set; }

		// Token: 0x06000ECA RID: 3786
		string GenerateSession();

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000ECB RID: 3787
		bool IsSessionIdValid { get; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000ECC RID: 3788
		bool IsNewSession { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000ECD RID: 3789
		bool IsPowerView { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000ECE RID: 3790
		PowerViewSessionType SessionType { get; }

		// Token: 0x06000ECF RID: 3791
		string MakeCacheKey();

		// Token: 0x06000ED0 RID: 3792
		void EnsureValidSessionExists(string userName, string operationName, out ProgressiveCacheEntry entry);

		// Token: 0x06000ED1 RID: 3793
		void ValidateSession();
	}
}
