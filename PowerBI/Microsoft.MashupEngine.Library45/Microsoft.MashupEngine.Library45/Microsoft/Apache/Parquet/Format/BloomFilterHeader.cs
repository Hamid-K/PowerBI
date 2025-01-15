using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FFC RID: 8188
	internal class BloomFilterHeader
	{
		// Token: 0x17002D29 RID: 11561
		// (get) Token: 0x0601115D RID: 69981 RVA: 0x003AE5D8 File Offset: 0x003AC7D8
		// (set) Token: 0x0601115E RID: 69982 RVA: 0x003AE5E0 File Offset: 0x003AC7E0
		public int? NumBytes { get; set; }

		// Token: 0x17002D2A RID: 11562
		// (get) Token: 0x0601115F RID: 69983 RVA: 0x003AE5E9 File Offset: 0x003AC7E9
		// (set) Token: 0x06011160 RID: 69984 RVA: 0x003AE5F1 File Offset: 0x003AC7F1
		public BloomFilterAlgorithm Algorithm { get; set; }

		// Token: 0x17002D2B RID: 11563
		// (get) Token: 0x06011161 RID: 69985 RVA: 0x003AE5FA File Offset: 0x003AC7FA
		// (set) Token: 0x06011162 RID: 69986 RVA: 0x003AE602 File Offset: 0x003AC802
		public BloomFilterHash Hash { get; set; }

		// Token: 0x17002D2C RID: 11564
		// (get) Token: 0x06011163 RID: 69987 RVA: 0x003AE60B File Offset: 0x003AC80B
		// (set) Token: 0x06011164 RID: 69988 RVA: 0x003AE613 File Offset: 0x003AC813
		public BloomFilterCompression Compression { get; set; }

		// Token: 0x06011165 RID: 69989 RVA: 0x003AE61C File Offset: 0x003AC81C
		public void Read(FastProtocol prot)
		{
			this.NumBytes = null;
			this.Algorithm = null;
			this.Hash = null;
			this.Compression = null;
			prot.IncrementRecursionDepth();
			try
			{
				prot.ReadStructBegin();
				for (;;)
				{
					TField tfield = prot.ReadFieldBegin();
					if (tfield.Type == TType.Stop)
					{
						break;
					}
					bool flag = true;
					switch (tfield.ID)
					{
					case 1:
						if (tfield.Type == TType.I32)
						{
							this.NumBytes = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.Struct)
						{
							BloomFilterAlgorithm bloomFilterAlgorithm = new BloomFilterAlgorithm();
							bloomFilterAlgorithm.Read(prot);
							this.Algorithm = bloomFilterAlgorithm;
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Struct)
						{
							BloomFilterHash bloomFilterHash = new BloomFilterHash();
							bloomFilterHash.Read(prot);
							this.Hash = bloomFilterHash;
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.Struct)
						{
							BloomFilterCompression bloomFilterCompression = new BloomFilterCompression();
							bloomFilterCompression.Read(prot);
							this.Compression = bloomFilterCompression;
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.NumBytes == null || this.Algorithm == null || this.Hash == null || this.Compression == null)
				{
					throw new InvalidOperationException("Required field not found");
				}
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
