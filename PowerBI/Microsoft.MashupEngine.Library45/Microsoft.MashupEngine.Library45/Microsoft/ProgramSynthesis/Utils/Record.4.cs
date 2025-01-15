using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E2 RID: 994
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3> : IEquatable<Record<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3>>, IRecordInternal
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x000417B8 File Offset: 0x0003F9B8
		public Record(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000417CF File Offset: 0x0003F9CF
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3> && this.Equals((Record<T1, T2, T3>)obj);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000417E8 File Offset: 0x0003F9E8
		public bool Equals(Record<T1, T2, T3> other)
		{
			return Record<T1, T2, T3>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3>.s_t3Comparer.Equals(this.Item3, other.Item3);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00041840 File Offset: 0x0003FA40
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3>))
			{
				return false;
			}
			Record<T1, T2, T3> record = (Record<T1, T2, T3>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000418BB File Offset: 0x0003FABB
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3>)other);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000418E8 File Offset: 0x0003FAE8
		public int CompareTo(Record<T1, T2, T3> other)
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

		// Token: 0x06001662 RID: 5730 RVA: 0x00041944 File Offset: 0x0003FB44
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3> record = (Record<T1, T2, T3>)other;
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
			return comparer.Compare(this.Item3, record.Item3);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000419D5 File Offset: 0x0003FBD5
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2, T3>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3>.s_t3Comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00041A0C File Offset: 0x0003FC0C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00041A15 File Offset: 0x0003FC15
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00041A0C File Offset: 0x0003FC0C
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00041A50 File Offset: 0x0003FC50
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

		// Token: 0x06001668 RID: 5736 RVA: 0x00041B30 File Offset: 0x0003FD30
		string IRecordInternal.ToStringEnd()
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0001B159 File Offset: 0x00019359
		int IRecordInternal.Size
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x04000AB5 RID: 2741
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000AB6 RID: 2742
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000AB7 RID: 2743
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000AB8 RID: 2744
		public T1 Item1;

		// Token: 0x04000AB9 RID: 2745
		public T2 Item2;

		// Token: 0x04000ABA RID: 2746
		public T3 Item3;
	}
}
