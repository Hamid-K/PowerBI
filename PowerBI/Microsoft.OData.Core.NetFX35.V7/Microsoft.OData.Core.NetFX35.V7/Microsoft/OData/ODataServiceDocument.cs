using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000098 RID: 152
	public sealed class ODataServiceDocument : ODataAnnotatable
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000FF1B File Offset: 0x0000E11B
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000FF23 File Offset: 0x0000E123
		public IEnumerable<ODataEntitySetInfo> EntitySets { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000FF2C File Offset: 0x0000E12C
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000FF34 File Offset: 0x0000E134
		public IEnumerable<ODataSingletonInfo> Singletons { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000FF3D File Offset: 0x0000E13D
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000FF45 File Offset: 0x0000E145
		public IEnumerable<ODataFunctionImportInfo> FunctionImports { get; set; }
	}
}
