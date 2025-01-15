using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018D RID: 397
	public sealed class NonResourceRangeVariable : RangeVariable
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x00039704 File Offset: 0x00037904
		public NonResourceRangeVariable(string name, IEdmTypeReference typeReference, CollectionNode collectionNode)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.name = name;
			if (typeReference != null && typeReference.Definition.TypeKind.IsStructured())
			{
				throw new ArgumentException(Strings.Nodes_NonentityParameterQueryNodeWithEntityType(typeReference.FullName()));
			}
			this.typeReference = typeReference;
			this.collectionNode = collectionNode;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0003975E File Offset: 0x0003795E
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00039766 File Offset: 0x00037966
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0003976E File Offset: 0x0003796E
		public CollectionNode CollectionNode
		{
			get
			{
				return this.collectionNode;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00002393 File Offset: 0x00000593
		public override int Kind
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x040008A6 RID: 2214
		private readonly string name;

		// Token: 0x040008A7 RID: 2215
		private readonly CollectionNode collectionNode;

		// Token: 0x040008A8 RID: 2216
		private readonly IEdmTypeReference typeReference;
	}
}
