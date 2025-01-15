using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E6 RID: 230
	[DebuggerDisplay("[Intersection] Id={Scope.Id} [Projected={IsProjected}]")]
	internal sealed class DataIntersectionPlanElement : ScopePlanElement
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00023C27 File Offset: 0x00021E27
		internal DataIntersectionPlanElement(DataIntersection dataIntersection, IList<NestedPlanElement> nestedElements, bool isProjected, FilterCondition filterCondition = null, Limit limit = null)
			: base(nestedElements, isProjected, filterCondition, limit)
		{
			this.m_dataIntersection = dataIntersection;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00023C3C File Offset: 0x00021E3C
		public DataIntersection DataIntersection
		{
			get
			{
				return this.m_dataIntersection;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00023C44 File Offset: 0x00021E44
		public override IScope Scope
		{
			get
			{
				return this.m_dataIntersection;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00023C4C File Offset: 0x00021E4C
		public override ScopePlanElement OmitProjection()
		{
			if (!base.IsProjected)
			{
				return this;
			}
			return new DataIntersectionPlanElement(this.DataIntersection, base.GetNestedElementsForOmitProjection(), false, base.FilterCondition, base.Limit);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00023C76 File Offset: 0x00021E76
		public override ScopePlanElement OmitNestedElements()
		{
			return new DataIntersectionPlanElement(this.DataIntersection, null, base.IsProjected, base.FilterCondition, base.Limit);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00023C96 File Offset: 0x00021E96
		public override ScopePlanElement AddNestedPlanElement(NestedPlanElement expression)
		{
			return new DataIntersectionPlanElement(this.DataIntersection, base.AddToNestedElementCollection(expression), base.IsProjected, base.FilterCondition, base.Limit);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00023CBC File Offset: 0x00021EBC
		public override void Accept(DataSetPlanElementVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00023CC5 File Offset: 0x00021EC5
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataIntersectionPlanElement");
			builder.WriteAttribute<DataIntersection>("DataIntersection", this.m_dataIntersection, false, false);
			base.WriteToBase(builder);
			builder.EndObject();
		}

		// Token: 0x0400046E RID: 1134
		private readonly DataIntersection m_dataIntersection;
	}
}
