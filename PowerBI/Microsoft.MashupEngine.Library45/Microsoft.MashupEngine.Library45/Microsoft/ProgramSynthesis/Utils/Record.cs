using System;
using System.Collections;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003DF RID: 991
	public struct Record : IEquatable<Record>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record>, IRecordInternal
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x00041154 File Offset: 0x0003F354
		public override bool Equals(object obj)
		{
			return obj is Record;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(Record other)
		{
			return true;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00041154 File Offset: 0x0003F354
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is Record;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004115F File Offset: 0x0003F35F
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return 0;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int CompareTo(Record other)
		{
			return 0;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0004115F File Offset: 0x0003F35F
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return 0;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0000FA11 File Offset: 0x0000DC11
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0000FA11 File Offset: 0x0000DC11
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004117F File Offset: 0x0003F37F
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00041186 File Offset: 0x0003F386
		string IRecordInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0000FA11 File Offset: 0x0000DC11
		int IRecordInternal.Size
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00041190 File Offset: 0x0003F390
		public static Record Create()
		{
			return default(Record);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000411A6 File Offset: 0x0003F3A6
		public static Record<T1> Create<T1>(T1 item1)
		{
			return new Record<T1>(item1);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000411AE File Offset: 0x0003F3AE
		public static Record<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Record<T1, T2>(item1, item2);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000411B7 File Offset: 0x0003F3B7
		public static Record<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Record<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000411C1 File Offset: 0x0003F3C1
		public static Record<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new Record<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000411CC File Offset: 0x0003F3CC
		public static Record<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new Record<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000411D9 File Offset: 0x0003F3D9
		public static Record<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new Record<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000411E8 File Offset: 0x0003F3E8
		public static Record<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new Record<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000411F9 File Offset: 0x0003F3F9
		public static Record<T1, T2, T3, T4, T5, T6, T7, Record<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new Record<T1, T2, T3, T4, T5, T6, T7, Record<T8>>(item1, item2, item3, item4, item5, item6, item7, Record.Create<T8>(item8));
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00041211 File Offset: 0x0003F411
		internal static int CombineHashCodes(int h1, int h2)
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00041224 File Offset: 0x0003F424
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00041233 File Offset: 0x0003F433
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00041243 File Offset: 0x0003F443
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00041255 File Offset: 0x0003F455
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00041269 File Offset: 0x0003F469
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0004127F File Offset: 0x0003F47F
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return HashHelpers.Combine(Record.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
