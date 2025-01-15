using System;

namespace Microsoft.OData
{
	// Token: 0x02000071 RID: 113
	public sealed class ODataDeltaDeletedLink : ODataDeltaLinkBase
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public ODataDeltaDeletedLink(Uri source, Uri target, string relationship)
			: base(source, target, relationship)
		{
		}
	}
}
