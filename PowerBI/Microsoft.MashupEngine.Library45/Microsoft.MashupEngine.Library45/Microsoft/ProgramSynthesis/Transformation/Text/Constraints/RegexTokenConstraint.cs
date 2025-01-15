using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DF6 RID: 7670
	internal class RegexTokenConstraint : Constraint<IRow, object>
	{
		// Token: 0x060100F4 RID: 65780 RVA: 0x00372EB8 File Offset: 0x003710B8
		public RegexTokenConstraint(IReadOnlyDictionary<string, Token> tokens)
		{
			this.AllowedTokens = tokens;
		}

		// Token: 0x17002AA0 RID: 10912
		// (get) Token: 0x060100F5 RID: 65781 RVA: 0x00372EC7 File Offset: 0x003710C7
		public IReadOnlyDictionary<string, Token> AllowedTokens { get; }

		// Token: 0x060100F6 RID: 65782 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x060100F7 RID: 65783 RVA: 0x00372ED0 File Offset: 0x003710D0
		public override bool Valid(Program<IRow, object> program)
		{
			return program.ProgramNode.AcceptVisitor<IEnumerable<RegularExpression>>(Session.RegularExpressionCollector.Instance).SelectMany((RegularExpression r) => r.Tokens).All((Token t) => this.AllowedTokens.ContainsKey(t.Name));
		}

		// Token: 0x060100F8 RID: 65784 RVA: 0x00372F22 File Offset: 0x00371122
		public override int GetHashCode()
		{
			return this.AllowedTokens.GetHashCode() ^ 47701;
		}

		// Token: 0x060100F9 RID: 65785 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<IRow, object> other)
		{
			return this == other;
		}

		// Token: 0x060100FA RID: 65786 RVA: 0x00372F35 File Offset: 0x00371135
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("Allowed regex tokens are constrained.", Array.Empty<object>()));
		}
	}
}
