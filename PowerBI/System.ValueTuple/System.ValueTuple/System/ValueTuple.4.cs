using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000008 RID: 8
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3> : IEquatable<global::System.ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3>>, ITupleInternal
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00004943 File Offset: 0x00002B43
		public ValueTuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000495A File Offset: 0x00002B5A
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3> && this.Equals((global::System.ValueTuple<T1, T2, T3>)obj);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004974 File Offset: 0x00002B74
		public bool Equals(global::System.ValueTuple<T1, T2, T3> other)
		{
			return global::System.ValueTuple<T1, T2, T3>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3>.s_t3Comparer.Equals(this.Item3, other.Item3);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000049CC File Offset: 0x00002BCC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3> valueTuple = (global::System.ValueTuple<T1, T2, T3>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A47 File Offset: 0x00002C47
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2, T3>)other);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A74 File Offset: 0x00002C74
		public int CompareTo(global::System.ValueTuple<T1, T2, T3> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T3>.Default.Compare(this.Item3, other.Item3);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004AD0 File Offset: 0x00002CD0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2, T3> valueTuple = (global::System.ValueTuple<T1, T2, T3>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item2, valueTuple.Item2);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B61 File Offset: 0x00002D61
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3>.s_t3Comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B98 File Offset: 0x00002D98
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004BA1 File Offset: 0x00002DA1
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004B98 File Offset: 0x00002D98
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004BDC File Offset: 0x00002DDC
		public override string ToString()
		{
			string[] array = new string[7];
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
			array[4] = ", ";
			int num3 = 5;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_00C5;
				}
			}
			text3 = ptr3.ToString();
			IL_00C5:
			array[num3] = text3;
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004CBC File Offset: 0x00002EBC
		string ITupleInternal.ToStringEnd()
		{
			string[] array = new string[6];
			int num = 0;
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
					goto IL_003D;
				}
			}
			text = ptr.ToString();
			IL_003D:
			array[num] = text;
			array[1] = ", ";
			int num2 = 2;
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
					goto IL_007D;
				}
			}
			text2 = ptr2.ToString();
			IL_007D:
			array[num2] = text2;
			array[3] = ", ";
			int num3 = 4;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_00BD;
				}
			}
			text3 = ptr3.ToString();
			IL_00BD:
			array[num3] = text3;
			array[5] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004D94 File Offset: 0x00002F94
		int ITupleInternal.Size
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x04000007 RID: 7
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000008 RID: 8
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000009 RID: 9
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x0400000A RID: 10
		public T1 Item1;

		// Token: 0x0400000B RID: 11
		public T2 Item2;

		// Token: 0x0400000C RID: 12
		public T3 Item3;
	}
}
