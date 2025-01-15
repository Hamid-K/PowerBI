using System;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000649 RID: 1609
	public sealed class ConnectionKey
	{
		// Token: 0x06005793 RID: 22419 RVA: 0x0016FB4C File Offset: 0x0016DD4C
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

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0016FB9B File Offset: 0x0016DD9B
		public string DataSourceType
		{
			get
			{
				return this.m_dataSourceType;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x0016FBA3 File Offset: 0x0016DDA3
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

		// Token: 0x06005796 RID: 22422 RVA: 0x0016FBD0 File Offset: 0x0016DDD0
		public string GetKeyString()
		{
			if (this.m_hashCodeString == null)
			{
				this.m_hashCodeString = this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			}
			return this.m_hashCodeString;
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x0016FC04 File Offset: 0x0016DE04
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

		// Token: 0x06005798 RID: 22424 RVA: 0x0016FCEC File Offset: 0x0016DEEC
		public override bool Equals(object obj)
		{
			ConnectionKey connectionKey = obj as ConnectionKey;
			return connectionKey != null && this.m_dataSourceType == connectionKey.m_dataSourceType && this.m_connectionString == connectionKey.m_connectionString && this.m_connectionSecurity == connectionKey.m_connectionSecurity && this.m_domainName == connectionKey.m_domainName && this.m_userName == connectionKey.m_userName && this.m_impersonateUser == connectionKey.m_impersonateUser && this.m_impersonateUserName == connectionKey.m_impersonateUserName;
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0016FD81 File Offset: 0x0016DF81
		public bool ShouldCheckIsAlive()
		{
			return this.DataSourceType == null || (!this.DataSourceType.EndsWith("-Native") && !this.DataSourceType.EndsWith("-Managed"));
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x0016FDB4 File Offset: 0x0016DFB4
		private static void HashCombine(ref int seed, int other)
		{
			uint num = (uint)seed;
			num ^= (uint)(other + -1640531527 + (int)((int)num << 6) + (int)(num >> 2));
			seed = (int)num;
		}

		// Token: 0x04002E5E RID: 11870
		private readonly string m_dataSourceType;

		// Token: 0x04002E5F RID: 11871
		private readonly string m_connectionString;

		// Token: 0x04002E60 RID: 11872
		private readonly ConnectionSecurity m_connectionSecurity;

		// Token: 0x04002E61 RID: 11873
		private readonly string m_domainName;

		// Token: 0x04002E62 RID: 11874
		private readonly string m_userName;

		// Token: 0x04002E63 RID: 11875
		private readonly bool m_impersonateUser;

		// Token: 0x04002E64 RID: 11876
		private readonly string m_impersonateUserName;

		// Token: 0x04002E65 RID: 11877
		private int m_hashCode = -1;

		// Token: 0x04002E66 RID: 11878
		private string m_hashCodeString;
	}
}
