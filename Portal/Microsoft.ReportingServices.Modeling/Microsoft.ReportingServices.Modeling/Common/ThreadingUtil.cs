using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000006 RID: 6
	internal static class ThreadingUtil
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000022A8 File Offset: 0x000004A8
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
