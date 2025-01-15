using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000038 RID: 56
	public class ContentNegotiationResult
	{
		// Token: 0x06000226 RID: 550 RVA: 0x000074ED File Offset: 0x000056ED
		public ContentNegotiationResult(MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._formatter = formatter;
			this.MediaType = mediaType;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007511 File Offset: 0x00005711
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00007519 File Offset: 0x00005719
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._formatter = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007530 File Offset: 0x00005730
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00007538 File Offset: 0x00005738
		public MediaTypeHeaderValue MediaType { get; set; }

		// Token: 0x040000A1 RID: 161
		private MediaTypeFormatter _formatter;
	}
}
