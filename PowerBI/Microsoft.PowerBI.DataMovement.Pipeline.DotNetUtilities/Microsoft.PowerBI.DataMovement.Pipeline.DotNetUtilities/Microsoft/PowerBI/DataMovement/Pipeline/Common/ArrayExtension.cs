using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ArrayExtension
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000218C File Offset: 0x0000038C
		internal static T[] ShrinkByteArray<[Nullable(2)] T>(this T[] byteArray, int len)
		{
			if (byteArray.Length == len)
			{
				return byteArray;
			}
			T[] array = new T[len];
			Array.Copy(byteArray, array, len);
			return array;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static T[] CloneArray<[Nullable(2)] T>(this T[] array)
		{
			if (array == null)
			{
				return null;
			}
			T[] array2 = new T[array.Length];
			Array.Copy(array, array2, array2.Length);
			return array2;
		}
	}
}
