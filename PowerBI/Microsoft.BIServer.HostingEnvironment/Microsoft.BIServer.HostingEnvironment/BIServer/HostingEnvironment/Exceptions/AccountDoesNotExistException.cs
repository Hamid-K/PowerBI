using System;

namespace Microsoft.BIServer.HostingEnvironment.Exceptions
{
	// Token: 0x0200002F RID: 47
	public class AccountDoesNotExistException : Exception
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000050E2 File Offset: 0x000032E2
		public AccountDoesNotExistException(AccountCredentials credentials)
			: base(string.Format("Account {0} does not exist", credentials.DomainUser))
		{
		}
	}
}
