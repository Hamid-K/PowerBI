using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000253 RID: 595
	internal static class Utils
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x00045D7C File Offset: 0x00043F7C
		internal static bool TryDispose(object o)
		{
			IDisposable disposable = o as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00045D9C File Offset: 0x00043F9C
		internal static KeyValuePair<int, T>[] StableSort<T>(this T[] array, Comparison<T> comparison)
		{
			ExceptionUtils.CheckArgumentNotNull<T[]>(array, "array");
			ExceptionUtils.CheckArgumentNotNull<Comparison<T>>(comparison, "comparison");
			KeyValuePair<int, T>[] array2 = new KeyValuePair<int, T>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new KeyValuePair<int, T>(i, array[i]);
			}
			Array.Sort<KeyValuePair<int, T>>(array2, new Utils.StableComparer<T>(comparison));
			return array2;
		}

		// Token: 0x02000254 RID: 596
		private sealed class StableComparer<T> : IComparer<KeyValuePair<int, T>>
		{
			// Token: 0x06001279 RID: 4729 RVA: 0x00045DFC File Offset: 0x00043FFC
			public StableComparer(Comparison<T> innerComparer)
			{
				this.innerComparer = innerComparer;
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x00045E0C File Offset: 0x0004400C
			public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = this.innerComparer.Invoke(x.Value, y.Value);
				if (num == 0)
				{
					num = x.Key - y.Key;
				}
				return num;
			}

			// Token: 0x040006F8 RID: 1784
			private readonly Comparison<T> innerComparer;
		}
	}
}
