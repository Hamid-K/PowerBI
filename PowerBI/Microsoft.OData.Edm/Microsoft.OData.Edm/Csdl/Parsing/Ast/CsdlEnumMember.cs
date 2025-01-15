using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D2 RID: 466
	internal class CsdlEnumMember : CsdlNamedElement
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x00025A8D File Offset: 0x00023C8D
		public CsdlEnumMember(string name, long? value, CsdlLocation location)
			: base(name, location)
		{
			this.Value = value;
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00025A9E File Offset: 0x00023C9E
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00025AA6 File Offset: 0x00023CA6
		public long? Value { get; set; }
	}
}
