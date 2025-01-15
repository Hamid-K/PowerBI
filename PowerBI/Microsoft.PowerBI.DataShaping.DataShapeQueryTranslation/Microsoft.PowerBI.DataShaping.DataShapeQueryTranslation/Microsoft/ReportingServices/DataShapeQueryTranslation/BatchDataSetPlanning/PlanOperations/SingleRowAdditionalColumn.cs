using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000227 RID: 551
	internal sealed class SingleRowAdditionalColumn : IEquatable<SingleRowAdditionalColumn>, IStructuredToString
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x00049BEA File Offset: 0x00047DEA
		internal SingleRowAdditionalColumn(string planName, ExpressionId expressionId, ExpressionContext expressionContext)
		{
			this.m_planName = planName;
			this.m_expressionId = expressionId;
			this.m_expressionContext = expressionContext;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00049C07 File Offset: 0x00047E07
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00049C0F File Offset: 0x00047E0F
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00049C17 File Offset: 0x00047E17
		public ExpressionContext ExpressionContext
		{
			get
			{
				return this.m_expressionContext;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00049C1F File Offset: 0x00047E1F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SingleRowAdditionalColumn);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00049C30 File Offset: 0x00047E30
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash(this.PlanName.GetHashCode(), this.ExpressionId.GetHashCode()), this.ExpressionContext.GetHashCode());
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00049C71 File Offset: 0x00047E71
		public bool Equals(SingleRowAdditionalColumn other)
		{
			return other != null && this.PlanName == other.PlanName && this.ExpressionId == other.ExpressionId && this.ExpressionContext.Equals(other.ExpressionContext);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00049CB0 File Offset: 0x00047EB0
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("SingleRowAdditionalColumn");
			builder.WriteAttribute<string>("PlanName", this.PlanName, false, false);
			builder.WriteProperty<ExpressionId>("ExpressionId", this.ExpressionId, false);
			builder.WriteProperty<ExpressionContext>("ExpressionContext", this.ExpressionContext, false);
			builder.EndObject();
		}

		// Token: 0x04000867 RID: 2151
		private readonly string m_planName;

		// Token: 0x04000868 RID: 2152
		private readonly ExpressionId m_expressionId;

		// Token: 0x04000869 RID: 2153
		private readonly ExpressionContext m_expressionContext;
	}
}
