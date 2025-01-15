using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public struct AccessToken
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00004FDA File Offset: 0x000031DA
		public AccessToken(string accessToken, DateTimeOffset expiresOn)
		{
			this.Token = accessToken;
			this.ExpiresOn = expiresOn;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004FEA File Offset: 0x000031EA
		public readonly string Token { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004FF2 File Offset: 0x000031F2
		public readonly DateTimeOffset ExpiresOn { get; }

		// Token: 0x0600015C RID: 348 RVA: 0x00004FFC File Offset: 0x000031FC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is AccessToken)
			{
				AccessToken accessToken = (AccessToken)obj;
				return accessToken.ExpiresOn == this.ExpiresOn && accessToken.Token == this.Token;
			}
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005042 File Offset: 0x00003242
		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine<string, DateTimeOffset>(this.Token, this.ExpiresOn);
		}
	}
}
