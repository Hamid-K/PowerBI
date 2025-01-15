using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000118F6 File Offset: 0x0000FAF6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000118FE File Offset: 0x0000FAFE
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00011906 File Offset: 0x0000FB06
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001190E File Offset: 0x0000FB0E
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001192B File Offset: 0x0000FB2B
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001193C File Offset: 0x0000FB3C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000119B4 File Offset: 0x0000FBB4
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000119C4 File Offset: 0x0000FBC4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
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
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00011A62 File Offset: 0x0000FC62
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00011A6F File Offset: 0x0000FC6F
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00011AA9 File Offset: 0x0000FCA9
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00011ADC File Offset: 0x0000FCDC
		string ITuple.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00011B49 File Offset: 0x0000FD49
		int ITuple.Size
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly T1 m_Item1;

		// Token: 0x0400003E RID: 62
		private readonly T2 m_Item2;

		// Token: 0x0400003F RID: 63
		private readonly T3 m_Item3;
	}
}
