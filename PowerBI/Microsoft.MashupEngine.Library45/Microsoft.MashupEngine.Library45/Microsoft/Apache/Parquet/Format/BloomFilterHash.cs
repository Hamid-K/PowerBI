using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FFB RID: 8187
	internal class BloomFilterHash
	{
		// Token: 0x17002D28 RID: 11560
		// (get) Token: 0x06011159 RID: 69977 RVA: 0x003AE538 File Offset: 0x003AC738
		// (set) Token: 0x0601115A RID: 69978 RVA: 0x003AE540 File Offset: 0x003AC740
		public XxHash XXHASH { get; set; }

		// Token: 0x0601115B RID: 69979 RVA: 0x003AE54C File Offset: 0x003AC74C
		public void Read(FastProtocol prot)
		{
			this.XXHASH = null;
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
						XxHash xxHash = new XxHash();
						xxHash.Read(prot);
						this.XXHASH = xxHash;
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
