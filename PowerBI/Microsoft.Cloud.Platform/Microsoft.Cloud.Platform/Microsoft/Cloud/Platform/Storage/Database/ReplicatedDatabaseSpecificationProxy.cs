using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200004D RID: 77
	public sealed class ReplicatedDatabaseSpecificationProxy
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x000064B2 File Offset: 0x000046B2
		public ReplicatedDatabaseSpecificationProxy(IDatabaseSpecificationProxy primary, IDatabaseSpecificationProxy secondary, IDatabaseSpecificationProxy active)
		{
			this.Primary = primary;
			this.Secondary = secondary;
			this.Active = active;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000064CF File Offset: 0x000046CF
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000064D7 File Offset: 0x000046D7
		public IDatabaseSpecificationProxy Primary { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000064E0 File Offset: 0x000046E0
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000064E8 File Offset: 0x000046E8
		public IDatabaseSpecificationProxy Secondary { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000064F1 File Offset: 0x000046F1
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000064F9 File Offset: 0x000046F9
		public IDatabaseSpecificationProxy Active { get; private set; }
	}
}
