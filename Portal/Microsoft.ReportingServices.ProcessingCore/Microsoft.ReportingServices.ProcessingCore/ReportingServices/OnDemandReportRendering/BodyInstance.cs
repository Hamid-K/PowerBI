using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000304 RID: 772
	public sealed class BodyInstance : ReportElementInstance
	{
		// Token: 0x06001C4C RID: 7244 RVA: 0x00070A21 File Offset: 0x0006EC21
		internal BodyInstance(Body bodyDef)
			: base(bodyDef)
		{
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00070A2C File Offset: 0x0006EC2C
		public string UniqueName
		{
			get
			{
				if (!this.m_reportElementDef.IsOldSnapshot)
				{
					ReportSection sectionDef = this.BodyDefinition.SectionDef;
					return InstancePathItem.GenerateUniqueNameString(sectionDef.ID, sectionDef.InstancePath) + "xB";
				}
				ReportInstanceInfo instanceInfo = this.BodyDefinition.RenderReport.InstanceInfo;
				if (instanceInfo != null)
				{
					return instanceInfo.BodyUniqueName.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00070A9B File Offset: 0x0006EC9B
		internal Body BodyDefinition
		{
			get
			{
				return (Body)this.m_reportElementDef;
			}
		}
	}
}
