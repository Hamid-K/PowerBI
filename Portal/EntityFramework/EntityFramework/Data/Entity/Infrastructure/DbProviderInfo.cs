using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000230 RID: 560
	public sealed class DbProviderInfo
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x000535CA File Offset: 0x000517CA
		public DbProviderInfo(string providerInvariantName, string providerManifestToken)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			this._providerInvariantName = providerInvariantName;
			this._providerManifestToken = providerManifestToken;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x000535F8 File Offset: 0x000517F8
		public string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x00053600 File Offset: 0x00051800
		public string ProviderManifestToken
		{
			get
			{
				return this._providerManifestToken;
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00053608 File Offset: 0x00051808
		private bool Equals(DbProviderInfo other)
		{
			return string.Equals(this._providerInvariantName, other._providerInvariantName) && string.Equals(this._providerManifestToken, other._providerManifestToken);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00053630 File Offset: 0x00051830
		public override bool Equals(object obj)
		{
			DbProviderInfo dbProviderInfo = obj as DbProviderInfo;
			return dbProviderInfo != null && this.Equals(dbProviderInfo);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00053650 File Offset: 0x00051850
		public override int GetHashCode()
		{
			return (this._providerInvariantName.GetHashCode() * 397) ^ this._providerManifestToken.GetHashCode();
		}

		// Token: 0x04000B21 RID: 2849
		private readonly string _providerInvariantName;

		// Token: 0x04000B22 RID: 2850
		private readonly string _providerManifestToken;
	}
}
