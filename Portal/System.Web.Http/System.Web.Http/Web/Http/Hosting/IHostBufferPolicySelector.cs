using System;
using System.Net.Http;

namespace System.Web.Http.Hosting
{
	// Token: 0x020000A8 RID: 168
	public interface IHostBufferPolicySelector
	{
		// Token: 0x06000406 RID: 1030
		bool UseBufferedInputStream(object hostContext);

		// Token: 0x06000407 RID: 1031
		bool UseBufferedOutputStream(HttpResponseMessage response);
	}
}
