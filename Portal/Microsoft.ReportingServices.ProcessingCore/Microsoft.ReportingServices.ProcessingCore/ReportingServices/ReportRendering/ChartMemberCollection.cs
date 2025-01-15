using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004B RID: 75
	internal sealed class ChartMemberCollection
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00013C3E File Offset: 0x00011E3E
		internal ChartMemberCollection(Chart owner, ChartMember parent, ChartHeading headingDef, ChartHeadingInstanceList headingInstances)
		{
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_headingInstances = headingInstances;
			this.m_headingDef = headingDef;
		}

		// Token: 0x17000494 RID: 1172
		public ChartMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				ChartMember chartMember = null;
				if (index == 0)
				{
					chartMember = this.m_firstMember;
				}
				else if (this.m_members != null)
				{
					chartMember = this.m_members[index - 1];
				}
				if (chartMember == null)
				{
					ChartHeadingInstance chartHeadingInstance = null;
					if (this.m_headingInstances != null && index < this.m_headingInstances.Count)
					{
						chartHeadingInstance = this.m_headingInstances[index];
					}
					chartMember = new ChartMember(this.m_owner, this.m_parent, this.m_headingDef, chartHeadingInstance, index);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (index == 0)
						{
							this.m_firstMember = chartMember;
						}
						else
						{
							if (this.m_members == null)
							{
								this.m_members = new ChartMember[this.Count - 1];
							}
							this.m_members[index - 1] = chartMember;
						}
					}
				}
				return chartMember;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00013D5C File Offset: 0x00011F5C
		public int Count
		{
			get
			{
				if (this.m_headingInstances != null && this.m_headingInstances.Count != 0)
				{
					return this.m_headingInstances.Count;
				}
				if (this.m_headingDef.Grouping == null && this.m_headingDef.Labels != null)
				{
					return this.m_headingDef.Labels.Count;
				}
				return 1;
			}
		}

		// Token: 0x04000166 RID: 358
		private Chart m_owner;

		// Token: 0x04000167 RID: 359
		private ChartHeading m_headingDef;

		// Token: 0x04000168 RID: 360
		private ChartHeadingInstanceList m_headingInstances;

		// Token: 0x04000169 RID: 361
		private ChartMember[] m_members;

		// Token: 0x0400016A RID: 362
		private ChartMember m_firstMember;

		// Token: 0x0400016B RID: 363
		private ChartMember m_parent;
	}
}
