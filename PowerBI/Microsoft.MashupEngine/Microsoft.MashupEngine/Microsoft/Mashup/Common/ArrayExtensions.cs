using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD5 RID: 7125
	public static class ArrayExtensions
	{
		// Token: 0x0600B1BC RID: 45500 RVA: 0x002441FC File Offset: 0x002423FC
		public static T[] Add<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array.CopyTo(array2, 0);
			array2[array.Length] = item;
			return array2;
		}

		// Token: 0x0600B1BD RID: 45501 RVA: 0x00244228 File Offset: 0x00242428
		public static T[] Add<T>(this T[] array, params T[] items)
		{
			T[] array2 = new T[array.Length + items.Length];
			array.CopyTo(array2, 0);
			items.CopyTo(array2, array.Length);
			return array2;
		}

		// Token: 0x0600B1BE RID: 45502 RVA: 0x00244258 File Offset: 0x00242458
		public static T[] ShallowCopy<T>(this T[] array)
		{
			T[] array2 = new T[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x0600B1BF RID: 45503 RVA: 0x0024427C File Offset: 0x0024247C
		public static T[] RemoveAt<T>(this T[] array, int position)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, array2, position);
			Array.Copy(array, position + 1, array2, position, array2.Length - position);
			return array2;
		}

		// Token: 0x0600B1C0 RID: 45504 RVA: 0x002442B0 File Offset: 0x002424B0
		public static T[] ReplaceAt<T>(this T[] array, int position, T item)
		{
			T[] array2 = new T[array.Length];
			array.CopyTo(array2, 0);
			array2[position] = item;
			return array2;
		}

		// Token: 0x0600B1C1 RID: 45505 RVA: 0x002442D8 File Offset: 0x002424D8
		public static TResult[] Select<T, TResult>(this T[] array, Func<T, TResult> transformer)
		{
			TResult[] array2 = new TResult[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = transformer(array[i]);
			}
			return array2;
		}

		// Token: 0x0600B1C2 RID: 45506 RVA: 0x00244314 File Offset: 0x00242514
		public static bool AllEqual<T>(this T[] array, T[] other) where T : IEquatable<T>
		{
			if (array.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num = i;
				ref T ptr = ref array[num];
				if (default(T) == null)
				{
					T t = array[num];
					ptr = ref t;
				}
				if (!ptr.Equals(other[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
