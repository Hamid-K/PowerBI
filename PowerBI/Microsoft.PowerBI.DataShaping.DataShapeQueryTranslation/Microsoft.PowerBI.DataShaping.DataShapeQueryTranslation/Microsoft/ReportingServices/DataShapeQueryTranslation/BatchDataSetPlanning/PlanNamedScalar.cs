using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A0 RID: 416
	internal sealed class PlanNamedScalar : IEquatable<PlanNamedScalar>, IPlanNamedItem, IStructuredToString
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003B9C9 File Offset: 0x00039BC9
		internal PlanNamedScalar(string name, PlanExpression expression)
		{
			this.m_name = name;
			this.m_expression = expression;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0003B9DF File Offset: 0x00039BDF
		public PlanNamedItemKind Kind
		{
			get
			{
				return PlanNamedItemKind.Scalar;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003B9E2 File Offset: 0x00039BE2
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0003B9EA File Offset: 0x00039BEA
		public PlanExpression Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003B9F2 File Offset: 0x00039BF2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanNamedScalar);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003BA00 File Offset: 0x00039C00
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003BA0D File Offset: 0x00039C0D
		public bool Equals(PlanNamedScalar other)
		{
			return other != null && this.Name == other.Name && this.Expression.Equals(other.Expression);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003BA38 File Offset: 0x00039C38
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003BA61 File Offset: 0x00039C61
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003BA6A File Offset: 0x00039C6A
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("NamedScalar");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			builder.WriteProperty<PlanExpression>("Expression", this.Expression, false);
			builder.EndObject();
		}

		// Token: 0x040006F2 RID: 1778
		private readonly string m_name;

		// Token: 0x040006F3 RID: 1779
		private readonly PlanExpression m_expression;
	}
}
