using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000027 RID: 39
	internal class CsdlEnumMember : CsdlNamedElement
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000035DB File Offset: 0x000017DB
		public CsdlEnumMember(string name, long? value, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.Value = value;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000035EE File Offset: 0x000017EE
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000035F6 File Offset: 0x000017F6
		public long? Value { get; set; }
	}
}
