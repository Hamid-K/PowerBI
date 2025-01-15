using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x0200013D RID: 317
	[DataContract]
	internal sealed class FunctionCallExpressionNode : ExpressionNode
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001035F File Offset: 0x0000E55F
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x00010367 File Offset: 0x0000E567
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal FunctionKind Kind { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00010370 File Offset: 0x0000E570
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00010378 File Offset: 0x0000E578
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<ExpressionNode> Arguments { get; set; }

		// Token: 0x06000870 RID: 2160 RVA: 0x00010381 File Offset: 0x0000E581
		internal override TResult Accept<TResult>(IExpressionNodeVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
