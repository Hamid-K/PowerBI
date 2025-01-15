using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EF RID: 495
	internal class CsdlEntityReferenceType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00025FF0 File Offset: 0x000241F0
		public CsdlEntityReferenceType(CsdlTypeReference entityType, CsdlLocation location)
			: base(location)
		{
			this.entityType = entityType;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00026000 File Offset: 0x00024200
		public CsdlTypeReference EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000775 RID: 1909
		private readonly CsdlTypeReference entityType;
	}
}
