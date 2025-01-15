using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000016 RID: 22
	public interface IUserContextContainer
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000093 RID: 147
		object UserContext { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000094 RID: 148
		IEnumerable<KeyValuePair<string, string>> UserCookies { get; }
	}
}
