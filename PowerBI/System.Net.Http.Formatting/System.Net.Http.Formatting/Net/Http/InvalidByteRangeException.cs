using System;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200000B RID: 11
	public class InvalidByteRangeException : Exception
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000027F8 File Offset: 0x000009F8
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002807 File Offset: 0x00000A07
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, string message)
			: base(message)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002817 File Offset: 0x00000A17
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, string message, Exception innerException)
			: base(message, innerException)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002828 File Offset: 0x00000A28
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002839 File Offset: 0x00000A39
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002841 File Offset: 0x00000A41
		public ContentRangeHeaderValue ContentRange { get; private set; }

		// Token: 0x06000033 RID: 51 RVA: 0x0000284A File Offset: 0x00000A4A
		private void Initialize(ContentRangeHeaderValue contentRange)
		{
			if (contentRange == null)
			{
				throw Error.ArgumentNull("contentRange");
			}
			this.ContentRange = contentRange;
		}
	}
}
