using System;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000154 RID: 340
	public sealed class Label
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x0006BA3B File Offset: 0x00069C3B
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x0006BA43 File Offset: 0x00069C43
		public string Name { get; private set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0006BA4C File Offset: 0x00069C4C
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x0006BA54 File Offset: 0x00069C54
		public string Id { get; private set; }

		// Token: 0x06001A39 RID: 6713 RVA: 0x0006BA5D File Offset: 0x00069C5D
		public Label(string name, string id)
		{
			this.Name = name;
			this.Id = id;
		}
	}
}
