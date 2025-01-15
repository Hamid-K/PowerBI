using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200000F RID: 15
	internal class CsdlEnumMember : CsdlNamedElement
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002AAF File Offset: 0x00000CAF
		public CsdlEnumMember(string name, long? value, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.Value = value;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002AC2 File Offset: 0x00000CC2
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002ACA File Offset: 0x00000CCA
		public long? Value { get; set; }
	}
}
