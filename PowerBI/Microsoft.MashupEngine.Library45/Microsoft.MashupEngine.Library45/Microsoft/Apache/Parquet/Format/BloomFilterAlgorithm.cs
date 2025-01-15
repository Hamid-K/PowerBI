using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FF9 RID: 8185
	internal class BloomFilterAlgorithm
	{
		// Token: 0x17002D26 RID: 11558
		// (get) Token: 0x06011151 RID: 69969 RVA: 0x003AE3F8 File Offset: 0x003AC5F8
		// (set) Token: 0x06011152 RID: 69970 RVA: 0x003AE400 File Offset: 0x003AC600
		public SplitBlockAlgorithm BLOCK { get; set; }

		// Token: 0x06011153 RID: 69971 RVA: 0x003AE40C File Offset: 0x003AC60C
		public void Read(FastProtocol prot)
		{
			this.BLOCK = null;
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
						SplitBlockAlgorithm splitBlockAlgorithm = new SplitBlockAlgorithm();
						splitBlockAlgorithm.Read(prot);
						this.BLOCK = splitBlockAlgorithm;
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
