using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D8 RID: 472
	internal class CsdlTerm : CsdlNamedElement
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x00023C0B File Offset: 0x00021E0B
		public CsdlTerm(string name, CsdlTypeReference type, string appliesTo, string defaultValue, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.appliesTo = appliesTo;
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00023C2E File Offset: 0x00021E2E
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00023C36 File Offset: 0x00021E36
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00023C3E File Offset: 0x00021E3E
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x040006EE RID: 1774
		private readonly CsdlTypeReference type;

		// Token: 0x040006EF RID: 1775
		private readonly string appliesTo;

		// Token: 0x040006F0 RID: 1776
		private readonly string defaultValue;
	}
}
