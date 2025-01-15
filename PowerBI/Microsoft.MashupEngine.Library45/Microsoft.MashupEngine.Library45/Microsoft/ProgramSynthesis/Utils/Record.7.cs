using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E5 RID: 997
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3, T4, T5, T6> : IEquatable<Record<T1, T2, T3, T4, T5, T6>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3, T4, T5, T6>>, IRecordInternal
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x000428C1 File Offset: 0x00040AC1
		public Record(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000428F0 File Offset: 0x00040AF0
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3, T4, T5, T6> && this.Equals((Record<T1, T2, T3, T4, T5, T6>)obj);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00042908 File Offset: 0x00040B08
		public bool Equals(Record<T1, T2, T3, T4, T5, T6> other)
		{
			return Record<T1, T2, T3, T4, T5, T6>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3, T4, T5, T6>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3, T4, T5, T6>.s_t3Comparer.Equals(this.Item3, other.Item3) && Record<T1, T2, T3, T4, T5, T6>.s_t4Comparer.Equals(this.Item4, other.Item4) && Record<T1, T2, T3, T4, T5, T6>.s_t5Comparer.Equals(this.Item5, other.Item5) && Record<T1, T2, T3, T4, T5, T6>.s_t6Comparer.Equals(this.Item6, other.Item6);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000429A8 File Offset: 0x00040BA8
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3, T4, T5, T6>))
			{
				return false;
			}
			Record<T1, T2, T3, T4, T5, T6> record = (Record<T1, T2, T3, T4, T5, T6>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3) && comparer.Equals(this.Item4, record.Item4) && comparer.Equals(this.Item5, record.Item5) && comparer.Equals(this.Item6, record.Item6);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00042A80 File Offset: 0x00040C80
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3, T4, T5, T6>)other);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00042AAC File Offset: 0x00040CAC
		public int CompareTo(Record<T1, T2, T3, T4, T5, T6> other)
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

		// Token: 0x0600168F RID: 5775 RVA: 0x00042B5C File Offset: 0x00040D5C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3, T4, T5, T6> record = (Record<T1, T2, T3, T4, T5, T6>)other;
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
			return comparer.Compare(this.Item6, record.Item6);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00042C54 File Offset: 0x00040E54
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4, T5, T6>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5, T6>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6>.s_t6Comparer.GetHashCode(this.Item6));
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00042CC6 File Offset: 0x00040EC6
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00042CD0 File Offset: 0x00040ED0
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6));
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00042CC6 File Offset: 0x00040EC6
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00042D48 File Offset: 0x00040F48
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
			array[12] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00042EF4 File Offset: 0x000410F4
		string IRecordInternal.ToStringEnd()
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
			array[11] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0001B291 File Offset: 0x00019491
		int IRecordInternal.Size
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x04000ACD RID: 2765
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000ACE RID: 2766
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000ACF RID: 2767
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000AD0 RID: 2768
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000AD1 RID: 2769
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x04000AD2 RID: 2770
		private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;

		// Token: 0x04000AD3 RID: 2771
		public T1 Item1;

		// Token: 0x04000AD4 RID: 2772
		public T2 Item2;

		// Token: 0x04000AD5 RID: 2773
		public T3 Item3;

		// Token: 0x04000AD6 RID: 2774
		public T4 Item4;

		// Token: 0x04000AD7 RID: 2775
		public T5 Item5;

		// Token: 0x04000AD8 RID: 2776
		public T6 Item6;
	}
}
