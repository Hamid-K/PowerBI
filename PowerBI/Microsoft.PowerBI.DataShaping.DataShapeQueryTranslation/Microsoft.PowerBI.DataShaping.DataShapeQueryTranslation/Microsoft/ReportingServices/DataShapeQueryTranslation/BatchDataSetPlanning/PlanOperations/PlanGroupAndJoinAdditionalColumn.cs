using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001EB RID: 491
	internal sealed class PlanGroupAndJoinAdditionalColumn : IEquatable<PlanGroupAndJoinAdditionalColumn>, IStructuredToString
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x00046560 File Offset: 0x00044760
		internal PlanGroupAndJoinAdditionalColumn(string planName, Expression expression, bool suppressJoinPredicate, ExpressionContext expressionContext)
		{
			this.m_planName = planName;
			this.m_expression = expression;
			this.m_suppressJoinPredicate = suppressJoinPredicate;
			this.m_expressionContext = expressionContext;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00046585 File Offset: 0x00044785
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0004658D File Offset: 0x0004478D
		public Expression Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00046595 File Offset: 0x00044795
		public bool SuppressJoinPredicate
		{
			get
			{
				return this.m_suppressJoinPredicate;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004659D File Offset: 0x0004479D
		public ExpressionContext ExpressionContext
		{
			get
			{
				return this.m_expressionContext;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000465A5 File Offset: 0x000447A5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanGroupAndJoinAdditionalColumn);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000465B4 File Offset: 0x000447B4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(this.PlanName.GetHashCode(), this.Expression.GetHashCode()), this.SuppressJoinPredicate.GetHashCode()), this.ExpressionContext.GetHashCode());
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00046600 File Offset: 0x00044800
		public bool Equals(PlanGroupAndJoinAdditionalColumn other)
		{
			return other != null && this.PlanName == other.PlanName && this.Expression == other.Expression && this.SuppressJoinPredicate == other.SuppressJoinPredicate && this.ExpressionContext.Equals(other.ExpressionContext);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00046654 File Offset: 0x00044854
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupAndJoinAdditionalColumn");
			builder.WriteAttribute<string>("PlanName", this.PlanName, false, false);
			builder.WriteAttribute<bool>("SuppressJoinPredicate", this.SuppressJoinPredicate, false, false);
			builder.WriteProperty<Expression>("Expression", this.Expression, false);
			builder.WriteProperty<ExpressionContext>("ExpressionContext", this.ExpressionContext, false);
			builder.EndObject();
		}

		// Token: 0x040007E4 RID: 2020
		private readonly string m_planName;

		// Token: 0x040007E5 RID: 2021
		private readonly Expression m_expression;

		// Token: 0x040007E6 RID: 2022
		private readonly bool m_suppressJoinPredicate;

		// Token: 0x040007E7 RID: 2023
		private readonly ExpressionContext m_expressionContext;
	}
}
