using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200000D RID: 13
	public class MultipartFileData
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002964 File Offset: 0x00000B64
		public MultipartFileData(HttpContentHeaders headers, string localFileName)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (localFileName == null)
			{
				throw Error.ArgumentNull("localFileName");
			}
			this.Headers = headers;
			this.LocalFileName = localFileName;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002996 File Offset: 0x00000B96
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000299E File Offset: 0x00000B9E
		public HttpContentHeaders Headers { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000029A7 File Offset: 0x00000BA7
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000029AF File Offset: 0x00000BAF
		public string LocalFileName { get; private set; }
	}
}
