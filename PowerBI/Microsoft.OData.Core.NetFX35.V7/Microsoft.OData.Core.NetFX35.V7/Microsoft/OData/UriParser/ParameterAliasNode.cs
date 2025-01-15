using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014E RID: 334
	public class ParameterAliasNode : SingleValueNode
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x0002AB8D File Offset: 0x00028D8D
		public ParameterAliasNode(string alias, IEdmTypeReference typeReference)
		{
			this.Alias = alias;
			this.typeReference = typeReference;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002ABA3 File Offset: 0x00028DA3
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x0002ABAB File Offset: 0x00028DAB
		public string Alias { get; private set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0002ABB4 File Offset: 0x00028DB4
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002ABBC File Offset: 0x00028DBC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.ParameterAlias;
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002ABC0 File Offset: 0x00028DC0
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000789 RID: 1929
		private readonly IEdmTypeReference typeReference;
	}
}
