using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A1 RID: 4513
	public class AllowedTokens<TInput, TOutput> : Constraint<TInput, TOutput>
	{
		// Token: 0x06008666 RID: 34406 RVA: 0x001C3B0E File Offset: 0x001C1D0E
		public AllowedTokens(IEnumerable<IToken> tokens)
		{
			this.Tokens = tokens.ConvertToHashSet<IToken>();
		}

		// Token: 0x06008667 RID: 34407 RVA: 0x001C3B0E File Offset: 0x001C1D0E
		public AllowedTokens(params IToken[] tokens)
		{
			this.Tokens = tokens.ConvertToHashSet<IToken>();
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06008668 RID: 34408 RVA: 0x001C3B22 File Offset: 0x001C1D22
		public HashSet<IToken> Tokens { get; }

		// Token: 0x06008669 RID: 34409 RVA: 0x001C3B2A File Offset: 0x001C1D2A
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((AllowedTokens<TInput, TOutput>)other)));
		}

		// Token: 0x0600866A RID: 34410 RVA: 0x001C3B58 File Offset: 0x001C1D58
		public bool Equals(AllowedTokens<TInput, TOutput> other)
		{
			return other != null && (this == other || this.Tokens.SetEquals(other.Tokens));
		}

		// Token: 0x0600866B RID: 34411 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x0600866C RID: 34412 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x0600866D RID: 34413 RVA: 0x001C3B76 File Offset: 0x001C1D76
		public override int GetHashCode()
		{
			return this.Tokens.GetHashCode();
		}
	}
}
