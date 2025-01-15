using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D3 RID: 1491
	internal static class ArrayUtil
	{
		// Token: 0x060053BD RID: 21437 RVA: 0x00160D0C File Offset: 0x0015EF0C
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

		// Token: 0x060053BE RID: 21438 RVA: 0x00160D3C File Offset: 0x0015EF3C
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

		// Token: 0x060053BF RID: 21439 RVA: 0x00160DA8 File Offset: 0x0015EFA8
		public static bool Contains<T>(T[] array, T item)
		{
			return Array.IndexOf<T>(array, item) != -1;
		}
	}
}
