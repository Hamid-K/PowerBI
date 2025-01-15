using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000592 RID: 1426
	[DataContract]
	internal sealed class Expression
	{
		// Token: 0x060051C7 RID: 20935 RVA: 0x0015A10C File Offset: 0x0015830C
		internal Expression(ExpressionPart expressionTree)
		{
			this.m_expressionTree = expressionTree;
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x060051C8 RID: 20936 RVA: 0x0015A11B File Offset: 0x0015831B
		internal ExpressionPart ExpressionTree
		{
			get
			{
				return this.m_expressionTree;
			}
		}

		// Token: 0x0400294B RID: 10571
		[DataMember(Name = "ExpressionTree", Order = 1)]
		private readonly ExpressionPart m_expressionTree;
	}
}
