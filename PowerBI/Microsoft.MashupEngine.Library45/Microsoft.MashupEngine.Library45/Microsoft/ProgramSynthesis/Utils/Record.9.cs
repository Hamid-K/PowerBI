using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E7 RID: 999
	[StructLayout(LayoutKind.Auto)]
	public struct Record<T1, T2, T3, T4, T5, T6, T7, TRest> : IEquatable<Record<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1, T2, T3, T4, T5, T6, T7, TRest>>, IRecordInternal where TRest : struct
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x00043A2C File Offset: 0x00041C2C
		public Record(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is IRecordInternal))
			{
				throw new ArgumentException("The TRest type argument of Record`8 must be a Record.");
			}
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
			this.Rest = rest;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00043A8F File Offset: 0x00041C8F
		public override bool Equals(object obj)
		{
			return obj is Record<T1, T2, T3, T4, T5, T6, T7, TRest> && this.Equals((Record<T1, T2, T3, T4, T5, T6, T7, TRest>)obj);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00043AA8 File Offset: 0x00041CA8
		public bool Equals(Record<T1, T2, T3, T4, T5, T6, T7, TRest> other)
		{
			return Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t1Comparer.Equals(this.Item1, other.Item1) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t2Comparer.Equals(this.Item2, other.Item2) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t3Comparer.Equals(this.Item3, other.Item3) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.Equals(this.Item4, other.Item4) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.Equals(this.Item5, other.Item5) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.Equals(this.Item6, other.Item6) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.Equals(this.Item7, other.Item7) && Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_tRestComparer.Equals(this.Rest, other.Rest);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00043B7C File Offset: 0x00041D7C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				return false;
			}
			Record<T1, T2, T3, T4, T5, T6, T7, TRest> record = (Record<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
			return comparer.Equals(this.Item1, record.Item1) && comparer.Equals(this.Item2, record.Item2) && comparer.Equals(this.Item3, record.Item3) && comparer.Equals(this.Item4, record.Item4) && comparer.Equals(this.Item5, record.Item5) && comparer.Equals(this.Item6, record.Item6) && comparer.Equals(this.Item7, record.Item7) && comparer.Equals(this.Rest, record.Rest);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00043C96 File Offset: 0x00041E96
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			return this.CompareTo((Record<T1, T2, T3, T4, T5, T6, T7, TRest>)other);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00043CC4 File Offset: 0x00041EC4
		public int CompareTo(Record<T1, T2, T3, T4, T5, T6, T7, TRest> other)
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
			num = Comparer<T7>.Default.Compare(this.Item7, other.Item7);
			if (num != 0)
			{
				return num;
			}
			return Comparer<TRest>.Default.Compare(this.Rest, other.Rest);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00043DAC File Offset: 0x00041FAC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1, T2, T3, T4, T5, T6, T7, TRest> record = (Record<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
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
			num = comparer.Compare(this.Item7, record.Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Rest, record.Rest);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00043EE8 File Offset: 0x000420E8
		public override int GetHashCode()
		{
			IRecordInternal recordInternal = this.Rest as IRecordInternal;
			if (recordInternal == null)
			{
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7));
			}
			int size = recordInternal.Size;
			if (size >= 8)
			{
				return recordInternal.GetHashCode();
			}
			switch (8 - size)
			{
			case 1:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 2:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 3:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 4:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 5:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 6:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			case 7:
			case 8:
				return Record.CombineHashCodes(Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t1Comparer.GetHashCode(this.Item1), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t2Comparer.GetHashCode(this.Item2), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t3Comparer.GetHashCode(this.Item3), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t4Comparer.GetHashCode(this.Item4), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t5Comparer.GetHashCode(this.Item5), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t6Comparer.GetHashCode(this.Item6), Record<T1, T2, T3, T4, T5, T6, T7, TRest>.s_t7Comparer.GetHashCode(this.Item7), recordInternal.GetHashCode());
			default:
				return -1;
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000441D7 File Offset: 0x000423D7
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000441E0 File Offset: 0x000423E0
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			IRecordInternal recordInternal = this.Rest as IRecordInternal;
			if (recordInternal == null)
			{
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7));
			}
			int size = recordInternal.Size;
			if (size >= 8)
			{
				return recordInternal.GetHashCode(comparer);
			}
			switch (8 - size)
			{
			case 1:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 2:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 3:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 4:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 5:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 6:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			case 7:
			case 8:
				return Record.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), recordInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000441D7 File Offset: 0x000423D7
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000444FC File Offset: 0x000426FC
		public override string ToString()
		{
			IRecordInternal recordInternal = this.Rest as IRecordInternal;
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (recordInternal == null)
			{
				string[] array = new string[17];
				array[0] = "(";
				int num = 1;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_005D;
					}
				}
				text = ptr.ToString();
				IL_005D:
				array[num] = text;
				array[2] = ", ";
				int num2 = 3;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_009D;
					}
				}
				text2 = ptr2.ToString();
				IL_009D:
				array[num2] = text2;
				array[4] = ", ";
				int num3 = 5;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_00DD;
					}
				}
				text3 = ptr3.ToString();
				IL_00DD:
				array[num3] = text3;
				array[6] = ", ";
				int num4 = 7;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_0120;
					}
				}
				text4 = ptr4.ToString();
				IL_0120:
				array[num4] = text4;
				array[8] = ", ";
				int num5 = 9;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_0164;
					}
				}
				text5 = ptr5.ToString();
				IL_0164:
				array[num5] = text5;
				array[10] = ", ";
				int num6 = 11;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_01A9;
					}
				}
				text6 = ptr6.ToString();
				IL_01A9:
				array[num6] = text6;
				array[12] = ", ";
				int num7 = 13;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_01EE;
					}
				}
				text7 = ptr7.ToString();
				IL_01EE:
				array[num7] = text7;
				array[14] = ", ";
				array[15] = this.Rest.ToString();
				array[16] = ")";
				return string.Concat(array);
			}
			string[] array2 = new string[16];
			array2[0] = "(";
			int num8 = 1;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_0262;
				}
			}
			text8 = ptr8.ToString();
			IL_0262:
			array2[num8] = text8;
			array2[2] = ", ";
			int num9 = 3;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_02A2;
				}
			}
			text9 = ptr9.ToString();
			IL_02A2:
			array2[num9] = text9;
			array2[4] = ", ";
			int num10 = 5;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_02E2;
				}
			}
			text10 = ptr10.ToString();
			IL_02E2:
			array2[num10] = text10;
			array2[6] = ", ";
			int num11 = 7;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_0325;
				}
			}
			text11 = ptr11.ToString();
			IL_0325:
			array2[num11] = text11;
			array2[8] = ", ";
			int num12 = 9;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_0369;
				}
			}
			text12 = ptr12.ToString();
			IL_0369:
			array2[num12] = text12;
			array2[10] = ", ";
			int num13 = 11;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_03AE;
				}
			}
			text13 = ptr13.ToString();
			IL_03AE:
			array2[num13] = text13;
			array2[12] = ", ";
			int num14 = 13;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_03F3;
				}
			}
			text14 = ptr14.ToString();
			IL_03F3:
			array2[num14] = text14;
			array2[14] = ", ";
			array2[15] = recordInternal.ToStringEnd();
			return string.Concat(array2);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00044918 File Offset: 0x00042B18
		string IRecordInternal.ToStringEnd()
		{
			IRecordInternal recordInternal = this.Rest as IRecordInternal;
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (recordInternal == null)
			{
				string[] array = new string[16];
				int num = 0;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_0055;
					}
				}
				text = ptr.ToString();
				IL_0055:
				array[num] = text;
				array[1] = ", ";
				int num2 = 2;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_0095;
					}
				}
				text2 = ptr2.ToString();
				IL_0095:
				array[num2] = text2;
				array[3] = ", ";
				int num3 = 4;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_00D5;
					}
				}
				text3 = ptr3.ToString();
				IL_00D5:
				array[num3] = text3;
				array[5] = ", ";
				int num4 = 6;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_0118;
					}
				}
				text4 = ptr4.ToString();
				IL_0118:
				array[num4] = text4;
				array[7] = ", ";
				int num5 = 8;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_015B;
					}
				}
				text5 = ptr5.ToString();
				IL_015B:
				array[num5] = text5;
				array[9] = ", ";
				int num6 = 10;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_01A0;
					}
				}
				text6 = ptr6.ToString();
				IL_01A0:
				array[num6] = text6;
				array[11] = ", ";
				int num7 = 12;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_01E5;
					}
				}
				text7 = ptr7.ToString();
				IL_01E5:
				array[num7] = text7;
				array[13] = ", ";
				array[14] = this.Rest.ToString();
				array[15] = ")";
				return string.Concat(array);
			}
			string[] array2 = new string[15];
			int num8 = 0;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_0251;
				}
			}
			text8 = ptr8.ToString();
			IL_0251:
			array2[num8] = text8;
			array2[1] = ", ";
			int num9 = 2;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_0291;
				}
			}
			text9 = ptr9.ToString();
			IL_0291:
			array2[num9] = text9;
			array2[3] = ", ";
			int num10 = 4;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_02D1;
				}
			}
			text10 = ptr10.ToString();
			IL_02D1:
			array2[num10] = text10;
			array2[5] = ", ";
			int num11 = 6;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_0314;
				}
			}
			text11 = ptr11.ToString();
			IL_0314:
			array2[num11] = text11;
			array2[7] = ", ";
			int num12 = 8;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_0357;
				}
			}
			text12 = ptr12.ToString();
			IL_0357:
			array2[num12] = text12;
			array2[9] = ", ";
			int num13 = 10;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_039C;
				}
			}
			text13 = ptr13.ToString();
			IL_039C:
			array2[num13] = text13;
			array2[11] = ", ";
			int num14 = 12;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_03E1;
				}
			}
			text14 = ptr14.ToString();
			IL_03E1:
			array2[num14] = text14;
			array2[13] = ", ";
			array2[14] = recordInternal.ToStringEnd();
			return string.Concat(array2);
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00044D20 File Offset: 0x00042F20
		int IRecordInternal.Size
		{
			get
			{
				IRecordInternal recordInternal = this.Rest as IRecordInternal;
				if (recordInternal != null)
				{
					return 7 + recordInternal.Size;
				}
				return 8;
			}
		}

		// Token: 0x04000AE7 RID: 2791
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000AE8 RID: 2792
		private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04000AE9 RID: 2793
		private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04000AEA RID: 2794
		private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

		// Token: 0x04000AEB RID: 2795
		private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

		// Token: 0x04000AEC RID: 2796
		private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;

		// Token: 0x04000AED RID: 2797
		private static readonly EqualityComparer<T7> s_t7Comparer = EqualityComparer<T7>.Default;

		// Token: 0x04000AEE RID: 2798
		private static readonly EqualityComparer<TRest> s_tRestComparer = EqualityComparer<TRest>.Default;

		// Token: 0x04000AEF RID: 2799
		public T1 Item1;

		// Token: 0x04000AF0 RID: 2800
		public T2 Item2;

		// Token: 0x04000AF1 RID: 2801
		public T3 Item3;

		// Token: 0x04000AF2 RID: 2802
		public T4 Item4;

		// Token: 0x04000AF3 RID: 2803
		public T5 Item5;

		// Token: 0x04000AF4 RID: 2804
		public T6 Item6;

		// Token: 0x04000AF5 RID: 2805
		public T7 Item7;

		// Token: 0x04000AF6 RID: 2806
		public TRest Rest;
	}
}
