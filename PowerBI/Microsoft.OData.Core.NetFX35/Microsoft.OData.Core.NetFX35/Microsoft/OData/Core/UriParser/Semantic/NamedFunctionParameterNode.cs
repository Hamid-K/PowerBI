using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000245 RID: 581
	public class NamedFunctionParameterNode : QueryNode
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x00049D54 File Offset: 0x00047F54
		public NamedFunctionParameterNode(string name, QueryNode value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00049D6A File Offset: 0x00047F6A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00049D72 File Offset: 0x00047F72
		public QueryNode Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00049D7A File Offset: 0x00047F7A
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00049D82 File Offset: 0x00047F82
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NamedFunctionParameter;
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00049D86 File Offset: 0x00047F86
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008B3 RID: 2227
		private readonly string name;

		// Token: 0x040008B4 RID: 2228
		private readonly QueryNode value;
	}
}
