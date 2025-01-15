using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200083D RID: 2109
	internal class DataRegionInstanceReference : ScopeInstanceReference, IReference<DataRegionInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060075F0 RID: 30192 RVA: 0x001E8FFD File Offset: 0x001E71FD
		internal DataRegionInstanceReference()
		{
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x001E9005 File Offset: 0x001E7205
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference;
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x001E9009 File Offset: 0x001E7209
		public new DataRegionInstance Value()
		{
			return (DataRegionInstance)base.InternalValue();
		}
	}
}
