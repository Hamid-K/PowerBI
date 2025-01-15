using System;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000022 RID: 34
	public class MultipartMemoryStreamProvider : MultipartStreamProvider
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000048E1 File Offset: 0x00002AE1
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			return new MemoryStream();
		}
	}
}
