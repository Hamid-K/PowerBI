using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200171A RID: 5914
	public sealed class OAuthFactory : IOAuthFactory
	{
		// Token: 0x06009654 RID: 38484 RVA: 0x001F1ECF File Offset: 0x001F00CF
		public OAuthFactory(Func<OAuthServices, OAuthClientApplication, string, IOAuthProvider> providerFactory)
		{
			this.resourceFactory = null;
			this.providerFactory = providerFactory;
		}

		// Token: 0x06009655 RID: 38485 RVA: 0x001F1EE5 File Offset: 0x001F00E5
		public OAuthFactory(Func<OAuthServices, string, OAuthResource> resourceFactory, Func<OAuthServices, OAuthClientApplication, string, IOAuthProvider> providerFactory)
		{
			this.resourceFactory = resourceFactory;
			this.providerFactory = providerFactory;
		}

		// Token: 0x06009656 RID: 38486 RVA: 0x001F1EFB File Offset: 0x001F00FB
		public object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl)
		{
			return this.providerFactory(OAuthFactory.CreateServices(engineHost), (OAuthClientApplication)clientApplication, resourceUrl);
		}

		// Token: 0x06009657 RID: 38487 RVA: 0x001F1F16 File Offset: 0x001F0116
		public object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl)
		{
			if (this.resourceFactory == null)
			{
				return null;
			}
			return this.resourceFactory(OAuthFactory.CreateServices(engineHost), resourceUrl);
		}

		// Token: 0x06009658 RID: 38488 RVA: 0x001F1F34 File Offset: 0x001F0134
		public static OAuthServices CreateServices(IEngineHost engineHost)
		{
			return OAuthServices.From(new OAuthFactory.TracingService(engineHost), new OAuthFactory.OAuthHttpClient(engineHost), new OAuthFactory.OAuthConfigService(engineHost));
		}

		// Token: 0x04004FED RID: 20461
		private readonly Func<OAuthServices, string, OAuthResource> resourceFactory;

		// Token: 0x04004FEE RID: 20462
		private readonly Func<OAuthServices, OAuthClientApplication, string, IOAuthProvider> providerFactory;

		// Token: 0x0200171B RID: 5915
		private sealed class TracingService : IOAuthTracingService
		{
			// Token: 0x06009659 RID: 38489 RVA: 0x001F1F4D File Offset: 0x001F014D
			public TracingService(IEngineHost engineHost)
			{
				this.tracingService = engineHost.QueryService<ITracingService>();
				this.evaluationConstants = engineHost.GetEvaluationConstants();
			}

			// Token: 0x0600965A RID: 38490 RVA: 0x001F1F70 File Offset: 0x001F0170
			public void WriteTrace(string traceName, TraceEventType severityLevel, Dictionary<string, object> traceValues, bool isPii)
			{
				if (this.tracingService != null)
				{
					using (IHostTrace hostTrace = this.tracingService.CreateTrace(this.evaluationConstants, traceName, severityLevel, false, null))
					{
						bool flag = false;
						foreach (KeyValuePair<string, object> keyValuePair in traceValues)
						{
							Exception ex = keyValuePair.Value as Exception;
							if (ex != null)
							{
								if (!flag)
								{
									hostTrace.Add(ex, true);
								}
								flag = true;
							}
							else
							{
								hostTrace.Add(keyValuePair.Key, keyValuePair.Value, isPii);
							}
						}
					}
				}
			}

			// Token: 0x04004FEF RID: 20463
			private readonly ITracingService tracingService;

			// Token: 0x04004FF0 RID: 20464
			private readonly IEvaluationConstants evaluationConstants;
		}

		// Token: 0x0200171C RID: 5916
		private sealed class OAuthHttpClient : IOAuthHttpClient
		{
			// Token: 0x0600965B RID: 38491 RVA: 0x001F2028 File Offset: 0x001F0228
			public OAuthHttpClient(IEngineHost engineHost)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x0600965C RID: 38492 RVA: 0x001F2038 File Offset: 0x001F0238
			public WebRequest CreateRequest(Uri requestUri)
			{
				IResource resource = Resource.New("Web", requestUri.AbsoluteUri);
				WebRequest webRequest = this.engineHost.CreateWebRequest(resource, requestUri);
				MashupHttpWebRequest mashupHttpWebRequest = webRequest as MashupHttpWebRequest;
				if (mashupHttpWebRequest != null)
				{
					mashupHttpWebRequest.AllowAutoRedirect = false;
					mashupHttpWebRequest.UserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";
				}
				return webRequest;
			}

			// Token: 0x0600965D RID: 38493 RVA: 0x001F2080 File Offset: 0x001F0280
			public HttpStatusCode GetResponseStatus(WebResponse response)
			{
				MashupHttpWebResponse mashupHttpWebResponse = response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					return mashupHttpWebResponse.StatusCode;
				}
				return (HttpStatusCode)0;
			}

			// Token: 0x04004FF1 RID: 20465
			private readonly IEngineHost engineHost;
		}

		// Token: 0x0200171D RID: 5917
		private sealed class OAuthConfigService : IOAuthConfigService
		{
			// Token: 0x0600965E RID: 38494 RVA: 0x001F209F File Offset: 0x001F029F
			public OAuthConfigService(IEngineHost engineHost)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x0600965F RID: 38495 RVA: 0x001F20AE File Offset: 0x001F02AE
			public bool TryLookupConfigValue(string key, out object value)
			{
				return this.engineHost.TryGetConfigurationProperty(key, out value);
			}

			// Token: 0x04004FF2 RID: 20466
			private readonly IEngineHost engineHost;
		}
	}
}
