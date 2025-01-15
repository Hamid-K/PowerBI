using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00011B4C File Offset: 0x0000FD4C
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00011B54 File Offset: 0x0000FD54
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00011B64 File Offset: 0x0000FD64
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00011B91 File Offset: 0x0000FD91
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3)) && comparer.Equals(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00011C36 File Offset: 0x0000FE36
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00011C44 File Offset: 0x0000FE44
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment40.GetResourceString("ArgumentException_TupleIncorrectType", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item4, tuple.m_Item4);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00011D04 File Offset: 0x0000FF04
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011D14 File Offset: 0x0000FF14
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00011D6A File Offset: 0x0000FF6A
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00011D74 File Offset: 0x0000FF74
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00011D9C File Offset: 0x0000FF9C
		string ITuple.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00011E27 File Offset: 0x00010027
		int ITuple.Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x04000040 RID: 64
		private readonly T1 m_Item1;

		// Token: 0x04000041 RID: 65
		private readonly T2 m_Item2;

		// Token: 0x04000042 RID: 66
		private readonly T3 m_Item3;

		// Token: 0x04000043 RID: 67
		private readonly T4 m_Item4;
	}
}
