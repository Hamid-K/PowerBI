using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200003F RID: 63
	public sealed class WritingEntryArgs
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000087AB File Offset: 0x000069AB
		public WritingEntryArgs(ODataResource entry, object entity)
		{
			Util.CheckArgumentNull<ODataResource>(entry, "entry");
			Util.CheckArgumentNull<object>(entity, "entity");
			this.Entry = entry;
			this.Entity = entity;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000087D9 File Offset: 0x000069D9
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000087E1 File Offset: 0x000069E1
		public ODataResource Entry { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000087EA File Offset: 0x000069EA
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000087F2 File Offset: 0x000069F2
		public object Entity { get; private set; }
	}
}
