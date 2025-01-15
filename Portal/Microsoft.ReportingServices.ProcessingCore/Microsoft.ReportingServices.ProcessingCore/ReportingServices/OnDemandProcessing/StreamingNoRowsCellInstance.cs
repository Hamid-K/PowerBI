using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000832 RID: 2098
	[PersistedWithinRequestOnly]
	[SkipStaticValidation]
	internal class StreamingNoRowsCellInstance : StreamingNoRowsScopeInstanceBase
	{
		// Token: 0x0600758E RID: 30094 RVA: 0x001E752B File Offset: 0x001E572B
		public StreamingNoRowsCellInstance(OnDemandProcessingContext odpContext, IRIFReportDataScope cell)
			: base(odpContext, cell)
		{
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x001E7535 File Offset: 0x001E5735
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StreamingNoRowsCellInstance;
		}
	}
}
