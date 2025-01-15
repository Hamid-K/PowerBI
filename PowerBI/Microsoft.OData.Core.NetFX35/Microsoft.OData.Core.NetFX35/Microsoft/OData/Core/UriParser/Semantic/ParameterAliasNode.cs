using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000255 RID: 597
	public class ParameterAliasNode : SingleValueNode
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0004AE5B File Offset: 0x0004905B
		public ParameterAliasNode(string alias, IEdmTypeReference typeReference)
		{
			this.Alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0004AE71 File Offset: 0x00049071
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0004AE79 File Offset: 0x00049079
		public string Alias { get; private set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0004AE82 File Offset: 0x00049082
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0004AE8A File Offset: 0x0004908A
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.ParameterAlias;
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004AE8E File Offset: 0x0004908E
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008D5 RID: 2261
		private readonly IEdmTypeReference typeReference;
	}
}
