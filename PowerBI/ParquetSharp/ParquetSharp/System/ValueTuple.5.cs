using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Auto)]
	internal struct ValueTuple<T1, T2, T3, T4> : IEquatable<global::System.ValueTuple<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3, T4>>, ITupleInternal
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00014F98 File Offset: 0x00013198
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014FB8 File Offset: 0x000131B8
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3, T4> && this.Equals((global::System.ValueTuple<T1, T2, T3, T4>)obj);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014FD4 File Offset: 0x000131D4
		public bool Equals(global::System.ValueTuple<T1, T2, T3, T4> other)
		{
			return global::System.ValueTuple<T1, T2, T3, T4>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3, T4>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3, T4>.s_t3Comparer.Equals(this.Item3, other.Item3) && global::System.ValueTuple<T1, T2, T3, T4>.s_t4Comparer.Equals(this.Item4, other.Item4);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015050 File Offset: 0x00013250
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3, T4>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3, T4> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000150FC File Offset: 0x000132FC
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

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015130 File Offset: 0x00013330
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

		// Token: 0x060005C9 RID: 1481 RVA: 0x000151B4 File Offset: 0x000133B4
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

		// Token: 0x060005CA RID: 1482 RVA: 0x0001527C File Offset: 0x0001347C
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3, T4>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3, T4>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3, T4>.s_t3Comparer.GetHashCode(this.Item3), global::System.ValueTuple<T1, T2, T3, T4>.s_t4Comparer.GetHashCode(this.Item4));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000152D4 File Offset: 0x000134D4
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000152E0 File Offset: 0x000134E0
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4));
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001533C File Offset: 0x0001353C
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00015348 File Offset: 0x00013548
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
					goto IL_004F;
				}
			}
			text = ptr.ToString();
			IL_004F:
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
					goto IL_0098;
				}
			}
			text2 = ptr2.ToString();
			IL_0098:
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
					goto IL_00E1;
				}
			}
			text3 = ptr3.ToString();
			IL_00E1:
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
					goto IL_012A;
				}
			}
			text4 = ptr4.ToString();
			IL_012A:
			array[num4] = text4;
			array[8] = ")";
			return string.Concat(array);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00015494 File Offset: 0x00013694
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
					goto IL_0121;
				}
			}
			text4 = ptr4.ToString();
			IL_0121:
			array[num4] = text4;
			array[7] = ")";
			return string.Concat(array);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000155D4 File Offset: 0x000137D4
		int ITupleInternal.Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x04000199 RID: 409
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x0400019A RID: 410
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x0400019B RID: 411
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x0400019C RID: 412
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x0400019D RID: 413
		public T1 Item1;

		// Token: 0x0400019E RID: 414
		public T2 Item2;

		// Token: 0x0400019F RID: 415
		public T3 Item3;

		// Token: 0x040001A0 RID: 416
		public T4 Item4;
	}
}
