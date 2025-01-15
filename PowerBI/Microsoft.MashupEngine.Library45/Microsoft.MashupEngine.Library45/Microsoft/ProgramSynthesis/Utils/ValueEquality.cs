using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000521 RID: 1313
	public class ValueEquality : IEqualityComparer, IEqualityComparer<object>
	{
		// Token: 0x06001D38 RID: 7480 RVA: 0x00002130 File Offset: 0x00000330
		private ValueEquality()
		{
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00056D46 File Offset: 0x00054F46
		public static ValueEquality Comparer { get; } = new ValueEquality();

		// Token: 0x06001D3A RID: 7482 RVA: 0x00056D4D File Offset: 0x00054F4D
		public bool Equals(object x, object y)
		{
			return this.EqualsImpl(x, y);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00056D58 File Offset: 0x00054F58
		private bool EqualsImpl(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return y == null;
			}
			if (y == null)
			{
				return false;
			}
			Type type = x.GetType();
			Type type2 = y.GetType();
			if (type == type2)
			{
				foreach (Type type3 in type.GetInterfaces())
				{
					if (type3.IsGenericType && type3.GetGenericTypeDefinition() == typeof(IEquatable<>) && type3.GetGenericArguments()[0].IsAssignableFrom(type2))
					{
						return x.Equals(y);
					}
				}
			}
			IEnumerable<object> enumerable = x.ToEnumerable<object>();
			IEnumerable<object> enumerable2 = y.ToEnumerable<object>();
			if (enumerable != null)
			{
				return enumerable2 != null && enumerable.SequenceEqual(enumerable2, this);
			}
			if (enumerable2 != null)
			{
				return false;
			}
			if (type != type2)
			{
				return false;
			}
			Regex regex = x as Regex;
			Regex regex2 = y as Regex;
			if (regex != null)
			{
				return regex2 != null && RegexValueEquality.Comparer.Equals(regex, regex2);
			}
			return regex2 == null && StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00056E58 File Offset: 0x00055058
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			Type type = obj.GetType();
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IEquatable<>) && type2.GetGenericArguments()[0].IsAssignableFrom(type))
				{
					return obj.GetHashCode();
				}
			}
			IEnumerable<object> enumerable = obj.ToEnumerable<object>();
			if (enumerable != null)
			{
				return enumerable.Aggregate(8324749, (int hash, object o) => hash * 49757 + this.GetHashCode(o));
			}
			Regex regex = obj as Regex;
			if (regex != null)
			{
				return RegexValueEquality.Comparer.GetHashCode(regex);
			}
			return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00056F0C File Offset: 0x0005510C
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return ((IEqualityComparer)this).Equals(x, y);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00056F16 File Offset: 0x00055116
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return ((IEqualityComparer)this).GetHashCode(obj);
		}
	}
}
