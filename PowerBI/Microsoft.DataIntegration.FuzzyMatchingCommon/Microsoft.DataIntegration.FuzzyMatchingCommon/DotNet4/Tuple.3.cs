using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class Tuple<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00011723 File Offset: 0x0000F923
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0001172B File Offset: 0x0000F92B
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00011733 File Offset: 0x0000F933
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00011749 File Offset: 0x0000F949
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00011758 File Offset: 0x0000F958
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000117B2 File Offset: 0x0000F9B2
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000117C0 File Offset: 0x0000F9C0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment40.GetResourceString("ArgumentException_TupleIncorrectType", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001183C File Offset: 0x0000FA3C
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00011849 File Offset: 0x0000FA49
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00011872 File Offset: 0x0000FA72
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001187C File Offset: 0x0000FA7C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000118A4 File Offset: 0x0000FAA4
		string ITuple.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000118F3 File Offset: 0x0000FAF3
		int ITuple.Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly T1 m_Item1;

		// Token: 0x0400003C RID: 60
		private readonly T2 m_Item2;
	}
}
