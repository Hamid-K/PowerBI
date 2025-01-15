using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000DD RID: 221
	internal static class Utils
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0001BD64 File Offset: 0x00019F64
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

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001BD84 File Offset: 0x00019F84
		internal static Task FlushAsync(this Stream stream)
		{
			return Task.Factory.StartNew(new Action(stream.Flush));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001BDA0 File Offset: 0x00019FA0
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

		// Token: 0x02000354 RID: 852
		private sealed class StableComparer<T> : IComparer<KeyValuePair<int, T>>
		{
			// Token: 0x06001EA6 RID: 7846 RVA: 0x0005968D File Offset: 0x0005788D
			public StableComparer(Comparison<T> innerComparer)
			{
				this.innerComparer = innerComparer;
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x0005969C File Offset: 0x0005789C
			public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
			{
				int num = this.innerComparer(x.Value, y.Value);
				if (num == 0)
				{
					num = x.Key - y.Key;
				}
				return num;
			}

			// Token: 0x04000E00 RID: 3584
			private readonly Comparison<T> innerComparer;
		}
	}
}
