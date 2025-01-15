using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200064A RID: 1610
	public sealed class ConnectionContext
	{
		// Token: 0x0600579B RID: 22427 RVA: 0x0016FDD9 File Offset: 0x0016DFD9
		public ConnectionContext()
		{
			this.ConnectionSecurity = ConnectionSecurity.None;
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x0600579C RID: 22428 RVA: 0x0016FDE8 File Offset: 0x0016DFE8
		// (set) Token: 0x0600579D RID: 22429 RVA: 0x0016FDF0 File Offset: 0x0016DFF0
		public string DataSourceType { get; set; }

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x0016FDF9 File Offset: 0x0016DFF9
		// (set) Token: 0x0600579F RID: 22431 RVA: 0x0016FE01 File Offset: 0x0016E001
		public ConnectionSecurity ConnectionSecurity { get; set; }

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x0016FE0A File Offset: 0x0016E00A
		// (set) Token: 0x060057A1 RID: 22433 RVA: 0x0016FE12 File Offset: 0x0016E012
		public string ConnectionString { get; set; }

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0016FE1B File Offset: 0x0016E01B
		// (set) Token: 0x060057A3 RID: 22435 RVA: 0x0016FE23 File Offset: 0x0016E023
		public string DomainName { get; set; }

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x0016FE2C File Offset: 0x0016E02C
		// (set) Token: 0x060057A5 RID: 22437 RVA: 0x0016FE34 File Offset: 0x0016E034
		public string UserName { get; set; }

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0016FE3D File Offset: 0x0016E03D
		// (set) Token: 0x060057A7 RID: 22439 RVA: 0x0016FE45 File Offset: 0x0016E045
		public bool ImpersonateUser { get; set; }

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x0016FE4E File Offset: 0x0016E04E
		// (set) Token: 0x060057A9 RID: 22441 RVA: 0x0016FE56 File Offset: 0x0016E056
		public string ImpersonateUserName { get; set; }

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x060057AA RID: 22442 RVA: 0x0016FE5F File Offset: 0x0016E05F
		// (set) Token: 0x060057AB RID: 22443 RVA: 0x0016FE67 File Offset: 0x0016E067
		public SecureStringWrapper Password { get; set; }

		// Token: 0x060057AC RID: 22444 RVA: 0x0016FE70 File Offset: 0x0016E070
		public ConnectionKey CreateConnectionKey()
		{
			return new ConnectionKey(this.DataSourceType, this.ConnectionString, this.ConnectionSecurity, this.DomainName, this.UserName, this.ImpersonateUser, this.ImpersonateUserName);
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x060057AD RID: 22445 RVA: 0x0016FEA1 File Offset: 0x0016E0A1
		public string DecryptedPassword
		{
			get
			{
				if (this.Password != null)
				{
					return this.Password.ToString();
				}
				return string.Empty;
			}
		}
	}
}
