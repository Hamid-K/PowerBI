using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000023 RID: 35
	public static class RandomUtil
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00005ECC File Offset: 0x000040CC
		public static IEnumerable<T> Next<T>(this Random random, IEnumerable<T> data, int k)
		{
			IEnumerator<T> enumerator = data.GetEnumerator();
			int num = 0;
			List<T> list = new List<T>();
			while (enumerator.MoveNext())
			{
				T t = enumerator.Current;
				if (num < k)
				{
					list.Add(t);
				}
				else
				{
					int num2 = random.Next(num + 1);
					if (num2 < k)
					{
						list[num2] = t;
					}
				}
				num++;
			}
			return list;
		}
	}
}
