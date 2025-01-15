using System;
using System.Security.Principal;
using Microsoft.BIServer.Configuration;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000016 RID: 22
	public interface IWebHostUserContext
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97
		IIdentity Identity { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000062 RID: 98
		string UserName { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99
		object Token { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000064 RID: 100
		AuthenticationType AuthenticationType { get; }
	}
}
