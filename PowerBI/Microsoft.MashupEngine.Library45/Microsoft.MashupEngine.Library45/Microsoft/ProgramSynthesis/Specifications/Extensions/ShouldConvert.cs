using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Specifications.Extensions
{
	// Token: 0x0200037B RID: 891
	public static class ShouldConvert
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0003A57E File Offset: 0x0003877E
		public static ShouldConvert.SpecBuilder Given(Grammar grammar)
		{
			return new ShouldConvert.SpecBuilder(grammar);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003A588 File Offset: 0x00038788
		private static Grammar GetGrammar(Spec spec)
		{
			return spec.ProvidedInputs.First<State>().Bindings.First<KeyValuePair<Symbol, object>>().Key.Grammar;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003A5B7 File Offset: 0x000387B7
		public static ExampleSpec To<TInput, TOutput>(this ExampleSpec spec, TInput input, TOutput output)
		{
			return spec.To(State.CreateForLearning(ShouldConvert.GetGrammar(spec).InputSymbol, input), output);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0003A5D6 File Offset: 0x000387D6
		public static ExampleSpec To<TOutput>(this ExampleSpec spec, State input, TOutput output)
		{
			return new ExampleSpec(spec.Examples.ToImmutableDictionary<State, object>().Add(input, output));
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0003A5F4 File Offset: 0x000387F4
		public static DisjunctiveExamplesSpec ToAnyOf<TInput, TOutput>(this DisjunctiveExamplesSpec spec, TInput input, IEnumerable<TOutput> outputs)
		{
			return spec.ToAnyOf(State.CreateForLearning(ShouldConvert.GetGrammar(spec).InputSymbol, input), outputs);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0003A613 File Offset: 0x00038813
		public static DisjunctiveExamplesSpec ToAnyOf<TOutput>(this DisjunctiveExamplesSpec spec, State input, IEnumerable<TOutput> outputs)
		{
			return DisjunctiveExamplesSpec.From(spec.DisjunctiveExamples.ToImmutableDictionary<State, IEnumerable<object>>().Add(input, outputs.Cast<object>()));
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003A631 File Offset: 0x00038831
		public static SubsequenceSpec ToSupersetOf<TInput, TOutput>(this SubsequenceSpec spec, TInput input, IEnumerable<TOutput> subset)
		{
			return ShouldConvert.ToSupersetOf<TOutput>(spec, State.CreateForLearning(ShouldConvert.GetGrammar(spec).InputSymbol, input), subset);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0003A650 File Offset: 0x00038850
		public static SubsequenceSpec ToSupersetOf<TOutput>(SubsequenceSpec spec, State input, IEnumerable<TOutput> subset)
		{
			return new SubsequenceSpec(spec.PositiveExamples.ToImmutableDictionary<State, IEnumerable<object>>().Add(input, subset.Cast<object>()));
		}

		// Token: 0x0200037C RID: 892
		public class SpecBuilder
		{
			// Token: 0x060013EA RID: 5098 RVA: 0x0003A66E File Offset: 0x0003886E
			public SpecBuilder(Grammar grammar)
			{
				this._grammar = grammar;
			}

			// Token: 0x060013EB RID: 5099 RVA: 0x0003A67D File Offset: 0x0003887D
			public ExampleSpec To<TInput, TOutput>(TInput input, TOutput output)
			{
				return this.To<TOutput>(State.CreateForLearning(this._grammar.InputSymbol, input), output);
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x0003A69C File Offset: 0x0003889C
			public ExampleSpec To<TOutput>(State input, TOutput output)
			{
				Dictionary<State, object> dictionary = new Dictionary<State, object>();
				dictionary[input] = output;
				return new ExampleSpec(dictionary);
			}

			// Token: 0x060013ED RID: 5101 RVA: 0x0003A6C2 File Offset: 0x000388C2
			public DisjunctiveExamplesSpec ToAnyOf<TInput, TOutput>(TInput input, IEnumerable<TOutput> outputs)
			{
				return this.ToAnyOf<TOutput>(State.CreateForLearning(this._grammar.InputSymbol, input), outputs);
			}

			// Token: 0x060013EE RID: 5102 RVA: 0x0003A6E4 File Offset: 0x000388E4
			public DisjunctiveExamplesSpec ToAnyOf<TOutput>(State input, IEnumerable<TOutput> outputs)
			{
				Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
				dictionary[input] = outputs.Cast<object>();
				return DisjunctiveExamplesSpec.From(dictionary);
			}

			// Token: 0x060013EF RID: 5103 RVA: 0x0003A70A File Offset: 0x0003890A
			public SubsequenceSpec ToSupersequenceOf<TInput, TOutput>(TInput input, IEnumerable<TOutput> subset)
			{
				return this.ToSupersequenceOf<TOutput>(State.CreateForLearning(this._grammar.InputSymbol, input), subset);
			}

			// Token: 0x060013F0 RID: 5104 RVA: 0x0003A729 File Offset: 0x00038929
			public SubsequenceSpec ToSupersequenceOf<TOutput>(State input, IEnumerable<TOutput> subset)
			{
				return new SubsequenceSpec(input, subset.Cast<object>(), null);
			}

			// Token: 0x040009E6 RID: 2534
			private readonly Grammar _grammar;
		}
	}
}
