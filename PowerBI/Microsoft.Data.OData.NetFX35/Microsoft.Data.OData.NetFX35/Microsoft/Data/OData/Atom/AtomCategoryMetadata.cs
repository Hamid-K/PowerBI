using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027C RID: 636
	public sealed class AtomCategoryMetadata : ODataAnnotatable
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x00049F35 File Offset: 0x00048135
		public AtomCategoryMetadata()
		{
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00049F3D File Offset: 0x0004813D
		internal AtomCategoryMetadata(AtomCategoryMetadata other)
		{
			if (other == null)
			{
				return;
			}
			this.Term = other.Term;
			this.Scheme = other.Scheme;
			this.Label = other.Label;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00049F6D File Offset: 0x0004816D
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00049F75 File Offset: 0x00048175
		public string Term { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00049F7E File Offset: 0x0004817E
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x00049F86 File Offset: 0x00048186
		public string Scheme { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00049F8F File Offset: 0x0004818F
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x00049F97 File Offset: 0x00048197
		public string Label { get; set; }
	}
}
