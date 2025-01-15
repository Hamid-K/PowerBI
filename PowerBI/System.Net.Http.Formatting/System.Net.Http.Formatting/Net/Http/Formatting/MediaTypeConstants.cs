using System;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000043 RID: 67
	internal static class MediaTypeConstants
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000906E File Offset: 0x0000726E
		public static MediaTypeHeaderValue ApplicationOctetStreamMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationOctetStreamMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000907A File Offset: 0x0000727A
		public static MediaTypeHeaderValue ApplicationXmlMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationXmlMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009086 File Offset: 0x00007286
		public static MediaTypeHeaderValue ApplicationJsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationJsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009092 File Offset: 0x00007292
		public static MediaTypeHeaderValue TextXmlMediaType
		{
			get
			{
				return MediaTypeConstants._defaultTextXmlMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000909E File Offset: 0x0000729E
		public static MediaTypeHeaderValue TextJsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultTextJsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000090AA File Offset: 0x000072AA
		public static MediaTypeHeaderValue ApplicationFormUrlEncodedMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationFormUrlEncodedMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000090B6 File Offset: 0x000072B6
		public static MediaTypeHeaderValue ApplicationBsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationBsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x040000B6 RID: 182
		private static readonly MediaTypeHeaderValue _defaultApplicationXmlMediaType = new MediaTypeHeaderValue("application/xml");

		// Token: 0x040000B7 RID: 183
		private static readonly MediaTypeHeaderValue _defaultTextXmlMediaType = new MediaTypeHeaderValue("text/xml");

		// Token: 0x040000B8 RID: 184
		private static readonly MediaTypeHeaderValue _defaultApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

		// Token: 0x040000B9 RID: 185
		private static readonly MediaTypeHeaderValue _defaultTextJsonMediaType = new MediaTypeHeaderValue("text/json");

		// Token: 0x040000BA RID: 186
		private static readonly MediaTypeHeaderValue _defaultApplicationOctetStreamMediaType = new MediaTypeHeaderValue("application/octet-stream");

		// Token: 0x040000BB RID: 187
		private static readonly MediaTypeHeaderValue _defaultApplicationFormUrlEncodedMediaType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

		// Token: 0x040000BC RID: 188
		private static readonly MediaTypeHeaderValue _defaultApplicationBsonMediaType = new MediaTypeHeaderValue("application/bson");
	}
}
