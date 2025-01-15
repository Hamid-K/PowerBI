using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000159 RID: 345
	public interface ITokenReplayCache
	{
		// Token: 0x06001002 RID: 4098
		bool TryAdd(string securityToken, DateTime expiresOn);

		// Token: 0x06001003 RID: 4099
		bool TryFind(string securityToken);
	}
}
