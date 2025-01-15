using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200022F RID: 559
	public sealed class ConvertNode : SingleValueNode
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x000492BF File Offset: 0x000474BF
		public ConvertNode(SingleValueNode source, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.source = source;
			this.typeReference = typeReference;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000492EB File Offset: 0x000474EB
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000492F3 File Offset: 0x000474F3
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x000492FB File Offset: 0x000474FB
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Convert;
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000492FE File Offset: 0x000474FE
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000880 RID: 2176
		private readonly SingleValueNode source;

		// Token: 0x04000881 RID: 2177
		private readonly IEdmTypeReference typeReference;
	}
}
