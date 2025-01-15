using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A3 RID: 675
	public static class ExtendedInterlocked
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x00040154 File Offset: 0x0003E354
		public static bool ReadModifyWrite(ref long valueLocation, Func<long, bool> bailOnValue, Func<long, long> determineNewValue)
		{
			long num = Interlocked.Read(ref valueLocation);
			while (!bailOnValue(num))
			{
				long num2 = determineNewValue(num);
				long num3 = Interlocked.CompareExchange(ref valueLocation, num2, num);
				if (num3 == num)
				{
					return true;
				}
				num = num3;
			}
			return false;
		}
	}
}
