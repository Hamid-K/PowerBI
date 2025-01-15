using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000018 RID: 24
	internal sealed class DataSetFieldReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00004527 File Offset: 0x00002727
		internal DataSetFieldReferenceExpressionNode(IDataSetPlan dataSetPlan, string fieldName, PlanNamedTable tablePlan = null)
		{
			this.m_dataSetPlan = dataSetPlan;
			this.m_fieldName = fieldName;
			this.m_tablePlan = tablePlan;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004544 File Offset: 0x00002744
		public IDataSetPlan DataSetPlan
		{
			get
			{
				return this.m_dataSetPlan;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000454C File Offset: 0x0000274C
		public PlanNamedTable TablePlan
		{
			get
			{
				return this.m_tablePlan;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004554 File Offset: 0x00002754
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000455C File Offset: 0x0000275C
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.DataSetFieldReference;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004560 File Offset: 0x00002760
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<DataSetFieldReferenceExpressionNode>(this, other, out flag, out dataSetFieldReferenceExpressionNode))
			{
				return flag;
			}
			return this.FieldName == dataSetFieldReferenceExpressionNode.FieldName && this.DataSetPlan == dataSetFieldReferenceExpressionNode.DataSetPlan && this.TablePlan == dataSetFieldReferenceExpressionNode.TablePlan;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045AD File Offset: 0x000027AD
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.DataSetPlan.GetHashCode(), this.FieldName.GetHashCode(), Hashing.GetHashCode<PlanNamedTable>(this.TablePlan, null));
		}

		// Token: 0x04000047 RID: 71
		private readonly IDataSetPlan m_dataSetPlan;

		// Token: 0x04000048 RID: 72
		private readonly PlanNamedTable m_tablePlan;

		// Token: 0x04000049 RID: 73
		private readonly string m_fieldName;
	}
}
