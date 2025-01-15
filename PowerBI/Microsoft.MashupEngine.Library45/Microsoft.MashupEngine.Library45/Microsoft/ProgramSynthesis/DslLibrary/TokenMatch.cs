using System;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000811 RID: 2065
	public struct TokenMatch : IEquatable<TokenMatch>
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x0007D932 File Offset: 0x0007BB32
		public readonly Token Token { get; }

		// Token: 0x06002C8D RID: 11405 RVA: 0x0007D93A File Offset: 0x0007BB3A
		public TokenMatch(Token t, uint l)
		{
			this.Token = t;
			this.Length = l;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0007D94A File Offset: 0x0007BB4A
		public bool Equals(TokenMatch other)
		{
			return this.Token.Equals(other.Token) && this.Length == other.Length;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0007D970 File Offset: 0x0007BB70
		public override bool Equals(object obj)
		{
			return obj != null && obj is TokenMatch && this.Equals((TokenMatch)obj);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0007D98D File Offset: 0x0007BB8D
		public override int GetHashCode()
		{
			return (this.Token.GetHashCode() * 12357559) ^ (int)this.Length;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0007D9A7 File Offset: 0x0007BBA7
		public static bool operator ==(TokenMatch left, TokenMatch right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0007D9B1 File Offset: 0x0007BBB1
		public static bool operator !=(TokenMatch left, TokenMatch right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400154D RID: 5453
		public readonly uint Length;
	}
}
