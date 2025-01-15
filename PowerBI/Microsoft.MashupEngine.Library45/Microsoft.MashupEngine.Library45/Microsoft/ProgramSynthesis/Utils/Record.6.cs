using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E4 RID: 996
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3, T4, T5> : IEquatable<Record<T1, T2, T3, T4, T5>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3, T4, T5>>, IRecordInternal
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x000421DA File Offset: 0x000403DA
		public Record(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00042201 File Offset: 0x00040401
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3, T4, T5> && this.Equals((Record<T1, T2, T3, T4, T5>)obj);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0004221C File Offset: 0x0004041C
		public bool Equals(Record<T1, T2, T3, T4, T5> other)
		{
			return Record<T1, T2, T3, T4, T5>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3, T4, T5>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3, T4, T5>.s_t3Comparer.Equals(this.Item3, other.Item3) && Record<T1, T2, T3, T4, T5>.s_t4Comparer.Equals(this.Item4, other.Item4) && Record<T1, T2, T3, T4, T5>.s_t5Comparer.Equals(this.Item5, other.Item5);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000422A4 File Offset: 0x000404A4
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3, T4, T5>))
			{
				return false;
			}
			Record<T1, T2, T3, T4, T5> record = (Record<T1, T2, T3, T4, T5>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3) && comparer.Equals(this.Item4, record.Item4) && comparer.Equals(this.Item5, record.Item5);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0004235B File Offset: 0x0004055B
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3, T4, T5>)other);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00042388 File Offset: 0x00040588
		public int CompareTo(Record<T1, T2, T3, T4, T5> other)
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

		// Token: 0x06001680 RID: 5760 RVA: 0x0004241C File Offset: 0x0004061C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3, T4, T5> record = (Record<T1, T2, T3, T4, T5>)other;
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
			return comparer.Compare(this.Item5, record.Item5);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000424F4 File Offset: 0x000406F4
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4, T5>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5>.s_t5Comparer.GetHashCode(this.Item5));
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00042556 File Offset: 0x00040756
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00042560 File Offset: 0x00040760
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5));
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00042556 File Offset: 0x00040756
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000425C8 File Offset: 0x000407C8
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
			array[10] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00042730 File Offset: 0x00040930
		string IRecordInternal.ToStringEnd()
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
			array[9] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0001B224 File Offset: 0x00019424
		int IRecordInternal.Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x04000AC3 RID: 2755
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000AC4 RID: 2756
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000AC5 RID: 2757
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000AC6 RID: 2758
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000AC7 RID: 2759
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x04000AC8 RID: 2760
		public T1 Item1;

		// Token: 0x04000AC9 RID: 2761
		public T2 Item2;

		// Token: 0x04000ACA RID: 2762
		public T3 Item3;

		// Token: 0x04000ACB RID: 2763
		public T4 Item4;

		// Token: 0x04000ACC RID: 2764
		public T5 Item5;
	}
}
