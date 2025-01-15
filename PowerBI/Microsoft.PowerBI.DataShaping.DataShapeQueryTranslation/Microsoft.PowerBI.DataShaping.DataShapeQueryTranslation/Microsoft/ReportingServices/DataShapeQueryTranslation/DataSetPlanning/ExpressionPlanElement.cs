using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E8 RID: 232
	[DebuggerDisplay("[Expression] Id={ExpressionId.Value} [SuppressJoinPredicate={SuppressJoinPredicate}]")]
	internal sealed class ExpressionPlanElement : NestedPlanElement
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x00023DCE File Offset: 0x00021FCE
		internal ExpressionPlanElement(ExpressionId expressionId, ExpressionContext expressionContext, bool suppressJoinPredicate)
		{
			this.m_expressionId = expressionId;
			this.m_expressionContext = expressionContext;
			this.m_suppressJoinPredicate = suppressJoinPredicate;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00023DEB File Offset: 0x00021FEB
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00023DF3 File Offset: 0x00021FF3
		public ExpressionContext ExpressionContext
		{
			get
			{
				return this.m_expressionContext;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00023DFB File Offset: 0x00021FFB
		public bool SuppressJoinPredicate
		{
			get
			{
				return this.m_suppressJoinPredicate;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00023E03 File Offset: 0x00022003
		public override NestedPlanElement OmitProjection()
		{
			return this;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00023E06 File Offset: 0x00022006
		public override void Accept(DataSetPlanElementVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00023E0F File Offset: 0x0002200F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ExpressionPlanElement");
			builder.WriteAttribute<ExpressionId>("Expression", this.m_expressionId, false, false);
			builder.WriteAttribute<bool>("SuppressJoinPredicate", this.m_suppressJoinPredicate, false, false);
			builder.EndObject();
		}

		// Token: 0x04000472 RID: 1138
		private readonly ExpressionId m_expressionId;

		// Token: 0x04000473 RID: 1139
		private readonly ExpressionContext m_expressionContext;

		// Token: 0x04000474 RID: 1140
		private readonly bool m_suppressJoinPredicate;
	}
}
