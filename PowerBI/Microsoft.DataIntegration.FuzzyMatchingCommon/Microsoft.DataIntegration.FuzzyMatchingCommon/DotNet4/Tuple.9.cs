using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITuple
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000129CC File Offset: 0x00010BCC
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000129D4 File Offset: 0x00010BD4
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000129DC File Offset: 0x00010BDC
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000129E4 File Offset: 0x00010BE4
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000129EC File Offset: 0x00010BEC
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000129F4 File Offset: 0x00010BF4
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000129FC File Offset: 0x00010BFC
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00012A04 File Offset: 0x00010C04
		public TRest Rest
		{
			get
			{
				return this.m_Rest;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00012A0C File Offset: 0x00010C0C
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is ITuple))
			{
				throw new ArgumentException(Environment40.GetResourceString("ArgumentException_TupleLastArgumentNotATuple"));
			}
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
			this.m_Rest = rest;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00012A7A File Offset: 0x00010C7A
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00012A88 File Offset: 0x00010C88
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6) && comparer.Equals(this.m_Item7, tuple.m_Item7)) && comparer.Equals(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00012B9F File Offset: 0x00010D9F
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00012BB0 File Offset: 0x00010DB0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
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
			num = comparer.Compare(this.m_Item7, tuple.m_Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00012D08 File Offset: 0x00010F08
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ITuple tuple = (ITuple)((object)this.m_Rest);
			if (tuple.Size >= 8)
			{
				return tuple.GetHashCode(comparer);
			}
			switch (8 - tuple.Size)
			{
			case 1:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 2:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 3:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 4:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 5:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 6:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			case 7:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tuple.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00012FA1 File Offset: 0x000111A1
		int ITuple.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00012FAC File Offset: 0x000111AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITuple)this).ToString(stringBuilder);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00012FD4 File Offset: 0x000111D4
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
			sb.Append(", ");
			return ((ITuple)((object)this.m_Rest)).ToString(sb);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000130C9 File Offset: 0x000112C9
		int ITuple.Size
		{
			get
			{
				return 7 + ((ITuple)((object)this.m_Rest)).Size;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly T1 m_Item1;

		// Token: 0x04000057 RID: 87
		private readonly T2 m_Item2;

		// Token: 0x04000058 RID: 88
		private readonly T3 m_Item3;

		// Token: 0x04000059 RID: 89
		private readonly T4 m_Item4;

		// Token: 0x0400005A RID: 90
		private readonly T5 m_Item5;

		// Token: 0x0400005B RID: 91
		private readonly T6 m_Item6;

		// Token: 0x0400005C RID: 92
		private readonly T7 m_Item7;

		// Token: 0x0400005D RID: 93
		private readonly TRest m_Rest;
	}
}
