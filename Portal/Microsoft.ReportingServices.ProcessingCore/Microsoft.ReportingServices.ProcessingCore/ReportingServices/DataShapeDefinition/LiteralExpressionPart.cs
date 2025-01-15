using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000596 RID: 1430
	[DataContract]
	internal sealed class LiteralExpressionPart : ExpressionPart
	{
		// Token: 0x060051D1 RID: 20945 RVA: 0x0015A19B File Offset: 0x0015839B
		public LiteralExpressionPart(object value)
		{
			this.m_value = value;
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x060051D2 RID: 20946 RVA: 0x0015A1AA File Offset: 0x001583AA
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x0015A1B2 File Offset: 0x001583B2
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.Literal;
			}
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0015A1B8 File Offset: 0x001583B8
		public override bool Equals(ExpressionPart other)
		{
			LiteralExpressionPart literalExpressionPart = other as LiteralExpressionPart;
			return literalExpressionPart != null && object.Equals(this.Value, literalExpressionPart.Value);
		}

		// Token: 0x04002955 RID: 10581
		[DataMember(Name = "Value", Order = 1)]
		private readonly object m_value;
	}
}
