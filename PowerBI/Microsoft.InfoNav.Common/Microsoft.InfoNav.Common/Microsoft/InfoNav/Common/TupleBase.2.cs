using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000077 RID: 119
	public abstract class TupleBase<TTuple, T1, T2, T3> : IEquatable<TTuple> where TTuple : TupleBase<TTuple, T1, T2, T3>
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000B2BC File Offset: 0x000094BC
		protected TupleBase(T1 item1, T2 item2, T3 item3)
		{
			this._item1 = item1;
			this._item2 = item2;
			this._item3 = item3;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B2D9 File Offset: 0x000094D9
		private static EqualityComparer<T1> Comparer1
		{
			get
			{
				return EqualityComparer<T1>.Default;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000B2E0 File Offset: 0x000094E0
		private static EqualityComparer<T2> Comparer2
		{
			get
			{
				return EqualityComparer<T2>.Default;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000B2E7 File Offset: 0x000094E7
		private static EqualityComparer<T3> Comparer3
		{
			get
			{
				return EqualityComparer<T3>.Default;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000B2EE File Offset: 0x000094EE
		protected T1 Item1
		{
			get
			{
				return this._item1;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000B2F6 File Offset: 0x000094F6
		protected T2 Item2
		{
			get
			{
				return this._item2;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000B2FE File Offset: 0x000094FE
		protected T3 Item3
		{
			get
			{
				return this._item3;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000B306 File Offset: 0x00009506
		public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
		{
			item1 = this.Item1;
			item2 = this.Item2;
			item3 = this.Item3;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000B32C File Offset: 0x0000952C
		public bool Equals(TTuple other)
		{
			return other != null && TupleBase<TTuple, T1, T2, T3>.Comparer1.Equals(this._item1, other._item1) && TupleBase<TTuple, T1, T2, T3>.Comparer2.Equals(this._item2, other._item2) && TupleBase<TTuple, T1, T2, T3>.Comparer3.Equals(this._item3, other._item3);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000B398 File Offset: 0x00009598
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TTuple);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000B3AC File Offset: 0x000095AC
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this._item1 != null) ? TupleBase<TTuple, T1, T2, T3>.Comparer1.GetHashCode(this._item1) : (-48879), (this._item2 != null) ? TupleBase<TTuple, T1, T2, T3>.Comparer2.GetHashCode(this._item2) : (-48879), (this._item3 != null) ? TupleBase<TTuple, T1, T2, T3>.Comparer3.GetHashCode(this._item3) : (-48879));
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000B42A File Offset: 0x0000962A
		public override string ToString()
		{
			return StringUtil.FormatInvariant("({0}, {1}, {2})", this._item1, this._item2, this._item3);
		}

		// Token: 0x040000F3 RID: 243
		private readonly T1 _item1;

		// Token: 0x040000F4 RID: 244
		private readonly T2 _item2;

		// Token: 0x040000F5 RID: 245
		private readonly T3 _item3;
	}
}
