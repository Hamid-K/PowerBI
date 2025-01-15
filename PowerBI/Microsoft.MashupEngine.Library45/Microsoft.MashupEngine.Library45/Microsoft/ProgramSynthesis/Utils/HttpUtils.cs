using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000497 RID: 1175
	public static class HttpUtils
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x0004FC03 File Offset: 0x0004DE03
		public static HttpClient CreateHttpClient()
		{
			ServicePointManager.CheckCertificateRevocationList = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			return new HttpClient();
		}
	}
}
