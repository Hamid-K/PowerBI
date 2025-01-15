using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006BE RID: 1726
	[DebuggerDisplay("{Symbol}")]
	public class LearningTask : IEquatable<LearningTask>
	{
		// Token: 0x06002561 RID: 9569 RVA: 0x00067E10 File Offset: 0x00066010
		protected LearningTask()
		{
			this.PruningRequest = PruningRequest.Empty;
			this.RecursionDepths = ImmutableDictionary.Create<Symbol, int>();
			this.AdditionalInputs = Enumerable.Empty<State>();
			this._featureCalculationContext = new Lazy<FeatureCalculationContext>(delegate
			{
				Spec spec = this.Spec;
				IReadOnlyList<State> readOnlyList;
				if ((readOnlyList = ((spec != null) ? spec.ProvidedInputs : null)) == null)
				{
					Spec spec2 = this.Spec;
					if (spec2 == null)
					{
						readOnlyList = null;
					}
					else
					{
						IReadOnlyList<State> providedInputs = spec2.ProvidedInputs;
						readOnlyList = ((providedInputs != null) ? providedInputs.ToList<State>() : null);
					}
				}
				IReadOnlyList<State> readOnlyList2;
				if ((readOnlyList2 = this.AdditionalInputs as IReadOnlyList<State>) == null)
				{
					IEnumerable<State> additionalInputs = this.AdditionalInputs;
					readOnlyList2 = ((additionalInputs != null) ? additionalInputs.ToList<State>() : null);
				}
				return FeatureCalculationContext.Create(readOnlyList, readOnlyList2, this.TopProgramsFeatureOptions.OrElseDefault<IFeatureOptions>());
			});
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00067E50 File Offset: 0x00066050
		protected LearningTask(ILanguage language, Spec spec)
			: this()
		{
			this.Language = language;
			this.Spec = spec;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00067E66 File Offset: 0x00066066
		public LearningTask(ProgramSet programSet, Spec spec)
			: this(programSet, spec)
		{
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00067E70 File Offset: 0x00066070
		public LearningTask(Symbol symbol, Spec spec)
			: this(symbol, spec)
		{
			this.RecursionDepths = this.RecursionDepths.SetItem(this.Symbol, 1 + this.RecursionDepths.GetValueOrDefault(this.Symbol, 0));
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00067EA5 File Offset: 0x000660A5
		protected LearningTask(Symbol symbol, Spec spec, PruningRequest pruningRequest)
			: this(symbol, spec)
		{
			this.PruningRequest = pruningRequest;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00067EB8 File Offset: 0x000660B8
		public LearningTask(Symbol symbol, Spec spec, int topProgramsK, IFeature feature, IFeatureOptions options = null)
			: this(symbol, spec, PruningRequest.Create(new int?(topProgramsK), null, feature, options, ProgramSamplingStrategy.UniformRandom))
		{
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00067EE8 File Offset: 0x000660E8
		public LearningTask(Symbol symbol, Spec spec, int randomProgramsK, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom)
			: this(symbol, spec, PruningRequest.Create(null, new int?(randomProgramsK), null, null, programSamplingStrategy))
		{
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00067F15 File Offset: 0x00066115
		public LearningTask(Symbol symbol, Spec spec, int topProgamsK, IFeature feature, int randomProgramsK, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom, IFeatureOptions featureOptions = null)
			: this(symbol, spec, PruningRequest.Create(new int?(topProgamsK), new int?(randomProgramsK), feature, featureOptions, samplingStrategy))
		{
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x00067F37 File Offset: 0x00066137
		public ILanguage Language { get; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x00067F3F File Offset: 0x0006613F
		public Symbol Symbol
		{
			get
			{
				return this.Language.LanguageSymbol;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x00067F4C File Offset: 0x0006614C
		public Spec Spec { get; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x00067F54 File Offset: 0x00066154
		// (set) Token: 0x0600256D RID: 9581 RVA: 0x00067F5C File Offset: 0x0006615C
		public IEnumerable<State> AdditionalInputs { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x00067F65 File Offset: 0x00066165
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x00067F6D File Offset: 0x0006616D
		public PruningRequest PruningRequest { get; protected set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x00067F76 File Offset: 0x00066176
		public bool IsOneShot
		{
			get
			{
				return this.Language is Symbol;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x00067F86 File Offset: 0x00066186
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x00067F8E File Offset: 0x0006618E
		public ImmutableDictionary<Symbol, int> RecursionDepths { get; private set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x00067F97 File Offset: 0x00066197
		public Optional<int> K
		{
			get
			{
				return this.PruningRequest.K.SomeIfNotNull<int>();
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x00067FA9 File Offset: 0x000661A9
		public Optional<int> RandomK
		{
			get
			{
				return this.PruningRequest.RandomK.SomeIfNotNull<int>();
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x00067FBB File Offset: 0x000661BB
		public Optional<IFeature> TopProgramsFeature
		{
			get
			{
				return this.PruningRequest.TopProgramsFeature.SomeIfNotNull<IFeature>();
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x00067FCD File Offset: 0x000661CD
		public Optional<IFeatureOptions> TopProgramsFeatureOptions
		{
			get
			{
				return this.PruningRequest.TopProgramsFeatureOptions.SomeIfNotNull<IFeatureOptions>();
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x00067FDF File Offset: 0x000661DF
		public bool RequiresPruning
		{
			get
			{
				return !this.PruningRequest.IsEmpty;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x00067FF0 File Offset: 0x000661F0
		public bool IsOrdered
		{
			get
			{
				return this.K.HasValue;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x0006800B File Offset: 0x0006620B
		public IEnumerable<State> ProvidedInputs
		{
			get
			{
				Spec spec = this.Spec;
				return ((spec != null) ? spec.ProvidedInputs.Concat(this.AdditionalInputs) : null) ?? this.AdditionalInputs;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x00068034 File Offset: 0x00066234
		public FeatureCalculationContext FeatureCalculationContext
		{
			get
			{
				return this._featureCalculationContext.Value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x00068041 File Offset: 0x00066241
		// (set) Token: 0x0600257C RID: 9596 RVA: 0x00068049 File Offset: 0x00066249
		public object Configuration { get; private set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x00068052 File Offset: 0x00066252
		private Grammar Grammar
		{
			get
			{
				return this.Symbol.Grammar;
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00068060 File Offset: 0x00066260
		public bool Equals(LearningTask other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Grammar.IsRecursionLimited && !this.RecursionDepths.DictionaryEquals(other.RecursionDepths, null))
			{
				return false;
			}
			if (this.Language is Symbol && other.Language is Symbol)
			{
				return object.Equals(this.Symbol, other.Symbol) && object.Equals(this.Spec, other.Spec) && this.PruningRequest.Equals(other.PruningRequest);
			}
			if (this.Language is ProgramSet && other.Language is ProgramSet)
			{
				return this.Language == other.Language && object.Equals(this.Spec, other.Spec);
			}
			return this.Language == other.Language && object.Equals(this.Spec, other.Spec) && this.PruningRequest.Equals(other.PruningRequest) && object.Equals(this.Configuration, other.Configuration);
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00068174 File Offset: 0x00066374
		protected void CloneInternals(LearningTask other)
		{
			this.RecursionDepths = other.RecursionDepths;
			this.Configuration = other.Configuration;
			this.AdditionalInputs = other.AdditionalInputs;
			Spec spec = other.Spec;
			IReadOnlyList<State> readOnlyList = ((spec != null) ? spec.ProvidedInputs : null);
			Spec spec2 = this.Spec;
			if (readOnlyList == ((spec2 != null) ? spec2.ProvidedInputs : null))
			{
				this._featureCalculationContext = other._featureCalculationContext;
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000681D7 File Offset: 0x000663D7
		public LearningTask Clone(Symbol newSymbol = null, Spec newSpec = null)
		{
			newSymbol = newSymbol ?? this.Symbol;
			newSpec = newSpec ?? this.Spec;
			LearningTask learningTask = new LearningTask(newSymbol, newSpec, this.PruningRequest);
			learningTask.CloneInternals(this);
			return learningTask;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00068208 File Offset: 0x00066408
		public LearningTask WithTopKPrograms(int? newK)
		{
			if (newK != null && !this.IsOneShot)
			{
				throw new InvalidOperationException("An incremental learning task cannot be extended with a top-k restriction");
			}
			if (newK == null)
			{
				if (!this.IsOrdered)
				{
					return this;
				}
				return this.WithoutTopKRequest();
			}
			else
			{
				if (!this.IsOrdered)
				{
					throw new InvalidOperationException("You cannot extended a learning task with a top-k restriction without specifying a ranking feature.");
				}
				return this.Clone(null, null).WithTopKRequest(newK.Value, this.TopProgramsFeature.Value, this.TopProgramsFeatureOptions.OrElse(null));
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x0006828C File Offset: 0x0006648C
		public LearningTask WithRandomKPrograms(int? newRandomK)
		{
			if (newRandomK != null && !this.IsOneShot)
			{
				throw new InvalidOperationException("An incremental learning task cannot be extended with a random-k restriction");
			}
			if (newRandomK != null)
			{
				return this.Clone(null, null).WithRandomKRequest(newRandomK.Value, this.PruningRequest.ProgramSamplingStrategy);
			}
			if (!this.RandomK.HasValue)
			{
				return this;
			}
			return this.WithoutRandomKRequest();
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000682F8 File Offset: 0x000664F8
		public LearningTask WithTopKRequest(int k, IFeature feature, IFeatureOptions options)
		{
			if (!this.IsOneShot)
			{
				throw new InvalidOperationException("An incremental learning task cannot be extended with a top-k restriction");
			}
			PruningRequest pruningRequest = this.PruningRequest.ReplaceWithTopProgramsK(k, feature, options);
			if (this.PruningRequest.Equals(pruningRequest))
			{
				return this;
			}
			LearningTask learningTask = this.Clone(null, null);
			learningTask.PruningRequest = pruningRequest;
			return learningTask;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00068348 File Offset: 0x00066548
		public LearningTask WithoutTopKRequest()
		{
			if (!this.IsOrdered)
			{
				return this;
			}
			LearningTask learningTask = this.Clone(null, null);
			PruningRequest pruningRequest = this.PruningRequest.WithoutTopK();
			learningTask.PruningRequest = pruningRequest;
			return learningTask;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0006837C File Offset: 0x0006657C
		public LearningTask WithRandomKRequest(int newRandomK, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			PruningRequest pruningRequest = this.PruningRequest.ReplaceWithRandomProgramsK(newRandomK, new ProgramSamplingStrategy?(samplingStrategy));
			LearningTask learningTask = this.Clone(null, null);
			learningTask.PruningRequest = pruningRequest;
			return learningTask;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000683AC File Offset: 0x000665AC
		public LearningTask WithoutRandomKRequest()
		{
			if (!this.RandomK.HasValue)
			{
				return this;
			}
			PruningRequest pruningRequest = this.PruningRequest.WithoutRandomK();
			LearningTask learningTask = this.Clone(null, null);
			learningTask.PruningRequest = pruningRequest;
			return learningTask;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000683E6 File Offset: 0x000665E6
		public LearningTask WithoutPruningRequests()
		{
			if (this.PruningRequest.IsEmpty)
			{
				return this;
			}
			LearningTask learningTask = this.Clone(null, null);
			learningTask.PruningRequest = PruningRequest.Empty;
			return learningTask;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0006840A File Offset: 0x0006660A
		public LearningTask WithConfiguration(object configuration)
		{
			LearningTask learningTask = this.Clone(null, null);
			learningTask.Configuration = configuration;
			return learningTask;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0006841C File Offset: 0x0006661C
		public LearningTask<TSpec> Cast<TSpec>() where TSpec : Spec
		{
			if (!(this.Spec is TSpec))
			{
				throw new InvalidCastException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot cast {0} as {1}", new object[]
				{
					this.Spec.GetType(),
					typeof(TSpec)
				})));
			}
			return new LearningTask<TSpec>(this);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00068474 File Offset: 0x00066674
		public LearningTask MakeSubtask(IJoinLanguage joinLanguage, int parameterIndex, Spec subtaskSpec)
		{
			NonterminalRule nonterminalRule = joinLanguage as NonterminalRule;
			if (nonterminalRule != null)
			{
				return this.MakeSubtask(nonterminalRule, parameterIndex, subtaskSpec);
			}
			JoinProgramSet joinProgramSet = joinLanguage as JoinProgramSet;
			if (joinProgramSet != null)
			{
				return this.MakeSubtask(joinProgramSet, parameterIndex, subtaskSpec);
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not handle type: {0} in call to {1}.", new object[]
			{
				joinLanguage.GetType(),
				"MakeSubtask"
			})));
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000684D4 File Offset: 0x000666D4
		public LearningTask MakeSubtask(ILanguage alternative)
		{
			LearningTask learningTask = new LearningTask(alternative, this.Spec);
			learningTask.CloneInternals(this);
			learningTask.PruningRequest = this.PruningRequest;
			return learningTask;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000684F8 File Offset: 0x000666F8
		private LearningTask MakeSubtask(JoinProgramSet joinProgramSet, int parameterIndex, Spec subtaskSpec)
		{
			LearningTask learningTask = new LearningTask(joinProgramSet.ParameterSpaces[parameterIndex], subtaskSpec);
			learningTask.CloneInternals(this);
			Symbol languageSymbol = joinProgramSet.ParameterSpaces[parameterIndex].LanguageSymbol;
			learningTask.RecursionDepths = this.RecursionDepths.SetItem(languageSymbol, this.RecursionDepths.GetValueOrDefault(languageSymbol, 0));
			return learningTask;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00068548 File Offset: 0x00066748
		private LearningTask MakeSubtask(NonterminalRule rule, int parameter, Spec subtaskSpec)
		{
			Symbol symbol = rule.Body[parameter];
			PruningRequest pruningRequest = (this.PruningRequest.HasTopKRequest ? this.PruningRequest.ReplaceWithTopProgramsK(this.PruningRequest.K.Value, this.PruningRequest.TopProgramsFeature.GetExternFeature(rule, parameter), this.PruningRequest.TopProgramsFeatureOptions) : this.PruningRequest);
			LearningTask learningTask = new LearningTask(symbol, subtaskSpec);
			learningTask.PruningRequest = pruningRequest;
			learningTask.CloneInternals(this);
			learningTask.RecursionDepths = this.RecursionDepths.SetItem(symbol, 1 + this.RecursionDepths.GetValueOrDefault(symbol, 0));
			return learningTask;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000685EC File Offset: 0x000667EC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			LearningTask learningTask = obj as LearningTask;
			return learningTask != null && this.Equals(learningTask);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00068618 File Offset: 0x00066818
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				if (this.Language is Symbol)
				{
					Symbol symbol = this.Symbol;
					this._hashCode = new int?((symbol != null) ? symbol.GetHashCode() : 50069);
					int? num = this._hashCode * 14549243;
					Spec spec = this.Spec;
					this._hashCode = num ^ ((spec != null) ? spec.GetHashCode() : 0);
					this._hashCode = (this._hashCode * 14549243) ^ this.PruningRequest.GetHashCode();
					num = this._hashCode * 14549243;
					object configuration = this.Configuration;
					this._hashCode = num ^ ((configuration != null) ? configuration.GetHashCode() : 0);
				}
				else
				{
					this._hashCode = new int?(this.Language.GetHashCode());
					int? num = this._hashCode * 397;
					Spec spec2 = this.Spec;
					this._hashCode = num ^ ((spec2 != null) ? spec2.GetHashCode() : 0);
				}
				if (this.Grammar.IsRecursionLimited)
				{
					this._hashCode = (this._hashCode * 104543) ^ this.RecursionDepths.OrderIndependentKeyValueHashCode<Symbol, int>();
				}
			}
			return this._hashCode.Value;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00068898 File Offset: 0x00066A98
		public static LearningTask Create(Symbol symbol, Spec spec, int? randomK, ProgramSamplingStrategy samplingStrategy, int? k, IFeature feature, IFeatureOptions featureOptions = null)
		{
			if (randomK != null && randomK.Value <= 0)
			{
				throw new ArgumentException("Must be > 0", "randomK");
			}
			if (k != null && k.Value <= 0)
			{
				throw new ArgumentException("Must be > 0", "k");
			}
			if (k != null && randomK != null)
			{
				return new LearningTask(symbol, spec, k.Value, feature, randomK.Value, samplingStrategy, featureOptions);
			}
			if (k != null)
			{
				return new LearningTask(symbol, spec, k.Value, feature, featureOptions);
			}
			if (randomK != null)
			{
				return new LearningTask(symbol, spec, randomK.Value, ProgramSamplingStrategy.UniformRandom);
			}
			return new LearningTask(symbol, spec);
		}

		// Token: 0x040011CF RID: 4559
		private Lazy<FeatureCalculationContext> _featureCalculationContext;

		// Token: 0x040011D1 RID: 4561
		private int? _hashCode;
	}
}
