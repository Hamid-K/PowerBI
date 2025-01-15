using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface.RequestTracing
{
	// Token: 0x0200013C RID: 316
	public interface IRequestTrace : IDisposable
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000587 RID: 1415
		int TraceId { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000588 RID: 1416
		Guid? ActivityId { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000589 RID: 1417
		IResource Resource { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600058A RID: 1418
		Guid SessionId { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600058B RID: 1419
		string Type { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600058C RID: 1420
		DateTime Timestamp { get; }

		// Token: 0x0600058D RID: 1421
		Stream GetContentStream();

		// Token: 0x0600058E RID: 1422
		void AddMetadata(string name, string value);
	}
}
