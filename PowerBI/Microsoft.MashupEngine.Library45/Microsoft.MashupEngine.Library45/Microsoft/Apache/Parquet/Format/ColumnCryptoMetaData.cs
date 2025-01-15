using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002000 RID: 8192
	internal class ColumnCryptoMetaData
	{
		// Token: 0x17002D36 RID: 11574
		// (get) Token: 0x0601117D RID: 70013 RVA: 0x003AEAF4 File Offset: 0x003ACCF4
		// (set) Token: 0x0601117E RID: 70014 RVA: 0x003AEAFC File Offset: 0x003ACCFC
		public EncryptionWithFooterKey ENCRYPTION_WITH_FOOTER_KEY { get; set; }

		// Token: 0x17002D37 RID: 11575
		// (get) Token: 0x0601117F RID: 70015 RVA: 0x003AEB05 File Offset: 0x003ACD05
		// (set) Token: 0x06011180 RID: 70016 RVA: 0x003AEB0D File Offset: 0x003ACD0D
		public EncryptionWithColumnKey ENCRYPTION_WITH_COLUMN_KEY { get; set; }

		// Token: 0x06011181 RID: 70017 RVA: 0x003AEB18 File Offset: 0x003ACD18
		public void Read(FastProtocol prot)
		{
			this.ENCRYPTION_WITH_FOOTER_KEY = null;
			this.ENCRYPTION_WITH_COLUMN_KEY = null;
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
								EncryptionWithColumnKey encryptionWithColumnKey = new EncryptionWithColumnKey();
								encryptionWithColumnKey.Read(prot);
								this.ENCRYPTION_WITH_COLUMN_KEY = encryptionWithColumnKey;
								flag = false;
							}
						}
					}
					else if (tfield.Type == TType.Struct)
					{
						EncryptionWithFooterKey encryptionWithFooterKey = new EncryptionWithFooterKey();
						encryptionWithFooterKey.Read(prot);
						this.ENCRYPTION_WITH_FOOTER_KEY = encryptionWithFooterKey;
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
