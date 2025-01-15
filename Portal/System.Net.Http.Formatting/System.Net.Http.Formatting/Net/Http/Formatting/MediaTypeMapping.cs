using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000048 RID: 72
	public abstract class MediaTypeMapping
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00009C5A File Offset: 0x00007E5A
		protected MediaTypeMapping(MediaTypeHeaderValue mediaType)
		{
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = mediaType;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009C77 File Offset: 0x00007E77
		protected MediaTypeMapping(string mediaType)
		{
			if (string.IsNullOrWhiteSpace(mediaType))
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = new MediaTypeHeaderValue(mediaType);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00009C9E File Offset: 0x00007E9E
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00009CA6 File Offset: 0x00007EA6
		public MediaTypeHeaderValue MediaType { get; private set; }

		// Token: 0x060002D5 RID: 725
		public abstract double TryMatchMediaType(HttpRequestMessage request);
	}
}
