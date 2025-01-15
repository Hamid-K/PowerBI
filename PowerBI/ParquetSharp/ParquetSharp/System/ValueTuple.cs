using System;
using System.Collections;
using System.Numerics.Hashing;

namespace System
{
	// Token: 0x020000B1 RID: 177
	internal struct ValueTuple : IEquatable<global::System.ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple>, ITupleInternal
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000142C8 File Offset: 0x000124C8
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000142D4 File Offset: 0x000124D4
		public bool Equals(global::System.ValueTuple other)
		{
			return true;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000142D8 File Offset: 0x000124D8
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is global::System.ValueTuple;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000142E4 File Offset: 0x000124E4
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

		// Token: 0x0600057F RID: 1407 RVA: 0x0001430C File Offset: 0x0001250C
		public int CompareTo(global::System.ValueTuple other)
		{
			return 0;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014310 File Offset: 0x00012510
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

		// Token: 0x06000581 RID: 1409 RVA: 0x00014338 File Offset: 0x00012538
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001433C File Offset: 0x0001253C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00014340 File Offset: 0x00012540
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00014344 File Offset: 0x00012544
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001434C File Offset: 0x0001254C
		string ITupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014354 File Offset: 0x00012554
		int ITupleInternal.Size
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014358 File Offset: 0x00012558
		public static global::System.ValueTuple Create()
		{
			return default(global::System.ValueTuple);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014374 File Offset: 0x00012574
		public static global::System.ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new global::System.ValueTuple<T1>(item1);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001437C File Offset: 0x0001257C
		public static global::System.ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new global::System.ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014388 File Offset: 0x00012588
		public static global::System.ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new global::System.ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014394 File Offset: 0x00012594
		public static global::System.ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000143A0 File Offset: 0x000125A0
		public static global::System.ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000143B0 File Offset: 0x000125B0
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000143C0 File Offset: 0x000125C0
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000143D4 File Offset: 0x000125D4
		public static global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7, global::System.ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new global::System.ValueTuple<T1, T2, T3, T4, T5, T6, T7, global::System.ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, global::System.ValueTuple.Create<T8>(item8));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000143EC File Offset: 0x000125EC
		internal static int CombineHashCodes(int h1, int h2)
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00014400 File Offset: 0x00012600
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00014410 File Offset: 0x00012610
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00014420 File Offset: 0x00012620
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00014434 File Offset: 0x00012634
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00014448 File Offset: 0x00012648
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00014460 File Offset: 0x00012660
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return HashHelpers.Combine(global::System.ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
