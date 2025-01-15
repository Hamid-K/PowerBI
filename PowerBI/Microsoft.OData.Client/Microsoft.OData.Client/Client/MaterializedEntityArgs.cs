using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000031 RID: 49
	public sealed class MaterializedEntityArgs
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000080F3 File Offset: 0x000062F3
		public MaterializedEntityArgs(ODataResource entry, object entity)
		{
			Util.CheckArgumentNull<ODataResource>(entry, "entry");
			Util.CheckArgumentNull<object>(entity, "entity");
			this.Entry = entry;
			this.Entity = entity;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008121 File Offset: 0x00006321
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00008129 File Offset: 0x00006329
		public ODataResource Entry { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008132 File Offset: 0x00006332
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000813A File Offset: 0x0000633A
		public object Entity { get; private set; }
	}
}
