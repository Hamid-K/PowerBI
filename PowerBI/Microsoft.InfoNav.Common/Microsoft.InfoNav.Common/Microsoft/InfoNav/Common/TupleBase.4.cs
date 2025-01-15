using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000079 RID: 121
	public abstract class TupleBase<TTuple, T1, T2, T3, T4, T5> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2, T3, T4, T5>
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0000B681 File Offset: 0x00009881
		protected TupleBase(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this._item1 = item1;
			this._item2 = item2;
			this._item3 = item3;
			this._item4 = item4;
			this._item5 = item5;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000B6AE File Offset: 0x000098AE
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000B6B5 File Offset: 0x000098B5
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000B6BC File Offset: 0x000098BC
		private static EqualityComparer<T3> Comparer3
		{
			get
			{
				return EqualityComparer<T3>.Default;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B6C3 File Offset: 0x000098C3
		private static EqualityComparer<T4> Comparer4
		{
			get
			{
				return EqualityComparer<T4>.Default;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000B6CA File Offset: 0x000098CA
		private static EqualityComparer<T5> Comparer5
		{
			get
			{
				return EqualityComparer<T5>.Default;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B6D1 File Offset: 0x000098D1
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000B6D9 File Offset: 0x000098D9
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000B6E1 File Offset: 0x000098E1
		protected T3 Item3
		{
			get
			{
				return this._item3;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B6E9 File Offset: 0x000098E9
		protected T4 Item4
		{
			get
			{
				return this._item4;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000B6F1 File Offset: 0x000098F1
		protected T5 Item5
		{
			get
			{
				return this._item5;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B6F9 File Offset: 0x000098F9
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
			item4 = this.Item4;
			item5 = this.Item5;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B73C File Offset: 0x0000993C
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer2.Equals(this._item2, other._item2) && TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer3.Equals(this._item3, other._item3) && TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer4.Equals(this._item4, other._item4) && TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer5.Equals(this._item5, other._item5);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000B7E5 File Offset: 0x000099E5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer2.GetHashCode(this._item2) : (-48879), (this._item3 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer3.GetHashCode(this._item3) : (-48879), (this._item4 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer4.GetHashCode(this._item4) : (-48879), (this._item5 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5>.Comparer5.GetHashCode(this._item5) : (-48879));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1}, {2}, {3}, {4})", new object[] { this._item1, this._item2, this._item3, this._item4, this._item5 });
		}

		// Token: 0x040000FA RID: 250
		private readonly T1 _item1;

		// Token: 0x040000FB RID: 251
		private readonly T2 _item2;

		// Token: 0x040000FC RID: 252
		private readonly T3 _item3;

		// Token: 0x040000FD RID: 253
		private readonly T4 _item4;

		// Token: 0x040000FE RID: 254
		private readonly T5 _item5;
	}
}
