using System;
using System.IO;

namespace Microsoft.PowerBI.ExploreServiceCommon.Interfaces
{
	// Token: 0x02000028 RID: 40
	public interface IScriptHandler
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000159 RID: 345
		string Name { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600015A RID: 346
		string VariableNameWordsSeparator { get; }

		// Token: 0x0600015B RID: 347
		Stream GenerateVisual(ScriptHandlerOptions options);
	}
}
