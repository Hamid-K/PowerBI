using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x02000140 RID: 320
	[DataContract]
	internal sealed class LiteralExpressionNode : ExpressionNode
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00010392 File Offset: 0x0000E592
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0001039A File Offset: 0x0000E59A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal object Value { get; set; }

		// Token: 0x06000877 RID: 2167 RVA: 0x000103A3 File Offset: 0x0000E5A3
		internal override TResult Accept<TResult>(IExpressionNodeVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
