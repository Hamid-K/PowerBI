using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000216 RID: 534
	internal sealed class PlanOperationProject : PlanOperation
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x00049046 File Offset: 0x00047246
		internal PlanOperationProject(PlanOperation input, bool enforceColumnOrder, params PlanProjectItem[] projects)
			: this(input, projects, enforceColumnOrder)
		{
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00049051 File Offset: 0x00047251
		internal PlanOperationProject(PlanOperation input, IReadOnlyList<PlanProjectItem> projects, bool enforceColumnOrder)
		{
			this.Input = input;
			this.EnforceColumnOrder = enforceColumnOrder;
			this.Projections = projects;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004906E File Offset: 0x0004726E
		public PlanOperation Input { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00049076 File Offset: 0x00047276
		public IReadOnlyList<PlanProjectItem> Projections { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004907E File Offset: 0x0004727E
		public bool EnforceColumnOrder { get; }

		// Token: 0x06001284 RID: 4740 RVA: 0x00049086 File Offset: 0x00047286
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00049090 File Offset: 0x00047290
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationProject planOperationProject;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationProject>(this, other, out flag, out planOperationProject))
			{
				return flag;
			}
			return this.Input.Equals(planOperationProject.Input) && this.Projections.SequenceEqual(planOperationProject.Projections) && this.EnforceColumnOrder == planOperationProject.EnforceColumnOrder;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000490E4 File Offset: 0x000472E4
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Project");
			builder.WriteProperty<IReadOnlyList<PlanProjectItem>>("Projections", this.Projections, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.WriteProperty<bool>("EnforceColumnOrder", this.EnforceColumnOrder, false);
			builder.EndObject();
		}
	}
}
