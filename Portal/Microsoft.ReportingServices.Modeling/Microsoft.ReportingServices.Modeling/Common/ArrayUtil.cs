using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000007 RID: 7
	internal static class ArrayUtil
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002314 File Offset: 0x00000514
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002344 File Offset: 0x00000544
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

		// Token: 0x0600002A RID: 42 RVA: 0x000023B0 File Offset: 0x000005B0
		public static bool Contains<T>(T[] array, T item)
		{
			return Array.IndexOf<T>(array, item) != -1;
		}
	}
}
