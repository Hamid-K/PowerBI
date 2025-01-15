using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000076 RID: 118
	public abstract class TupleBase<TTuple, T1, T2> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2>
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000B18B File Offset: 0x0000938B
		protected TupleBase(T1 item1, T2 item2)
		{
			this._item1 = item1;
			this._item2 = item2;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B1A1 File Offset: 0x000093A1
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B1A8 File Offset: 0x000093A8
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B1AF File Offset: 0x000093AF
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000B1B7 File Offset: 0x000093B7
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B1BF File Offset: 0x000093BF
		public void Deconstruct(out T1 item1, out T2 item2)
		{
			item1 = this.Item1;
			item2 = this.Item2;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B1DC File Offset: 0x000093DC
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2>.Comparer2.Equals(this._item2, other._item2);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B22B File Offset: 0x0000942B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B240 File Offset: 0x00009440
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2>.Comparer2.GetHashCode(this._item2) : (-48879));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B29A File Offset: 0x0000949A
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1})", this._item1, this._item2);
		}

		// Token: 0x040000F1 RID: 241
		private readonly T1 _item1;

		// Token: 0x040000F2 RID: 242
		private readonly T2 _item2;
	}
}
