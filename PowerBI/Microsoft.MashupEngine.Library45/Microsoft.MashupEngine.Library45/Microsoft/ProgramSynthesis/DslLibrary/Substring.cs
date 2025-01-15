using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007EF RID: 2031
	public class Substring
	{
		// Token: 0x06002B48 RID: 11080 RVA: 0x00078E44 File Offset: 0x00077044
		private static int SubstringHash(string s, int startIndex, int endIndex)
		{
			int num = 5381;
			for (int i = startIndex; i < endIndex; i++)
			{
				num = ((num << 5) + num) ^ (int)s[i];
			}
			return num;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00078E74 File Offset: 0x00077074
		public Substring(string source, uint start, uint end)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", "The string is null");
			}
			if (end < start)
			{
				throw new ArgumentOutOfRangeException("end", FormattableString.Invariant(FormattableStringFactory.Create(" The ending position {0} is less than the starting position {1}", new object[] { end, start })));
			}
			if ((ulong)end > (ulong)((long)source.Length))
			{
				throw new ArgumentOutOfRangeException("end", FormattableString.Invariant(FormattableStringFactory.Create(" The ending position {0} exceeds the string length {1}", new object[] { end, source.Length })));
			}
			this.Info = new SubstringInfo(source, start, end);
			if (start == 0U && (ulong)end == (ulong)((long)source.Length))
			{
				this._value = source;
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00078F35 File Offset: 0x00077135
		public Substring(Substring other)
			: this(other.Source, other.Start, other.End)
		{
			this._value = other._value;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x00078F5B File Offset: 0x0007715B
		public Substring(string source)
			: this(source, 0U, (uint)source.Length)
		{
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x00078F6B File Offset: 0x0007716B
		public SubstringInfo Info { get; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x00078F74 File Offset: 0x00077174
		public string Source
		{
			get
			{
				return this.Info.Source;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x00078F90 File Offset: 0x00077190
		public uint Start
		{
			get
			{
				return this.Info.Start;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x00078FAC File Offset: 0x000771AC
		public uint End
		{
			get
			{
				return this.Info.End;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x00078FC7 File Offset: 0x000771C7
		protected bool IsValueAlreadyComputed
		{
			get
			{
				return this._value != null;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x00078FD4 File Offset: 0x000771D4
		public string Value
		{
			get
			{
				if (this._value != null)
				{
					return this._value;
				}
				SubstringInfo info = this.Info;
				return this._value = info.Source.Substring((int)info.Start, (int)(info.End - info.Start));
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x00079024 File Offset: 0x00077224
		public uint Length
		{
			get
			{
				return this.Info.End - this.Info.Start;
			}
		}

		// Token: 0x170007A4 RID: 1956
		public char this[uint index]
		{
			get
			{
				if (index >= this.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.Source[(int)(this.Start + index)];
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x00079077 File Offset: 0x00077277
		public static IEqualityComparer<Substring> ValueEquality { get; } = new Substring.SubstringValueEqualityComparer();

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x0007907E File Offset: 0x0007727E
		public static IEqualityComparer<Substring> PositionEquality { get; } = new Substring.SubstringPositionEqualityComparer();

		// Token: 0x040014BE RID: 5310
		private string _value;

		// Token: 0x020007F0 RID: 2032
		private class SubstringValueEqualityComparer : IEqualityComparer<Substring>
		{
			// Token: 0x06002B57 RID: 11095 RVA: 0x0007909C File Offset: 0x0007729C
			public bool Equals(Substring x, Substring y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null)
				{
					return false;
				}
				if (y == null)
				{
					return false;
				}
				uint length = x.Length;
				if (y.Length != length)
				{
					return false;
				}
				string source = y.Source;
				string source2 = x.Source;
				if (source == source2 && y.Start == x.Start)
				{
					return true;
				}
				if (x.IsValueAlreadyComputed && y.IsValueAlreadyComputed)
				{
					return x.Value.Equals(y.Value);
				}
				if (x.IsValueAlreadyComputed)
				{
					return y.Source.SubstringEquals(x.Value, (int)y.Start);
				}
				if (y.IsValueAlreadyComputed)
				{
					return x.Source.SubstringEquals(y.Value, (int)x.Start);
				}
				int start = (int)x.Start;
				int start2 = (int)y.Start;
				int num = 0;
				while ((long)num < (long)((ulong)length))
				{
					if (source2[start++] != source[start2++])
					{
						return false;
					}
					num++;
				}
				return true;
			}

			// Token: 0x06002B58 RID: 11096 RVA: 0x0007918D File Offset: 0x0007738D
			public int GetHashCode(Substring obj)
			{
				return Substring.SubstringHash(obj.Source, (int)obj.Start, (int)obj.End);
			}
		}

		// Token: 0x020007F1 RID: 2033
		private class SubstringPositionEqualityComparer : IEqualityComparer<Substring>
		{
			// Token: 0x06002B5A RID: 11098 RVA: 0x000791A6 File Offset: 0x000773A6
			public bool Equals(Substring x, Substring y)
			{
				return x == y || (x != null && y != null && (x.Source == y.Source && x.Start == y.Start) && x.End == y.End);
			}

			// Token: 0x06002B5B RID: 11099 RVA: 0x000791E4 File Offset: 0x000773E4
			public int GetHashCode(Substring obj)
			{
				return (((RuntimeHelpers.GetHashCode(obj.Source) * 5458421) ^ (int)obj.Start) * 5458421) ^ (int)obj.End;
			}
		}
	}
}
