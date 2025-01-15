using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C3 RID: 451
	internal class CsdlEnumMember : CsdlNamedElement
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x000238C0 File Offset: 0x00021AC0
		public CsdlEnumMember(string name, long? value, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.Value = value;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000238D3 File Offset: 0x00021AD3
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x000238DB File Offset: 0x00021ADB
		public long? Value { get; set; }
	}
}
