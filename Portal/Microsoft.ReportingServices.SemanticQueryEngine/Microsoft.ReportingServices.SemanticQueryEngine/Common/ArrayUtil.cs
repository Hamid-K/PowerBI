using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200006F RID: 111
	internal static class ArrayUtil
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00015258 File Offset: 0x00013458
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

		// Token: 0x060004ED RID: 1261 RVA: 0x00015288 File Offset: 0x00013488
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

		// Token: 0x060004EE RID: 1262 RVA: 0x000152F4 File Offset: 0x000134F4
		public static bool Contains<T>(T[] array, T item)
		{
			return Array.IndexOf<T>(array, item) != -1;
		}
	}
}
