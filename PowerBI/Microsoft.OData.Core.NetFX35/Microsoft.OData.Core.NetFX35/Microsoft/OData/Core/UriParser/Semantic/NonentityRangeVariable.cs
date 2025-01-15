using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000249 RID: 585
	public sealed class NonentityRangeVariable : RangeVariable
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x00049FA0 File Offset: 0x000481A0
		public NonentityRangeVariable(string name, IEdmTypeReference typeReference, CollectionNode collectionNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			if (typeReference != null && typeReference.Definition.TypeKind == EdmTypeKind.Entity)
			{
				throw new ArgumentException(Strings.Nodes_NonentityParameterQueryNodeWithEntityType(typeReference.FullName()));
			}
			this.typeReference = typeReference;
			this.collectionNode = collectionNode;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00049FF5 File Offset: 0x000481F5
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00049FFD File Offset: 0x000481FD
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0004A005 File Offset: 0x00048205
		public CollectionNode CollectionNode
		{
			get
			{
				return this.collectionNode;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0004A00D File Offset: 0x0004820D
		public override int Kind
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x040008B8 RID: 2232
		private readonly string name;

		// Token: 0x040008B9 RID: 2233
		private readonly CollectionNode collectionNode;

		// Token: 0x040008BA RID: 2234
		private readonly IEdmTypeReference typeReference;
	}
}
