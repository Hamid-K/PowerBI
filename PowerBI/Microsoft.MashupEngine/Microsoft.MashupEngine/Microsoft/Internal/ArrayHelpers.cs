using System;

namespace Microsoft.Internal
{
	// Token: 0x0200017F RID: 383
	internal static class ArrayHelpers
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public static int[] ComplementWithin(this int[] array, int bounds)
		{
			bool[] array2 = new bool[bounds];
			for (int i = 0; i < array.Length; i++)
			{
				array2[array[i]] = true;
			}
			int[] array3 = new int[bounds - array.Length];
			int num = 0;
			for (int j = 0; j < array2.Length; j++)
			{
				if (!array2[j])
				{
					array3[num] = j;
					num++;
				}
			}
			return array3;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static T[] NewArray<T>(int count, Func<int, T> getter)
		{
			T[] array = new T[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = getter(i);
			}
			return array;
		}
	}
}
