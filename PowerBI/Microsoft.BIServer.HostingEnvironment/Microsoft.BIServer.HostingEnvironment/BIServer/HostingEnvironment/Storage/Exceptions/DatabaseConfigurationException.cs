using System;

namespace Microsoft.BIServer.HostingEnvironment.Storage.Exceptions
{
	// Token: 0x02000025 RID: 37
	public class DatabaseConfigurationException : DatabaseException
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004B5A File Offset: 0x00002D5A
		public DatabaseConfigurationException()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004B62 File Offset: 0x00002D62
		public DatabaseConfigurationException(string message)
			: base(message)
		{
		}
	}
}
