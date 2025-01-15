using System;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002C3 RID: 707
	internal interface IInternetSecurityManager
	{
		// Token: 0x06001C28 RID: 7208
		void MapUrlToZone(string url, out int zone, int flags);
	}
}
