using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E9 RID: 489
	internal class CsdlOperationParameter : CsdlNamedElement
	{
		// Token: 0x06000CF9 RID: 3321 RVA: 0x00023FF2 File Offset: 0x000221F2
		public CsdlOperationParameter(string name, CsdlTypeReference type, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00024005 File Offset: 0x00022205
		public CsdlOperationParameter(string name, CsdlTypeReference type, CsdlDocumentation documentation, CsdlLocation location, bool isOptional, string defaultValue)
			: this(name, type, documentation, location)
		{
			this.isOptional = isOptional;
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00024022 File Offset: 0x00022222
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002402A File Offset: 0x0002222A
		public bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00024032 File Offset: 0x00022232
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0400070B RID: 1803
		private readonly CsdlTypeReference type;

		// Token: 0x0400070C RID: 1804
		private readonly bool isOptional;

		// Token: 0x0400070D RID: 1805
		private readonly string defaultValue;
	}
}
