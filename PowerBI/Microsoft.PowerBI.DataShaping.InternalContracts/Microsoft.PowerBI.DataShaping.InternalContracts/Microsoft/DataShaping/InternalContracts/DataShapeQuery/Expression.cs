using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200008A RID: 138
	[DebuggerDisplay("[Expression] ExpressionId={ExpressionId} Value={Value}")]
	internal sealed class Expression : IStructuredToString
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00006E70 File Offset: 0x00005070
		internal Expression(ExpressionNode node, ExpressionId? expressionId)
		{
			this.OriginalNode = node;
			this.ExpressionId = expressionId;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00006E86 File Offset: 0x00005086
		internal Expression(ExpressionNode node)
		{
			this.OriginalNode = node;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00006E95 File Offset: 0x00005095
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00006E9D File Offset: 0x0000509D
		public ExpressionNode OriginalNode { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00006EA6 File Offset: 0x000050A6
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00006EAE File Offset: 0x000050AE
		public ExpressionId? ExpressionId { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x00006EB7 File Offset: 0x000050B7
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.WriteValue<ExpressionId>(this.ExpressionId);
		}
	}
}
