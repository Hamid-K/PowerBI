using System;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000160 RID: 352
	internal sealed class DbConnectionPoolAuthenticationContextKey
	{
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0006C16F File Offset: 0x0006A36F
		internal string StsAuthority
		{
			get
			{
				return this._stsAuthority;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0006C177 File Offset: 0x0006A377
		internal string ServicePrincipalName
		{
			get
			{
				return this._servicePrincipalName;
			}
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0006C17F File Offset: 0x0006A37F
		internal DbConnectionPoolAuthenticationContextKey(string stsAuthority, string servicePrincipalName)
		{
			this._stsAuthority = stsAuthority;
			this._servicePrincipalName = servicePrincipalName;
			this._hashCode = this.ComputeHashCode();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0006C1A4 File Offset: 0x0006A3A4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DbConnectionPoolAuthenticationContextKey dbConnectionPoolAuthenticationContextKey = obj as DbConnectionPoolAuthenticationContextKey;
			return dbConnectionPoolAuthenticationContextKey != null && string.Equals(this.StsAuthority, dbConnectionPoolAuthenticationContextKey.StsAuthority, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.ServicePrincipalName, dbConnectionPoolAuthenticationContextKey.ServicePrincipalName, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0006C1EA File Offset: 0x0006A3EA
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0006C1F4 File Offset: 0x0006A3F4
		private int ComputeHashCode()
		{
			int num = 33;
			num = num * 17 + this.StsAuthority.GetHashCode();
			return num * 17 + this.ServicePrincipalName.GetHashCode();
		}

		// Token: 0x04000AB9 RID: 2745
		private readonly string _stsAuthority;

		// Token: 0x04000ABA RID: 2746
		private readonly string _servicePrincipalName;

		// Token: 0x04000ABB RID: 2747
		private readonly int _hashCode;
	}
}
