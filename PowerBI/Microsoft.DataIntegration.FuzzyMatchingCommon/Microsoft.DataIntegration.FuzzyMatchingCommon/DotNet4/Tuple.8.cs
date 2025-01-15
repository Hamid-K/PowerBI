using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0001256A File Offset: 0x0001076A
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00012572 File Offset: 0x00010772
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0001257A File Offset: 0x0001077A
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00012582 File Offset: 0x00010782
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0001258A File Offset: 0x0001078A
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00012592 File Offset: 0x00010792
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0001259A File Offset: 0x0001079A
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000125A2 File Offset: 0x000107A2
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000125DF File Offset: 0x000107DF
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000125F0 File Offset: 0x000107F0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6)) && comparer.Equals(this.m_Item7, tuple.m_Item7);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000126E6 File Offset: 0x000108E6
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000126F4 File Offset: 0x000108F4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
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
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item6, tuple.m_Item6);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item7, tuple.m_Item7);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001281A File Offset: 0x00010A1A
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00012828 File Offset: 0x00010A28
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000128B1 File Offset: 0x00010AB1
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000128BC File Offset: 0x00010ABC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000128E4 File Offset: 0x00010AE4
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
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(", ");
			sb.Append(this.m_Item7);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000129C9 File Offset: 0x00010BC9
		int ITuple.Size
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly T1 m_Item1;

		// Token: 0x04000050 RID: 80
		private readonly T2 m_Item2;

		// Token: 0x04000051 RID: 81
		private readonly T3 m_Item3;

		// Token: 0x04000052 RID: 82
		private readonly T4 m_Item4;

		// Token: 0x04000053 RID: 83
		private readonly T5 m_Item5;

		// Token: 0x04000054 RID: 84
		private readonly T6 m_Item6;

		// Token: 0x04000055 RID: 85
		private readonly T7 m_Item7;
	}
}
