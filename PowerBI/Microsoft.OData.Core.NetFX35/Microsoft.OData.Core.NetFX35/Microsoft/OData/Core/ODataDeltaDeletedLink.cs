using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000162 RID: 354
	public sealed class ODataDeltaDeletedLink : ODataDeltaLinkBase
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00030FE3 File Offset: 0x0002F1E3
		public ODataDeltaDeletedLink(Uri source, Uri target, string relationship)
			: base(source, target, relationship)
		{
		}
	}
}
