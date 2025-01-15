using System;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A5 RID: 165
	internal sealed class MemoryRequest
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00010048 File Offset: 0x0000E248
		public void DoWork()
		{
			Random random = new NormalRandom();
			int num = random.Next(10000000);
			Console.WriteLine("Beginning MemoryRequest() --> {0} Allocations", num);
			long num2 = 0L;
			using (TestMemoryAuditProxy testMemoryAuditProxy = new TestMemoryAuditProxy())
			{
				int num3 = 0;
				while (num-- >= 0)
				{
					int num4 = random.Next(128);
					num2 += (long)num4;
					testMemoryAuditProxy.Allocate(num4);
					if ((num3 = (num3 + 1) % 1000) == 0)
					{
						Thread.Sleep(0);
					}
				}
			}
		}

		// Token: 0x040002F1 RID: 753
		public const int MaxAllocationCout = 10000000;

		// Token: 0x040002F2 RID: 754
		public const int MaxMemoryPerRequest = 128;
	}
}
