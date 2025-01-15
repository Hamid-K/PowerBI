using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019A RID: 410
	internal class CsdlSemanticsEntityReferenceTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		public CsdlSemanticsEntityReferenceTypeDefinition(CsdlSemanticsSchema schema, CsdlEntityReferenceType entityTypeReference)
			: base(entityTypeReference)
		{
			this.schema = schema;
			this.entityTypeReference = entityTypeReference;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001EC1A File Offset: 0x0001CE1A
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityTypeCache.GetValue(this, CsdlSemanticsEntityReferenceTypeDefinition.ComputeEntityTypeFunc, null);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001EC2E File Offset: 0x0001CE2E
		public override CsdlElement Element
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001EC36 File Offset: 0x0001CE36
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001EC44 File Offset: 0x0001CE44
		private IEdmEntityType ComputeEntityType()
		{
			IEdmTypeReference edmTypeReference = CsdlSemanticsModel.WrapTypeReference(this.schema, this.entityTypeReference.EntityType);
			if (edmTypeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return new UnresolvedEntityType(this.schema.UnresolvedName(edmTypeReference.FullName()), base.Location);
			}
			return edmTypeReference.AsEntity().EntityDefinition();
		}

		// Token: 0x040006B4 RID: 1716
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040006B5 RID: 1717
		private readonly Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> entityTypeCache = new Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType>();

		// Token: 0x040006B6 RID: 1718
		private static readonly Func<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> ComputeEntityTypeFunc = (CsdlSemanticsEntityReferenceTypeDefinition me) => me.ComputeEntityType();

		// Token: 0x040006B7 RID: 1719
		private readonly CsdlEntityReferenceType entityTypeReference;
	}
}
