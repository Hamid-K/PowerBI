using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002011 RID: 8209
	internal class FileCryptoMetaData
	{
		// Token: 0x17002D63 RID: 11619
		// (get) Token: 0x060111F1 RID: 70129 RVA: 0x003B004C File Offset: 0x003AE24C
		// (set) Token: 0x060111F2 RID: 70130 RVA: 0x003B0054 File Offset: 0x003AE254
		public EncryptionAlgorithm EncryptionAlgorithm { get; set; }

		// Token: 0x17002D64 RID: 11620
		// (get) Token: 0x060111F3 RID: 70131 RVA: 0x003B005D File Offset: 0x003AE25D
		// (set) Token: 0x060111F4 RID: 70132 RVA: 0x003B0065 File Offset: 0x003AE265
		public byte[] KeyMetadata { get; set; }

		// Token: 0x060111F5 RID: 70133 RVA: 0x003B0070 File Offset: 0x003AE270
		public void Read(FastProtocol prot)
		{
			this.EncryptionAlgorithm = null;
			this.KeyMetadata = null;
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
					short id = tfield.ID;
					if (id != 1)
					{
						if (id == 2)
						{
							if (tfield.Type == TType.String)
							{
								this.KeyMetadata = prot.ReadBytes();
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.Struct)
					{
						EncryptionAlgorithm encryptionAlgorithm = new EncryptionAlgorithm();
						encryptionAlgorithm.Read(prot);
						this.EncryptionAlgorithm = encryptionAlgorithm;
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.EncryptionAlgorithm == null)
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
