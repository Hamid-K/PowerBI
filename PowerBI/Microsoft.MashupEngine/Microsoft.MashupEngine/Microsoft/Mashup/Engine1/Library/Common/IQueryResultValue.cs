using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D7 RID: 4311
	internal interface IQueryResultValue
	{
		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x060070E2 RID: 28898
		EnvironmentBase Environment { get; }

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x060070E3 RID: 28899
		IEngineHost Host { get; }

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x060070E4 RID: 28900
		Identifier Identifier { get; }
	}
}
