using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019A RID: 410
	public class ParameterAliasNode : SingleValueNode
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x0003A9A9 File Offset: 0x00038BA9
		public ParameterAliasNode(string alias, IEdmTypeReference typeReference)
		{
			this.Alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0003A9BF File Offset: 0x00038BBF
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0003A9C7 File Offset: 0x00038BC7
		public string Alias { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0003A9D0 File Offset: 0x00038BD0
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.ParameterAlias;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003A9DC File Offset: 0x00038BDC
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008C4 RID: 2244
		private readonly IEdmTypeReference typeReference;
	}
}
