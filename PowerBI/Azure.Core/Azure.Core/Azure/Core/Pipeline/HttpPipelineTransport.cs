using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008F RID: 143
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class HttpPipelineTransport
	{
		// Token: 0x0600049F RID: 1183
		public abstract void Process(HttpMessage message);

		// Token: 0x060004A0 RID: 1184
		public abstract ValueTask ProcessAsync(HttpMessage message);

		// Token: 0x060004A1 RID: 1185
		public abstract Request CreateRequest();

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E040 File Offset: 0x0000C240
		internal static HttpPipelineTransport Create([Nullable(2)] HttpPipelineTransportOptions options = null)
		{
			if (!AppContextSwitchHelper.GetConfigValue("Azure.Core.Pipeline.DisableHttpWebRequestTransport", "AZURE_CORE_DISABLE_HTTPWEBREQUESTTRANSPORT"))
			{
				HttpWebRequestTransport httpWebRequestTransport;
				if (options == null)
				{
					httpWebRequestTransport = HttpWebRequestTransport.Shared;
				}
				else
				{
					httpWebRequestTransport = new HttpWebRequestTransport(options);
				}
				return httpWebRequestTransport;
			}
			HttpClientTransport httpClientTransport;
			if (options == null)
			{
				httpClientTransport = HttpClientTransport.Shared;
			}
			else
			{
				httpClientTransport = new HttpClientTransport(options);
			}
			return httpClientTransport;
		}
	}
}
