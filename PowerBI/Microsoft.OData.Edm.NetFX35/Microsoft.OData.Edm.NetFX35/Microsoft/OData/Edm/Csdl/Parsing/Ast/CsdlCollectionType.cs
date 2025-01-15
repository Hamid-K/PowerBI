using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000036 RID: 54
	internal class CsdlCollectionType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00003829 File Offset: 0x00001A29
		public CsdlCollectionType(CsdlTypeReference elementType, CsdlLocation location)
			: base(location)
		{
			this.elementType = elementType;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003839 File Offset: 0x00001A39
		public CsdlTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly CsdlTypeReference elementType;
	}
}
