using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000007 RID: 7
	public class MultipartRemoteFileData
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002668 File Offset: 0x00000868
		public MultipartRemoteFileData(HttpContentHeaders headers, string location, string fileName)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (location == null)
			{
				throw Error.ArgumentNull("location");
			}
			if (fileName == null)
			{
				throw Error.ArgumentNull("fileName");
			}
			this.FileName = fileName;
			this.Headers = headers;
			this.Location = location;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026BA File Offset: 0x000008BA
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000026C2 File Offset: 0x000008C2
		public string FileName { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000026CB File Offset: 0x000008CB
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000026D3 File Offset: 0x000008D3
		public HttpContentHeaders Headers { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026DC File Offset: 0x000008DC
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000026E4 File Offset: 0x000008E4
		public string Location { get; private set; }
	}
}
