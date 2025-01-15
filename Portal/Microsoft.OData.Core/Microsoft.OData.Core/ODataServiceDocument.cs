using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000BA RID: 186
	public sealed class ODataServiceDocument : ODataAnnotatable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00013916 File Offset: 0x00011B16
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001391E File Offset: 0x00011B1E
		public IEnumerable<ODataEntitySetInfo> EntitySets { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00013927 File Offset: 0x00011B27
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001392F File Offset: 0x00011B2F
		public IEnumerable<ODataSingletonInfo> Singletons { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00013938 File Offset: 0x00011B38
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00013940 File Offset: 0x00011B40
		public IEnumerable<ODataFunctionImportInfo> FunctionImports { get; set; }
	}
}
