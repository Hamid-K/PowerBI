using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00011E2A File Offset: 0x0001002A
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00011E32 File Offset: 0x00010032
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00011E3A File Offset: 0x0001003A
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00011E42 File Offset: 0x00010042
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00011E4A File Offset: 0x0001004A
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00011E52 File Offset: 0x00010052
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00011E7F File Offset: 0x0001007F
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00011E90 File Offset: 0x00010090
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4)) && comparer.Equals(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00011F44 File Offset: 0x00010144
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00011F54 File Offset: 0x00010154
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
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
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00012036 File Offset: 0x00010236
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00012044 File Offset: 0x00010244
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000120AB File Offset: 0x000102AB
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000120B4 File Offset: 0x000102B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000120DC File Offset: 0x000102DC
		string ITuple.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00012185 File Offset: 0x00010385
		int ITuple.Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly T1 m_Item1;

		// Token: 0x04000045 RID: 69
		private readonly T2 m_Item2;

		// Token: 0x04000046 RID: 70
		private readonly T3 m_Item3;

		// Token: 0x04000047 RID: 71
		private readonly T4 m_Item4;

		// Token: 0x04000048 RID: 72
		private readonly T5 m_Item5;
	}
}
