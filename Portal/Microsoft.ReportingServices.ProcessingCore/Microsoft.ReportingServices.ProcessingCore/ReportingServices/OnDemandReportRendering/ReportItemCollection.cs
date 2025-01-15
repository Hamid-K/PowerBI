using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000308 RID: 776
	public sealed class ReportItemCollection : ReportElementCollectionBase<Microsoft.ReportingServices.OnDemandReportRendering.ReportItem>
	{
		// Token: 0x06001C96 RID: 7318 RVA: 0x00071AD5 File Offset: 0x0006FCD5
		internal ReportItemCollection(IReportScope reportScope, IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection reportItemColDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_reportScope = reportScope;
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_isOldSnapshot = false;
			this.m_reportItemColDef = reportItemColDef;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00071B01 File Offset: 0x0006FD01
		internal ReportItemCollection(IDefinitionPath parentDefinitionPath, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItemCollection renderReportItemCollection, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_isOldSnapshot = true;
			this.m_inSubtotal = inSubtotal;
			this.m_renderReportItemCollection = renderReportItemCollection;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17001005 RID: 4101
		public override Microsoft.ReportingServices.OnDemandReportRendering.ReportItem this[int index]
		{
			get
			{
				return this.GetItem(index).ExposeAs(this.m_renderingContext);
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00071B44 File Offset: 0x0006FD44
		public override int Count
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_renderReportItemCollection == null)
					{
						return 0;
					}
					return this.m_renderReportItemCollection.Count;
				}
				else
				{
					if (this.m_reportItemColDef.ROMIndexMap != null)
					{
						return this.m_reportItemColDef.ROMIndexMap.Count;
					}
					return this.m_reportItemColDef.Count;
				}
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x00071B98 File Offset: 0x0006FD98
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00071BA0 File Offset: 0x0006FDA0
		internal Microsoft.ReportingServices.ReportRendering.ReportItemCollection RenderReportItemCollection
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReportItemCollection;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00071BBC File Offset: 0x0006FDBC
		internal void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItemCollection renderReportItemCollection)
		{
			if (!this.m_isOldSnapshot)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (renderReportItemCollection != null)
			{
				this.m_renderReportItemCollection = renderReportItemCollection;
			}
			if (this.m_reportItems == null)
			{
				return;
			}
			for (int i = 0; i < this.m_reportItems.Length; i++)
			{
				if (this.m_reportItems[i] != null)
				{
					this.m_reportItems[i].UpdateRenderReportItem(renderReportItemCollection[i]);
				}
			}
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00071C20 File Offset: 0x0006FE20
		internal void SetNewContext()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.GetItem(i).SetNewContext();
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00071C4C File Offset: 0x0006FE4C
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem GetItem(int index)
		{
			if (0 > index || index >= this.Count)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
			}
			if (this.m_reportItems == null)
			{
				this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItem[this.Count];
			}
			Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = this.m_reportItems[index];
			if (reportItem == null)
			{
				if (this.m_isOldSnapshot)
				{
					reportItem = (this.m_reportItems[index] = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateShim(this.m_parentDefinitionPath, index, this.m_inSubtotal, this.m_renderReportItemCollection[index], this.m_renderingContext));
				}
				else
				{
					int num = ((this.m_reportItemColDef.ROMIndexMap == null) ? index : this.m_reportItemColDef.ROMIndexMap[index]);
					reportItem = (this.m_reportItems[index] = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateItem(this.m_reportScope, this.m_parentDefinitionPath, num, this.m_reportItemColDef[num], this.m_renderingContext));
				}
			}
			return reportItem;
		}

		// Token: 0x04000F00 RID: 3840
		private IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000F01 RID: 3841
		private bool m_isOldSnapshot;

		// Token: 0x04000F02 RID: 3842
		private bool m_inSubtotal;

		// Token: 0x04000F03 RID: 3843
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem[] m_reportItems;

		// Token: 0x04000F04 RID: 3844
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection m_reportItemColDef;

		// Token: 0x04000F05 RID: 3845
		private Microsoft.ReportingServices.ReportRendering.ReportItemCollection m_renderReportItemCollection;

		// Token: 0x04000F06 RID: 3846
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_renderingContext;

		// Token: 0x04000F07 RID: 3847
		private IReportScope m_reportScope;
	}
}
