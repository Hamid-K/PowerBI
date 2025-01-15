using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x02000006 RID: 6
	public struct ValueTuple<T1> : IEquatable<global::System.ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1>>, ITupleInternal
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000441F File Offset: 0x0000261F
		public ValueTuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004428 File Offset: 0x00002628
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1> && this.Equals((global::System.ValueTuple<T1>)obj);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004440 File Offset: 0x00002640
		public bool Equals(global::System.ValueTuple<T1> other)
		{
			return global::System.ValueTuple<T1>.s_t1Comparer.Equals(this.Item1, other.Item1);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004458 File Offset: 0x00002658
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1>))
			{
				return false;
			}
			global::System.ValueTuple<T1> valueTuple = (global::System.ValueTuple<T1>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004498 File Offset: 0x00002698
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

		// Token: 0x06000066 RID: 102 RVA: 0x000044DF File Offset: 0x000026DF
		public int CompareTo(global::System.ValueTuple<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000044F8 File Offset: 0x000026F8
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

		// Token: 0x06000068 RID: 104 RVA: 0x00004545 File Offset: 0x00002745
		public override int GetHashCode()
		{
			return global::System.ValueTuple<T1>.s_t1Comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004557 File Offset: 0x00002757
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004557 File Offset: 0x00002757
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000456C File Offset: 0x0000276C
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
					goto IL_003A;
				}
			}
			text2 = ptr.ToString();
			IL_003A:
			return text + text2 + ")";
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000045C0 File Offset: 0x000027C0
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
					goto IL_0035;
				}
			}
			text = ptr.ToString();
			IL_0035:
			return text + ")";
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000042E4 File Offset: 0x000024E4
		int ITupleInternal.Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000002 RID: 2
		public T1 Item1;
	}
}
