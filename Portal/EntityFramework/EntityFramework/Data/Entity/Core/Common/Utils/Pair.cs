using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FB RID: 1531
	internal class Pair<TFirst, TSecond> : InternalBase
	{
		// Token: 0x06004AE2 RID: 19170 RVA: 0x00109667 File Offset: 0x00107867
		internal Pair(TFirst first, TSecond second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x0010967D File Offset: 0x0010787D
		internal TFirst First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x00109685 File Offset: 0x00107885
		internal TSecond Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00109690 File Offset: 0x00107890
		public override int GetHashCode()
		{
			TFirst tfirst = this.first;
			int num = tfirst.GetHashCode() << 5;
			TSecond tsecond = this.second;
			return num ^ tsecond.GetHashCode();
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x001096C8 File Offset: 0x001078C8
		public bool Equals(Pair<TFirst, TSecond> other)
		{
			TFirst tfirst = this.first;
			if (tfirst.Equals(other.first))
			{
				TSecond tsecond = this.second;
				return tsecond.Equals(other.second);
			}
			return false;
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00109718 File Offset: 0x00107918
		public override bool Equals(object other)
		{
			Pair<TFirst, TSecond> pair = other as Pair<TFirst, TSecond>;
			return pair != null && this.Equals(pair);
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x00109738 File Offset: 0x00107938
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("<");
			builder.Append(this.first);
			string text = ", ";
			TSecond tsecond = this.second;
			builder.Append(text + ((tsecond != null) ? tsecond.ToString() : null));
			builder.Append(">");
		}

		// Token: 0x04001A41 RID: 6721
		private readonly TFirst first;

		// Token: 0x04001A42 RID: 6722
		private readonly TSecond second;

		// Token: 0x02000C44 RID: 3140
		internal class PairComparer : IEqualityComparer<Pair<TFirst, TSecond>>
		{
			// Token: 0x06006A3A RID: 27194 RVA: 0x0016B8C7 File Offset: 0x00169AC7
			private PairComparer()
			{
			}

			// Token: 0x06006A3B RID: 27195 RVA: 0x0016B8CF File Offset: 0x00169ACF
			public bool Equals(Pair<TFirst, TSecond> x, Pair<TFirst, TSecond> y)
			{
				return Pair<TFirst, TSecond>.PairComparer._firstComparer.Equals(x.First, y.First) && Pair<TFirst, TSecond>.PairComparer._secondComparer.Equals(x.Second, y.Second);
			}

			// Token: 0x06006A3C RID: 27196 RVA: 0x0016B901 File Offset: 0x00169B01
			public int GetHashCode(Pair<TFirst, TSecond> source)
			{
				return source.GetHashCode();
			}

			// Token: 0x040030A7 RID: 12455
			internal static readonly Pair<TFirst, TSecond>.PairComparer Instance = new Pair<TFirst, TSecond>.PairComparer();

			// Token: 0x040030A8 RID: 12456
			private static readonly EqualityComparer<TFirst> _firstComparer = EqualityComparer<TFirst>.Default;

			// Token: 0x040030A9 RID: 12457
			private static readonly EqualityComparer<TSecond> _secondComparer = EqualityComparer<TSecond>.Default;
		}
	}
}
