using System;
using System.IO;
using System.Net.Http;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework
{
	// Token: 0x0200046F RID: 1135
	public static class TlsExtensions
	{
		// Token: 0x0600236B RID: 9067 RVA: 0x0007FE8C File Offset: 0x0007E08C
		public static void TryTraceSslProtocol(this HttpResponseMessage httpResponseMessage, string name)
		{
			try
			{
				httpResponseMessage.Content.GetStream().TryTraceSslProtocol(name, httpResponseMessage.RequestMessage.RequestUri);
			}
			catch (Exception ex)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Unable to get SslProtocol for this {0}.  Exception: {1}", new object[]
				{
					name,
					ex.ToString()
				});
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0007FEEC File Offset: 0x0007E0EC
		public static void TryTraceSslProtocol(this Stream responseStream, string name, Uri uri)
		{
			try
			{
				string sslProtocolTraceString = responseStream.GetSslProtocolTraceString();
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("{0}:{1}:Uri Info:{2}", new object[]
				{
					name,
					sslProtocolTraceString,
					(uri != null) ? uri.GetLeftPart(UriPartial.Authority) : string.Empty
				});
			}
			catch (Exception ex)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Unable to get SslProtocol for this {0}.  Exception: {1}", new object[]
				{
					name,
					ex.ToString()
				});
			}
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0007FF70 File Offset: 0x0007E170
		public static string GetSslProtocolTraceString(this Stream responseStream)
		{
			return "SslProtocol:" + responseStream.GetSslProtocol();
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0007FF84 File Offset: 0x0007E184
		public static void TryTraceTlsInformation(this OperationContext operationContext, string name)
		{
			try
			{
				operationContext.RequestContext.GetHttpListenerContext().Request.TryTraceTlsInfo(name);
			}
			catch (Exception ex)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Unable to get TlsInfo for this {0}.  Exception: {1}", new object[]
				{
					name,
					ex.ToString()
				});
			}
		}
	}
}
