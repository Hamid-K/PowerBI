using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public sealed class DatasourceCredentials
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00009C2C File Offset: 0x00007E2C
		public DatasourceCredentials(string promptID, string userName, string password)
		{
			this.m_promptID = promptID;
			this.m_userName = userName;
			this.m_password = password;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009C49 File Offset: 0x00007E49
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00009C51 File Offset: 0x00007E51
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00009C59 File Offset: 0x00007E59
		public string Password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009C62 File Offset: 0x00007E62
		public string PromptID
		{
			get
			{
				return this.m_promptID;
			}
		}

		// Token: 0x04000151 RID: 337
		private string m_userName;

		// Token: 0x04000152 RID: 338
		private string m_password;

		// Token: 0x04000153 RID: 339
		private string m_promptID;
	}
}
