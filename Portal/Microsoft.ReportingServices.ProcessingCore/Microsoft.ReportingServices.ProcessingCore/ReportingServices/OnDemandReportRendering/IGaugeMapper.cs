using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200013D RID: 317
	internal interface IGaugeMapper : IDVMappingLayer, IDisposable
	{
		// Token: 0x06000DDC RID: 3548
		void RenderGaugePanel();

		// Token: 0x06000DDD RID: 3549
		void RenderDataGaugePanel();
	}
}
