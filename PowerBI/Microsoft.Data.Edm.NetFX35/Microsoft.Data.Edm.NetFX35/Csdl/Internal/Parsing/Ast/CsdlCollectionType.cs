using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200001C RID: 28
	internal class CsdlCollectionType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public CsdlCollectionType(CsdlTypeReference elementType, CsdlLocation location)
			: base(location)
		{
			this.elementType = elementType;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public CsdlTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x0400002B RID: 43
		private readonly CsdlTypeReference elementType;
	}
}
