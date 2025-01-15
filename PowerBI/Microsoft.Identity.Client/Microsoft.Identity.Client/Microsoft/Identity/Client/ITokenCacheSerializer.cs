using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000158 RID: 344
	public interface ITokenCacheSerializer
	{
		// Token: 0x06001116 RID: 4374
		byte[] SerializeMsalV3();

		// Token: 0x06001117 RID: 4375
		void DeserializeMsalV3(byte[] msalV3State, bool shouldClearExistingCache = false);

		// Token: 0x06001118 RID: 4376
		byte[] SerializeAdalV3();

		// Token: 0x06001119 RID: 4377
		void DeserializeAdalV3(byte[] adalV3State);

		// Token: 0x0600111A RID: 4378
		[Obsolete("Support for the MSAL v2 token cache format will be dropped in the next major version", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		byte[] SerializeMsalV2();

		// Token: 0x0600111B RID: 4379
		[Obsolete("Support for the MSAL v2 token cache format will be dropped in the next major version", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void DeserializeMsalV2(byte[] msalV2State);
	}
}
