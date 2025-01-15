using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Diagnostics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000084 RID: 132
	[DataContract]
	public class Grammar
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B14C File Offset: 0x0000934C
		public bool IsRecursionLimited
		{
			get
			{
				bool? isRecursionLimited = this._isRecursionLimited;
				if (isRecursionLimited == null)
				{
					bool? flag = (this._isRecursionLimited = new bool?(this.Rules.Any((GrammarRule rule) => rule.RecursionLimit.Any((Optional<int> limit) => limit.HasValue))));
					return flag.Value;
				}
				return isRecursionLimited.GetValueOrDefault();
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public Grammar(string name, DiagnosticsContext diagnosticsContext)
		{
			this._diagnosticsContext = diagnosticsContext;
			this.Name = name;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B243 File Offset: 0x00009443
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B24B File Offset: 0x0000944B
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B254 File Offset: 0x00009454
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B25C File Offset: 0x0000945C
		public ReadOnlyCollection<GrammarRule> RulesOfHead(Symbol symbol)
		{
			return this._rulesByHead.GetOrCreateValue(symbol).AsReadOnly();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B26F File Offset: 0x0000946F
		public ReadOnlyCollection<GrammarRule> NonDeprecatedRulesOfHead(Symbol symbol)
		{
			return this._nonDeprecatedRulesByHead.GetOrCreateValue(symbol).AsReadOnly();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B282 File Offset: 0x00009482
		public ReadOnlyCollection<GrammarRule> RulesOfHead(string symbol)
		{
			return this.RulesOfHead(this.Symbol(symbol));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B291 File Offset: 0x00009491
		public ReadOnlyCollection<GrammarRule> RulesOfBody(Symbol symbol)
		{
			return this._rulesByBody.GetOrCreateValue(symbol).AsReadOnly();
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public ReadOnlyCollection<GrammarRule> Rules
		{
			get
			{
				return this._rules.ToList<GrammarRule>().AsReadOnly();
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B2B6 File Offset: 0x000094B6
		public GrammarRule Rule(string name)
		{
			return this._rulesByName.MaybeGet(name).OrElseDefault<GrammarRule>();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B2CC File Offset: 0x000094CC
		public void Validate(DiagnosticsContext diagnosticsContext)
		{
			int num = this._symbols.Count((KeyValuePair<string, Symbol> s) => s.Value.IsStart);
			Location.Source source = new Location.Source(diagnosticsContext.FileName, null, null);
			if (num == 0)
			{
				diagnosticsContext.AddDiagnostic(new Diagnostic.Syntax_NoStartSymbols(source, Array.Empty<object>()));
			}
			else if (num > 1)
			{
				string text = string.Join<Symbol>(", ", this._symbols.Values.Where((Symbol s) => s.IsStart));
				diagnosticsContext.AddDiagnostic(new Diagnostic.Syntax_MoreThanOneStart(source, new object[] { text }));
			}
			int num2 = this._symbols.Count((KeyValuePair<string, Symbol> s) => s.Value.IsInput);
			if (num2 == 0)
			{
				diagnosticsContext.AddDiagnostic(new Diagnostic.Syntax_NoInputSymbols(source, Array.Empty<object>()));
			}
			else if (num2 > 1)
			{
				string text2 = string.Join<Symbol>(", ", this._symbols.Values.Where((Symbol s) => s.IsInput));
				diagnosticsContext.AddDiagnostic(new Diagnostic.Syntax_MoreThanOneInput(source, new object[] { text2 }));
			}
			this.Rules.ForEach(delegate(GrammarRule r)
			{
				r.Validate(diagnosticsContext);
			});
			if ((diagnosticsContext.ValidationFlags & GrammarValidation.Features) != GrammarValidation.None)
			{
				this.Feature.Values.ForEach(delegate(FeatureInfo feature)
				{
					feature.Validate(diagnosticsContext, this);
				});
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B498 File Offset: 0x00009698
		public void AddRule(GrammarRule rule)
		{
			this._rules.Add(rule);
			this._rulesByHead.AddOrCreate(rule.Head, rule);
			foreach (Symbol symbol in rule.Body)
			{
				this._rulesByBody.AddOrCreate(symbol, rule);
			}
			if (!rule.Deprecated)
			{
				this._nonDeprecatedRulesByHead.AddOrCreate(rule.Head, rule);
			}
			if (rule.Id != null)
			{
				this._rulesByName[rule.Id] = rule;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B540 File Offset: 0x00009740
		public ReadOnlyDictionary<string, Grammar> GrammarReferences
		{
			get
			{
				return new ReadOnlyDictionary<string, Grammar>(this._grammarReferences);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B54D File Offset: 0x0000974D
		public ReadOnlyCollection<GrammarType> SemanticsLocations
		{
			get
			{
				return this._semanticsLocations.AsReadOnly();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B55A File Offset: 0x0000975A
		public ReadOnlyCollection<GrammarType> LearnerLocations
		{
			get
			{
				return this._learnerLocations.AsReadOnly();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B567 File Offset: 0x00009767
		public ReadOnlyDictionary<string, FeatureInfo> Feature
		{
			get
			{
				return new ReadOnlyDictionary<string, FeatureInfo>(this._features.ToDictionary((FeatureInfo f) => f.Name));
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B598 File Offset: 0x00009798
		public void AddFeature(Type propertyType, string name, IEnumerable<Type> holderTypes, bool isComplete)
		{
			this._features.Add(new FeatureInfo(propertyType, name, holderTypes, isComplete));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public void AddSemanticsLocation(GrammarType semanticsClass)
		{
			bool? isStatic = semanticsClass.IsStatic;
			bool flag = true;
			if (!((isStatic.GetValueOrDefault() == flag) & (isStatic != null)))
			{
				this._diagnosticsContext.AddDiagnostic(new Diagnostic.Core_TypeIsNotStatic(semanticsClass.Location, new object[] { semanticsClass.Name }));
			}
			this._semanticsLocations.Add(semanticsClass);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B60B File Offset: 0x0000980B
		public void AddLearnerLocation(GrammarType learnerClass)
		{
			this._learnerLocations.Add(learnerClass);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B619 File Offset: 0x00009819
		public void AddGrammar(string externalGrammarName, Grammar externalGrammar)
		{
			this._grammarReferences[externalGrammarName] = externalGrammar;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B628 File Offset: 0x00009828
		public ReadOnlyDictionary<string, Symbol> Symbols
		{
			get
			{
				return new ReadOnlyDictionary<string, Symbol>(this._symbols);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B638 File Offset: 0x00009838
		public Symbol StartSymbol
		{
			get
			{
				if (this._startSymbol == null)
				{
					lock (this)
					{
						if (this._startSymbol == null)
						{
							this._startSymbol = this._symbols.Values.Single((Symbol s) => s.IsStart);
						}
					}
				}
				return this._startSymbol;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public Symbol InputSymbol
		{
			get
			{
				if (this._inputSymbol == null)
				{
					lock (this)
					{
						if (this._inputSymbol == null)
						{
							this._inputSymbol = this._symbols.Values.Single((Symbol s) => s.IsInput);
						}
					}
				}
				return this._inputSymbol;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B750 File Offset: 0x00009950
		public TerminalRule InputRule
		{
			get
			{
				return this.InputSymbol.TerminalRule;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B75D File Offset: 0x0000995D
		public Symbol Symbol(string symbolName)
		{
			return this._symbols.MaybeGet(symbolName).OrElseDefault<Symbol>();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B770 File Offset: 0x00009970
		public Symbol AddSymbol(string name, GrammarType type, bool isStart = false)
		{
			this._symbols[name] = new Symbol(this, type, name, isStart);
			if (isStart)
			{
				if (this._startSymbol == null)
				{
					this._startSymbol = this._symbols[name];
				}
				else
				{
					Location.Source source = new Location.Source(this._diagnosticsContext.FileName, null, null);
					string text = string.Join<Symbol>(", ", this._symbols.Values.Where((Symbol s) => s.IsStart));
					this._diagnosticsContext.AddDiagnostic(new Diagnostic.Syntax_MoreThanOneStart(source, new object[] { text }));
				}
			}
			return this._symbols[name];
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B840 File Offset: 0x00009A40
		private Optional<int> CalculateFreeVariableHeight(Symbol current, ImmutableDictionary<Symbol, int> seen, ImmutableDictionary<Symbol, int> longestPaths, int freeVarCount)
		{
			if (current.Grammar != this)
			{
				ImmutableDictionary<Symbol, int> immutableDictionary = ImmutableDictionary<Microsoft.ProgramSynthesis.Symbol, int>.Empty.Add(current, 1);
				foreach (Symbol symbol in current.Grammar.Symbols.Values)
				{
					longestPaths = longestPaths.SetItem(symbol, 0);
				}
				return current.Grammar.CalculateFreeVariableHeight(current, immutableDictionary, longestPaths, 1);
			}
			longestPaths = longestPaths.SetItem(current, Math.Max(freeVarCount, longestPaths[current]));
			if (current.IsTerminal || current.IsVariable)
			{
				return freeVarCount.Some<int>();
			}
			Optional<int>[] array = new Optional<int>[current.RHS.Count];
			for (int i = 0; i < current.RHS.Count; i++)
			{
				GrammarRule grammarRule = current.RHS[i];
				if (grammarRule is LetRule)
				{
					Optional<int> optional;
					if (!this.TryDescend(grammarRule, 0, seen, longestPaths, freeVarCount, out optional))
					{
						return Optional<int>.Nothing;
					}
					int num = 1 + freeVarCount;
					Optional<int> optional2;
					if (!this.TryDescend(grammarRule, 1, seen, longestPaths, num, out optional2))
					{
						return Optional<int>.Nothing;
					}
					array[i] = Grammar.MaybeMax(optional, optional2);
				}
				else if (grammarRule is LambdaRule)
				{
					if (!this.TryDescend(grammarRule, 0, seen, longestPaths, 1 + freeVarCount, out array[i]))
					{
						return Optional<int>.Nothing;
					}
				}
				else
				{
					Optional<int>[] array2 = new Optional<int>[grammarRule.Body.Count];
					for (int j = 0; j < array2.Length; j++)
					{
						if (!this.TryDescend(grammarRule, j, seen, longestPaths, freeVarCount, out array2[j]))
						{
							return Optional<int>.Nothing;
						}
					}
					Optional<int>[] array3 = array;
					int num2 = i;
					IEnumerable<Optional<int>> enumerable = array2;
					Optional<int> optional3 = freeVarCount.Some<int>();
					Func<Optional<int>, Optional<int>, Optional<int>> func;
					if ((func = Grammar.<>O.<0>__MaybeMax) == null)
					{
						func = (Grammar.<>O.<0>__MaybeMax = new Func<Optional<int>, Optional<int>, Optional<int>>(Grammar.MaybeMax));
					}
					array3[num2] = enumerable.Aggregate(optional3, func);
				}
			}
			IEnumerable<Optional<int>> enumerable2 = array;
			Optional<int> optional4 = freeVarCount.Some<int>();
			Func<Optional<int>, Optional<int>, Optional<int>> func2;
			if ((func2 = Grammar.<>O.<0>__MaybeMax) == null)
			{
				func2 = (Grammar.<>O.<0>__MaybeMax = new Func<Optional<int>, Optional<int>, Optional<int>>(Grammar.MaybeMax));
			}
			return enumerable2.Aggregate(optional4, func2);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BA48 File Offset: 0x00009C48
		private bool TryDescend(GrammarRule rule, int paramIndex, ImmutableDictionary<Symbol, int> seen, ImmutableDictionary<Symbol, int> longestPaths, int freeVarCount, out Optional<int> result)
		{
			Symbol symbol = rule.Body[paramIndex];
			int num = seen.MaybeGet(symbol).OrElseDefault<int>();
			if (num > 0 && !rule.RecursionLimit[paramIndex].HasValue)
			{
				if (freeVarCount > longestPaths[symbol])
				{
					result = Optional<int>.Nothing;
					return false;
				}
				result = freeVarCount.Some<int>();
				return true;
			}
			else
			{
				if (rule.RecursionLimit[paramIndex].OrElse(2147483647) <= num)
				{
					result = freeVarCount.Some<int>();
					return true;
				}
				ImmutableDictionary<Symbol, int> immutableDictionary = seen.SetItem(symbol, 1 + seen.GetValueOrDefault(symbol, 0));
				result = this.CalculateFreeVariableHeight(symbol, immutableDictionary, longestPaths, freeVarCount);
				return true;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BAFF File Offset: 0x00009CFF
		private static Optional<int> MaybeMax(Optional<int> x, Optional<int> y)
		{
			if (!x.HasValue || !y.HasValue)
			{
				return Optional<int>.Nothing;
			}
			return Math.Max(x.Value, y.Value).Some<int>();
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BB34 File Offset: 0x00009D34
		internal Optional<int> FreeVariableHeight
		{
			get
			{
				if (this._freeVariableHeight == null)
				{
					ImmutableDictionary<Symbol, int>.Builder builder = ImmutableDictionary.CreateBuilder<Symbol, int>();
					foreach (Symbol symbol in this.Symbols.Values)
					{
						builder[symbol] = 0;
					}
					this._freeVariableHeight = new Optional<int>?(this.CalculateFreeVariableHeight(this.StartSymbol, ImmutableDictionary<Microsoft.ProgramSynthesis.Symbol, int>.Empty.Add(this.StartSymbol, 1), builder.ToImmutable(), 1));
				}
				return this._freeVariableHeight.Value;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		internal Grammar Clone()
		{
			Grammar grammar = new Grammar(this.Name, this._diagnosticsContext);
			grammar._features = this._features;
			grammar._freeVariableHeight = this._freeVariableHeight;
			grammar._grammarReferences = this._grammarReferences;
			grammar._rulesByHead = this._rulesByHead.ToDictionary((KeyValuePair<Symbol, List<GrammarRule>> kvp) => kvp.Key, (KeyValuePair<Symbol, List<GrammarRule>> kvp) => new List<GrammarRule>(kvp.Value));
			grammar._nonDeprecatedRulesByHead = this._nonDeprecatedRulesByHead.ToDictionary((KeyValuePair<Symbol, List<GrammarRule>> kvp) => kvp.Key, (KeyValuePair<Symbol, List<GrammarRule>> kvp) => new List<GrammarRule>(kvp.Value));
			grammar._rulesByBody = this._rulesByBody.ToDictionary((KeyValuePair<Symbol, List<GrammarRule>> kvp) => kvp.Key, (KeyValuePair<Symbol, List<GrammarRule>> kvp) => new List<GrammarRule>(kvp.Value));
			grammar._rulesByName = new Dictionary<string, GrammarRule>(this._rulesByName);
			grammar._learnerLocations = this._learnerLocations;
			grammar._rules = new List<GrammarRule>(this._rules);
			grammar._semanticsLocations = this._semanticsLocations;
			grammar._symbols = new Dictionary<string, Symbol>(this._symbols);
			return grammar;
		}

		// Token: 0x04000161 RID: 353
		private readonly DiagnosticsContext _diagnosticsContext;

		// Token: 0x04000162 RID: 354
		private bool? _isRecursionLimited;

		// Token: 0x04000163 RID: 355
		[DataMember]
		private Dictionary<Symbol, List<GrammarRule>> _rulesByHead = new Dictionary<Symbol, List<GrammarRule>>();

		// Token: 0x04000164 RID: 356
		[DataMember]
		private Dictionary<Symbol, List<GrammarRule>> _nonDeprecatedRulesByHead = new Dictionary<Symbol, List<GrammarRule>>();

		// Token: 0x04000165 RID: 357
		[DataMember]
		private Dictionary<Symbol, List<GrammarRule>> _rulesByBody = new Dictionary<Symbol, List<GrammarRule>>();

		// Token: 0x04000166 RID: 358
		[DataMember]
		private Dictionary<string, GrammarRule> _rulesByName = new Dictionary<string, GrammarRule>();

		// Token: 0x04000167 RID: 359
		[DataMember]
		private List<GrammarRule> _rules = new List<GrammarRule>();

		// Token: 0x04000169 RID: 361
		[DataMember]
		private List<FeatureInfo> _features = new List<FeatureInfo>();

		// Token: 0x0400016A RID: 362
		[DataMember]
		private Dictionary<string, Grammar> _grammarReferences = new Dictionary<string, Grammar>();

		// Token: 0x0400016B RID: 363
		[DataMember]
		private List<GrammarType> _semanticsLocations = new List<GrammarType>();

		// Token: 0x0400016C RID: 364
		[DataMember]
		private List<GrammarType> _learnerLocations = new List<GrammarType>();

		// Token: 0x0400016D RID: 365
		[DataMember]
		private Dictionary<string, Symbol> _symbols = new Dictionary<string, Symbol>();

		// Token: 0x0400016E RID: 366
		private Symbol _startSymbol;

		// Token: 0x0400016F RID: 367
		private Symbol _inputSymbol;

		// Token: 0x04000170 RID: 368
		private Optional<int>? _freeVariableHeight;

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000171 RID: 369
			public static Func<Optional<int>, Optional<int>, Optional<int>> <0>__MaybeMax;
		}
	}
}
