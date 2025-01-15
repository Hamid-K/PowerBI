using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x0200000E RID: 14
	internal interface IServiceTokenStore
	{
		// Token: 0x0600003E RID: 62
		byte[] UploadValuesPort(Uri tokenUrl, string httpWebRequestMethod, NameValueCollection values);
	}
}
