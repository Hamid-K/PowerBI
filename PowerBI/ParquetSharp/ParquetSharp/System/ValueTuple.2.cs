using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x020000B2 RID: 178
	internal struct ValueTuple<T1> : IEquatable<global::System.ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1>>, ITupleInternal
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00014478 File Offset: 0x00012678
		public ValueTuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014484 File Offset: 0x00012684
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1> && this.Equals((global::System.ValueTuple<T1>)obj);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000144A0 File Offset: 0x000126A0
		public bool Equals(global::System.ValueTuple<T1> other)
		{
			return global::System.ValueTuple<T1>.s_t1Comparer.Equals(this.Item1, other.Item1);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000144B8 File Offset: 0x000126B8
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1>))
			{
				return false;
			}
			global::System.ValueTuple<T1> valueTuple = (global::System.ValueTuple<T1>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00014500 File Offset: 0x00012700
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1> valueTuple = (global::System.ValueTuple<T1>)other;
			return Comparer<T1>.Default.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00014554 File Offset: 0x00012754
		public int CompareTo(global::System.ValueTuple<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001456C File Offset: 0x0001276C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1> valueTuple = (global::System.ValueTuple<T1>)other;
			return comparer.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000145C4 File Offset: 0x000127C4
		public override int GetHashCode()
		{
			return global::System.ValueTuple<T1>.s_t1Comparer.GetHashCode(this.Item1);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000145D8 File Offset: 0x000127D8
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000145EC File Offset: 0x000127EC
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00014600 File Offset: 0x00012800
		public override string ToString()
		{
			string text = "(";
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text2;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text2 = null;
					goto IL_0043;
				}
			}
			text2 = ptr.ToString();
			IL_0043:
			return text + text2 + ")";
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00014660 File Offset: 0x00012860
		string ITupleInternal.ToStringEnd()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_003E;
				}
			}
			text = ptr.ToString();
			IL_003E:
			return text + ")";
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000146BC File Offset: 0x000128BC
		int ITupleInternal.Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400018D RID: 397
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x0400018E RID: 398
		public T1 Item1;
	}
}
