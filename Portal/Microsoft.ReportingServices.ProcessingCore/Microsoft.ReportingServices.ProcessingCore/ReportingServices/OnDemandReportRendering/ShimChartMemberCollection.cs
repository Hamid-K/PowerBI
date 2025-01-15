using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000275 RID: 629
	internal sealed class ShimChartMemberCollection : ChartMemberCollection
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x00064BF0 File Offset: 0x00062DF0
		internal ShimChartMemberCollection(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, bool isCategoryGroup, ShimChartMember parent, ChartMemberCollection renderMemberCollection)
			: base(parentDefinitionPath, owner)
		{
			this.m_isCategoryGroup = isCategoryGroup;
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			int count = renderMemberCollection.Count;
			if (renderMemberCollection[0].IsStatic)
			{
				this.m_isDynamic = false;
				DataRegionMember[] array = new ShimChartMember[count];
				this.m_children = array;
				for (int i = 0; i < count; i++)
				{
					this.m_children[i] = new ShimChartMember(this, owner, parent, i, isCategoryGroup, renderMemberCollection[i]);
				}
			}
			else
			{
				this.m_isDynamic = true;
				DataRegionMember[] array = new ShimChartMember[1];
				this.m_children = array;
				this.m_children[0] = new ShimChartMember(this, owner, parent, 0, isCategoryGroup, new ShimRenderGroups(renderMemberCollection));
			}
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x17000DF1 RID: 3569
		public override ChartMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return (ChartMember)this.m_children[index];
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00064D10 File Offset: 0x00062F10
		public override int Count
		{
			get
			{
				return this.m_children.Length;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00064D1A File Offset: 0x00062F1A
		internal void UpdateContext()
		{
			if (this.m_children == null)
			{
				return;
			}
			if (this.m_isCategoryGroup)
			{
				this.ResetContext(base.OwnerChart.RenderChart.CategoryMemberCollection);
				return;
			}
			this.ResetContext(base.OwnerChart.RenderChart.SeriesMemberCollection);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00064D5C File Offset: 0x00062F5C
		internal void ResetContext(ChartMemberCollection newRenderMemberCollection)
		{
			if (this.m_children == null)
			{
				return;
			}
			if (this.m_isDynamic)
			{
				ShimRenderGroups shimRenderGroups = ((newRenderMemberCollection != null) ? new ShimRenderGroups(newRenderMemberCollection) : null);
				((ShimChartMember)this.m_children[0]).ResetContext(null, shimRenderGroups);
				return;
			}
			for (int i = 0; i < this.m_children.Length; i++)
			{
				ChartMember chartMember = ((newRenderMemberCollection != null) ? newRenderMemberCollection[i] : null);
				((ShimChartMember)this.m_children[i]).ResetContext(chartMember, null);
			}
		}

		// Token: 0x04000C6E RID: 3182
		private bool m_isDynamic;

		// Token: 0x04000C6F RID: 3183
		private bool m_isCategoryGroup;

		// Token: 0x04000C70 RID: 3184
		private int m_definitionStartIndex = -1;

		// Token: 0x04000C71 RID: 3185
		private int m_definitionEndIndex = -1;
	}
}
