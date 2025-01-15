using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020B RID: 523
	internal sealed class PlanGroupByColumn : PlanGroupByItem
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00048A21 File Offset: 0x00046C21
		internal PlanGroupByColumn(string name)
		{
			this.m_name = name;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00048A30 File Offset: 0x00046C30
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00048A38 File Offset: 0x00046C38
		internal override void Accept(IPlanGroupByItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00048A41 File Offset: 0x00046C41
		protected override int GetHashCodeInternal()
		{
			return this.m_name.GetHashCode();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00048A50 File Offset: 0x00046C50
		public override bool Equals(PlanGroupByItem other)
		{
			PlanGroupByColumn planGroupByColumn = other as PlanGroupByColumn;
			return planGroupByColumn != null && this.Name == planGroupByColumn.Name;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00048A7A File Offset: 0x00046C7A
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupByColumn");
			builder.WriteAttribute<string>("Name", this.m_name, false, false);
			builder.EndObject();
		}

		// Token: 0x04000836 RID: 2102
		private readonly string m_name;
	}
}
