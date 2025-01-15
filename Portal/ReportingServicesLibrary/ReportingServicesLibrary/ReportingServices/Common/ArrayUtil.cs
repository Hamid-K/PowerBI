using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000364 RID: 868
	internal static class ArrayUtil
	{
		// Token: 0x06001C98 RID: 7320 RVA: 0x0007356C File Offset: 0x0007176C
		public static T[] ToArray<T>(ICollection<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			T[] array = new T[items.Count];
			items.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0007359C File Offset: 0x0007179C
		public static object[] ToObjectArray<T>(ICollection<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			object[] array = new object[items.Count];
			int num = 0;
			foreach (T t in items)
			{
				object obj = t;
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00073608 File Offset: 0x00071808
		public static bool Contains<T>(T[] array, T item)
		{
			return Array.IndexOf<T>(array, item) != -1;
		}
	}
}
