using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017F RID: 383
	internal class CsdlEntityReferenceType : CsdlElement, ICsdlTypeExpression
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00011871 File Offset: 0x0000FA71
		public CsdlEntityReferenceType(CsdlTypeReference entityType, CsdlLocation location)
			: base(location)
		{
			this.entityType = entityType;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00011881 File Offset: 0x0000FA81
		public CsdlTypeReference EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x040003BF RID: 959
		private readonly CsdlTypeReference entityType;
	}
}
