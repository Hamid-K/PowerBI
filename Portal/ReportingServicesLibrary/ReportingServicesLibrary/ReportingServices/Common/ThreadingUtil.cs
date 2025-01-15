using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000363 RID: 867
	internal static class ThreadingUtil
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x00073500 File Offset: 0x00071700
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
