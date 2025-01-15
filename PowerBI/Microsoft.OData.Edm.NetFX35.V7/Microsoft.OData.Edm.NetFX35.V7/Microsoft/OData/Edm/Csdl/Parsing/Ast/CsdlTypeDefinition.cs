using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D6 RID: 470
	internal class CsdlTypeDefinition : CsdlNamedElement
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00023BF1 File Offset: 0x00021DF1
		public CsdlTypeDefinition(string name, string underlyingTypeName, CsdlLocation location)
			: base(name, null, location)
		{
			this.underlyingTypeName = underlyingTypeName;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00023C03 File Offset: 0x00021E03
		public string UnderlyingTypeName
		{
			get
			{
				return this.underlyingTypeName;
			}
		}

		// Token: 0x040006ED RID: 1773
		private readonly string underlyingTypeName;
	}
}
