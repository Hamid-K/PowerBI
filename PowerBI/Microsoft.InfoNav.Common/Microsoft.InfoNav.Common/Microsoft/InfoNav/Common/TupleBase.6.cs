using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007B RID: 123
	public abstract class TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0000BC45 File Offset: 0x00009E45
		protected TupleBase(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this._item1 = item1;
			this._item2 = item2;
			this._item3 = item3;
			this._item4 = item4;
			this._item5 = item5;
			this._item6 = item6;
			this._item7 = item7;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000BC82 File Offset: 0x00009E82
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000BC89 File Offset: 0x00009E89
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000BC90 File Offset: 0x00009E90
		private static EqualityComparer<T3> Comparer3
		{
			get
			{
				return EqualityComparer<T3>.Default;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000BC97 File Offset: 0x00009E97
		private static EqualityComparer<T4> Comparer4
		{
			get
			{
				return EqualityComparer<T4>.Default;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000BC9E File Offset: 0x00009E9E
		private static EqualityComparer<T5> Comparer5
		{
			get
			{
				return EqualityComparer<T5>.Default;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		private static EqualityComparer<T6> Comparer6
		{
			get
			{
				return EqualityComparer<T6>.Default;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000BCAC File Offset: 0x00009EAC
		private static EqualityComparer<T7> Comparer7
		{
			get
			{
				return EqualityComparer<T7>.Default;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000BCB3 File Offset: 0x00009EB3
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000BCBB File Offset: 0x00009EBB
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000BCC3 File Offset: 0x00009EC3
		protected T3 Item3
		{
			get
			{
				return this._item3;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000BCCB File Offset: 0x00009ECB
		protected T4 Item4
		{
			get
			{
				return this._item4;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000BCD3 File Offset: 0x00009ED3
		protected T5 Item5
		{
			get
			{
				return this._item5;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000BCDB File Offset: 0x00009EDB
		protected T6 Item6
		{
			get
			{
				return this._item6;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000BCE3 File Offset: 0x00009EE3
		protected T7 Item7
		{
			get
			{
				return this._item7;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
			item4 = this.Item4;
			item5 = this.Item5;
			item6 = this.Item6;
			item7 = this.Item7;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BD54 File Offset: 0x00009F54
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer2.Equals(this._item2, other._item2) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer3.Equals(this._item3, other._item3) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer4.Equals(this._item4, other._item4) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer5.Equals(this._item5, other._item5) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer6.Equals(this._item6, other._item6) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer7.Equals(this._item7, other._item7);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BE3D File Offset: 0x0000A03D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BE50 File Offset: 0x0000A050
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer2.GetHashCode(this._item2) : (-48879), (this._item3 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer3.GetHashCode(this._item3) : (-48879), (this._item4 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer4.GetHashCode(this._item4) : (-48879), (this._item5 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer5.GetHashCode(this._item5) : (-48879), (this._item6 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer6.GetHashCode(this._item6) : (-48879), (this._item7 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6, T7>.Comparer7.GetHashCode(this._item7) : (-48879));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BF60 File Offset: 0x0000A160
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1}, {2}, {3}, {4}, {5}, {6})", new object[] { this._item1, this._item2, this._item3, this._item4, this._item5, this._item6, this._item7 });
		}

		// Token: 0x04000105 RID: 261
		private readonly T1 _item1;

		// Token: 0x04000106 RID: 262
		private readonly T2 _item2;

		// Token: 0x04000107 RID: 263
		private readonly T3 _item3;

		// Token: 0x04000108 RID: 264
		private readonly T4 _item4;

		// Token: 0x04000109 RID: 265
		private readonly T5 _item5;

		// Token: 0x0400010A RID: 266
		private readonly T6 _item6;

		// Token: 0x0400010B RID: 267
		private readonly T7 _item7;
	}
}
