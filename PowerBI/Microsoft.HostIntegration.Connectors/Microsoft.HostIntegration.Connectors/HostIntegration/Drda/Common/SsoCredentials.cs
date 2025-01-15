using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200083E RID: 2110
	public class SsoCredentials
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x000E0C93 File Offset: 0x000DEE93
		// (set) Token: 0x0600430D RID: 17165 RVA: 0x000E0C9B File Offset: 0x000DEE9B
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x000E0CA4 File Offset: 0x000DEEA4
		// (set) Token: 0x0600430F RID: 17167 RVA: 0x000E0CAC File Offset: 0x000DEEAC
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000E0CB5 File Offset: 0x000DEEB5
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x000E0CBD File Offset: 0x000DEEBD
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x04002F58 RID: 12120
		private string userName;

		// Token: 0x04002F59 RID: 12121
		private string password;

		// Token: 0x04002F5A RID: 12122
		private string connectionString;
	}
}
