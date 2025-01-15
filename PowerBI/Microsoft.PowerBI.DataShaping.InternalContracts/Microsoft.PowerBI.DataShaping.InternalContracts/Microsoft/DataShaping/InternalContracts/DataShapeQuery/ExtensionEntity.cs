using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200008D RID: 141
	internal sealed class ExtensionEntity
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00006F0B File Offset: 0x0000510B
		public ExtensionEntity()
		{
			this.Columns = new List<ExtensionColumn>();
			this.Measures = new List<ExtensionMeasure>();
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00006F29 File Offset: 0x00005129
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00006F31 File Offset: 0x00005131
		public string Name { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00006F3A File Offset: 0x0000513A
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00006F42 File Offset: 0x00005142
		public string Extends { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00006F4B File Offset: 0x0000514B
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00006F53 File Offset: 0x00005153
		public List<ExtensionColumn> Columns { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00006F5C File Offset: 0x0000515C
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00006F64 File Offset: 0x00005164
		public List<ExtensionMeasure> Measures { get; set; }
	}
}
