using System;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000023 RID: 35
	internal class AuthenticationAccount : IAccount
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003F93 File Offset: 0x00002193
		internal AuthenticationAccount(AuthenticationRecord profile)
		{
			this._profile = profile;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003FA2 File Offset: 0x000021A2
		string IAccount.Username
		{
			get
			{
				return this._profile.Username;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003FAF File Offset: 0x000021AF
		string IAccount.Environment
		{
			get
			{
				return this._profile.Authority;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003FBC File Offset: 0x000021BC
		AccountId IAccount.HomeAccountId
		{
			get
			{
				return this._profile.AccountId;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FC9 File Offset: 0x000021C9
		public static explicit operator AuthenticationAccount(AuthenticationRecord profile)
		{
			return new AuthenticationAccount(profile);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FD1 File Offset: 0x000021D1
		public static explicit operator AuthenticationRecord(AuthenticationAccount account)
		{
			return account._profile;
		}

		// Token: 0x0400005A RID: 90
		private AuthenticationRecord _profile;
	}
}
