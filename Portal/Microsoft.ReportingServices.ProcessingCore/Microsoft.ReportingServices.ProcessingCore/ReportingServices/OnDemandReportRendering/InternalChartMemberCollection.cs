using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000274 RID: 628
	internal sealed class InternalChartMemberCollection : ChartMemberCollection
	{
		// Token: 0x06001865 RID: 6245 RVA: 0x00064B04 File Offset: 0x00062D04
		internal InternalChartMemberCollection(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ChartMember parent, ChartMemberList memberDefs)
			: base(parentDefinitionPath, owner)
		{
			this.m_parent = parent;
			this.m_memberDefs = memberDefs;
		}

		// Token: 0x17000DEF RID: 3567
		public override ChartMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_children == null)
				{
					this.m_children = new DataRegionMember[this.Count];
				}
				ChartMember chartMember = (ChartMember)this.m_children[index];
				if (chartMember == null)
				{
					IReportScope reportScope = ((this.m_parent != null) ? this.m_parent.ReportScope : this.m_owner.ReportScope);
					chartMember = (this.m_children[index] = new InternalChartMember(reportScope, this, base.OwnerChart, this.m_parent, this.m_memberDefs[index], index));
				}
				return chartMember;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00064BE1 File Offset: 0x00062DE1
		public override int Count
		{
			get
			{
				return this.m_memberDefs.OriginalNodeCount;
			}
		}

		// Token: 0x04000C6C RID: 3180
		private ChartMember m_parent;

		// Token: 0x04000C6D RID: 3181
		private ChartMemberList m_memberDefs;
	}
}
