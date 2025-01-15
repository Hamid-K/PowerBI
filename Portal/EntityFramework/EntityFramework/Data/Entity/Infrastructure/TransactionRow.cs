using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000267 RID: 615
	public class TransactionRow
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00056C1C File Offset: 0x00054E1C
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00056C24 File Offset: 0x00054E24
		public Guid Id { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x00056C2D File Offset: 0x00054E2D
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x00056C35 File Offset: 0x00054E35
		public DateTime CreationTime { get; set; }

		// Token: 0x06001F50 RID: 8016 RVA: 0x00056C40 File Offset: 0x00054E40
		public override bool Equals(object obj)
		{
			TransactionRow transactionRow = obj as TransactionRow;
			return transactionRow != null && this.Id == transactionRow.Id;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00056C6C File Offset: 0x00054E6C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
