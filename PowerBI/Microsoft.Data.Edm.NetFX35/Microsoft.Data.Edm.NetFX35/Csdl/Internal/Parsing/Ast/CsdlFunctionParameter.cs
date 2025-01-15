using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000141 RID: 321
	internal class CsdlFunctionParameter : CsdlNamedElement
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0000F6B2 File Offset: 0x0000D8B2
		public CsdlFunctionParameter(string name, CsdlTypeReference type, EdmFunctionParameterMode mode, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.mode = mode;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000F6CD File Offset: 0x0000D8CD
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		public EdmFunctionParameterMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x04000337 RID: 823
		private readonly CsdlTypeReference type;

		// Token: 0x04000338 RID: 824
		private readonly EdmFunctionParameterMode mode;
	}
}
