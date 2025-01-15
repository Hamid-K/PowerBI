using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DE RID: 478
	internal class CsdlCollectionType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x00025C6A File Offset: 0x00023E6A
		public CsdlCollectionType(CsdlTypeReference elementType, CsdlLocation location)
			: base(location)
		{
			this.elementType = elementType;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00025C7A File Offset: 0x00023E7A
		public CsdlTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x0400075D RID: 1885
		private readonly CsdlTypeReference elementType;
	}
}
