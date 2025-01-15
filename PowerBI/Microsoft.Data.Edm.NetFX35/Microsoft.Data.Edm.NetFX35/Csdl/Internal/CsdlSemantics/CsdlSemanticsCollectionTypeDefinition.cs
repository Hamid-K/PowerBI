using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200006D RID: 109
	internal class CsdlSemanticsCollectionTypeDefinition : CsdlSemanticsTypeDefinition, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00005425 File Offset: 0x00003625
		public CsdlSemanticsCollectionTypeDefinition(CsdlSemanticsSchema schema, CsdlCollectionType collection)
			: base(collection)
		{
			this.collection = collection;
			this.schema = schema;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005447 File Offset: 0x00003647
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000544A File Offset: 0x0000364A
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementTypeCache.GetValue(this, CsdlSemanticsCollectionTypeDefinition.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000545E File Offset: 0x0000365E
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000546B File Offset: 0x0000366B
		public override CsdlElement Element
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005473 File Offset: 0x00003673
		private IEdmTypeReference ComputeElementType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.schema, this.collection.ElementType);
		}

		// Token: 0x040000C3 RID: 195
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040000C4 RID: 196
		private readonly CsdlCollectionType collection;

		// Token: 0x040000C5 RID: 197
		private readonly Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> elementTypeCache = new Cache<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference>();

		// Token: 0x040000C6 RID: 198
		private static readonly Func<CsdlSemanticsCollectionTypeDefinition, IEdmTypeReference> ComputeElementTypeFunc = (CsdlSemanticsCollectionTypeDefinition me) => me.ComputeElementType();
	}
}
