using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Owin.Security;

namespace Microsoft.Owin
{
	// Token: 0x0200000C RID: 12
	public interface IOwinContext
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73
		IOwinRequest Request { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74
		IOwinResponse Response { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75
		IAuthenticationManager Authentication { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76
		IDictionary<string, object> Environment { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004D RID: 77
		// (set) Token: 0x0600004E RID: 78
		TextWriter TraceOutput { get; set; }

		// Token: 0x0600004F RID: 79
		T Get<T>(string key);

		// Token: 0x06000050 RID: 80
		IOwinContext Set<T>(string key, T value);
	}
}
