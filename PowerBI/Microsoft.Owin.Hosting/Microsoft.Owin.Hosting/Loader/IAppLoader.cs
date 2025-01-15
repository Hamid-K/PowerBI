using System;
using System.Collections.Generic;
using Owin;

namespace Microsoft.Owin.Hosting.Loader
{
	// Token: 0x02000025 RID: 37
	public interface IAppLoader
	{
		// Token: 0x060000A3 RID: 163
		Action<IAppBuilder> Load(string appName, IList<string> errors);
	}
}
