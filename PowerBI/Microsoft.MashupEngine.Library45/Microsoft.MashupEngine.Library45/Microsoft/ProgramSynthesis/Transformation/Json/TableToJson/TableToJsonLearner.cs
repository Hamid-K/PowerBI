using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Json.Build;
using Microsoft.ProgramSynthesis.Transformation.Json.Learning;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Transformation.Json.TableToJson.Constraint;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson
{
	// Token: 0x02001A73 RID: 6771
	public class TableToJsonLearner : ProgramLearner<TableToJsonProgram, Table, Newtonsoft.Json.Linq.JToken>
	{
		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x0600DEFF RID: 57087 RVA: 0x002F557E File Offset: 0x002F377E
		public static TableToJsonLearner Instance { get; } = new TableToJsonLearner(Language.Grammar);

		// Token: 0x0600DF00 RID: 57088 RVA: 0x002F5585 File Offset: 0x002F3785
		private TableToJsonLearner(Grammar grammar)
			: base(false, true)
		{
			this._build = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x0600DF01 RID: 57089 RVA: 0x002F55AB File Offset: 0x002F37AB
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x0600DF02 RID: 57090 RVA: 0x002F55B4 File Offset: 0x002F37B4
		protected override ProgramCollection<TableToJsonProgram, Table, Newtonsoft.Json.Linq.JToken, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<Table, Newtonsoft.Json.Linq.JToken>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<Table> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return ProgramCollection<TableToJsonProgram, Table, Newtonsoft.Json.Linq.JToken, TFeatureValue>.From(this.LearnImpl(constraints, additionalInputs, cancel).Prune(new int?(k), numRandomProgramsToInclude, feature, null, null, samplingStrategy, new Random(0), null), (ProgramNode x) => new TableToJsonProgram(x), feature);
		}

		// Token: 0x0600DF03 RID: 57091 RVA: 0x002F560A File Offset: 0x002F380A
		public override ProgramSet LearnAll(IEnumerable<Constraint<Table, Newtonsoft.Json.Linq.JToken>> constraints, IEnumerable<Table> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnImpl(constraints, additionalInputs, cancel);
		}

		// Token: 0x0600DF04 RID: 57092 RVA: 0x002F5618 File Offset: 0x002F3818
		private ProgramSet LearnImpl(IEnumerable<Constraint<Table, Newtonsoft.Json.Linq.JToken>> constraints, IEnumerable<Table> additionalInputs, CancellationToken cancel)
		{
			List<AutoTransform> list;
			List<TableToJsonExample> list2;
			TableToJsonLearner.VerifyAndSplitConstraints(constraints, out list, out list2);
			if (list2.IsAny<TableToJsonExample>())
			{
				return this.LearnFromExamples(list2, null, cancel);
			}
			ProgramNode programNode = this.LearnAutoTransform(list);
			if (!(programNode == null))
			{
				return new DirectProgramSet(Language.Grammar.StartSymbol, new ProgramNode[] { programNode });
			}
			return ProgramSet.Empty(Language.Grammar.StartSymbol);
		}

		// Token: 0x0600DF05 RID: 57093 RVA: 0x002F5684 File Offset: 0x002F3884
		private static void VerifyAndSplitConstraints(IEnumerable<Constraint<Table, Newtonsoft.Json.Linq.JToken>> constraints, out List<AutoTransform> autoConstraints, out List<TableToJsonExample> exampleConstraints)
		{
			if (constraints == null)
			{
				throw new ArgumentNullException("constraints");
			}
			autoConstraints = new List<AutoTransform>();
			exampleConstraints = new List<TableToJsonExample>();
			foreach (Constraint<Table, Newtonsoft.Json.Linq.JToken> constraint in constraints)
			{
				AutoTransform autoTransform = constraint as AutoTransform;
				if (autoTransform != null)
				{
					autoConstraints.Add(autoTransform);
				}
				else
				{
					TableToJsonExample tableToJsonExample = constraint as TableToJsonExample;
					if (tableToJsonExample == null)
					{
						throw new ArgumentOutOfRangeException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown constraint type {0}. Only accept {1} and {2}", new object[]
						{
							constraint.GetType(),
							typeof(AutoTransform),
							typeof(TableToJsonExample)
						})), "constraint");
					}
					exampleConstraints.Add(tableToJsonExample);
				}
			}
			if (autoConstraints.Count == 0 && exampleConstraints.Count == 0)
			{
				throw new ArgumentException("No applicable constraint!", "constraints");
			}
			if (autoConstraints.IsAny<AutoTransform>() && exampleConstraints.IsAny<TableToJsonExample>())
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Conflicting constraints. Can't have both {0} and {1}", new object[]
				{
					typeof(AutoTransform),
					typeof(TableToJsonExample)
				})), "constraints");
			}
		}

		// Token: 0x0600DF06 RID: 57094 RVA: 0x002F57C8 File Offset: 0x002F39C8
		private ProgramSet LearnFromExamples(IEnumerable<TableToJsonExample> examples, int? k = 1, CancellationToken cancel = default(CancellationToken))
		{
			Grammar grammar = Language.Grammar;
			ExampleSpec exampleSpec = new ExampleSpec(examples.ToDictionary((TableToJsonExample e) => State.CreateForLearning(grammar.InputSymbol, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(e.Input.ToJArray())), (TableToJsonExample e) => Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(e.Output)));
			Witnesses witnesses = new Witnesses(Language.Grammar);
			SynthesisEngine synthesisEngine = new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				UseThreads = false
			}, null);
			LearningTask learningTask = ((k != null) ? new LearningTask(grammar.StartSymbol, exampleSpec, k.Value, this.ScoreFeature, null) : new LearningTask(grammar.StartSymbol, exampleSpec));
			ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
			LogListener logListener = synthesisEngine.Configuration.LogListener;
			if (logListener != null)
			{
				logListener.SaveLogToXML("table2json-log.xml");
			}
			return programSet;
		}

		// Token: 0x0600DF07 RID: 57095 RVA: 0x002F58C8 File Offset: 0x002F3AC8
		private ProgramNode LearnAutoTransform(IEnumerable<AutoTransform> autoTransforms)
		{
			HashSet<string> hashSet = null;
			foreach (AutoTransform autoTransform in autoTransforms)
			{
				if (hashSet == null)
				{
					hashSet = autoTransform.Input.ColumnNames.ConvertToHashSet<string>();
				}
				else if (hashSet.Intersect(autoTransform.Input.ColumnNames).Count<string>() != hashSet.Count)
				{
					return null;
				}
			}
			return this._build.Node.UnnamedConversion.output_v(this._build.Node.Variable.v).Node;
		}

		// Token: 0x040054A5 RID: 21669
		private readonly GrammarBuilders _build;
	}
}
