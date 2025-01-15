using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000873 RID: 2163
	internal class RuntimeMapDataRegionObjReference : RuntimeChartCriObjReference, IReference<RuntimeMapDataRegionObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007773 RID: 30579 RVA: 0x001ED9EA File Offset: 0x001EBBEA
		internal RuntimeMapDataRegionObjReference()
		{
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x001ED9F2 File Offset: 0x001EBBF2
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObjReference;
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x001ED9F9 File Offset: 0x001EBBF9
		public new RuntimeMapDataRegionObj Value()
		{
			return (RuntimeMapDataRegionObj)base.InternalValue();
		}
	}
}
