using System;
using System.Net;
using System.Text;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework
{
	// Token: 0x02000470 RID: 1136
	public static class HttpListenerRequestExtension
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x0007FFE0 File Offset: 0x0007E1E0
		public static void TryTraceTlsInfo(this HttpListenerRequest httpListenerRequest, string name)
		{
			if (httpListenerRequest.IsSecureConnection || httpListenerRequest.Url.Scheme == "https")
			{
				try
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation(string.Concat(new string[]
					{
						name,
						":",
						httpListenerRequest.GetTlsInfoAsString(),
						"\nUri Info:",
						httpListenerRequest.Url.GetLeftPart(UriPartial.Authority)
					}));
					return;
				}
				catch (Exception ex)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Unable to get TlsInfo for this {0}.  Exception: {1}", new object[]
					{
						name,
						ex.ToString()
					});
					return;
				}
			}
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceInformation("Connection is not secured with HTTPS.");
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00080094 File Offset: 0x0007E294
		public static string GetTlsInfoAsString(this HttpListenerRequest httpListenerRequest)
		{
			HttpApi.HTTP_SSL_PROTOCOL_INFO tlsInfo = httpListenerRequest.GetTlsInfo();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("TlsInfo:SslProtocol:{0}:".FormatWithInvariantCulture(new object[] { tlsInfo.ProtocolName() }));
			stringBuilder.AppendLine("Cipher:{0};Bits:{1}".FormatWithInvariantCulture(new object[]
			{
				tlsInfo.AlgName(tlsInfo.CipherType),
				tlsInfo.CipherStrength
			}));
			stringBuilder.AppendLine("Hash:{0};Bits:{1}".FormatWithInvariantCulture(new object[]
			{
				tlsInfo.AlgName(tlsInfo.HashType),
				tlsInfo.HashStrength
			}));
			stringBuilder.AppendLine("KeyExchange:{0};Bits:{1}".FormatWithInvariantCulture(new object[]
			{
				tlsInfo.AlgName(tlsInfo.KeyExchangeType),
				tlsInfo.KeyExchangeStrength
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00080174 File Offset: 0x0007E374
		internal static HttpApi.HTTP_SSL_PROTOCOL_INFO GetTlsInfo(this HttpListenerRequest httpListenerRequest)
		{
			return HttpApi.GetSslProtocolInfo(httpListenerRequest.GetRequestBuffer(), httpListenerRequest.GetOriginalBlobAddress());
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00080187 File Offset: 0x0007E387
		internal static HttpApi.HTTP_REQUEST_AUTH_INFO GetAuthInfo(this HttpListenerRequest httpListenerRequest)
		{
			return HttpApi.GetAuthInfo(httpListenerRequest.GetRequestBuffer(), httpListenerRequest.GetOriginalBlobAddress());
		}
	}
}
