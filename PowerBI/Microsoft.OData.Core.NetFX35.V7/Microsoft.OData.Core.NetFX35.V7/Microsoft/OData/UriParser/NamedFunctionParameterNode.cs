using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013F RID: 319
	public class NamedFunctionParameterNode : QueryNode
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x000297F7 File Offset: 0x000279F7
		public NamedFunctionParameterNode(string name, QueryNode value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002980D File Offset: 0x00027A0D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00029815 File Offset: 0x00027A15
		public QueryNode Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00028DFB File Offset: 0x00026FFB
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002981D File Offset: 0x00027A1D
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NamedFunctionParameter;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00029821 File Offset: 0x00027A21
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0400076C RID: 1900
		private readonly string name;

		// Token: 0x0400076D RID: 1901
		private readonly QueryNode value;
	}
}
