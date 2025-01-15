using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200200D RID: 8205
	internal class EncryptionWithColumnKey
	{
		// Token: 0x17002D61 RID: 11617
		// (get) Token: 0x060111E7 RID: 70119 RVA: 0x003AFE70 File Offset: 0x003AE070
		// (set) Token: 0x060111E8 RID: 70120 RVA: 0x003AFE78 File Offset: 0x003AE078
		public string[] PathInSchema { get; set; }

		// Token: 0x17002D62 RID: 11618
		// (get) Token: 0x060111E9 RID: 70121 RVA: 0x003AFE81 File Offset: 0x003AE081
		// (set) Token: 0x060111EA RID: 70122 RVA: 0x003AFE89 File Offset: 0x003AE089
		public byte[] KeyMetadata { get; set; }

		// Token: 0x060111EB RID: 70123 RVA: 0x003AFE94 File Offset: 0x003AE094
		public void Read(FastProtocol prot)
		{
			this.PathInSchema = null;
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
					else if (tfield.Type == TType.List)
					{
						TList tlist = prot.ReadListBegin();
						this.PathInSchema = new string[tlist.Count];
						for (int i = 0; i < tlist.Count; i++)
						{
							this.PathInSchema[i] = prot.ReadString();
						}
						prot.ReadListEnd();
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.PathInSchema == null)
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
