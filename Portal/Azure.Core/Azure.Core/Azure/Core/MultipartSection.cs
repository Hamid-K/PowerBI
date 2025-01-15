using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	internal class MultipartSection
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00007DEB File Offset: 0x00005FEB
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00007DF3 File Offset: 0x00005FF3
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		public Dictionary<string, string[]> Headers
		{
			[return: Nullable(new byte[] { 2, 1, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1, 1 })]
			set;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00007DFC File Offset: 0x00005FFC
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00007E04 File Offset: 0x00006004
		public Stream Body { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00007E0D File Offset: 0x0000600D
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00007E15 File Offset: 0x00006015
		public long? BaseStreamOffset { get; set; }
	}
}
