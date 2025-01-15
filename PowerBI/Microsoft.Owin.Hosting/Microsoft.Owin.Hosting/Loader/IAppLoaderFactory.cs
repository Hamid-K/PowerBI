using System;
using System.Collections.Generic;
using Owin;

namespace Microsoft.Owin.Hosting.Loader
{
	// Token: 0x02000026 RID: 38
	public interface IAppLoaderFactory
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A4 RID: 164
		int Order { get; }

		// Token: 0x060000A5 RID: 165
		Func<string, IList<string>, Action<IAppBuilder>> Create(Func<string, IList<string>, Action<IAppBuilder>> nextLoader);
	}
}
