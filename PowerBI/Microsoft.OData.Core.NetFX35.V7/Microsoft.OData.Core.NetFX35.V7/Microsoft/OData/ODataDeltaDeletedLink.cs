using System;

namespace Microsoft.OData
{
	// Token: 0x0200004E RID: 78
	public sealed class ODataDeltaDeletedLink : ODataDeltaLinkBase
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00009AFB File Offset: 0x00007CFB
		public ODataDeltaDeletedLink(Uri source, Uri target, string relationship)
			: base(source, target, relationship)
		{
		}
	}
}
