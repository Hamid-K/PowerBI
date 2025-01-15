using System;
using Microsoft.ReportingServices.CatalogAccess;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006A RID: 106
	internal sealed class CatalogConfiguration : ICatalogConfiguration
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000150F1 File Offset: 0x000132F1
		public CatalogConfiguration(string connectionString)
		{
			this.ConnectionString = connectionString;
			this.UseImpersonation = false;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00015107 File Offset: 0x00013307
		public CatalogConfiguration(string connectionString, string windowsUser, string windowsDomain, string windowsPassword)
		{
			this.ConnectionString = connectionString;
			this.WindowsUser = windowsUser;
			this.WindowsPassword = windowsPassword;
			this.WindowsDomain = windowsDomain;
			this.UseImpersonation = true;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00015133 File Offset: 0x00013333
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0001513B File Offset: 0x0001333B
		public string ConnectionString { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00015144 File Offset: 0x00013344
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0001514C File Offset: 0x0001334C
		public string WindowsUser { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00015155 File Offset: 0x00013355
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0001515D File Offset: 0x0001335D
		public string WindowsDomain { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00015166 File Offset: 0x00013366
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001516E File Offset: 0x0001336E
		public string WindowsPassword { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00015177 File Offset: 0x00013377
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0001517F File Offset: 0x0001337F
		public bool UseImpersonation { get; private set; }
	}
}
