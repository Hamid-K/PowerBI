using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000045 RID: 69
	public sealed class ReadingEntryArgs
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00008C90 File Offset: 0x00006E90
		public ReadingEntryArgs(ODataResource entry)
		{
			this.Entry = entry;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008C9F File Offset: 0x00006E9F
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00008CA7 File Offset: 0x00006EA7
		public ODataResource Entry { get; private set; }
	}
}
