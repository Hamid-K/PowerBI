using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000404 RID: 1028
	public interface IConfigurationManager
	{
		// Token: 0x06001F6B RID: 8043
		void Subscribe(IEnumerable<Type> configurationTypes, CcsEventHandler registeredEventHandler);

		// Token: 0x06001F6C RID: 8044
		void Subscribe(IEnumerable<string> configurationSections, ConfigurationSectionEventHandler registeredEventHandler);

		// Token: 0x06001F6D RID: 8045
		void Unsubscribe(CcsEventHandler registeredEventHandler);

		// Token: 0x06001F6E RID: 8046
		void Unsubscribe(ConfigurationSectionEventHandler registeredEventHandler);

		// Token: 0x06001F6F RID: 8047
		IConfigurationDiagnostics GetDiagnostics();
	}
}
