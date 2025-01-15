using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200009D RID: 157
	internal class CsdlSemanticsCollectionTypeDefinition : CsdlSemanticsTypeDefinition, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x000069C5 File Offset: 0x00004BC5
		public CsdlSemanticsCollectionTypeDefinition(CsdlSemanticsSchema schema, CsdlCollectionType collection)
			: base(collection)
		{
			this.collection = collection;
			this.schema = schema;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000069E7 File Offset: 0x00004BE7
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000069EA File Offset: 0x00004BEA
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementTypeCache.GetValue(this, CsdlSemanticsCollectionTypeDefinition.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000069FE File Offset: 0x00004BFE
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00006A0B File Offset: 0x00004C0B
		public override CsdlElement Element
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00006A13 File Offset: 0x00004C13
		private IEdmTypeReference ComputeElementType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.schema, this.collection.ElementType);
		}

		// Token: 0x04000117 RID: 279
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000118 RID: 280
		private readonly CsdlCollectionType collection;

		// Token: 0x04000119 RID: 281
		private readonly Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> elementTypeCache = new Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference>();

		// Token: 0x0400011A RID: 282
		private static readonly Func<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> ComputeElementTypeFunc = (CsdlSemanticsCollectionTypeDefinition me) => me.ComputeElementType();
	}
}
