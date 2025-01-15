using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000099 RID: 153
	[DataContract(IsReference = true)]
	public class Symbol : IEquatable<Symbol>, IAlternatingLanguage, ILanguage
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000CE44 File Offset: 0x0000B044
		public Symbol(Grammar grammar, GrammarType type, string name, bool isStart = false)
		{
			this.Grammar = grammar;
			this.GrammarType = type;
			this.Name = name;
			this.IsStart = isStart;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000CE69 File Offset: 0x0000B069
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000CE71 File Offset: 0x0000B071
		[DataMember]
		public Grammar Grammar { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000CE7A File Offset: 0x0000B07A
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000CE82 File Offset: 0x0000B082
		[DataMember]
		public GrammarType GrammarType { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000CE8B File Offset: 0x0000B08B
		public Type ResolvedType
		{
			get
			{
				return this.GrammarType.Type;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CE98 File Offset: 0x0000B098
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CEA9 File Offset: 0x0000B0A9
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000CEB1 File Offset: 0x0000B0B1
		[DataMember]
		public bool IsStart { get; internal set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000CEBA File Offset: 0x0000B0BA
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		internal Location OriginLocation { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000CECB File Offset: 0x0000B0CB
		public ReadOnlyCollection<GrammarRule> DependentRules
		{
			get
			{
				if (this._dependentRules == null)
				{
					this._dependentRules = new Lazy<ReadOnlyCollection<GrammarRule>>(() => this.DependentRulesRecursive(new HashSet<Symbol>()).ToList<GrammarRule>().AsReadOnly());
				}
				return this._dependentRules.Value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000CEF7 File Offset: 0x0000B0F7
		public ReadOnlyCollection<GrammarRule> RHS
		{
			get
			{
				return this.Grammar.RulesOfHead(this);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000CF05 File Offset: 0x0000B105
		public ReadOnlyCollection<GrammarRule> RHSForLearning
		{
			get
			{
				return this.Grammar.NonDeprecatedRulesOfHead(this);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000CF13 File Offset: 0x0000B113
		public bool IsInput
		{
			get
			{
				return this.IsTerminal && this.TerminalRule.IsInput;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000CF2A File Offset: 0x0000B12A
		public bool IsVariable
		{
			get
			{
				if (this._isVariable == null)
				{
					this._isVariable = new Lazy<bool>(() => this.IsInput || this.Grammar.Rules.OfType<LambdaRule>().Any((LambdaRule r) => r.Variable.Equals(this)) || this.Grammar.Rules.OfType<LetRule>().Any((LetRule r) => r.Variable.Equals(this)));
				}
				return this._isVariable.Value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000CF56 File Offset: 0x0000B156
		public bool IsTerminal
		{
			get
			{
				if (this._isTerminal == null)
				{
					this._isTerminal = new Lazy<bool>(() => this.RHS.OfType<TerminalRule>().Any<TerminalRule>());
				}
				return this._isTerminal.Value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000CF82 File Offset: 0x0000B182
		public bool IsNonterminal
		{
			get
			{
				return !this.IsTerminal;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000CF8D File Offset: 0x0000B18D
		public LambdaRule LambdaRule
		{
			get
			{
				if (this._lambdaRule == null)
				{
					this._lambdaRule = new Lazy<LambdaRule>(() => this.RHS.OfType<LambdaRule>().SingleOrDefault<LambdaRule>());
				}
				return this._lambdaRule.Value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000CFB9 File Offset: 0x0000B1B9
		public TerminalRule TerminalRule
		{
			get
			{
				if (this._terminalRule == null)
				{
					this._terminalRule = new Lazy<TerminalRule>(() => this.RHS.OfType<TerminalRule>().SingleOrDefault<TerminalRule>());
				}
				return this._terminalRule.Value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00004FAE File Offset: 0x000031AE
		public Symbol LanguageSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
		public IEnumerable<ProgramNode> AllElements
		{
			get
			{
				return (from ps in this.TryGetAllPrograms(true, true)
					select ps.RealizedPrograms).OrElse(Enumerable.Empty<ProgramNode>());
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D01D File Offset: 0x0000B21D
		public ILanguage Intersect(ILanguage other)
		{
			if (!(other.LanguageSymbol != this))
			{
				return other;
			}
			return null;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D030 File Offset: 0x0000B230
		public ProgramSet Intersect(ProgramSet other)
		{
			if (!(other.LanguageSymbol != this))
			{
				return other;
			}
			return ProgramSet.Empty(this);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D048 File Offset: 0x0000B248
		public IEnumerable<ILanguage> Alternatives
		{
			get
			{
				return this.RHSForLearning;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D050 File Offset: 0x0000B250
		public bool Equals(Symbol other)
		{
			return this == other;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D056 File Offset: 0x0000B256
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D05E File Offset: 0x0000B25E
		private IEnumerable<GrammarRule> DependentRulesRecursive(HashSet<Symbol> seen)
		{
			if (seen.Contains(this))
			{
				yield break;
			}
			seen.Add(this);
			foreach (GrammarRule rule in this.RHSForLearning)
			{
				yield return rule;
				foreach (Symbol symbol in rule.Body)
				{
					foreach (GrammarRule grammarRule in symbol.DependentRulesRecursive(seen))
					{
						yield return grammarRule;
					}
					IEnumerator<GrammarRule> enumerator3 = null;
				}
				IEnumerator<Symbol> enumerator2 = null;
				rule = null;
			}
			IEnumerator<GrammarRule> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000D075 File Offset: 0x0000B275
		public ReadOnlyCollection<GrammarRule> ReferencedBy
		{
			get
			{
				if (this._referencedBy == null)
				{
					this._referencedBy = new Lazy<ReadOnlyCollection<GrammarRule>>(() => this.Grammar.Rules.Where((GrammarRule r) => r.Body.Contains(this)).ToList<GrammarRule>().AsReadOnly());
				}
				return this._referencedBy.Value;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		public Optional<ProgramSet> TryGetAllPrograms(bool allowLiterals = true, bool returnIncomplete = true)
		{
			return this.TryGetAllProgramsRecursive(BindingManager.Standard(this.Grammar), allowLiterals, returnIncomplete, ImmutableDictionary.Create<Symbol, int>().Add(this, 1));
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		private Optional<ProgramSet> TryGetAllProgramsRecursive(BindingManager bindingManager, bool allowLiterals, bool returnIncomplete, ImmutableDictionary<Symbol, int> seen)
		{
			if (this.IsVariable)
			{
				return ProgramSet.List(this, new ProgramNode[]
				{
					new VariableNode(this)
				}).Some<ProgramSet>();
			}
			if (!this.IsTerminal)
			{
				List<ProgramSet> list = new List<ProgramSet>(this.RHSForLearning.Count);
				foreach (GrammarRule grammarRule in this.RHSForLearning)
				{
					ProgramSet[] array = new ProgramSet[grammarRule.Body.Count];
					for (int i = 0; i < grammarRule.Body.Count; i++)
					{
						Symbol symbol = grammarRule.Body[i];
						int num = seen.MaybeGet(symbol).OrElseDefault<int>();
						if (num > 0 && !grammarRule.RecursionLimit[i].HasValue)
						{
							return Optional<ProgramSet>.Nothing;
						}
						if (num > grammarRule.RecursionLimit[i].OrElse(2147483647))
						{
							array[i] = ProgramSet.Empty(symbol);
						}
						if (array[i] == null)
						{
							ImmutableDictionary<Symbol, int> immutableDictionary = seen.SetItem(symbol, 1 + seen.GetValueOrDefault(symbol, 0));
							Optional<ProgramSet> optional = symbol.TryGetAllProgramsRecursive(bindingManager, allowLiterals, returnIncomplete, immutableDictionary);
							if (!optional.HasValue && !returnIncomplete)
							{
								return Optional<ProgramSet>.Nothing;
							}
							array[i] = optional.OrElseDefault<ProgramSet>();
						}
					}
					IEnumerable<ProgramSet> enumerable = array;
					Func<ProgramSet, bool> func;
					if ((func = Symbol.<>O.<0>__IsNullOrEmpty) == null)
					{
						func = (Symbol.<>O.<0>__IsNullOrEmpty = new Func<ProgramSet, bool>(ProgramSet.IsNullOrEmpty));
					}
					if (!enumerable.Any(func))
					{
						list.Add(new JoinProgramSet(grammarRule as NonterminalRule, array));
					}
				}
				return list.NormalizedUnion().SomeIfNotNull<ProgramSet>();
			}
			if (!allowLiterals)
			{
				return Optional<ProgramSet>.Nothing;
			}
			LiteralGenerator literalGenerator = bindingManager.ExplicitGenerator(this.TerminalRule);
			if (literalGenerator == null)
			{
				return Optional<ProgramSet>.Nothing;
			}
			return ProgramSet.List(this, from o in literalGenerator()
				select this.TerminalRule.BuildASTNode(o)).Some<ProgramSet>();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Symbol)obj)));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D050 File Offset: 0x0000B250
		public static bool operator ==(Symbol left, Symbol right)
		{
			return left == right;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		public static bool operator !=(Symbol left, Symbol right)
		{
			return left != right;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D304 File Offset: 0x0000B504
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			Grammar grammar = this.Grammar;
			int num = ((grammar != null) ? grammar.GetHashCode() : 0);
			int num2 = num * 100987;
			string name = this.Name;
			num = num2 ^ ((name != null) ? name.GetHashCode() : 0);
			int num3 = num * 100987;
			GrammarType grammarType = this.GrammarType;
			num = num3 ^ ((grammarType != null) ? grammarType.GetHashCode() : 0);
			this._hashCode = new int?(num);
			return this._hashCode.Value;
		}

		// Token: 0x0400019B RID: 411
		private Lazy<ReadOnlyCollection<GrammarRule>> _dependentRules;

		// Token: 0x0400019C RID: 412
		private Lazy<bool> _isTerminal;

		// Token: 0x0400019D RID: 413
		private Lazy<bool> _isVariable;

		// Token: 0x0400019E RID: 414
		private Lazy<LambdaRule> _lambdaRule;

		// Token: 0x0400019F RID: 415
		private Lazy<TerminalRule> _terminalRule;

		// Token: 0x040001A5 RID: 421
		private Lazy<ReadOnlyCollection<GrammarRule>> _referencedBy;

		// Token: 0x040001A6 RID: 422
		private int? _hashCode;

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001A7 RID: 423
			public static Func<ProgramSet, bool> <0>__IsNullOrEmpty;
		}
	}
}
