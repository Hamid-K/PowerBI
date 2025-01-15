using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x0200019D RID: 413
	public sealed class ODataServiceDocument : ODataAnnotatable
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003598B File Offset: 0x00033B8B
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00035993 File Offset: 0x00033B93
		public IEnumerable<ODataEntitySetInfo> EntitySets { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003599C File Offset: 0x00033B9C
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x000359A4 File Offset: 0x00033BA4
		public IEnumerable<ODataSingletonInfo> Singletons { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x000359AD File Offset: 0x00033BAD
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x000359B5 File Offset: 0x00033BB5
		public IEnumerable<ODataFunctionImportInfo> FunctionImports { get; set; }
	}
}
