using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FFA RID: 8186
	internal class BloomFilterCompression
	{
		// Token: 0x17002D27 RID: 11559
		// (get) Token: 0x06011155 RID: 69973 RVA: 0x003AE498 File Offset: 0x003AC698
		// (set) Token: 0x06011156 RID: 69974 RVA: 0x003AE4A0 File Offset: 0x003AC6A0
		public Uncompressed UNCOMPRESSED { get; set; }

		// Token: 0x06011157 RID: 69975 RVA: 0x003AE4AC File Offset: 0x003AC6AC
		public void Read(FastProtocol prot)
		{
			this.UNCOMPRESSED = null;
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
					if (tfield.ID == 1 && tfield.Type == TType.Struct)
					{
						Uncompressed uncompressed = new Uncompressed();
						uncompressed.Read(prot);
						this.UNCOMPRESSED = uncompressed;
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
