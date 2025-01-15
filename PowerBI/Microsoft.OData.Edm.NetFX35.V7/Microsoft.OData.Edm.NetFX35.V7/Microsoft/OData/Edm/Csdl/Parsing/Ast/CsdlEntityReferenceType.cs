using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E2 RID: 482
	internal class CsdlEntityReferenceType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x00023E90 File Offset: 0x00022090
		public CsdlEntityReferenceType(CsdlTypeReference entityType, CsdlLocation location)
			: base(location)
		{
			this.entityType = entityType;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00023EA0 File Offset: 0x000220A0
		public CsdlTypeReference EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x040006FF RID: 1791
		private readonly CsdlTypeReference entityType;
	}
}
