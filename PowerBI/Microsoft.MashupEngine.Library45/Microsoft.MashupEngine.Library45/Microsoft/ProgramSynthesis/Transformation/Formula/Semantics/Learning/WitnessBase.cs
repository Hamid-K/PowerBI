using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x020016B1 RID: 5809
	internal abstract class WitnessBase : DomainLearningLogic
	{
		// Token: 0x0600C1E1 RID: 49633 RVA: 0x0029C638 File Offset: 0x0029A838
		internal WitnessBase(Grammar grammar, LearnOptions options, IEnumerable<Example<IRow, object>> examples, IEnumerable<IRow> additionalInputs, CancellationToken cancellationToken, LearnDebugTrace debugTrace)
			: base(grammar)
		{
			this.CancellationToken = cancellationToken;
			this.Builder = new GrammarBuilders(grammar);
			this.Options = options;
			this.Examples = examples.ToReadOnlyList<Example<IRow, object>>();
			this.AdditionalInputs = additionalInputs.ToReadOnlyList<IRow>();
			this.DebugTrace = debugTrace;
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x0600C1E2 RID: 49634 RVA: 0x0029C68F File Offset: 0x0029A88F
		public IReadOnlyCollection<IRow> AdditionalInputs { get; }

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x0600C1E3 RID: 49635 RVA: 0x0029C697 File Offset: 0x0029A897
		protected GrammarBuilders Builder { get; }

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x0600C1E4 RID: 49636 RVA: 0x0029C69F File Offset: 0x0029A89F
		protected CancellationToken CancellationToken { get; }

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x0600C1E5 RID: 49637 RVA: 0x0029C6A7 File Offset: 0x0029A8A7
		protected IReadOnlyCollection<Example<IRow, object>> Examples { get; }

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x0600C1E6 RID: 49638 RVA: 0x0029C6AF File Offset: 0x0029A8AF
		protected LearnOptions Options { get; }

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x0600C1E7 RID: 49639 RVA: 0x0029C6B7 File Offset: 0x0029A8B7
		internal LearnDebugTrace DebugTrace { get; }

		// Token: 0x0600C1E8 RID: 49640 RVA: 0x0029C6C0 File Offset: 0x0029A8C0
		public DisjunctiveExamplesSpec WitnessOutput<TOperatorOutput, TArgumentInput>(GrammarRule rule, DisjunctiveExamplesSpec spec, Func<WitnessContext<TOperatorOutput>, IEnumerable<TArgumentInput>> argumentTransform, ExampleSpec dependentSpec1 = null, ExampleSpec dependentSpec2 = null, ExampleSpec dependentSpec3 = null, [CallerMemberName] string witnessName = null)
		{
			this.CancellationToken.ThrowIfCancellationRequested();
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				List<object> list = new List<object>();
				foreach (TOperatorOutput toperatorOutput in spec.DisjunctiveExamples[state].OfType<TOperatorOutput>())
				{
					WitnessContext<TOperatorOutput> witnessContext = this.Context<TOperatorOutput>(state, spec, dependentSpec1, dependentSpec2, dependentSpec3);
					witnessName = ((witnessName != null) ? witnessName.Replace("Witness", "") : null);
					LearnDebugTrace debugTrace = this.DebugTrace;
					TimedEvent timedEvent;
					if (debugTrace != null)
					{
						string text = witnessName;
						bool flag = witnessName == "FormatDateTimeSubject" || witnessName == "FormatNumberSubject" || witnessName == "ConcatPrefix";
						timedEvent = debugTrace.StartTimedEvent("Witness", text, flag, true);
					}
					else
					{
						timedEvent = null;
					}
					using (TimedEvent timedEvent2 = timedEvent)
					{
						witnessContext.OperatorOutput = toperatorOutput;
						IEnumerable<TArgumentInput> enumerable = argumentTransform(witnessContext);
						IEnumerable<object> enumerable2 = ((enumerable != null) ? enumerable.Cast<object>().Distinct<object>().ToList<object>() : null);
						if (timedEvent2 != null)
						{
							timedEvent2.Stop();
						}
						LearnDebugTrace debugTrace2 = this.DebugTrace;
						if (debugTrace2 != null)
						{
							List<WitnessEvent> witnessEvents = debugTrace2.WitnessEvents;
							WitnessEvent witnessEvent = new WitnessEvent();
							witnessEvent.WitnessName = ((witnessName != null) ? witnessName.Replace("Witness", "") : null);
							witnessEvent.InputRow = witnessContext.InputRow;
							witnessEvent.OperatorOutput = toperatorOutput;
							witnessEvent.ArgumentInputs = enumerable2;
							witnessEvent.DependentArg1 = witnessContext.DependentArg1;
							witnessEvent.DependentArg2 = witnessContext.DependentArg2;
							witnessEvent.DependentArg3 = witnessContext.DependentArg3;
							int witnessDebugItemOrder = this._witnessDebugItemOrder;
							this._witnessDebugItemOrder = witnessDebugItemOrder + 1;
							witnessEvent.Order = witnessDebugItemOrder;
							witnessEvents.Add(witnessEvent);
						}
						this.CancellationToken.ThrowIfCancellationRequested();
						bool flag = !(((enumerable2 != null) ? new bool?(enumerable2.Any<object>()) : null) ?? false);
						if (!flag)
						{
							list.AddRange(enumerable2);
						}
					}
				}
				if (!list.Any<object>())
				{
					return null;
				}
				dictionary[state] = list;
			}
			this.CancellationToken.ThrowIfCancellationRequested();
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600C1E9 RID: 49641 RVA: 0x0029C990 File Offset: 0x0029AB90
		private WitnessContext<TOperatorOutput> Context<TOperatorOutput>(State state, DisjunctiveExamplesSpec spec, ExampleSpec dependentSpec1 = null, ExampleSpec dependentSpec2 = null, ExampleSpec dependentSpec3 = null)
		{
			List<WitnessDisjunctiveContext<TOperatorOutput>> list = (from providedInput in spec.ProvidedInputs
				let inputRow = providedInput[this.Grammar.InputSymbol] as IRow
				let operatorOutputs = spec.DisjunctiveExamples[providedInput].OfType<TOperatorOutput>()
				select new WitnessDisjunctiveContext<TOperatorOutput>
				{
					InputRow = inputRow,
					OperatorOutputs = operatorOutputs.Distinct<TOperatorOutput>().ToList<TOperatorOutput>()
				}).ToList<WitnessDisjunctiveContext<TOperatorOutput>>();
			IRow inputRow2 = state[base.Grammar.InputSymbol] as IRow;
			return new WitnessContext<TOperatorOutput>
			{
				Example = this.Examples.FirstOrDefault((Example<IRow, object> e) => e.Input.Equals(inputRow2)),
				DisjunctiveContexts = list,
				DependentArg1 = ((dependentSpec1 != null) ? dependentSpec1.Examples.GetOrDefault(state, null) : null),
				DependentArg2 = ((dependentSpec2 != null) ? dependentSpec2.Examples.GetOrDefault(state, null) : null),
				DependentArg3 = ((dependentSpec3 != null) ? dependentSpec3.Examples.GetOrDefault(state, null) : null)
			};
		}

		// Token: 0x04004B15 RID: 19221
		private int _witnessDebugItemOrder = 1;
	}
}
