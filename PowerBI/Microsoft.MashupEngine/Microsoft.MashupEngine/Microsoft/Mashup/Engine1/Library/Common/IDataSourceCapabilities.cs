using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D1 RID: 4305
	internal interface IDataSourceCapabilities
	{
		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x060070C5 RID: 28869
		bool SupportsForeignKeys { get; }

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x060070C6 RID: 28870
		bool SupportsStoredFunctions { get; }

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x060070C7 RID: 28871
		bool SupportsStoredProcedures { get; }
	}
}
