using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E0 RID: 992
	public struct Record<T1> : IEquatable<Record<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<Record<T1>>, IRecordInternal
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x00041297 File Offset: 0x0003F497
		public Record(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000412A0 File Offset: 0x0003F4A0
		public override bool Equals(object obj)
		{
			return obj is Record<T1> && this.Equals((Record<T1>)obj);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000412B8 File Offset: 0x0003F4B8
		public bool Equals(Record<T1> other)
		{
			return Record<T1>.s_t1Comparer.Equals(this.Item1, other.Item1);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000412D0 File Offset: 0x0003F4D0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is Record<T1>))
			{
				return false;
			}
			Record<T1> record = (Record<T1>)other;
			return comparer.Equals(this.Item1, record.Item1);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00041310 File Offset: 0x0003F510
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1> record = (Record<T1>)other;
			return Comparer<T1>.Default.Compare(this.Item1, record.Item1);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00041357 File Offset: 0x0003F557
		public int CompareTo(Record<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00041370 File Offset: 0x0003F570
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is Record<T1>))
			{
				throw new ArgumentException("The parameter should be a Record type of appropriate arity.", "other");
			}
			Record<T1> record = (Record<T1>)other;
			return comparer.Compare(this.Item1, record.Item1);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000413BD File Offset: 0x0003F5BD
		public override int GetHashCode()
		{
			return Record<T1>.s_t1Comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000413CF File Offset: 0x0003F5CF
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000413CF File Offset: 0x0003F5CF
		int IRecordInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000413E4 File Offset: 0x0003F5E4
		public override string ToString()
		{
			string text = "(";
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text2;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text2 = null;
					goto IL_003A;
				}
			}
			text2 = ptr.ToString();
			IL_003A:
			return text + text2 + ")";
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00041438 File Offset: 0x0003F638
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
			return text + ")";
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0000A5FD File Offset: 0x000087FD
		int IRecordInternal.Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000AAF RID: 2735
		private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000AB0 RID: 2736
		public T1 Item1;
	}
}
