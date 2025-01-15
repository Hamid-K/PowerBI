using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007A RID: 122
	public abstract class TupleBase<TTuple, T1, T2, T3, T4, T5, T6> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2, T3, T4, T5, T6>
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000B923 File Offset: 0x00009B23
		protected TupleBase(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this._item1 = item1;
			this._item2 = item2;
			this._item3 = item3;
			this._item4 = item4;
			this._item5 = item5;
			this._item6 = item6;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000B958 File Offset: 0x00009B58
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000B95F File Offset: 0x00009B5F
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B966 File Offset: 0x00009B66
		private static EqualityComparer<T3> Comparer3
		{
			get
			{
				return EqualityComparer<T3>.Default;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000B96D File Offset: 0x00009B6D
		private static EqualityComparer<T4> Comparer4
		{
			get
			{
				return EqualityComparer<T4>.Default;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000B974 File Offset: 0x00009B74
		private static EqualityComparer<T5> Comparer5
		{
			get
			{
				return EqualityComparer<T5>.Default;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000B97B File Offset: 0x00009B7B
		private static EqualityComparer<T6> Comparer6
		{
			get
			{
				return EqualityComparer<T6>.Default;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000B982 File Offset: 0x00009B82
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000B98A File Offset: 0x00009B8A
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000B992 File Offset: 0x00009B92
		protected T3 Item3
		{
			get
			{
				return this._item3;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000B99A File Offset: 0x00009B9A
		protected T4 Item4
		{
			get
			{
				return this._item4;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000B9A2 File Offset: 0x00009BA2
		protected T5 Item5
		{
			get
			{
				return this._item5;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000B9AA File Offset: 0x00009BAA
		protected T6 Item6
		{
			get
			{
				return this._item6;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
			item4 = this.Item4;
			item5 = this.Item5;
			item6 = this.Item6;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer2.Equals(this._item2, other._item2) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer3.Equals(this._item3, other._item3) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer4.Equals(this._item4, other._item4) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer5.Equals(this._item5, other._item5) && TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer6.Equals(this._item6, other._item6);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000BAD5 File Offset: 0x00009CD5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer2.GetHashCode(this._item2) : (-48879), (this._item3 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer3.GetHashCode(this._item3) : (-48879), (this._item4 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer4.GetHashCode(this._item4) : (-48879), (this._item5 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer5.GetHashCode(this._item5) : (-48879), (this._item6 != null) ? TupleBase<TTuple, T1, T2, T3, T4, T5, T6>.Comparer6.GetHashCode(this._item6) : (-48879));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1}, {2}, {3}, {4}, {5})", new object[] { this._item1, this._item2, this._item3, this._item4, this._item5, this._item6 });
		}

		// Token: 0x040000FF RID: 255
		private readonly T1 _item1;

		// Token: 0x04000100 RID: 256
		private readonly T2 _item2;

		// Token: 0x04000101 RID: 257
		private readonly T3 _item3;

		// Token: 0x04000102 RID: 258
		private readonly T4 _item4;

		// Token: 0x04000103 RID: 259
		private readonly T5 _item5;

		// Token: 0x04000104 RID: 260
		private readonly T6 _item6;
	}
}
