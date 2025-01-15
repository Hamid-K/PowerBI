using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000233 RID: 563
	internal sealed class PlanOperationUnion : PlanOperation
	{
		// Token: 0x06001348 RID: 4936 RVA: 0x0004A561 File Offset: 0x00048761
		internal PlanOperationUnion(IList<PlanOperation> tables)
		{
			this.m_tables = tables.AsReadOnlyCollection<PlanOperation>();
			Contract.RetailAssert(this.m_tables.Count > 1, "at least 2 tables required");
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0004A58D File Offset: 0x0004878D
		public ReadOnlyCollection<PlanOperation> Tables
		{
			get
			{
				return this.m_tables;
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004A595 File Offset: 0x00048795
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004A5A0 File Offset: 0x000487A0
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationUnion planOperationUnion;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationUnion>(this, other, out flag, out planOperationUnion))
			{
				return flag;
			}
			return this.Tables.SequenceEqual(planOperationUnion.Tables);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004A5CD File Offset: 0x000487CD
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Union");
			builder.WriteProperty<ReadOnlyCollection<PlanOperation>>("Tables", this.Tables, false);
			builder.EndObject();
		}

		// Token: 0x04000880 RID: 2176
		private readonly ReadOnlyCollection<PlanOperation> m_tables;
	}
}
