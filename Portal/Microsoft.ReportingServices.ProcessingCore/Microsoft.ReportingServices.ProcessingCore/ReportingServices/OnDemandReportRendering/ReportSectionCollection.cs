using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000311 RID: 785
	public sealed class ReportSectionCollection : ReportElementCollectionBase<ReportSection>
	{
		// Token: 0x06001D0D RID: 7437 RVA: 0x00073264 File Offset: 0x00071464
		internal ReportSectionCollection(Microsoft.ReportingServices.OnDemandReportRendering.Report reportDef)
		{
			List<ReportSection> reportSections = reportDef.ReportDef.ReportSections;
			this.m_sections = new ReportSection[reportSections.Count];
			for (int i = 0; i < this.m_sections.Length; i++)
			{
				this.m_sections[i] = new ReportSection(reportDef, reportSections[i], i);
			}
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000732BD File Offset: 0x000714BD
		internal ReportSectionCollection(Microsoft.ReportingServices.OnDemandReportRendering.Report reportDef, Microsoft.ReportingServices.ReportRendering.Report renderReport)
		{
			this.m_sections = new ReportSection[1];
			this.m_sections[0] = new ReportSection(reportDef, renderReport, 0);
		}

		// Token: 0x1700103F RID: 4159
		public override ReportSection this[int index]
		{
			get
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_sections[index];
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00073337 File Offset: 0x00071537
		public override int Count
		{
			get
			{
				return this.m_sections.Length;
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00073344 File Offset: 0x00071544
		internal void SetNewContext()
		{
			for (int i = 0; i < this.m_sections.Length; i++)
			{
				this.m_sections[i].SetNewContext();
			}
		}

		// Token: 0x04000F32 RID: 3890
		private ReportSection[] m_sections;
	}
}
