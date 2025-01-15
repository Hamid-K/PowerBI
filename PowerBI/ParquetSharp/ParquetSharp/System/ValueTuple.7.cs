using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Auto)]
	internal struct ValueTuple<T1, T2, T3, T4, T5, T6> : IEquatable<global::System.ValueTuple<T1, T2, T3, T4, T5, T6>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2, T3, T4, T5, T6>>, ITupleInternal
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00015DC0 File Offset: 0x00013FC0
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00015DF0 File Offset: 0x00013FF0
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2, T3, T4, T5, T6> && this.Equals((global::System.ValueTuple<T1, T2, T3, T4, T5, T6>)obj);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00015E0C File Offset: 0x0001400C
		public bool Equals(global::System.ValueTuple<T1, T2, T3, T4, T5, T6> other)
		{
			return global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t2Comparer.Equals(this.Item2, other.Item2) && global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t3Comparer.Equals(this.Item3, other.Item3) && global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t4Comparer.Equals(this.Item4, other.Item4) && global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t5Comparer.Equals(this.Item5, other.Item5) && global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t6Comparer.Equals(this.Item6, other.Item6);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00015EBC File Offset: 0x000140BC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2, T3, T4, T5, T6> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4, T5, T6>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5) && comparer.Equals(this.Item6, valueTuple.Item6);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00015FAC File Offset: 0x000141AC
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2, T3, T4, T5, T6>)other);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00015FE0 File Offset: 0x000141E0
		public int CompareTo(global::System.ValueTuple<T1, T2, T3, T4, T5, T6> other)
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
			num = Comparer<T5>.Default.Compare(this.Item5, other.Item5);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T6>.Default.Compare(this.Item6, other.Item6);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000160A4 File Offset: 0x000142A4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2, T3, T4, T5, T6> valueTuple = (global::System.ValueTuple<T1, T2, T3, T4, T5, T6>)other;
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
			num = comparer.Compare(this.Item5, valueTuple.Item5);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item6, valueTuple.Item6);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000161B4 File Offset: 0x000143B4
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t2Comparer.GetHashCode(this.Item2), global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t3Comparer.GetHashCode(this.Item3), global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t4Comparer.GetHashCode(this.Item4), global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t5Comparer.GetHashCode(this.Item5), global::System.ValueTuple<T1, T2, T3, T4, T5, T6>.s_t6Comparer.GetHashCode(this.Item6));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001622C File Offset: 0x0001442C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00016238 File Offset: 0x00014438
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000162B4 File Offset: 0x000144B4
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000162C0 File Offset: 0x000144C0
		public override string ToString()
		{
			string[] array = new string[13];
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
			array[10] = ", ";
			int num6 = 11;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_01C5;
				}
			}
			text6 = ptr6.ToString();
			IL_01C5:
			array[num6] = text6;
			array[12] = ")";
			return string.Concat(array);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000164A8 File Offset: 0x000146A8
		string ITupleInternal.ToStringEnd()
		{
			string[] array = new string[12];
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
			array[9] = ", ";
			int num6 = 10;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_01BC;
				}
			}
			text6 = ptr6.ToString();
			IL_01BC:
			array[num6] = text6;
			array[11] = ")";
			return string.Concat(array);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00016684 File Offset: 0x00014884
		int ITupleInternal.Size
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x040001AB RID: 427
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x040001AC RID: 428
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x040001AD RID: 429
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x040001AE RID: 430
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x040001AF RID: 431
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x040001B0 RID: 432
		private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;

		// Token: 0x040001B1 RID: 433
		public T1 Item1;

		// Token: 0x040001B2 RID: 434
		public T2 Item2;

		// Token: 0x040001B3 RID: 435
		public T3 Item3;

		// Token: 0x040001B4 RID: 436
		public T4 Item4;

		// Token: 0x040001B5 RID: 437
		public T5 Item5;

		// Token: 0x040001B6 RID: 438
		public T6 Item6;
	}
}
