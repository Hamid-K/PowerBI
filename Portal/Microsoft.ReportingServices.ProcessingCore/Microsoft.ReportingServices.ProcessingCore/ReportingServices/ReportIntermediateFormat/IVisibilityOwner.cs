using System;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D6 RID: 1238
	public interface IVisibilityOwner : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable
	{
		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06003E87 RID: 16007
		Visibility Visibility { get; }

		// Token: 0x06003E88 RID: 16008
		bool ComputeHidden(RenderingContext renderingContext, ToggleCascadeDirection direction);

		// Token: 0x06003E89 RID: 16009
		bool ComputeDeepHidden(RenderingContext renderingContext, ToggleCascadeDirection direction);

		// Token: 0x06003E8A RID: 16010
		bool ComputeStartHidden(RenderingContext renderingContext);

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06003E8B RID: 16011
		// (set) Token: 0x06003E8C RID: 16012
		IVisibilityOwner ContainingDynamicVisibility { get; set; }

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06003E8D RID: 16013
		// (set) Token: 0x06003E8E RID: 16014
		IVisibilityOwner ContainingDynamicColumnVisibility { get; set; }

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06003E8F RID: 16015
		// (set) Token: 0x06003E90 RID: 16016
		IVisibilityOwner ContainingDynamicRowVisibility { get; set; }

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06003E91 RID: 16017
		string SenderUniqueName { get; }
	}
}
