using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000116 RID: 278
	public class AccountId
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00036E0E File Offset: 0x0003500E
		public string Identifier { get; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00036E16 File Offset: 0x00035016
		public string ObjectId { get; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00036E1E File Offset: 0x0003501E
		public string TenantId { get; }

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00036E26 File Offset: 0x00035026
		public AccountId(string identifier, string objectId, string tenantId)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			this.Identifier = identifier;
			this.ObjectId = objectId;
			this.TenantId = tenantId;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00036E52 File Offset: 0x00035052
		public AccountId(string adfsIdentifier)
			: this(adfsIdentifier, adfsIdentifier, null)
		{
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00036E60 File Offset: 0x00035060
		internal static AccountId ParseFromString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			int num = str.LastIndexOf('.');
			if (num == -1)
			{
				return new AccountId(str);
			}
			return new AccountId(str, str.Substring(0, num), str.Substring(num + 1));
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00036EA4 File Offset: 0x000350A4
		public override bool Equals(object obj)
		{
			AccountId accountId = obj as AccountId;
			return accountId != null && string.Equals(this.Identifier, accountId.Identifier, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00036ECF File Offset: 0x000350CF
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode();
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00036EDC File Offset: 0x000350DC
		public override string ToString()
		{
			return "AccountId: " + this.Identifier;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00036EF0 File Offset: 0x000350F0
		[Conditional("DEBUG")]
		private void ValidateId()
		{
			if (!string.Equals((this.TenantId == null) ? this.ObjectId : (this.ObjectId + "." + this.TenantId), this.Identifier, StringComparison.Ordinal))
			{
				throw new InvalidOperationException("Internal Error (debug only) - Expecting Identifier = ObjectId.TenantId but have " + this.ToString());
			}
		}
	}
}
