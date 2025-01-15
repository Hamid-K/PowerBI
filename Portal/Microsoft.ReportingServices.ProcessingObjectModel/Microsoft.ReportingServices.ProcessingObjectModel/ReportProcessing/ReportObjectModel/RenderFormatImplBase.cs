using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200001D RID: 29
	internal abstract class RenderFormatImplBase : MarshalByRefObject
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600006A RID: 106
		internal abstract string Name { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600006B RID: 107
		internal abstract bool IsInteractive { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600006C RID: 108
		internal abstract ReadOnlyNameValueCollection DeviceInfo { get; }
	}
}
