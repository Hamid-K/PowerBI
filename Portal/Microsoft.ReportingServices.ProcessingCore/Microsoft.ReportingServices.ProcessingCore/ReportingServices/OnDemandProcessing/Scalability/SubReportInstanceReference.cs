using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200083E RID: 2110
	internal class SubReportInstanceReference : ScopeInstanceReference, IReference<SubReportInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060075F3 RID: 30195 RVA: 0x001E9016 File Offset: 0x001E7216
		internal SubReportInstanceReference()
		{
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x001E901E File Offset: 0x001E721E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference;
		}

		// Token: 0x060075F5 RID: 30197 RVA: 0x001E9022 File Offset: 0x001E7222
		public new SubReportInstance Value()
		{
			return (SubReportInstance)base.InternalValue();
		}
	}
}
