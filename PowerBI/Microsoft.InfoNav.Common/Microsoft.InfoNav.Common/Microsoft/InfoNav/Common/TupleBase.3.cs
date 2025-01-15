using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000078 RID: 120
	public abstract class TupleBase<TTuple, T1, T2, T3, T4> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2, T3, T4>
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x0000B457 File Offset: 0x00009657
		protected TupleBase(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this._item1 = item1;
			this._item2 = item2;
			this._item3 = item3;
			this._item4 = item4;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000B47C File Offset: 0x0000967C
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000B483 File Offset: 0x00009683
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000B48A File Offset: 0x0000968A
		private static EqualityComparer<T3> Comparer3
		{
			get
			{
				return EqualityComparer<T3>.Default;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000B491 File Offset: 0x00009691
		private static EqualityComparer<T4> Comparer4
		{
			get
			{
				return EqualityComparer<T4>.Default;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000B498 File Offset: 0x00009698
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000B4A0 File Offset: 0x000096A0
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000B4A8 File Offset: 0x000096A8
		protected T3 Item3
		{
			get
			{
				return this._item3;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000B4B0 File Offset: 0x000096B0
		protected T4 Item4
		{
			get
			{
				return this._item4;
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
			item4 = this.Item4;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000B4EC File Offset: 0x000096EC
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2, T3, T4>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2, T3, T4>.Comparer2.Equals(this._item2, other._item2) && TupleBase<TTuple, T1, T2, T3, T4>.Comparer3.Equals(this._item3, other._item3) && TupleBase<TTuple, T1, T2, T3, T4>.Comparer4.Equals(this._item4, other._item4);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000B575 File Offset: 0x00009775
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000B588 File Offset: 0x00009788
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2, T3, T4>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2, T3, T4>.Comparer2.GetHashCode(this._item2) : (-48879), (this._item3 != null) ? TupleBase<TTuple, T1, T2, T3, T4>.Comparer3.GetHashCode(this._item3) : (-48879), (this._item4 != null) ? TupleBase<TTuple, T1, T2, T3, T4>.Comparer4.GetHashCode(this._item4) : (-48879));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B62C File Offset: 0x0000982C
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1}, {2}, {3})", new object[] { this._item1, this._item2, this._item3, this._item4 });
		}

		// Token: 0x040000F6 RID: 246
		private readonly T1 _item1;

		// Token: 0x040000F7 RID: 247
		private readonly T2 _item2;

		// Token: 0x040000F8 RID: 248
		private readonly T3 _item3;

		// Token: 0x040000F9 RID: 249
		private readonly T4 _item4;
	}
}
