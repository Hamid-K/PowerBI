using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002008 RID: 8200
	internal class DateType
	{
		// Token: 0x060111D1 RID: 70097 RVA: 0x003AFAA8 File Offset: 0x003ADCA8
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
