using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000B3 RID: 179
	internal static class Utils
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00013B34 File Offset: 0x00011D34
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

		// Token: 0x060006EC RID: 1772 RVA: 0x00013B54 File Offset: 0x00011D54
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

		// Token: 0x0200029D RID: 669
		private sealed class StableComparer<T> : IComparer<KeyValuePair<int, T>>
		{
			// Token: 0x06001848 RID: 6216 RVA: 0x000486E8 File Offset: 0x000468E8
			public StableComparer(Comparison<T> innerComparer)
			{
				this.innerComparer = innerComparer;
			}

			// Token: 0x06001849 RID: 6217 RVA: 0x000486F8 File Offset: 0x000468F8
			public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = this.innerComparer.Invoke(x.Value, y.Value);
				if (num == 0)
				{
					num = x.Key - y.Key;
				}
				return num;
			}

			// Token: 0x04000BAC RID: 2988
			private readonly Comparison<T> innerComparer;
		}
	}
}
