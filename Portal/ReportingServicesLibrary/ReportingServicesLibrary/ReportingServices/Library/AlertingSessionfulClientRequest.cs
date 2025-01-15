using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000203 RID: 515
	internal class AlertingSessionfulClientRequest : SessionfulClientRequest
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x0004185C File Offset: 0x0003FA5C
		internal AlertingSessionfulClientRequest(DatabaseSessionStorage sessionDB, string sessionId)
			: base(sessionDB)
		{
			this.m_sessionId = sessionId;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004186C File Offset: 0x0003FA6C
		protected override string GetSessionId()
		{
			return this.m_sessionId;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void DeleteSessionId(string sessionId)
		{
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void WriteSessionId(string sessionId)
		{
		}

		// Token: 0x04000693 RID: 1683
		private readonly string m_sessionId;
	}
}
