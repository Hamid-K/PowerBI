using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000121 RID: 289
	internal class BaseConfigurationComparer : IEqualityComparer<BaseConfiguration>
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x000394B0 File Offset: 0x000376B0
		public bool Equals(BaseConfiguration config1, BaseConfiguration config2)
		{
			if (config1 == null && config2 == null)
			{
				return true;
			}
			if (config1 == null || config2 == null)
			{
				return false;
			}
			if (config1.Issuer == config2.Issuer && config1.SigningKeys.Count == config2.SigningKeys.Count)
			{
				if (!config1.SigningKeys.Select((SecurityKey x) => x.InternalId).Except(config2.SigningKeys.Select((SecurityKey x) => x.InternalId)).Any<string>())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003955C File Offset: 0x0003775C
		public int GetHashCode(BaseConfiguration config)
		{
			int hashCode = string.Empty.GetHashCode();
			int num = hashCode;
			num ^= (string.IsNullOrEmpty(config.Issuer) ? hashCode : config.Issuer.GetHashCode());
			foreach (string text in config.SigningKeys.Select((SecurityKey x) => x.InternalId))
			{
				num ^= text.GetHashCode();
			}
			return num;
		}
	}
}
