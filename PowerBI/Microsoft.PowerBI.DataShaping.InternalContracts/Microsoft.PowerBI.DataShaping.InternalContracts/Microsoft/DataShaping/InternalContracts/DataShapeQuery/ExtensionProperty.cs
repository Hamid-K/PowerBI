using System;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200008E RID: 142
	internal abstract class ExtensionProperty
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00006F6D File Offset: 0x0000516D
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00006F75 File Offset: 0x00005175
		public string Name { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00006F7E File Offset: 0x0000517E
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00006F86 File Offset: 0x00005186
		public Expression Expression { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00006F8F File Offset: 0x0000518F
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00006F97 File Offset: 0x00005197
		public Candidate<ConceptualPrimitiveType> DataType { get; set; }
	}
}
