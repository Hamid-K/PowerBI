using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008D6 RID: 2262
	internal struct ParseContext
	{
		// Token: 0x060030AA RID: 12458 RVA: 0x0008F930 File Offset: 0x0008DB30
		internal ParseContext(Grammar grammar, ParseSettings settings, ImmutableStack<ScopeElement> scope, Dictionary<int, object> identityCache)
		{
			this.Grammar = grammar;
			this.Settings = settings;
			this.Scope = scope;
			this.IdentityCache = identityCache;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0008F94F File Offset: 0x0008DB4F
		internal ParseContext WithGrammar(Grammar grammar)
		{
			return new ParseContext(grammar, this.Settings, this.Scope, this.IdentityCache);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0008F969 File Offset: 0x0008DB69
		internal ParseContext WithScope(ImmutableStack<ScopeElement> scope)
		{
			return new ParseContext(this.Grammar, this.Settings, scope, this.IdentityCache);
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x0008F983 File Offset: 0x0008DB83
		internal readonly Grammar Grammar { get; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0008F98B File Offset: 0x0008DB8B
		internal readonly ParseSettings Settings { get; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x0008F993 File Offset: 0x0008DB93
		internal readonly ImmutableStack<ScopeElement> Scope { get; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0008F99B File Offset: 0x0008DB9B
		internal readonly Dictionary<int, object> IdentityCache { get; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x0008F9A4 File Offset: 0x0008DBA4
		internal DeserializationContext Context
		{
			get
			{
				return this.Settings.Context;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x0008F9C0 File Offset: 0x0008DBC0
		internal ConversionRule RuleForBackCompatParsing
		{
			get
			{
				return this.Settings.RuleForBackCompatParsing;
			}
		}
	}
}
