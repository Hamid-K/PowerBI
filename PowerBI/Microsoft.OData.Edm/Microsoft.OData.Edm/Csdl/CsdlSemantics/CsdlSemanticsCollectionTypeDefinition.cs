using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000181 RID: 385
	internal class CsdlSemanticsCollectionTypeDefinition : CsdlSemanticsTypeDefinition, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0001CDC9 File Offset: 0x0001AFC9
		public CsdlSemanticsCollectionTypeDefinition(CsdlSemanticsSchema schema, CsdlCollectionType collection)
			: base(collection)
		{
			this.collection = collection;
			this.schema = schema;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001CDEB File Offset: 0x0001AFEB
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementTypeCache.GetValue(this, CsdlSemanticsCollectionTypeDefinition.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0001CDFF File Offset: 0x0001AFFF
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001CE0C File Offset: 0x0001B00C
		public override CsdlElement Element
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001CE14 File Offset: 0x0001B014
		private IEdmTypeReference ComputeElementType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.schema, this.collection.ElementType);
		}

		// Token: 0x04000652 RID: 1618
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000653 RID: 1619
		private readonly CsdlCollectionType collection;

		// Token: 0x04000654 RID: 1620
		private readonly Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> elementTypeCache = new Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference>();

		// Token: 0x04000655 RID: 1621
		private static readonly Func<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> ComputeElementTypeFunc = (CsdlSemanticsCollectionTypeDefinition me) => me.ComputeElementType();
	}
}
