using System;
using System.Collections.Generic;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002AD RID: 685
	internal interface ITokenCacheSerializable
	{
		// Token: 0x060019C5 RID: 6597
		IDictionary<string, JToken> Deserialize(byte[] bytes, bool clearExistingCacheData);

		// Token: 0x060019C6 RID: 6598
		byte[] Serialize(IDictionary<string, JToken> additionalNodes);
	}
}
