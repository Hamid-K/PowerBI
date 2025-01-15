using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000A RID: 10
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ExcelStreamWriterMetadata
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002F6E File Offset: 0x0000116E
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002F76 File Offset: 0x00001176
		public IList<Tuple<string, string>> PrimarySelectsMap { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F7F File Offset: 0x0000117F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002F87 File Offset: 0x00001187
		public IList<string> ColumnsFormatting { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F90 File Offset: 0x00001190
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002F98 File Offset: 0x00001198
		public IList<int> Ordering { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002FA1 File Offset: 0x000011A1
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002FA9 File Offset: 0x000011A9
		public string TableDescription { get; set; }
	}
}
