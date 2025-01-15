using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E5 RID: 485
	internal class CsdlTypeDefinition : CsdlNamedElement
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x00025DB7 File Offset: 0x00023FB7
		public CsdlTypeDefinition(string name, string underlyingTypeName, CsdlLocation location)
			: base(name, location)
		{
			this.underlyingTypeName = underlyingTypeName;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00025DC8 File Offset: 0x00023FC8
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x04000766 RID: 1894
		private readonly string underlyingTypeName;
	}
}
