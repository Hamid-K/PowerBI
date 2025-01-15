using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000023 RID: 35
	public abstract class Identifier : IEquatable<Identifier>, ISyntaxNode
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002DDA File Offset: 0x00000FDA
		public static Identifier New()
		{
			return new Identifier.UniqueIdentifier();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public static Identifier New(string name)
		{
			return new Identifier.StringIdentifier(name);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public static Identifier New(string name, TokenReference token)
		{
			return new Identifier.TokenStringIdentifier(name, token);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public static Identifier New(string name, TokenRange range)
		{
			return new Identifier.TokenRangeStringIdentifier(name, range);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000084 RID: 132
		public abstract bool IsUnique { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000085 RID: 133
		public abstract string Name { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000086 RID: 134
		public abstract TokenRange Range { get; }

		// Token: 0x06000087 RID: 135
		public abstract bool Equals(Identifier other);

		// Token: 0x06000088 RID: 136 RVA: 0x00002DFB File Offset: 0x00000FFB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Identifier);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002E09 File Offset: 0x00001009
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002E11 File Offset: 0x00001011
		public static implicit operator Identifier(string value)
		{
			return Identifier.New(value);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002E19 File Offset: 0x00001019
		public static implicit operator string(Identifier identifier)
		{
			return identifier.Name;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002E21 File Offset: 0x00001021
		public static bool operator !=(Identifier left, Identifier right)
		{
			return !(left == right);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002E2D File Offset: 0x0000102D
		public static bool operator ==(Identifier left, Identifier right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002E19 File Offset: 0x00001019
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000080 RID: 128
		public static readonly Identifier Underscore = Identifier.New("_");

		// Token: 0x02000024 RID: 36
		protected class UniqueIdentifier : Identifier
		{
			// Token: 0x06000091 RID: 145 RVA: 0x00002E57 File Offset: 0x00001057
			public UniqueIdentifier()
			{
				this.id = Interlocked.Increment(ref Identifier.UniqueIdentifier.counter);
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000092 RID: 146 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsUnique
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00002E6F File Offset: 0x0000106F
			public override string Name
			{
				get
				{
					return "t" + this.id.ToString(CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00002E8B File Offset: 0x0000108B
			public override TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00002E92 File Offset: 0x00001092
			public override bool Equals(Identifier other)
			{
				return this == other;
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00002E98 File Offset: 0x00001098
			public override int GetHashCode()
			{
				return this.id;
			}

			// Token: 0x04000081 RID: 129
			private static int counter;

			// Token: 0x04000082 RID: 130
			private int id;
		}

		// Token: 0x02000025 RID: 37
		private class StringIdentifier : Identifier, IStringIdentifier, ISyntaxNode
		{
			// Token: 0x06000097 RID: 151 RVA: 0x00002EA0 File Offset: 0x000010A0
			public StringIdentifier(string name)
			{
				this.name = name;
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsUnique
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000099 RID: 153 RVA: 0x00002EAF File Offset: 0x000010AF
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x0600009A RID: 154 RVA: 0x00002E8B File Offset: 0x0000108B
			public override TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x0600009B RID: 155 RVA: 0x00002EB7 File Offset: 0x000010B7
			public override bool Equals(Identifier other)
			{
				return other != null && !other.IsUnique && this.Name == other.Name;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00002ED7 File Offset: 0x000010D7
			public override int GetHashCode()
			{
				return this.name.GetHashCode();
			}

			// Token: 0x04000083 RID: 131
			private string name;
		}

		// Token: 0x02000026 RID: 38
		private class TokenStringIdentifier : Identifier.StringIdentifier
		{
			// Token: 0x0600009D RID: 157 RVA: 0x00002EE4 File Offset: 0x000010E4
			public TokenStringIdentifier(string text, TokenReference token)
				: base(text)
			{
				this.token = token;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00002EF4 File Offset: 0x000010F4
			public override TokenRange Range
			{
				get
				{
					return new TokenRange(this.token);
				}
			}

			// Token: 0x04000084 RID: 132
			private TokenReference token;
		}

		// Token: 0x02000027 RID: 39
		private class TokenRangeStringIdentifier : Identifier.StringIdentifier
		{
			// Token: 0x0600009F RID: 159 RVA: 0x00002F01 File Offset: 0x00001101
			public TokenRangeStringIdentifier(string text, TokenRange range)
				: base(text)
			{
				this.range = range;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002F11 File Offset: 0x00001111
			public override TokenRange Range
			{
				get
				{
					return this.range;
				}
			}

			// Token: 0x04000085 RID: 133
			private TokenRange range;
		}
	}
}
