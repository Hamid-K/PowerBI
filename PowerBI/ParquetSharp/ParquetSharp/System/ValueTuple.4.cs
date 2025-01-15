using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Auto)]
	internal struct ValueTuple<T1, T2, T3> : IEquatable<global::System.ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3>>, ITupleInternal
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x00014A70 File Offset: 0x00012C70
		public ValueTuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014A88 File Offset: 0x00012C88
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3> && this.Equals((global::System.ValueTuple<T1, T2, T3>)obj);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00014AA4 File Offset: 0x00012CA4
		public bool Equals(global::System.ValueTuple<T1, T2, T3> other)
		{
			return global::System.ValueTuple<T1, T2, T3>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3>.s_t3Comparer.Equals(this.Item3, other.Item3);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00014B04 File Offset: 0x00012D04
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3> valueTuple = (global::System.ValueTuple<T1, T2, T3>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00014B90 File Offset: 0x00012D90
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

		// Token: 0x060005B9 RID: 1465 RVA: 0x00014BC4 File Offset: 0x00012DC4
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

		// Token: 0x060005BA RID: 1466 RVA: 0x00014C2C File Offset: 0x00012E2C
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

		// Token: 0x060005BB RID: 1467 RVA: 0x00014CD0 File Offset: 0x00012ED0
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3>.s_t3Comparer.GetHashCode(this.Item3));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014D18 File Offset: 0x00012F18
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00014D24 File Offset: 0x00012F24
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00014D70 File Offset: 0x00012F70
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00014D7C File Offset: 0x00012F7C
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
					goto IL_004E;
				}
			}
			text = ptr.ToString();
			IL_004E:
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
					goto IL_0097;
				}
			}
			text2 = ptr2.ToString();
			IL_0097:
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
					goto IL_00E0;
				}
			}
			text3 = ptr3.ToString();
			IL_00E0:
			array[num3] = text3;
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00014E7C File Offset: 0x0001307C
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
					goto IL_0046;
				}
			}
			text = ptr.ToString();
			IL_0046:
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
					goto IL_008F;
				}
			}
			text2 = ptr2.ToString();
			IL_008F:
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
					goto IL_00D8;
				}
			}
			text3 = ptr3.ToString();
			IL_00D8:
			array[num3] = text3;
			array[5] = ")";
			return string.Concat(array);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00014F74 File Offset: 0x00013174
		int ITupleInternal.Size
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x04000193 RID: 403
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000194 RID: 404
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000195 RID: 405
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000196 RID: 406
		public T1 Item1;

		// Token: 0x04000197 RID: 407
		public T2 Item2;

		// Token: 0x04000198 RID: 408
		public T3 Item3;
	}
}
