using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000703 RID: 1795
	public class Xid
	{
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000BE9AD File Offset: 0x000BCBAD
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000BE9B5 File Offset: 0x000BCBB5
		public int FormatId { get; private set; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000BE9BE File Offset: 0x000BCBBE
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000BE9C6 File Offset: 0x000BCBC6
		public int TransactionIdLength { get; private set; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000BE9CF File Offset: 0x000BCBCF
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000BE9D7 File Offset: 0x000BCBD7
		public int BranchQualifierLength { get; private set; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000BE9E0 File Offset: 0x000BCBE0
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000BE9E8 File Offset: 0x000BCBE8
		public byte[] Data { get; private set; }

		// Token: 0x060038E9 RID: 14569 RVA: 0x000BE9F1 File Offset: 0x000BCBF1
		public Xid(int formatId, int transactionIdLength, int branchQualifierLength, byte[] data)
		{
			this.FormatId = formatId;
			this.TransactionIdLength = transactionIdLength;
			this.BranchQualifierLength = branchQualifierLength;
			this.Data = new byte[data.Length];
			Buffer.BlockCopy(data, 0, this.Data, 0, data.Length);
		}
	}
}
