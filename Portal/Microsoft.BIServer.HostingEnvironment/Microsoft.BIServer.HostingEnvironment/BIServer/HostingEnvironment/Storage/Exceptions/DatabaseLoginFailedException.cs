using System;

namespace Microsoft.BIServer.HostingEnvironment.Storage.Exceptions
{
	// Token: 0x02000027 RID: 39
	public class DatabaseLoginFailedException : DatabaseException
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00004B62 File Offset: 0x00002D62
		public DatabaseLoginFailedException(string message)
			: base(message)
		{
		}
	}
}
