using System;

namespace Microsoft.Owin
{
	// Token: 0x02000011 RID: 17
	internal static class OwinConstants
	{
		// Token: 0x04000018 RID: 24
		public const string RequestScheme = "owin.RequestScheme";

		// Token: 0x04000019 RID: 25
		public const string RequestMethod = "owin.RequestMethod";

		// Token: 0x0400001A RID: 26
		public const string RequestPathBase = "owin.RequestPathBase";

		// Token: 0x0400001B RID: 27
		public const string RequestPath = "owin.RequestPath";

		// Token: 0x0400001C RID: 28
		public const string RequestQueryString = "owin.RequestQueryString";

		// Token: 0x0400001D RID: 29
		public const string RequestProtocol = "owin.RequestProtocol";

		// Token: 0x0400001E RID: 30
		public const string RequestHeaders = "owin.RequestHeaders";

		// Token: 0x0400001F RID: 31
		public const string RequestBody = "owin.RequestBody";

		// Token: 0x04000020 RID: 32
		public const string ResponseStatusCode = "owin.ResponseStatusCode";

		// Token: 0x04000021 RID: 33
		public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";

		// Token: 0x04000022 RID: 34
		public const string ResponseProtocol = "owin.ResponseProtocol";

		// Token: 0x04000023 RID: 35
		public const string ResponseHeaders = "owin.ResponseHeaders";

		// Token: 0x04000024 RID: 36
		public const string ResponseBody = "owin.ResponseBody";

		// Token: 0x04000025 RID: 37
		public const string CallCancelled = "owin.CallCancelled";

		// Token: 0x04000026 RID: 38
		public const string OwinVersion = "owin.Version";

		// Token: 0x0200004B RID: 75
		internal static class Builder
		{
			// Token: 0x04000083 RID: 131
			public const string AddSignatureConversion = "builder.AddSignatureConversion";

			// Token: 0x04000084 RID: 132
			public const string DefaultApp = "builder.DefaultApp";
		}

		// Token: 0x0200004C RID: 76
		internal static class CommonKeys
		{
			// Token: 0x04000085 RID: 133
			public const string ClientCertificate = "ssl.ClientCertificate";

			// Token: 0x04000086 RID: 134
			public const string RemoteIpAddress = "server.RemoteIpAddress";

			// Token: 0x04000087 RID: 135
			public const string RemotePort = "server.RemotePort";

			// Token: 0x04000088 RID: 136
			public const string LocalIpAddress = "server.LocalIpAddress";

			// Token: 0x04000089 RID: 137
			public const string LocalPort = "server.LocalPort";

			// Token: 0x0400008A RID: 138
			public const string IsLocal = "server.IsLocal";

			// Token: 0x0400008B RID: 139
			public const string TraceOutput = "host.TraceOutput";

			// Token: 0x0400008C RID: 140
			public const string Addresses = "host.Addresses";

			// Token: 0x0400008D RID: 141
			public const string AppName = "host.AppName";

			// Token: 0x0400008E RID: 142
			public const string Capabilities = "server.Capabilities";

			// Token: 0x0400008F RID: 143
			public const string OnSendingHeaders = "server.OnSendingHeaders";

			// Token: 0x04000090 RID: 144
			public const string OnAppDisposing = "host.OnAppDisposing";

			// Token: 0x04000091 RID: 145
			public const string Scheme = "scheme";

			// Token: 0x04000092 RID: 146
			public const string Host = "host";

			// Token: 0x04000093 RID: 147
			public const string Port = "port";

			// Token: 0x04000094 RID: 148
			public const string Path = "path";
		}

		// Token: 0x0200004D RID: 77
		internal static class SendFiles
		{
			// Token: 0x04000095 RID: 149
			public const string Version = "sendfile.Version";

			// Token: 0x04000096 RID: 150
			public const string Support = "sendfile.Support";

			// Token: 0x04000097 RID: 151
			public const string Concurrency = "sendfile.Concurrency";

			// Token: 0x04000098 RID: 152
			public const string SendAsync = "sendfile.SendAsync";
		}

		// Token: 0x0200004E RID: 78
		internal static class OpaqueConstants
		{
			// Token: 0x04000099 RID: 153
			public const string Version = "opaque.Version";

			// Token: 0x0400009A RID: 154
			public const string Upgrade = "opaque.Upgrade";

			// Token: 0x0400009B RID: 155
			public const string Stream = "opaque.Stream";

			// Token: 0x0400009C RID: 156
			public const string CallCancelled = "opaque.CallCancelled";
		}

		// Token: 0x0200004F RID: 79
		internal static class WebSocket
		{
			// Token: 0x0400009D RID: 157
			public const string Version = "websocket.Version";

			// Token: 0x0400009E RID: 158
			public const string Accept = "websocket.Accept";

			// Token: 0x0400009F RID: 159
			public const string SubProtocol = "websocket.SubProtocol";

			// Token: 0x040000A0 RID: 160
			public const string SendAsync = "websocket.SendAsync";

			// Token: 0x040000A1 RID: 161
			public const string ReceiveAsync = "websocket.ReceiveAsync";

			// Token: 0x040000A2 RID: 162
			public const string CloseAsync = "websocket.CloseAsync";

			// Token: 0x040000A3 RID: 163
			public const string CallCancelled = "websocket.CallCancelled";

			// Token: 0x040000A4 RID: 164
			public const string ClientCloseStatus = "websocket.ClientCloseStatus";

			// Token: 0x040000A5 RID: 165
			public const string ClientCloseDescription = "websocket.ClientCloseDescription";
		}

		// Token: 0x02000050 RID: 80
		internal static class Security
		{
			// Token: 0x040000A6 RID: 166
			public const string User = "server.User";

			// Token: 0x040000A7 RID: 167
			public const string Authenticate = "security.Authenticate";

			// Token: 0x040000A8 RID: 168
			public const string SignIn = "security.SignIn";

			// Token: 0x040000A9 RID: 169
			public const string SignOut = "security.SignOut";

			// Token: 0x040000AA RID: 170
			public const string SignOutProperties = "security.SignOutProperties";

			// Token: 0x040000AB RID: 171
			public const string Challenge = "security.Challenge";
		}
	}
}
