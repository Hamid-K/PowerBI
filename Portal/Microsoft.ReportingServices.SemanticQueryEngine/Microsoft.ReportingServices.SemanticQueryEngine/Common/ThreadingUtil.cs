using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200006E RID: 110
	internal static class ThreadingUtil
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x000151EC File Offset: 0x000133EC
		internal static T ReturnOnDemandValue<T>(ref T valueStorage, object valueLock, CreatorGetter<T> getValue)
		{
			if (valueStorage != null)
			{
				return valueStorage;
			}
			T t;
			lock (valueLock)
			{
				if (valueStorage == null)
				{
					valueStorage = getValue();
				}
				t = valueStorage;
			}
			return t;
		}
	}
}
