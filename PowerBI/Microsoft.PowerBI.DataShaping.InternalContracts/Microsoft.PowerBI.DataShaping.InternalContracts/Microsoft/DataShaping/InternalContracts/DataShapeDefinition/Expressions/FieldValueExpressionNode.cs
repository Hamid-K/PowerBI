using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x0200013C RID: 316
	[DataContract]
	internal sealed class FieldValueExpressionNode : ExpressionNode
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001032C File Offset: 0x0000E52C
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00010334 File Offset: 0x0000E534
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string FieldId { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001033D File Offset: 0x0000E53D
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00010345 File Offset: 0x0000E545
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string TableId { get; set; }

		// Token: 0x0600086A RID: 2154 RVA: 0x0001034E File Offset: 0x0000E54E
		internal override TResult Accept<TResult>(IExpressionNodeVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
