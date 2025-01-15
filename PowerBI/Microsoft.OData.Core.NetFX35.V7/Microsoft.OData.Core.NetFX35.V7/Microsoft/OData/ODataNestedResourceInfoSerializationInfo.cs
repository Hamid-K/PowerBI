using System;

namespace Microsoft.OData
{
	// Token: 0x020000C4 RID: 196
	public sealed class ODataNestedResourceInfoSerializationInfo
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000153C2 File Offset: 0x000135C2
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x000153CA File Offset: 0x000135CA
		public bool IsComplex { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000153D3 File Offset: 0x000135D3
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x000153DB File Offset: 0x000135DB
		public bool IsUndeclared { get; set; }
	}
}
