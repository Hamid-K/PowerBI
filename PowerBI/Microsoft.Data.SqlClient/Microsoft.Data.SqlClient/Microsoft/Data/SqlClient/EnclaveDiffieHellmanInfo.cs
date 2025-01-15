using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000023 RID: 35
	internal class EnclaveDiffieHellmanInfo
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000CC5B File Offset: 0x0000AE5B
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000CC63 File Offset: 0x0000AE63
		public int Size { get; private set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public byte[] PublicKey { get; private set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000CC7D File Offset: 0x0000AE7D
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0000CC85 File Offset: 0x0000AE85
		public byte[] PublicKeySignature { get; private set; }

		// Token: 0x0600069B RID: 1691 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public EnclaveDiffieHellmanInfo(byte[] payload)
		{
			this.Size = payload.Length;
			int num = BitConverter.ToInt32(payload, 0);
			int num2 = BitConverter.ToInt32(payload, 4);
			this.PublicKey = new byte[num];
			this.PublicKeySignature = new byte[num2];
			Buffer.BlockCopy(payload, 8, this.PublicKey, 0, num);
			Buffer.BlockCopy(payload, 8 + num, this.PublicKeySignature, 0, num2);
		}
	}
}
