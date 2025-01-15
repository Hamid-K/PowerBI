using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E3 RID: 1251
	public class RouterIdentifier
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x00086CC2 File Offset: 0x00084EC2
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x00086CCA File Offset: 0x00084ECA
		public Type RouterType { get; private set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x00086CD3 File Offset: 0x00084ED3
		// (set) Token: 0x060025ED RID: 9709 RVA: 0x00086CDB File Offset: 0x00084EDB
		public string Id { get; private set; }

		// Token: 0x060025EE RID: 9710 RVA: 0x00086CE4 File Offset: 0x00084EE4
		public RouterIdentifier(Type type, string id)
		{
			this.RouterType = type;
			this.Id = id;
		}
	}
}
