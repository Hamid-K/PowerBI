using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CF RID: 463
	internal class CsdlCollectionType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x00023AA5 File Offset: 0x00021CA5
		public CsdlCollectionType(CsdlTypeReference elementType, CsdlLocation location)
			: base(location)
		{
			this.elementType = elementType;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00023AB5 File Offset: 0x00021CB5
		public CsdlTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x040006E4 RID: 1764
		private readonly CsdlTypeReference elementType;
	}
}
