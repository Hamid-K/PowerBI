using System;

namespace Microsoft.OData
{
	// Token: 0x02000019 RID: 25
	public sealed class ODataNestedResourceInfoSerializationInfo
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003687 File Offset: 0x00001887
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000368F File Offset: 0x0000188F
		public bool IsComplex { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000036A0 File Offset: 0x000018A0
		public bool IsUndeclared { get; set; }
	}
}
