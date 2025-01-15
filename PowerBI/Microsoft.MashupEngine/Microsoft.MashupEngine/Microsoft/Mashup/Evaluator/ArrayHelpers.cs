using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C43 RID: 7235
	internal static class ArrayHelpers
	{
		// Token: 0x0600B48A RID: 46218 RVA: 0x00249BA4 File Offset: 0x00247DA4
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
