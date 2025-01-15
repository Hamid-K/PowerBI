using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003EA RID: 1002
	public class ArrayEquality<T> : IEqualityComparer<T[]>
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x00045D79 File Offset: 0x00043F79
		public bool Equals(T[] x, T[] y)
		{
			return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00045D88 File Offset: 0x00043F88
		public int GetHashCode(T[] obj)
		{
			int num = 0;
			if (obj == null)
			{
				return num;
			}
			int i = 0;
			while (i < obj.Length)
			{
				T t = obj[i];
				int num2 = 1000000007 * num;
				ref T ptr = ref t;
				T t2 = default(T);
				if (t2 != null)
				{
					goto IL_0045;
				}
				t2 = t;
				ptr = ref t2;
				if (t2 != null)
				{
					goto IL_0045;
				}
				int num3 = 0;
				IL_0050:
				num = num2 ^ num3;
				i++;
				continue;
				IL_0045:
				num3 = ptr.GetHashCode();
				goto IL_0050;
			}
			return num;
		}
	}
}
