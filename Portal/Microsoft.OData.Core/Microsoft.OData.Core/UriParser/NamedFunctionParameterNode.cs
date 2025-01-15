using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000189 RID: 393
	public class NamedFunctionParameterNode : QueryNode
	{
		// Token: 0x06001348 RID: 4936 RVA: 0x000394BF File Offset: 0x000376BF
		public NamedFunctionParameterNode(string name, QueryNode value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000394D5 File Offset: 0x000376D5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000394DD File Offset: 0x000376DD
		public QueryNode Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0003872F File Offset: 0x0003692F
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000394E5 File Offset: 0x000376E5
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NamedFunctionParameter;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000394E9 File Offset: 0x000376E9
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008A1 RID: 2209
		private readonly string name;

		// Token: 0x040008A2 RID: 2210
		private readonly QueryNode value;
	}
}
