using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E0 RID: 736
	internal interface IStreamRequest
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001A37 RID: 6711
		Stream InputStream { get; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001A38 RID: 6712
		NameValueCollection RequestHeaders { get; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001A39 RID: 6713
		NameValueCollection RequestParameters { get; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001A3A RID: 6714
		Dictionary<string, Stream> RequestFiles { get; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001A3B RID: 6715
		NameValueCollection BrowserCapabilities { get; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001A3C RID: 6716
		string RequestedItemPath { get; }
	}
}
