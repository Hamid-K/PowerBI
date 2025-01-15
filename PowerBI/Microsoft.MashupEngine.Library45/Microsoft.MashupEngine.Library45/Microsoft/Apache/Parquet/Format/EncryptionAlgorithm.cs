using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200200C RID: 8204
	internal class EncryptionAlgorithm
	{
		// Token: 0x17002D5F RID: 11615
		// (get) Token: 0x060111E1 RID: 70113 RVA: 0x003AFD88 File Offset: 0x003ADF88
		// (set) Token: 0x060111E2 RID: 70114 RVA: 0x003AFD90 File Offset: 0x003ADF90
		public AesGcmV1 AES_GCM_V1 { get; set; }

		// Token: 0x17002D60 RID: 11616
		// (get) Token: 0x060111E3 RID: 70115 RVA: 0x003AFD99 File Offset: 0x003ADF99
		// (set) Token: 0x060111E4 RID: 70116 RVA: 0x003AFDA1 File Offset: 0x003ADFA1
		public AesGcmCtrV1 AES_GCM_CTR_V1 { get; set; }

		// Token: 0x060111E5 RID: 70117 RVA: 0x003AFDAC File Offset: 0x003ADFAC
		public void Read(FastProtocol prot)
		{
			this.AES_GCM_V1 = null;
			this.AES_GCM_CTR_V1 = null;
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
							if (tfield.Type == TType.Struct)
							{
								AesGcmCtrV1 aesGcmCtrV = new AesGcmCtrV1();
								aesGcmCtrV.Read(prot);
								this.AES_GCM_CTR_V1 = aesGcmCtrV;
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.Struct)
					{
						AesGcmV1 aesGcmV = new AesGcmV1();
						aesGcmV.Read(prot);
						this.AES_GCM_V1 = aesGcmV;
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
