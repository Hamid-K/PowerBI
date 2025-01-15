using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200006F RID: 111
	public interface IImage
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060006FC RID: 1788
		byte[] ImageData { get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060006FD RID: 1789
		string MIMEType { get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060006FE RID: 1790
		string StreamName { get; }
	}
}
