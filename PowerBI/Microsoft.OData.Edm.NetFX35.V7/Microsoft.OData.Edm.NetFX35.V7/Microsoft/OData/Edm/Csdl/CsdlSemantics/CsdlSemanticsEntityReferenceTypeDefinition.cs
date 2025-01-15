using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018B RID: 395
	internal class CsdlSemanticsEntityReferenceTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		public CsdlSemanticsEntityReferenceTypeDefinition(CsdlSemanticsSchema schema, CsdlEntityReferenceType entityTypeReference)
			: base(entityTypeReference)
		{
			this.schema = schema;
			this.entityTypeReference = entityTypeReference;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001C8DA File Offset: 0x0001AADA
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityTypeCache.GetValue(this, CsdlSemanticsEntityReferenceTypeDefinition.ComputeEntityTypeFunc, null);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001C8EE File Offset: 0x0001AAEE
		public override CsdlElement Element
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001C8F6 File Offset: 0x0001AAF6
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001C904 File Offset: 0x0001AB04
		private IEdmEntityType ComputeEntityType()
		{
			IEdmTypeReference edmTypeReference = CsdlSemanticsModel.WrapTypeReference(this.schema, this.entityTypeReference.EntityType);
			if (edmTypeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return new UnresolvedEntityType(this.schema.UnresolvedName(edmTypeReference.FullName()), base.Location);
			}
			return edmTypeReference.AsEntity().EntityDefinition();
		}

		// Token: 0x04000633 RID: 1587
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000634 RID: 1588
		private readonly Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> entityTypeCache = new Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType>();

		// Token: 0x04000635 RID: 1589
		private static readonly Func<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> ComputeEntityTypeFunc = (CsdlSemanticsEntityReferenceTypeDefinition me) => me.ComputeEntityType();

		// Token: 0x04000636 RID: 1590
		private readonly CsdlEntityReferenceType entityTypeReference;
	}
}
