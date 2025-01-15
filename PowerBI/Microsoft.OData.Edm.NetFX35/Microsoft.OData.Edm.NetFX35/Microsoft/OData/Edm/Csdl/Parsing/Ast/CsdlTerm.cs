using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000097 RID: 151
	internal class CsdlTerm : CsdlNamedElement
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000681D File Offset: 0x00004A1D
		public CsdlTerm(string name, CsdlTypeReference type, string appliesTo, string defaultValue, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.appliesTo = appliesTo;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00006840 File Offset: 0x00004A40
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00006848 File Offset: 0x00004A48
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00006850 File Offset: 0x00004A50
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0400010B RID: 267
		private readonly CsdlTypeReference type;

		// Token: 0x0400010C RID: 268
		private readonly string appliesTo;

		// Token: 0x0400010D RID: 269
		private readonly string defaultValue;
	}
}
