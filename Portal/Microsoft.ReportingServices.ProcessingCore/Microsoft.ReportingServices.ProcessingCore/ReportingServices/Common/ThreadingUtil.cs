using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D2 RID: 1490
	internal static class ThreadingUtil
	{
		// Token: 0x060053BC RID: 21436 RVA: 0x00160CA0 File Offset: 0x0015EEA0
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
