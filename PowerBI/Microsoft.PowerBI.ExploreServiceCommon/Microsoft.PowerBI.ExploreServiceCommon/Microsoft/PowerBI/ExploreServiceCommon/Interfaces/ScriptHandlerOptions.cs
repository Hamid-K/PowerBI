using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.PowerBI.ExploreServiceCommon.Interfaces
{
	// Token: 0x02000027 RID: 39
	public class ScriptHandlerOptions
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000052E2 File Offset: 0x000034E2
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000052EA File Offset: 0x000034EA
		public IList<InputColumn> InputColumns { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000052F3 File Offset: 0x000034F3
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000052FB File Offset: 0x000034FB
		public IList<IDataParameter> InputParameters { get; set; }

		// Token: 0x040000E3 RID: 227
		public string Script;

		// Token: 0x040000E4 RID: 228
		public string InputVariableName;

		// Token: 0x040000E5 RID: 229
		public IDataReader Reader;

		// Token: 0x040000E8 RID: 232
		public double ViewportWidthPx;

		// Token: 0x040000E9 RID: 233
		public double ViewportHeightPx;

		// Token: 0x040000EA RID: 234
		public string ScriptOutputType;
	}
}
