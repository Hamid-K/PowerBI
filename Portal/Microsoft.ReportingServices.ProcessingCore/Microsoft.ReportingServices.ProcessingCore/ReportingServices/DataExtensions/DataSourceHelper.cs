using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B4 RID: 1460
	internal sealed class DataSourceHelper
	{
		// Token: 0x060052E5 RID: 21221 RVA: 0x0015CF57 File Offset: 0x0015B157
		public DataSourceHelper(byte[] encryptedDomainAndUserName, byte[] encryptedPassword, IDataProtection dataProtection)
		{
			this.m_encryptedDomainAndUserName = encryptedDomainAndUserName;
			this.m_encryptedPassword = encryptedPassword;
			if (dataProtection == null)
			{
				throw new ArgumentNullException("dataProtection");
			}
			this.m_dp = dataProtection;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0015CF82 File Offset: 0x0015B182
		public string GetPassword()
		{
			return this.m_dp.UnprotectDataToString(this.m_encryptedPassword, "Password");
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0015CF9A File Offset: 0x0015B19A
		public string GetUserName()
		{
			return DataSourceInfo.GetUserNameOnly(this.m_dp.UnprotectDataToString(this.m_encryptedDomainAndUserName, "UserName"));
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0015CFB7 File Offset: 0x0015B1B7
		public string GetDomainName()
		{
			return DataSourceInfo.GetDomainOnly(this.m_dp.UnprotectDataToString(this.m_encryptedDomainAndUserName, "UserName"));
		}

		// Token: 0x040029CF RID: 10703
		private readonly byte[] m_encryptedDomainAndUserName;

		// Token: 0x040029D0 RID: 10704
		private readonly byte[] m_encryptedPassword;

		// Token: 0x040029D1 RID: 10705
		private readonly IDataProtection m_dp;
	}
}
