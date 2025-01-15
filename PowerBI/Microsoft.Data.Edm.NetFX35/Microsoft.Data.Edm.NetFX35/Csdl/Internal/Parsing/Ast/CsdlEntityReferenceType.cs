using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200013A RID: 314
	internal class CsdlEntityReferenceType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0000F572 File Offset: 0x0000D772
		public CsdlEntityReferenceType(CsdlTypeReference entityType, CsdlLocation location)
			: base(location)
		{
			this.entityType = entityType;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000F582 File Offset: 0x0000D782
		public CsdlTypeReference EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000329 RID: 809
		private readonly CsdlTypeReference entityType;
	}
}
