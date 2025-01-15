using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x0200029D RID: 669
	internal static class Utils
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x0004EDDC File Offset: 0x0004CFDC
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

		// Token: 0x06001701 RID: 5889 RVA: 0x0004EDFC File Offset: 0x0004CFFC
		[SuppressMessage("Microsoft.MSInternal", "CA908:AvoidTypesThatRequireJitCompilationInPrecompiledAssemblies", Justification = "Array.Sort is causing this, but it is needed in order to sort the KeyValuePairs using our own comparer.")]
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

		// Token: 0x0200029E RID: 670
		private sealed class StableComparer<T> : IComparer<KeyValuePair<int, T>>
		{
			// Token: 0x06001702 RID: 5890 RVA: 0x0004EE5C File Offset: 0x0004D05C
			public StableComparer(Comparison<T> innerComparer)
			{
				this.innerComparer = innerComparer;
			}

			// Token: 0x06001703 RID: 5891 RVA: 0x0004EE6C File Offset: 0x0004D06C
			public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = this.innerComparer.Invoke(x.Value, y.Value);
				if (num == 0)
				{
					num = x.Key - y.Key;
				}
				return num;
			}

			// Token: 0x04000A07 RID: 2567
			private readonly Comparison<T> innerComparer;
		}
	}
}
