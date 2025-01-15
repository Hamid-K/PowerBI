using System;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038C RID: 908
	public interface ISinkFactory
	{
		// Token: 0x06001C18 RID: 7192
		ISink Create(SinkIdentifier sid);

		// Token: 0x06001C19 RID: 7193
		void Destroy(ISink sink);
	}
}
