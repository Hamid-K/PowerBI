using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E6 RID: 998
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Record<T1, T2, T3, T4, T5, T6, T7>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3, T4, T5, T6, T7>>, IRecordInternal
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x000430D4 File Offset: 0x000412D4
		public Record(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0004310B File Offset: 0x0004130B
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3, T4, T5, T6, T7> && this.Equals((Record<T1, T2, T3, T4, T5, T6, T7>)obj);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00043124 File Offset: 0x00041324
		public bool Equals(Record<T1, T2, T3, T4, T5, T6, T7> other)
		{
			return Record<T1, T2, T3, T4, T5, T6, T7>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t3Comparer.Equals(this.Item3, other.Item3) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t4Comparer.Equals(this.Item4, other.Item4) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t5Comparer.Equals(this.Item5, other.Item5) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t6Comparer.Equals(this.Item6, other.Item6) && Record<T1, T2, T3, T4, T5, T6, T7>.s_t7Comparer.Equals(this.Item7, other.Item7);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000431DC File Offset: 0x000413DC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3, T4, T5, T6, T7>))
			{
				return false;
			}
			Record<T1, T2, T3, T4, T5, T6, T7> record = (Record<T1, T2, T3, T4, T5, T6, T7>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3) && comparer.Equals(this.Item4, record.Item4) && comparer.Equals(this.Item5, record.Item5) && comparer.Equals(this.Item6, record.Item6) && comparer.Equals(this.Item7, record.Item7);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000432D5 File Offset: 0x000414D5
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6, T7>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3, T4, T5, T6, T7>)other);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00043300 File Offset: 0x00041500
		public int CompareTo(Record<T1, T2, T3, T4, T5, T6, T7> other)
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
			num = Comparer<T6>.Default.Compare(this.Item6, other.Item6);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T7>.Default.Compare(this.Item7, other.Item7);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000433CC File Offset: 0x000415CC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6, T7>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3, T4, T5, T6, T7> record = (Record<T1, T2, T3, T4, T5, T6, T7>)other;
			int num = comparer.Compare(this.Item1, record.Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item2, record.Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item3, record.Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item4, record.Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item5, record.Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item6, record.Item6);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item7, record.Item7);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000434E8 File Offset: 0x000416E8
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4, T5, T6, T7>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5, T6, T7>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6, T7>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7>.s_t7Comparer.GetHashCode(this.Item7));
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0004356A File Offset: 0x0004176A
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00043574 File Offset: 0x00041774
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7));
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0004356A File Offset: 0x0004176A
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00043600 File Offset: 0x00041800
		public override string ToString()
		{
			string[] array = new string[15];
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
					goto IL_014A;
				}
			}
			text5 = ptr5.ToString();
			IL_014A:
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
					goto IL_018F;
				}
			}
			text6 = ptr6.ToString();
			IL_018F:
			array[num6] = text6;
			array[12] = ", ";
			int num7 = 13;
			ref T7 ptr7 = ref this.Item7;
			T7 t7 = default(T7);
			string text7;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr7 = ref t7;
				if (t7 == null)
				{
					text7 = null;
					goto IL_01D4;
				}
			}
			text7 = ptr7.ToString();
			IL_01D4:
			array[num7] = text7;
			array[14] = ")";
			return string.Concat(array);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000437F0 File Offset: 0x000419F0
		string IRecordInternal.ToStringEnd()
		{
			string[] array = new string[14];
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
					goto IL_003E;
				}
			}
			text = ptr.ToString();
			IL_003E:
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
					goto IL_007E;
				}
			}
			text2 = ptr2.ToString();
			IL_007E:
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
					goto IL_00BE;
				}
			}
			text3 = ptr3.ToString();
			IL_00BE:
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
					goto IL_00FE;
				}
			}
			text4 = ptr4.ToString();
			IL_00FE:
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
					goto IL_0141;
				}
			}
			text5 = ptr5.ToString();
			IL_0141:
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
					goto IL_0186;
				}
			}
			text6 = ptr6.ToString();
			IL_0186:
			array[num6] = text6;
			array[11] = ", ";
			int num7 = 12;
			ref T7 ptr7 = ref this.Item7;
			T7 t7 = default(T7);
			string text7;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr7 = ref t7;
				if (t7 == null)
				{
					text7 = null;
					goto IL_01CB;
				}
			}
			text7 = ptr7.ToString();
			IL_01CB:
			array[num7] = text7;
			array[13] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0001B319 File Offset: 0x00019519
		int IRecordInternal.Size
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x04000AD9 RID: 2777
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000ADA RID: 2778
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000ADB RID: 2779
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000ADC RID: 2780
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000ADD RID: 2781
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x04000ADE RID: 2782
		private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;

		// Token: 0x04000ADF RID: 2783
		private static readonly EqualityComparer<T7> s_t7Comparer = EqualityComparer<T7>.Default;

		// Token: 0x04000AE0 RID: 2784
		public T1 Item1;

		// Token: 0x04000AE1 RID: 2785
		public T2 Item2;

		// Token: 0x04000AE2 RID: 2786
		public T3 Item3;

		// Token: 0x04000AE3 RID: 2787
		public T4 Item4;

		// Token: 0x04000AE4 RID: 2788
		public T5 Item5;

		// Token: 0x04000AE5 RID: 2789
		public T6 Item6;

		// Token: 0x04000AE6 RID: 2790
		public T7 Item7;
	}
}
