using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000018 RID: 24
	public sealed class OAuthBrowserNavigation
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004772 File Offset: 0x00002972
		public OAuthBrowserNavigation(Uri loginUri, Uri callbackUri, int windowHeight, int windowWidth)
			: this(loginUri, callbackUri, windowHeight, windowWidth, null)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004780 File Offset: 0x00002980
		public OAuthBrowserNavigation(Uri loginUri, Uri callbackUri, int windowHeight, int windowWidth, byte[] serializedContext)
		{
			if (loginUri == null)
			{
				throw new ArgumentNullException("loginUri");
			}
			if (callbackUri == null)
			{
				throw new ArgumentNullException("callbackUri");
			}
			this.loginUri = loginUri;
			this.callbackUri = callbackUri;
			this.windowHeight = windowHeight;
			this.windowWidth = windowWidth;
			this.serializedContext = serializedContext;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000047E0 File Offset: 0x000029E0
		public Uri LoginUri
		{
			get
			{
				return this.loginUri;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000047E8 File Offset: 0x000029E8
		public Uri CallbackUri
		{
			get
			{
				return this.callbackUri;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000047F0 File Offset: 0x000029F0
		public int WindowHeight
		{
			get
			{
				return this.windowHeight;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000047F8 File Offset: 0x000029F8
		public int WindowWidth
		{
			get
			{
				return this.windowWidth;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004800 File Offset: 0x00002A00
		public byte[] SerializedContext
		{
			get
			{
				return this.serializedContext;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004808 File Offset: 0x00002A08
		public static bool IsCallbackUri(Uri callbackUri, Uri uri)
		{
			return uri.AbsoluteUri.StartsWith(callbackUri.AbsoluteUri, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040000A4 RID: 164
		private readonly Uri loginUri;

		// Token: 0x040000A5 RID: 165
		private readonly Uri callbackUri;

		// Token: 0x040000A6 RID: 166
		private readonly int windowHeight;

		// Token: 0x040000A7 RID: 167
		private readonly int windowWidth;

		// Token: 0x040000A8 RID: 168
		private readonly byte[] serializedContext;
	}
}
