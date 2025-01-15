using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001EF RID: 495
	internal sealed class PlanOperationAddMissingItems : PlanOperation
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x00046898 File Offset: 0x00044A98
		internal PlanOperationAddMissingItems(PlanOperation table, IEnumerable<PlanGroupByMember> groups, IEnumerable<PlanGroupByMember> showAllMembers, IEnumerable<PlanOperation> contextTables)
		{
			this.m_table = table;
			this.m_groups = groups.ToReadOnlyCollection<PlanGroupByMember>();
			this.m_showAllMembers = showAllMembers.ToReadOnlyCollection<PlanGroupByMember>();
			this.m_contextTables = contextTables.ToReadOnlyCollection<PlanOperation>();
			Contract.RetailAssert(this.m_groups.Count > 0, "AddMissingItems requires at least one group.");
			Contract.RetailAssert(this.m_showAllMembers.Count > 0, "AddMissingItems requires at least one showAll member.");
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00046907 File Offset: 0x00044B07
		public PlanOperation Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0004690F File Offset: 0x00044B0F
		public ReadOnlyCollection<PlanGroupByMember> Groups
		{
			get
			{
				return this.m_groups;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00046917 File Offset: 0x00044B17
		public ReadOnlyCollection<PlanGroupByMember> ShowAllMembers
		{
			get
			{
				return this.m_showAllMembers;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0004691F File Offset: 0x00044B1F
		public ReadOnlyCollection<PlanOperation> ContextTables
		{
			get
			{
				return this.m_contextTables;
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00046927 File Offset: 0x00044B27
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00046930 File Offset: 0x00044B30
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationAddMissingItems planOperationAddMissingItems;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationAddMissingItems>(this, other, out flag, out planOperationAddMissingItems))
			{
				return flag;
			}
			return this.Table.Equals(planOperationAddMissingItems.Table) && this.Groups.SequenceEqual(planOperationAddMissingItems.Groups) && this.ContextTables.SequenceEqual(planOperationAddMissingItems.ContextTables) && this.ShowAllMembers.SequenceEqual(planOperationAddMissingItems.ShowAllMembers);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00046998 File Offset: 0x00044B98
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AddMissingItems");
			builder.WriteProperty<ReadOnlyCollection<PlanGroupByMember>>("Groups", this.Groups, false);
			builder.WriteProperty<ReadOnlyCollection<PlanGroupByMember>>("ShowAllMembers", this.ShowAllMembers, false);
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.WriteProperty<ReadOnlyCollection<PlanOperation>>("ContextTables", this.ContextTables, false);
			builder.EndObject();
		}

		// Token: 0x040007F0 RID: 2032
		private readonly PlanOperation m_table;

		// Token: 0x040007F1 RID: 2033
		private readonly ReadOnlyCollection<PlanGroupByMember> m_groups;

		// Token: 0x040007F2 RID: 2034
		private readonly ReadOnlyCollection<PlanGroupByMember> m_showAllMembers;

		// Token: 0x040007F3 RID: 2035
		private readonly ReadOnlyCollection<PlanOperation> m_contextTables;
	}
}
