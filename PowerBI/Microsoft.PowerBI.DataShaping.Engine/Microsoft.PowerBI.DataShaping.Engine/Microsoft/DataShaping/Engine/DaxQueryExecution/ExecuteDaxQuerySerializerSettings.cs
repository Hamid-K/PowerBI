using System;

namespace Microsoft.DataShaping.Engine.DaxQueryExecution
{
	// Token: 0x02000027 RID: 39
	public sealed class ExecuteDaxQuerySerializerSettings
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000038DA File Offset: 0x00001ADA
		public ExecuteDaxQuerySerializerSettings(bool includeNulls)
		{
			this.IncludeNulls = includeNulls;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000038E9 File Offset: 0x00001AE9
		public bool IncludeNulls { get; }
	}
}
