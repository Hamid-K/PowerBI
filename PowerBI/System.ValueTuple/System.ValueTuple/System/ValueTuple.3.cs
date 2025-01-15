using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000007 RID: 7
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2> : IEquatable<global::System.ValueTuple<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2>>, ITupleInternal
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004618 File Offset: 0x00002818
		public ValueTuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004628 File Offset: 0x00002828
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2> && this.Equals((global::System.ValueTuple<T1, T2>)obj);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004640 File Offset: 0x00002840
		public bool Equals(global::System.ValueTuple<T1, T2> other)
		{
			return global::System.ValueTuple<T1, T2>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2>.s_t2Comparer.Equals(this.Item2, other.Item2);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004674 File Offset: 0x00002874
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2> valueTuple = (global::System.ValueTuple<T1, T2>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000046D1 File Offset: 0x000028D1
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2>)other);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000046FC File Offset: 0x000028FC
		public int CompareTo(global::System.ValueTuple<T1, T2> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T2>.Default.Compare(this.Item2, other.Item2);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000473C File Offset: 0x0000293C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2> valueTuple = (global::System.ValueTuple<T1, T2>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item2, valueTuple.Item2);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000047AB File Offset: 0x000029AB
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2>.s_t2Comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000047D2 File Offset: 0x000029D2
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000047DB File Offset: 0x000029DB
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000047D2 File Offset: 0x000029D2
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004804 File Offset: 0x00002A04
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "(";
			int num = 1;
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
					goto IL_0045;
				}
			}
			text = ptr.ToString();
			IL_0045:
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text2 = null;
					goto IL_0085;
				}
			}
			text2 = ptr2.ToString();
			IL_0085:
			array[num2] = text2;
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000048A4 File Offset: 0x00002AA4
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
			string text2 = ", ";
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text3;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text3 = null;
					goto IL_006F;
				}
			}
			text3 = ptr2.ToString();
			IL_006F:
			return text + text2 + text3 + ")";
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000492A File Offset: 0x00002B2A
		int ITupleInternal.Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x04000003 RID: 3
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000004 RID: 4
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000005 RID: 5
		public T1 Item1;

		// Token: 0x04000006 RID: 6
		public T2 Item2;
	}
}
