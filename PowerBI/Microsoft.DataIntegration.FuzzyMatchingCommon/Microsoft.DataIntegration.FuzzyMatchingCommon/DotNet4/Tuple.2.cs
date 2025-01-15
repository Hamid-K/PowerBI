using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000115E8 File Offset: 0x0000F7E8
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000115F7 File Offset: 0x0000F7F7
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00011608 File Offset: 0x0000F808
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00011642 File Offset: 0x0000F842
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00011650 File Offset: 0x0000F850
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment40.GetResourceString("ArgumentException_TupleIncorrectType", base.GetType().ToString()), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000116B5 File Offset: 0x0000F8B5
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000116C8 File Offset: 0x0000F8C8
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000116D4 File Offset: 0x0000F8D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000116FA File Offset: 0x0000F8FA
		string ITuple.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00011720 File Offset: 0x0000F920
		int ITuple.Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400003A RID: 58
		private readonly T1 m_Item1;
	}
}
