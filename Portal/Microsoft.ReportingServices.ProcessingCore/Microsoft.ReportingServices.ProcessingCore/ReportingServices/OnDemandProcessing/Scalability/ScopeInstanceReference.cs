using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200083C RID: 2108
	internal class ScopeInstanceReference : Reference<ScopeInstance>
	{
		// Token: 0x060075ED RID: 30189 RVA: 0x001E8FE4 File Offset: 0x001E71E4
		internal ScopeInstanceReference()
		{
		}

		// Token: 0x060075EE RID: 30190 RVA: 0x001E8FEC File Offset: 0x001E71EC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstanceReference;
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x001E8FF0 File Offset: 0x001E71F0
		public ScopeInstance Value()
		{
			return (ScopeInstance)base.InternalValue();
		}
	}
}
