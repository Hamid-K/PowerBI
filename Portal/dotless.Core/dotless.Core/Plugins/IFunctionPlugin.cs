using System;
using System.Collections.Generic;

namespace dotless.Core.Plugins
{
	// Token: 0x02000018 RID: 24
	public interface IFunctionPlugin : IPlugin
	{
		// Token: 0x0600009C RID: 156
		Dictionary<string, Type> GetFunctions();
	}
}
