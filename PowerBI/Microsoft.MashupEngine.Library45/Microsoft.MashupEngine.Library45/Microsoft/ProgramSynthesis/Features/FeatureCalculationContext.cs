using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007C8 RID: 1992
	public class FeatureCalculationContext : IEquatable<FeatureCalculationContext>
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00077310 File Offset: 0x00075510
		public IReadOnlyList<State> ReferenceSpecInputs
		{
			get
			{
				return this._inputs.ReferenceSpecInputs;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x0007731D File Offset: 0x0007551D
		public IReadOnlyList<State> ReferenceAdditionalInputs
		{
			get
			{
				return this._inputs.ReferenceAdditionalInputs;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x0007732A File Offset: 0x0007552A
		public IFeatureOptions Options { get; }

		// Token: 0x06002A6C RID: 10860 RVA: 0x00077332 File Offset: 0x00075532
		public IReadOnlyList<State> ComputeSpecInputs()
		{
			return this._inputs.ComputeSpecInputs();
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0007733F File Offset: 0x0007553F
		public IReadOnlyList<State> MaterializeSpecInputs()
		{
			return this._inputs.MaterializeSpecInputs();
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x0007734C File Offset: 0x0007554C
		public IReadOnlyList<State> AdditionalInputs
		{
			get
			{
				return this.ReferenceAdditionalInputs;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x00077354 File Offset: 0x00075554
		public IReadOnlyList<State> AllInputs
		{
			get
			{
				return this._allInputsLazy.Value;
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00077361 File Offset: 0x00075561
		private FeatureCalculationContext(IReadOnlyList<State> referenceSpecInputs, IReadOnlyList<State> referenceAdditionalInputs, IFeatureOptions options)
			: this(new ReferenceFeatureCalculationContextInputs(referenceSpecInputs, referenceAdditionalInputs), options)
		{
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x00077371 File Offset: 0x00075571
		private FeatureCalculationContext(FeatureCalculationContextInputs parent, IInputTransformer transform, IFeatureOptions options)
			: this(new TransformedFeatureCalculationContextInputs(parent, transform), options)
		{
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00077381 File Offset: 0x00075581
		private FeatureCalculationContext(FeatureCalculationContextInputs inputs, IFeatureOptions options)
		{
			this._inputs = inputs;
			this.Options = options ?? FeatureOptions.Default;
			this._allInputsLazy = new Lazy<IReadOnlyList<State>>(() => this.MaterializeSpecInputs().Concat(this.AdditionalInputs.Select((State state) => this.Options.Substitutions.Aggregate(state, (State s, KeyValuePair<Symbol, Symbol> subst) => s.Substitute(subst.Key, subst.Value)))).ToList<State>());
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000773B7 File Offset: 0x000755B7
		public static FeatureCalculationContext Create(IReadOnlyList<State> specInputs, IReadOnlyList<State> additionalInputs, IFeatureOptions options)
		{
			return new FeatureCalculationContext(specInputs ?? FeatureCalculationContext.NoStates, additionalInputs ?? FeatureCalculationContext.NoStates, options);
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000773D3 File Offset: 0x000755D3
		public static FeatureCalculationContext Empty { get; } = FeatureCalculationContext.Create(null, null, null);

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000773DA File Offset: 0x000755DA
		public int AllInputsCount
		{
			get
			{
				if (!this._allInputsLazy.IsValueCreated)
				{
					return this.MaterializeSpecInputs().Count + this.AdditionalInputs.Count;
				}
				return this.AllInputs.Count;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0007740C File Offset: 0x0007560C
		public FeatureCalculationContext TransformForChild(ProgramNode program, int childIndex)
		{
			GrammarRule grammarRule = program.GrammarRule;
			ConversionRule conversionRule = grammarRule as ConversionRule;
			if (conversionRule != null)
			{
				return this.TransformForConversionRule(conversionRule);
			}
			IInputTransformer inputTransformer = grammarRule.GetInputTransformer(program, childIndex);
			if (inputTransformer == null)
			{
				return this;
			}
			return new FeatureCalculationContext(this._inputs, inputTransformer, this.Options);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00077454 File Offset: 0x00075654
		public FeatureCalculationContext TransformForConversionRule(ConversionRule conversionRule)
		{
			if (conversionRule.Substitutions == null || conversionRule.Substitutions.Count == 0)
			{
				return this;
			}
			return new FeatureCalculationContext((from state in this.MaterializeSpecInputs()
				select conversionRule.ApplySubstitutions(state, false)).ToList<State>(), this.AdditionalInputs, this.Options.WithAddedSubstitutions(conversionRule.Substitutions));
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000774CC File Offset: 0x000756CC
		public FeatureCalculationContext WithAdditionalTransform(IInputTransformer transform)
		{
			return new FeatureCalculationContext(this._inputs.GetBaseContextInputsForTransform(), transform, this.Options);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000774E5 File Offset: 0x000756E5
		public bool Equals(FeatureCalculationContext other)
		{
			return other != null && (this == other || (this.Options.Equals(other.Options) && this._inputs.Equals(other._inputs)));
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00077518 File Offset: 0x00075718
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FeatureCalculationContext)obj)));
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00077546 File Offset: 0x00075746
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?(this._inputs.GetHashCode() ^ this.Options.GetHashCode());
			}
			return this._hashCode.Value;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00077582 File Offset: 0x00075782
		private bool EqualsOnComputedInputs(FeatureCalculationContext other)
		{
			return other == this || (other != null && this.Options.Equals(other.Options) && this._inputs.EqualsOnComputedInputs(other._inputs));
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000775B5 File Offset: 0x000757B5
		private int GetHashCodeOnComputedInputs()
		{
			if (this._hashCodeOnComputedInputs == null)
			{
				this._hashCodeOnComputedInputs = new int?(this._inputs.GetHashCodeOnComputedInputs() ^ this.Options.GetHashCode());
			}
			return this._hashCodeOnComputedInputs.Value;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000775F4 File Offset: 0x000757F4
		public IReadOnlyList<State> GetInputs(InputKind kind)
		{
			switch (kind)
			{
			case InputKind.Spec:
				return this.MaterializeSpecInputs();
			case InputKind.Additional:
				return this.AdditionalInputs;
			case InputKind.All:
				return this.AllInputs;
			default:
				throw new ArgumentException("Invalid InputKind: " + kind.ToString());
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00077648 File Offset: 0x00075848
		public static IEqualityComparer<FeatureCalculationContext> EqualityComparerOnComputedInputs { get; } = new FeatureCalculationContext.ComputedInputsEqualityComparer();

		// Token: 0x04001474 RID: 5236
		private static readonly IReadOnlyList<State> NoStates = new State[0];

		// Token: 0x04001475 RID: 5237
		private int? _hashCode;

		// Token: 0x04001476 RID: 5238
		private int? _hashCodeOnComputedInputs;

		// Token: 0x04001477 RID: 5239
		private readonly FeatureCalculationContextInputs _inputs;

		// Token: 0x04001479 RID: 5241
		private readonly Lazy<IReadOnlyList<State>> _allInputsLazy;

		// Token: 0x020007C9 RID: 1993
		private class ComputedInputsEqualityComparer : IEqualityComparer<FeatureCalculationContext>
		{
			// Token: 0x06002A83 RID: 10883 RVA: 0x000776CE File Offset: 0x000758CE
			public bool Equals(FeatureCalculationContext x, FeatureCalculationContext y)
			{
				return x == y || (x != null && x.EqualsOnComputedInputs(y));
			}

			// Token: 0x06002A84 RID: 10884 RVA: 0x000776E2 File Offset: 0x000758E2
			public int GetHashCode(FeatureCalculationContext obj)
			{
				return obj.GetHashCodeOnComputedInputs();
			}
		}
	}
}
