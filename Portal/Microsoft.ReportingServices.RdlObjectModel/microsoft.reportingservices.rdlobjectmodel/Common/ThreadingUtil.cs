using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200005D RID: 93
	internal static class ThreadingUtil
	{
		// Token: 0x060003AB RID: 939 RVA: 0x000158D0 File Offset: 0x00013AD0
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
