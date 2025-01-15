using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002026 RID: 8230
	internal class SplitBlockAlgorithm
	{
		// Token: 0x06011297 RID: 70295 RVA: 0x003B1CF4 File Offset: 0x003AFEF4
		public void Read(FastProtocol prot)
		{
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
