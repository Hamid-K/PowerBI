using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200083F RID: 2111
	internal class ReportInstanceReference : ScopeInstanceReference, IReference<ReportInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060075F6 RID: 30198 RVA: 0x001E902F File Offset: 0x001E722F
		internal ReportInstanceReference()
		{
		}

		// Token: 0x060075F7 RID: 30199 RVA: 0x001E9037 File Offset: 0x001E7237
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference;
		}

		// Token: 0x060075F8 RID: 30200 RVA: 0x001E903B File Offset: 0x001E723B
		public new ReportInstance Value()
		{
			return (ReportInstance)base.InternalValue();
		}
	}
}
