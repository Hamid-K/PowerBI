using System;
using System.Collections;
using System.Numerics.Hashing;

namespace System
{
	// Token: 0x02000005 RID: 5
	public struct ValueTuple : IEquatable<global::System.ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple>, ITupleInternal
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000042D9 File Offset: 0x000024D9
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000042E4 File Offset: 0x000024E4
		public bool Equals(global::System.ValueTuple other)
		{
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000042D9 File Offset: 0x000024D9
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is global::System.ValueTuple;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000042E7 File Offset: 0x000024E7
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004307 File Offset: 0x00002507
		public int CompareTo(global::System.ValueTuple other)
		{
			return 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000042E7 File Offset: 0x000024E7
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return 0;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004307 File Offset: 0x00002507
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004307 File Offset: 0x00002507
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004307 File Offset: 0x00002507
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000430A File Offset: 0x0000250A
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004311 File Offset: 0x00002511
		string ITupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004307 File Offset: 0x00002507
		int ITupleInternal.Size
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004318 File Offset: 0x00002518
		public static global::System.ValueTuple Create()
		{
			return default(global::System.ValueTuple);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000432E File Offset: 0x0000252E
		public static global::System.ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new global::System.ValueTuple<T1>(item1);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004336 File Offset: 0x00002536
		public static global::System.ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new global::System.ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000433F File Offset: 0x0000253F
		public static global::System.ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new global::System.ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004349 File Offset: 0x00002549
		public static global::System.ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004354 File Offset: 0x00002554
		public static global::System.ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004361 File Offset: 0x00002561
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004370 File Offset: 0x00002570
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004381 File Offset: 0x00002581
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7, global::System.ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7, global::System.ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, global::System.ValueTuple.Create<T8>(item8));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004399 File Offset: 0x00002599
		internal static int CombineHashCodes(int h1, int h2)
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000043AC File Offset: 0x000025AC
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043BB File Offset: 0x000025BB
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000043CB File Offset: 0x000025CB
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000043DD File Offset: 0x000025DD
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000043F1 File Offset: 0x000025F1
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004407 File Offset: 0x00002607
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
