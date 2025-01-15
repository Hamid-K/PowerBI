using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000073 RID: 115
	public interface IHttpUriRewritingService
	{
		// Token: 0x060001BA RID: 442
		bool TryRewriteRequestUri(Uri requestUri, out Uri rewrittenUri);

		// Token: 0x060001BB RID: 443
		bool TryRewriteResponseUri(Uri responseUri, out Uri rewrittenUri);
	}
}
