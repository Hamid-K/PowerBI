using System;
using System.Threading;

namespace NLog.Internal
{
	// Token: 0x02000149 RID: 329
	internal static class ThreadLocalStorageHelper
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00028BD6 File Offset: 0x00026DD6
		public static object AllocateDataSlot()
		{
			return Thread.AllocateDataSlot();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00028BE0 File Offset: 0x00026DE0
		public static T GetDataForSlot<T>(object slot, bool create = true) where T : class, new()
		{
			LocalDataStoreSlot localDataStoreSlot = (LocalDataStoreSlot)slot;
			object obj = Thread.GetData(localDataStoreSlot);
			if (obj == null)
			{
				if (!create)
				{
					return default(T);
				}
				obj = new T();
				Thread.SetData(localDataStoreSlot, obj);
			}
			return (T)((object)obj);
		}
	}
}
