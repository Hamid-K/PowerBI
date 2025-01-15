using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200201A RID: 8218
	internal class MicroSeconds
	{
		// Token: 0x0601123B RID: 70203 RVA: 0x003B0CD0 File Offset: 0x003AEED0
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
