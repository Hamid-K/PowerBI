using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E1 RID: 993
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2> : IEquatable<Record<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2>>, IRecordInternal
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x00041490 File Offset: 0x0003F690
		public Record(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000414A0 File Offset: 0x0003F6A0
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2> && this.Equals((Record<T1, T2>)obj);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000414B8 File Offset: 0x0003F6B8
		public bool Equals(Record<T1, T2> other)
		{
			return Record<T1, T2>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2>.s_t2Comparer.Equals(this.Item2, other.Item2);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000414EC File Offset: 0x0003F6EC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2>))
			{
				return false;
			}
			Record<T1, T2> record = (Record<T1, T2>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00041549 File Offset: 0x0003F749
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2>)other);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00041574 File Offset: 0x0003F774
		public int CompareTo(Record<T1, T2> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T2>.Default.Compare(this.Item2, other.Item2);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000415B4 File Offset: 0x0003F7B4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2> record = (Record<T1, T2>)other;
			int num = comparer.Compare(this.Item1, record.Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item2, record.Item2);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00041623 File Offset: 0x0003F823
		public override int GetHashCode()
		{
			return Record.CombineHashCodes(Record<T1, T2>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2>.s_t2Comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0004164A File Offset: 0x0003F84A
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00041653 File Offset: 0x0003F853
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004164A File Offset: 0x0003F84A
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0004167C File Offset: 0x0003F87C
		public override string ToString()
		{
			string[] array = new string[5];
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
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0004171C File Offset: 0x0003F91C
		string IRecordInternal.ToStringEnd()
		{
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
					goto IL_0035;
				}
			}
			text = ptr.ToString();
			IL_0035:
			string text2 = ", ";
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text3;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text3 = null;
					goto IL_006F;
				}
			}
			text3 = ptr2.ToString();
			IL_006F:
			return text + text2 + text3 + ")";
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0001AFD7 File Offset: 0x000191D7
		int IRecordInternal.Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x04000AB1 RID: 2737
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000AB2 RID: 2738
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000AB3 RID: 2739
		public T1 Item1;

		// Token: 0x04000AB4 RID: 2740
		public T2 Item2;
	}
}
