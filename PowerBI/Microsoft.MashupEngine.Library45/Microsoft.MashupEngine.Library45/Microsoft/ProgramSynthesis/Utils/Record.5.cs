using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E3 RID: 995
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3, T4> : IEquatable<Record<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3, T4>>, IRecordInternal
	{
		// Token: 0x0600166B RID: 5739 RVA: 0x00041C28 File Offset: 0x0003FE28
		public Record(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00041C47 File Offset: 0x0003FE47
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3, T4> && this.Equals((Record<T1, T2, T3, T4>)obj);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00041C60 File Offset: 0x0003FE60
		public bool Equals(Record<T1, T2, T3, T4> other)
		{
			return Record<T1, T2, T3, T4>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3, T4>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3, T4>.s_t3Comparer.Equals(this.Item3, other.Item3) && Record<T1, T2, T3, T4>.s_t4Comparer.Equals(this.Item4, other.Item4);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00041CD0 File Offset: 0x0003FED0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3, T4>))
			{
				return false;
			}
			Record<T1, T2, T3, T4> record = (Record<T1, T2, T3, T4>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3) && comparer.Equals(this.Item4, record.Item4);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00041D69 File Offset: 0x0003FF69
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3, T4>)other);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00041D94 File Offset: 0x0003FF94
		public int CompareTo(Record<T1, T2, T3, T4> other)
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

		// Token: 0x06001671 RID: 5745 RVA: 0x00041E0C File Offset: 0x0004000C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3, T4> record = (Record<T1, T2, T3, T4>)other;
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
			return comparer.Compare(this.Item4, record.Item4);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00041EC0 File Offset: 0x000400C0
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2, T3, T4>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4>.s_t4Comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00041F12 File Offset: 0x00040112
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00041F1C File Offset: 0x0004011C
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00041F12 File Offset: 0x00040112
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00041F74 File Offset: 0x00040174
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

		// Token: 0x06001677 RID: 5751 RVA: 0x00042098 File Offset: 0x00040298
		string IRecordInternal.ToStringEnd()
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0001B1B1 File Offset: 0x000193B1
		int IRecordInternal.Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x04000ABB RID: 2747
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000ABC RID: 2748
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000ABD RID: 2749
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000ABE RID: 2750
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000ABF RID: 2751
		public T1 Item1;

		// Token: 0x04000AC0 RID: 2752
		public T2 Item2;

		// Token: 0x04000AC1 RID: 2753
		public T3 Item3;

		// Token: 0x04000AC2 RID: 2754
		public T4 Item4;
	}
}
