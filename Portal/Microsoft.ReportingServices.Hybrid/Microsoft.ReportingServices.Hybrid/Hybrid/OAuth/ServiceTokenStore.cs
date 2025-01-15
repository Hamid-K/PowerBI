using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x0200000F RID: 15
	internal sealed class ServiceTokenStore : IServiceTokenStore
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002C70 File Offset: 0x00000E70
		public byte[] UploadValuesPort(Uri tokenUrl, string webReqestMethod, NameValueCollection values)
		{
			byte[] array;
			using (WebClient webClient = new WebClient
			{
				BaseAddress = tokenUrl.ToString()
			})
			{
				array = webClient.UploadValues(tokenUrl, webReqestMethod, values);
			}
			return array;
		}
	}
}
