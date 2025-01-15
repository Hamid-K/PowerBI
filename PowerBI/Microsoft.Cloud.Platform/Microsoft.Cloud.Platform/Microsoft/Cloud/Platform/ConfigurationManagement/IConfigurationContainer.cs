using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000408 RID: 1032
	public interface IConfigurationContainer
	{
		// Token: 0x06001F79 RID: 8057
		T GetConfiguration<T>();
	}
}
