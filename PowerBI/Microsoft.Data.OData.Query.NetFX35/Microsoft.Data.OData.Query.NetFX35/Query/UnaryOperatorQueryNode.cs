using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003A RID: 58
	public sealed class UnaryOperatorQueryNode : SingleValueQueryNode
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008840 File Offset: 0x00006A40
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00008848 File Offset: 0x00006A48
		public UnaryOperatorKind OperatorKind { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008851 File Offset: 0x00006A51
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00008859 File Offset: 0x00006A59
		public SingleValueQueryNode Operand { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008862 File Offset: 0x00006A62
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Operand == null || this.Operand.TypeReference == null)
				{
					return null;
				}
				return this.Operand.TypeReference;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00008886 File Offset: 0x00006A86
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.UnaryOperator;
			}
		}
	}
}
