using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000009 RID: 9
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3, T4> : IEquatable<global::System.ValueTuple<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3, T4>>, ITupleInternal
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004DB7 File Offset: 0x00002FB7
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3, T4> && this.Equals((global::System.ValueTuple<T1, T2, T3, T4>)obj);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public bool Equals(global::System.ValueTuple<T1, T2, T3, T4> other)
		{
			return global::System.ValueTuple<T1, T2, T3, T4>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3, T4>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3, T4>.s_t3Comparer.Equals(this.Item3, other.Item3) && global::System.ValueTuple<T1, T2, T3, T4>.s_t4Comparer.Equals(this.Item4, other.Item4);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E60 File Offset: 0x00003060
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3, T4>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3, T4> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004EF9 File Offset: 0x000030F9
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2, T3, T4>)other);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004F24 File Offset: 0x00003124
		public int CompareTo(global::System.ValueTuple<T1, T2, T3, T4> other)
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
			num = Comparer<T3>.Default.Compare(this.Item3, other.Item3);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T4>.Default.Compare(this.Item4, other.Item4);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F9C File Offset: 0x0000319C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2, T3, T4> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4>)other;
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
			num = comparer.Compare(this.Item3, valueTuple.Item3);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005050 File Offset: 0x00003250
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3, T4>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3, T4>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3, T4>.s_t3Comparer.GetHashCode(this.Item3), global::System.ValueTuple<T1, T2, T3, T4>.s_t4Comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000050A2 File Offset: 0x000032A2
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000050AC File Offset: 0x000032AC
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000050A2 File Offset: 0x000032A2
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005104 File Offset: 0x00003304
		public override string ToString()
		{
			string[] array = new string[9];
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
					goto IL_0046;
				}
			}
			text = ptr.ToString();
			IL_0046:
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
					goto IL_0086;
				}
			}
			text2 = ptr2.ToString();
			IL_0086:
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
					goto IL_00C6;
				}
			}
			text3 = ptr3.ToString();
			IL_00C6:
			array[num3] = text3;
			array[6] = ", ";
			int num4 = 7;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_0106;
				}
			}
			text4 = ptr4.ToString();
			IL_0106:
			array[num4] = text4;
			array[8] = ")";
			return string.Concat(array);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005228 File Offset: 0x00003428
		string ITupleInternal.ToStringEnd()
		{
			string[] array = new string[8];
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
			array[5] = ", ";
			int num4 = 6;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_00FD;
				}
			}
			text4 = ptr4.ToString();
			IL_00FD:
			array[num4] = text4;
			array[7] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005340 File Offset: 0x00003540
		int ITupleInternal.Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0400000D RID: 13
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x0400000E RID: 14
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x0400000F RID: 15
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000010 RID: 16
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000011 RID: 17
		public T1 Item1;

		// Token: 0x04000012 RID: 18
		public T2 Item2;

		// Token: 0x04000013 RID: 19
		public T3 Item3;

		// Token: 0x04000014 RID: 20
		public T4 Item4;
	}
}
