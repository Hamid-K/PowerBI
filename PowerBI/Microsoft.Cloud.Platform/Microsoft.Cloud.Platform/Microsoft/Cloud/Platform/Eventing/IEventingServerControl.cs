using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038D RID: 909
	public interface IEventingServerControl
	{
		// Token: 0x06001C1A RID: 7194
		void RegisterSinkFactory(ISinkFactory factory);

		// Token: 0x06001C1B RID: 7195
		void UnregisterSinkFactory(ISinkFactory factory);
	}
}
