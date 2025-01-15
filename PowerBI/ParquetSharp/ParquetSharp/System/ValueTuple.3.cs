using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Auto)]
	internal struct ValueTuple<T1, T2> : IEquatable<global::System.ValueTuple<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<global::System.ValueTuple<T1, T2>>, ITupleInternal
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x000146CC File Offset: 0x000128CC
		public ValueTuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000146DC File Offset: 0x000128DC
		public override bool Equals(object obj)
		{
			return obj is global::System.ValueTuple<T1, T2> && this.Equals((global::System.ValueTuple<T1, T2>)obj);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000146F8 File Offset: 0x000128F8
		public bool Equals(global::System.ValueTuple<T1, T2> other)
		{
			return global::System.ValueTuple<T1, T2>.s_t1Comparer.Equals(this.Item1, other.Item1) && global::System.ValueTuple<T1, T2>.s_t2Comparer.Equals(this.Item2, other.Item2);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00014730 File Offset: 0x00012930
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is global::System.ValueTuple<T1, T2>))
			{
				return false;
			}
			global::System.ValueTuple<T1, T2> valueTuple = (global::System.ValueTuple<T1, T2>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001479C File Offset: 0x0001299C
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			return this.CompareTo((global::System.ValueTuple<T1, T2>)other);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000147D0 File Offset: 0x000129D0
		public int CompareTo(global::System.ValueTuple<T1, T2> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T2>.Default.Compare(this.Item2, other.Item2);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014818 File Offset: 0x00012A18
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is global::System.ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleIncorrectType, "other");
			}
			global::System.ValueTuple<T1, T2> valueTuple = (global::System.ValueTuple<T1, T2>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item2, valueTuple.Item2);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00014894 File Offset: 0x00012A94
		public override int GetHashCode()
		{
			return global::System.ValueTuple.CombineHashCodes(global::System.ValueTuple<T1, T2>.s_t1Comparer.GetHashCode(this.Item1), global::System.ValueTuple<T1, T2>.s_t2Comparer.GetHashCode(this.Item2));
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000148BC File Offset: 0x00012ABC
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000148C8 File Offset: 0x00012AC8
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return global::System.ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000148F4 File Offset: 0x00012AF4
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014900 File Offset: 0x00012B00
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
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000149B8 File Offset: 0x00012BB8
		string ITupleInternal.ToStringEnd()
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
					goto IL_003E;
				}
			}
			text = ptr.ToString();
			IL_003E:
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
					goto IL_0081;
				}
			}
			text3 = ptr2.ToString();
			IL_0081:
			return text + text2 + text3 + ")";
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00014A54 File Offset: 0x00012C54
		int ITupleInternal.Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0400018F RID: 399
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000190 RID: 400
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000191 RID: 401
		public T1 Item1;

		// Token: 0x04000192 RID: 402
		public T2 Item2;
	}
}
