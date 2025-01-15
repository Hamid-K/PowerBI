using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00012188 File Offset: 0x00010388
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00012190 File Offset: 0x00010390
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00012198 File Offset: 0x00010398
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000121A0 File Offset: 0x000103A0
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000121A8 File Offset: 0x000103A8
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000121B0 File Offset: 0x000103B0
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000121B8 File Offset: 0x000103B8
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000121ED File Offset: 0x000103ED
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000121FC File Offset: 0x000103FC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5)) && comparer.Equals(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000122D1 File Offset: 0x000104D1
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000122E0 File Offset: 0x000104E0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
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
			return comparer.Compare(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000123E4 File Offset: 0x000105E4
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000123F4 File Offset: 0x000105F4
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001246C File Offset: 0x0001066C
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00012478 File Offset: 0x00010678
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000124A0 File Offset: 0x000106A0
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
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00012567 File Offset: 0x00010767
		int ITuple.Size
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x04000049 RID: 73
		private readonly T1 m_Item1;

		// Token: 0x0400004A RID: 74
		private readonly T2 m_Item2;

		// Token: 0x0400004B RID: 75
		private readonly T3 m_Item3;

		// Token: 0x0400004C RID: 76
		private readonly T4 m_Item4;

		// Token: 0x0400004D RID: 77
		private readonly T5 m_Item5;

		// Token: 0x0400004E RID: 78
		private readonly T6 m_Item6;
	}
}
