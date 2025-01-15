using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F3 RID: 499
	internal class CsdlProperty : CsdlNamedElement
	{
		// Token: 0x06000D2C RID: 3372 RVA: 0x0002430F File Offset: 0x0002250F
		public CsdlProperty(string name, CsdlTypeReference type, string defaultValue, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.type = type;
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0002432A File Offset: 0x0002252A
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00024332 File Offset: 0x00022532
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04000728 RID: 1832
		private readonly CsdlTypeReference type;

		// Token: 0x04000729 RID: 1833
		private readonly string defaultValue;
	}
}
