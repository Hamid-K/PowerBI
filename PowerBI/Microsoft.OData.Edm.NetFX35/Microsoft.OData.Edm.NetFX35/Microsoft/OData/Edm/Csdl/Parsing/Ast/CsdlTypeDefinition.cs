using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000041 RID: 65
	internal class CsdlTypeDefinition : CsdlNamedElement
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000039F4 File Offset: 0x00001BF4
		public CsdlTypeDefinition(string name, string underlyingTypeName, CsdlLocation location)
			: base(name, null, location)
		{
			this.underlyingTypeName = underlyingTypeName;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003A06 File Offset: 0x00001C06
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x04000060 RID: 96
		private readonly string underlyingTypeName;
	}
}
