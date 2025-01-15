using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A7E RID: 6782
	public class Learner : ProgramLearner<Program, ITable<object>, ITable<object>>, ICachefulObject
	{
		// Token: 0x0600DF31 RID: 57137 RVA: 0x002F5D27 File Offset: 0x002F3F27
		private Learner()
			: base(false, true)
		{
			this._suggestionLearner = new SuggestionLearner(this._builder);
		}

		// Token: 0x0600DF32 RID: 57138 RVA: 0x002F5D62 File Offset: 0x002F3F62
		private Learner(SuggestionLearner suggestionLearner)
			: base(false, true)
		{
			this._suggestionLearner = suggestionLearner;
		}

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x0600DF33 RID: 57139 RVA: 0x002F5D93 File Offset: 0x002F3F93
		public static Learner Instance { get; } = new Learner();

		// Token: 0x0600DF34 RID: 57140 RVA: 0x002F5D9C File Offset: 0x002F3F9C
		public override ProgramSet LearnAll(IEnumerable<Constraint<ITable<object>, ITable<object>>> constraints, IEnumerable<ITable<object>> inputs = null, CancellationToken cancel = default(CancellationToken))
		{
			ITable<object> table = inputs.SingleOrDefault<ITable<object>>();
			if (table == null)
			{
				throw new ArgumentException("Expected an input table from ", "inputs");
			}
			ITable<object> table2 = table;
			this._options = Learner.GetOptions(constraints);
			ProgramSetBuilder<@out> programSetBuilder = this._suggestionLearner.GenerateSuggestions(table2, this._options);
			if (programSetBuilder == null)
			{
				return ProgramSet.Empty(Language.Grammar.StartSymbol);
			}
			return programSetBuilder.Set;
		}

		// Token: 0x0600DF35 RID: 57141 RVA: 0x002F5DFD File Offset: 0x002F3FFD
		public ICachefulObject CloneWithCurrentCacheState()
		{
			return new Learner(this._suggestionLearner);
		}

		// Token: 0x0600DF36 RID: 57142 RVA: 0x002F5E0A File Offset: 0x002F400A
		public void ClearCaches()
		{
			this._suggestionLearner = new SuggestionLearner(this._builder);
		}

		// Token: 0x0600DF37 RID: 57143 RVA: 0x002F5E20 File Offset: 0x002F4020
		private static Options GetOptions(IEnumerable<Constraint<ITable<object>, ITable<object>>> constraints)
		{
			Options options = new Options();
			foreach (Constraint<ITable<object>, ITable<object>> constraint in constraints)
			{
				IOptionConstraint<Options> optionConstraint = constraint as IOptionConstraint<Options>;
				if (optionConstraint != null)
				{
					optionConstraint.SetOptions(options);
				}
			}
			return options;
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x0600DF38 RID: 57144 RVA: 0x002F5E78 File Offset: 0x002F4078
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x0600DF39 RID: 57145 RVA: 0x002F5E80 File Offset: 0x002F4080
		protected override ProgramCollection<Program, ITable<object>, ITable<object>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<ITable<object>, ITable<object>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<ITable<object>> inputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return new ProgramCollection<Program, ITable<object>, ITable<object>, TFeatureValue>(from p in (from a in this.GetRankedProgramsWithScores(this.LearnAll(constraints, inputs, default(CancellationToken)).RealizedPrograms, inputs, cancel)
					orderby a.Item2 descending
					select a.Item1).Take(k)
				select Loader.Instance.Create(p), null, null, null);
		}

		// Token: 0x0600DF3A RID: 57146 RVA: 0x002F5F28 File Offset: 0x002F4128
		internal IEnumerable<Tuple<ProgramNode, double>> GetRankedProgramsWithScores(IEnumerable<ProgramNode> programNodes, IEnumerable<ITable<object>> inputs, CancellationToken cancel)
		{
			ITable<object> table = inputs.SingleOrDefault<ITable<object>>();
			return this._suggestionLearner.GetRankedProgramsWithScores(programNodes, table, this._options, cancel);
		}

		// Token: 0x040054B1 RID: 21681
		private SuggestionLearner _suggestionLearner;

		// Token: 0x040054B2 RID: 21682
		private readonly GrammarBuilders _builder = GrammarBuilders.Instance(Language.Grammar);

		// Token: 0x040054B3 RID: 21683
		private Options _options;
	}
}
