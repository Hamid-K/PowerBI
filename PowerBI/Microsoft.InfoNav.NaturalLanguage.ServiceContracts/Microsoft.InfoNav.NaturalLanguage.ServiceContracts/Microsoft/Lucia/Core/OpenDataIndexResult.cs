using System;
using System.ComponentModel;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B3 RID: 179
	[ImmutableObject(true)]
	public sealed class OpenDataIndexResult
	{
		// Token: 0x06000396 RID: 918 RVA: 0x00006D29 File Offset: 0x00004F29
		public OpenDataIndexResult(DataIndex dataIndex, OpenDataIndexWarnings warnings = OpenDataIndexWarnings.None)
		{
			this.DataIndex = dataIndex;
			this.Warnings = warnings;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00006D3F File Offset: 0x00004F3F
		public OpenDataIndexResult(OpenDataIndexWarnings warnings)
		{
			this.Warnings = warnings;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00006D4E File Offset: 0x00004F4E
		public DataIndex DataIndex { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00006D56 File Offset: 0x00004F56
		public OpenDataIndexWarnings Warnings { get; }
	}
}
