using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200005E RID: 94
	internal static class ArrayUtil
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001593C File Offset: 0x00013B3C
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

		// Token: 0x060003AD RID: 941 RVA: 0x0001596C File Offset: 0x00013B6C
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

		// Token: 0x060003AE RID: 942 RVA: 0x000159D8 File Offset: 0x00013BD8
		public static bool Contains<T>(T[] array, T item)
		{
			return Array.IndexOf<T>(array, item) != -1;
		}
	}
}
