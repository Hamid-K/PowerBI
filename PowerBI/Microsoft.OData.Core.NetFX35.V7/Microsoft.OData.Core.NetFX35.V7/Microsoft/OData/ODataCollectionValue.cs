using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000044 RID: 68
	public sealed class ODataCollectionValue : ODataValue
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000089BF File Offset: 0x00006BBF
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000089C7 File Offset: 0x00006BC7
		public string TypeName { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000089D0 File Offset: 0x00006BD0
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000089D8 File Offset: 0x00006BD8
		public IEnumerable<object> Items { get; set; }
	}
}
