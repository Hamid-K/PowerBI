using System;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000003 RID: 3
	internal static class Constants
	{
		// Token: 0x04000001 RID: 1
		internal const string VersionKey = "owin.Version";

		// Token: 0x04000002 RID: 2
		internal const string OwinVersion = "1.0";

		// Token: 0x04000003 RID: 3
		internal const string CallCancelledKey = "owin.CallCancelled";

		// Token: 0x04000004 RID: 4
		internal const string ServerCapabilitiesKey = "server.Capabilities";

		// Token: 0x04000005 RID: 5
		internal const string RequestBodyKey = "owin.RequestBody";

		// Token: 0x04000006 RID: 6
		internal const string RequestHeadersKey = "owin.RequestHeaders";

		// Token: 0x04000007 RID: 7
		internal const string RequestSchemeKey = "owin.RequestScheme";

		// Token: 0x04000008 RID: 8
		internal const string RequestMethodKey = "owin.RequestMethod";

		// Token: 0x04000009 RID: 9
		internal const string RequestPathBaseKey = "owin.RequestPathBase";

		// Token: 0x0400000A RID: 10
		internal const string RequestPathKey = "owin.RequestPath";

		// Token: 0x0400000B RID: 11
		internal const string RequestQueryStringKey = "owin.RequestQueryString";

		// Token: 0x0400000C RID: 12
		internal const string HttpRequestProtocolKey = "owin.RequestProtocol";

		// Token: 0x0400000D RID: 13
		internal const string HttpResponseProtocolKey = "owin.ResponseProtocol";

		// Token: 0x0400000E RID: 14
		internal const string ResponseStatusCodeKey = "owin.ResponseStatusCode";

		// Token: 0x0400000F RID: 15
		internal const string ResponseReasonPhraseKey = "owin.ResponseReasonPhrase";

		// Token: 0x04000010 RID: 16
		internal const string ResponseHeadersKey = "owin.ResponseHeaders";

		// Token: 0x04000011 RID: 17
		internal const string ResponseBodyKey = "owin.ResponseBody";

		// Token: 0x04000012 RID: 18
		internal const string ClientCertifiateKey = "ssl.ClientCertificate";

		// Token: 0x04000013 RID: 19
		internal const string LoadClientCertAsyncKey = "ssl.LoadClientCertAsync";

		// Token: 0x04000014 RID: 20
		internal const string RemoteIpAddressKey = "server.RemoteIpAddress";

		// Token: 0x04000015 RID: 21
		internal const string RemotePortKey = "server.RemotePort";

		// Token: 0x04000016 RID: 22
		internal const string LocalIpAddressKey = "server.LocalIpAddress";

		// Token: 0x04000017 RID: 23
		internal const string LocalPortKey = "server.LocalPort";

		// Token: 0x04000018 RID: 24
		internal const string IsLocalKey = "server.IsLocal";

		// Token: 0x04000019 RID: 25
		internal const string ServerOnSendingHeadersKey = "server.OnSendingHeaders";

		// Token: 0x0400001A RID: 26
		internal const string ServerUserKey = "server.User";

		// Token: 0x0400001B RID: 27
		internal const string ServerLoggerFactoryKey = "server.LoggerFactory";

		// Token: 0x0400001C RID: 28
		internal const string HostAddressesKey = "host.Addresses";

		// Token: 0x0400001D RID: 29
		internal const string WebSocketVersionKey = "websocket.Version";

		// Token: 0x0400001E RID: 30
		internal const string WebSocketVersion = "1.0";

		// Token: 0x0400001F RID: 31
		internal const string WebSocketAcceptKey = "websocket.Accept";

		// Token: 0x04000020 RID: 32
		internal const string WebSocketSubProtocolKey = "websocket.SubProtocol";

		// Token: 0x04000021 RID: 33
		internal const string WebSocketReceiveBufferSizeKey = "websocket.ReceiveBufferSize";

		// Token: 0x04000022 RID: 34
		internal const string WebSocketKeepAliveIntervalKey = "websocket.KeepAliveInterval";

		// Token: 0x04000023 RID: 35
		internal const string WebSocketBufferKey = "websocket.Buffer";

		// Token: 0x04000024 RID: 36
		internal const string HostHeader = "Host";

		// Token: 0x04000025 RID: 37
		internal const string WwwAuthenticateHeader = "WWW-Authenticate";

		// Token: 0x04000026 RID: 38
		internal const string ContentLengthHeader = "Content-Length";

		// Token: 0x04000027 RID: 39
		internal const string TransferEncodingHeader = "Transfer-Encoding";

		// Token: 0x04000028 RID: 40
		internal const string KeepAliveHeader = "Keep-Alive";

		// Token: 0x04000029 RID: 41
		internal const string ConnectionHeader = "Connection";

		// Token: 0x0400002A RID: 42
		internal const string SecWebSocketProtocol = "Sec-WebSocket-Protocol";

		// Token: 0x0400002B RID: 43
		internal const string SecWebSocketVersion = "Sec-WebSocket-Version";

		// Token: 0x0400002C RID: 44
		internal const int ErrorConnectionNoLongerValid = 1229;
	}
}
