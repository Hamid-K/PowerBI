using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B2 RID: 434
	public sealed class NonResourceRangeVariable : RangeVariable
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x0003072C File Offset: 0x0002E92C
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
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00030786 File Offset: 0x0002E986
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003078E File Offset: 0x0002E98E
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00030796 File Offset: 0x0002E996
		public CollectionNode CollectionNode
		{
			get
			{
				return this.collectionNode;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00002503 File Offset: 0x00000703
		public override int Kind
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x040008D6 RID: 2262
		private readonly string name;

		// Token: 0x040008D7 RID: 2263
		private readonly CollectionNode collectionNode;

		// Token: 0x040008D8 RID: 2264
		private readonly IEdmTypeReference typeReference;
	}
}
