using System;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038B RID: 907
	public interface ISinkServices
	{
		// Token: 0x06001C16 RID: 7190
		IEventsKitExplorer GetEventsKitExplorer();

		// Token: 0x06001C17 RID: 7191
		IConfigurationManager GetConfigManager();
	}
}
