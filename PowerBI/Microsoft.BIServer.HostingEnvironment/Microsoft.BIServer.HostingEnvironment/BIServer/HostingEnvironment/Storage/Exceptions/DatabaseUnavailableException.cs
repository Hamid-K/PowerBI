using System;

namespace Microsoft.BIServer.HostingEnvironment.Storage.Exceptions
{
	// Token: 0x02000028 RID: 40
	public class DatabaseUnavailableException : DatabaseException
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00004B62 File Offset: 0x00002D62
		public DatabaseUnavailableException(string message)
			: base(message)
		{
		}
	}
}
