using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AA RID: 426
	internal class CsdlSemanticsEntityReferenceTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00016B3F File Offset: 0x00014D3F
		public CsdlSemanticsEntityReferenceTypeDefinition(CsdlSemanticsSchema schema, CsdlEntityReferenceType entityTypeReference)
			: base(entityTypeReference)
		{
			this.schema = schema;
			this.entityTypeReference = entityTypeReference;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00016B61 File Offset: 0x00014D61
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00016B64 File Offset: 0x00014D64
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityTypeCache.GetValue(this, CsdlSemanticsEntityReferenceTypeDefinition.ComputeEntityTypeFunc, null);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00016B78 File Offset: 0x00014D78
		public override CsdlElement Element
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00016B80 File Offset: 0x00014D80
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00016B90 File Offset: 0x00014D90
		private IEdmEntityType ComputeEntityType()
		{
			IEdmTypeReference edmTypeReference = CsdlSemanticsModel.WrapTypeReference(this.schema, this.entityTypeReference.EntityType);
			if (edmTypeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return new UnresolvedEntityType(this.schema.UnresolvedName(edmTypeReference.FullName()), base.Location);
			}
			return edmTypeReference.AsEntity().EntityDefinition();
		}

		// Token: 0x04000452 RID: 1106
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000453 RID: 1107
		private readonly Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> entityTypeCache = new Cache<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType>();

		// Token: 0x04000454 RID: 1108
		private static readonly Func<CsdlSemanticsEntityReferenceTypeDefinition, IEdmEntityType> ComputeEntityTypeFunc = (CsdlSemanticsEntityReferenceTypeDefinition me) => me.ComputeEntityType();

		// Token: 0x04000455 RID: 1109
		private readonly CsdlEntityReferenceType entityTypeReference;
	}
}
