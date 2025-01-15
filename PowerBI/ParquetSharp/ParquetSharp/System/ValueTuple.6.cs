using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B6 RID: 182
	[StructLayout(LayoutKind.Auto)]
	internal struct ValueTuple<T1, T2, T3, T4, T5> : IEquatable<global::System.ValueTuple<T1, T2, T3, T4, T5>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3, T4, T5>>, ITupleInternal
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00015604 File Offset: 0x00013804
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001562C File Offset: 0x0001382C
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3, T4, T5> && this.Equals((global::System.ValueTuple<T1, T2, T3, T4, T5>)obj);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00015648 File Offset: 0x00013848
		public bool Equals(global::System.ValueTuple<T1, T2, T3, T4, T5> other)
		{
			return global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t3Comparer.Equals(this.Item3, other.Item3) && global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t4Comparer.Equals(this.Item4, other.Item4) && global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t5Comparer.Equals(this.Item5, other.Item5);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000156E0 File Offset: 0x000138E0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3, T4, T5>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3, T4, T5> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4, T5>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000157B0 File Offset: 0x000139B0
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4, T5>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2, T3, T4, T5>)other);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000157E4 File Offset: 0x000139E4
		public int CompareTo(global::System.ValueTuple<T1, T2, T3, T4, T5> other)
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
			num = Comparer<T4>.Default.Compare(this.Item4, other.Item4);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T5>.Default.Compare(this.Item5, other.Item5);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015888 File Offset: 0x00013A88
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4, T5>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2, T3, T4, T5> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4, T5>)other;
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
			num = comparer.Compare(this.Item4, valueTuple.Item4);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item5, valueTuple.Item5);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00015974 File Offset: 0x00013B74
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t3Comparer.GetHashCode(this.Item3), global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t4Comparer.GetHashCode(this.Item4), global::System.ValueTuple<T1, T2, T3, T4, T5>.s_t5Comparer.GetHashCode(this.Item5));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000159DC File Offset: 0x00013BDC
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000159E8 File Offset: 0x00013BE8
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5));
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015A54 File Offset: 0x00013C54
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015A60 File Offset: 0x00013C60
		public override string ToString()
		{
			string[] array = new string[11];
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
			array[8] = ", ";
			int num5 = 9;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_0177;
				}
			}
			text5 = ptr5.ToString();
			IL_0177:
			array[num5] = text5;
			array[10] = ")";
			return string.Concat(array);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015BF8 File Offset: 0x00013DF8
		string ITupleInternal.ToStringEnd()
		{
			string[] array = new string[10];
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
					goto IL_0047;
				}
			}
			text = ptr.ToString();
			IL_0047:
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
					goto IL_0090;
				}
			}
			text2 = ptr2.ToString();
			IL_0090:
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
					goto IL_00D9;
				}
			}
			text3 = ptr3.ToString();
			IL_00D9:
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
					goto IL_0122;
				}
			}
			text4 = ptr4.ToString();
			IL_0122:
			array[num4] = text4;
			array[7] = ", ";
			int num5 = 8;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_016E;
				}
			}
			text5 = ptr5.ToString();
			IL_016E:
			array[num5] = text5;
			array[9] = ")";
			return string.Concat(array);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00015D88 File Offset: 0x00013F88
		int ITupleInternal.Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x040001A1 RID: 417
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x040001A2 RID: 418
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x040001A3 RID: 419
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x040001A4 RID: 420
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x040001A5 RID: 421
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x040001A6 RID: 422
		public T1 Item1;

		// Token: 0x040001A7 RID: 423
		public T2 Item2;

		// Token: 0x040001A8 RID: 424
		public T3 Item3;

		// Token: 0x040001A9 RID: 425
		public T4 Item4;

		// Token: 0x040001AA RID: 426
		public T5 Item5;
	}
}
