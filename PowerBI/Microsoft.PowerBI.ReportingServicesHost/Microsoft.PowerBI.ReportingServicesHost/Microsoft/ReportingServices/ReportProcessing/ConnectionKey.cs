using System;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000011 RID: 17
	internal sealed class ConnectionKey
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002404 File Offset: 0x00000604
		public ConnectionKey(string dataSourceType, string connectionString, ConnectionSecurity connectionSecurity, string domainName, string userName, bool impersonateUser, string impersonateUserName)
		{
			this.m_dataSourceType = dataSourceType;
			this.m_connectionString = connectionString;
			this.m_connectionSecurity = connectionSecurity;
			this.m_domainName = domainName;
			this.m_userName = userName;
			this.m_impersonateUser = impersonateUser;
			this.m_impersonateUserName = impersonateUserName;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002453 File Offset: 0x00000653
		public string DataSourceType
		{
			get
			{
				return this.m_dataSourceType;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000245B File Offset: 0x0000065B
		public bool IsOnPremiseConnection
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_connectionString) && new DbConnectionStringBuilder
				{
					ConnectionString = this.m_connectionString
				}.ContainsKey("External Tenant Id");
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002488 File Offset: 0x00000688
		public string GetKeyString()
		{
			if (this.m_hashCodeString == null)
			{
				this.m_hashCodeString = this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			}
			return this.m_hashCodeString;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024BC File Offset: 0x000006BC
		public override int GetHashCode()
		{
			if (this.m_hashCode == -1)
			{
				this.m_hashCode = this.m_connectionSecurity.GetHashCode();
				ConnectionKey.HashCombine(ref this.m_hashCode, this.m_impersonateUser.GetHashCode());
				if (this.m_dataSourceType != null)
				{
					ConnectionKey.HashCombine(ref this.m_hashCode, this.m_dataSourceType.GetHashCode());
				}
				if (this.m_connectionString != null)
				{
					ConnectionKey.HashCombine(ref this.m_hashCode, this.m_connectionString.GetHashCode());
				}
				if (this.m_domainName != null)
				{
					ConnectionKey.HashCombine(ref this.m_hashCode, this.m_domainName.GetHashCode());
				}
				if (this.m_userName != null)
				{
					ConnectionKey.HashCombine(ref this.m_hashCode, this.m_userName.GetHashCode());
				}
				if (this.m_impersonateUserName != null)
				{
					ConnectionKey.HashCombine(ref this.m_hashCode, this.m_impersonateUserName.GetHashCode());
				}
			}
			return this.m_hashCode;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025A4 File Offset: 0x000007A4
		public override bool Equals(object obj)
		{
			ConnectionKey connectionKey = obj as ConnectionKey;
			return connectionKey != null && this.m_dataSourceType == connectionKey.m_dataSourceType && this.m_connectionString == connectionKey.m_connectionString && this.m_connectionSecurity == connectionKey.m_connectionSecurity && this.m_domainName == connectionKey.m_domainName && this.m_userName == connectionKey.m_userName && this.m_impersonateUser == connectionKey.m_impersonateUser && this.m_impersonateUserName == connectionKey.m_impersonateUserName;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002639 File Offset: 0x00000839
		public bool ShouldCheckIsAlive()
		{
			return this.DataSourceType == null || (!this.DataSourceType.EndsWith("-Native") && !this.DataSourceType.EndsWith("-Managed"));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000266C File Offset: 0x0000086C
		private static void HashCombine(ref int seed, int other)
		{
			uint num = (uint)seed;
			num ^= (uint)(other + -1640531527 + (int)((int)num << 6) + (int)(num >> 2));
			seed = (int)num;
		}

		// Token: 0x0400004D RID: 77
		private readonly string m_dataSourceType;

		// Token: 0x0400004E RID: 78
		private readonly string m_connectionString;

		// Token: 0x0400004F RID: 79
		private readonly ConnectionSecurity m_connectionSecurity;

		// Token: 0x04000050 RID: 80
		private readonly string m_domainName;

		// Token: 0x04000051 RID: 81
		private readonly string m_userName;

		// Token: 0x04000052 RID: 82
		private readonly bool m_impersonateUser;

		// Token: 0x04000053 RID: 83
		private readonly string m_impersonateUserName;

		// Token: 0x04000054 RID: 84
		private int m_hashCode = -1;

		// Token: 0x04000055 RID: 85
		private string m_hashCodeString;
	}
}
