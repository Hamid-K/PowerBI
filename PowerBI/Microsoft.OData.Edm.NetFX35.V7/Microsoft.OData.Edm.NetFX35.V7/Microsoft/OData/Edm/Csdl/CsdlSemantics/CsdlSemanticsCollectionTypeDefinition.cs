using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000172 RID: 370
	internal class CsdlSemanticsCollectionTypeDefinition : CsdlSemanticsTypeDefinition, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0001AC81 File Offset: 0x00018E81
		public CsdlSemanticsCollectionTypeDefinition(CsdlSemanticsSchema schema, CsdlCollectionType collection)
			: base(collection)
		{
			this.collection = collection;
			this.schema = schema;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001ACA3 File Offset: 0x00018EA3
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementTypeCache.GetValue(this, CsdlSemanticsCollectionTypeDefinition.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		public override CsdlElement Element
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001ACCC File Offset: 0x00018ECC
		private IEdmTypeReference ComputeElementType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.schema, this.collection.ElementType);
		}

		// Token: 0x040005D6 RID: 1494
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040005D7 RID: 1495
		private readonly CsdlCollectionType collection;

		// Token: 0x040005D8 RID: 1496
		private readonly Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> elementTypeCache = new Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference>();

		// Token: 0x040005D9 RID: 1497
		private static readonly Func<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> ComputeElementTypeFunc = (CsdlSemanticsCollectionTypeDefinition me) => me.ComputeElementType();
	}
}
