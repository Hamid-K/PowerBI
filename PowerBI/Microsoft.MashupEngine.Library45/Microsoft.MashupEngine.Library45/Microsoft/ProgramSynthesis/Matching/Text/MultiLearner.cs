using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011B3 RID: 4531
	public class MultiLearner : ProgramLearner<MultiProgram, IEnumerable<string>, IEnumerable<bool>>
	{
		// Token: 0x060086E7 RID: 34535 RVA: 0x001C52FC File Offset: 0x001C34FC
		public MultiLearner(Feature<double> scoreFeature)
			: base(false, false)
		{
			this.ScoreFeature = scoreFeature;
			this._build = Language.Build;
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x060086E8 RID: 34536 RVA: 0x001C5318 File Offset: 0x001C3518
		public static MultiLearner Instance { get; } = new MultiLearner(Learner.Instance.ScoreFeature);

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x060086E9 RID: 34537 RVA: 0x001C531F File Offset: 0x001C351F
		public override Feature<double> ScoreFeature { get; }

		// Token: 0x060086EA RID: 34538 RVA: 0x001C5328 File Offset: 0x001C3528
		private ProgramSetBuilder<multi_result> LearnImpl(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<bool>>> constraints, int? k = 1, CancellationToken cancel = default(CancellationToken))
		{
			IList<Constraint<IEnumerable<string>, IEnumerable<bool>>> list = (constraints as IList<Constraint<IEnumerable<string>, IEnumerable<bool>>>) ?? constraints.ToList<Constraint<IEnumerable<string>, IEnumerable<bool>>>();
			List<Constraint<string, bool>> otherConstraints = (from constraint in list.OfType<AllowedTokens<IEnumerable<string>, IEnumerable<bool>>>()
				select new AllowedTokens<string, bool>(constraint.Tokens)).ToList<Constraint<string, bool>>();
			otherConstraints.AddRange(from constraint in list.OfType<OutlierLimit<IEnumerable<string>, IEnumerable<bool>>>()
				select new OutlierLimit<string, bool>(constraint.MaxOutlierRate));
			otherConstraints.AddRange(from constraint in list.OfType<ClusteringMethod<IEnumerable<string>, IEnumerable<bool>>>()
				select new ClusteringMethod<string, bool>(constraint.Algorithm));
			if (list.OfType<Example<IEnumerable<string>, IEnumerable<bool>>>().Any(delegate(Example<IEnumerable<string>, IEnumerable<bool>> ex)
			{
				IEnumerable<bool> output = ex.Output;
				bool flag;
				if (output == null)
				{
					flag = false;
				}
				else
				{
					flag = output.Any((bool b) => !b);
				}
				return flag || !ex.IsSoft;
			}))
			{
				throw new NotImplementedException("Negative and non-soft examples are unsupported.");
			}
			multi_result_matches multi_result_matches = (from column in (from example in list.OfType<Example<IEnumerable<string>, IEnumerable<bool>>>()
					select example.Input).Transpose<string>().Reverse<IEnumerable<string>>()
				select Learner.Instance.LearnTopK(otherConstraints, 1, column, default(CancellationToken))).Select(delegate(ProgramCollection<Program, string, bool, double> programs)
			{
				Program program = programs.FirstOrDefault<Program>();
				if (program == null)
				{
					return null;
				}
				return program.ProgramNode;
			}).Select(delegate(ProgramNode programNode)
			{
				GrammarBuilders.Nodes.NodeCast cast = this._build.Node.Cast;
				LetNode letNode = programNode as LetNode;
				return cast.disjunctive_match((letNode != null) ? letNode.BodyNode : null);
			}).Aggregate(this._build.Node.Rule.Nil(this._build.Node.Variable.sRegions), (multi_result_matches resultNode, disjunctive_match node) => this._build.Node.Rule.LetHead(this._build.Node.Rule.Head(this._build.Node.Variable.sRegions), this._build.Node.Rule.LetTail(this._build.Node.Rule.Tail(this._build.Node.Variable.sRegions), this._build.Node.Rule.MatchColumns(node, resultNode))));
			multi_result multi_result = this._build.Node.Rule.LetMultiResult(this._build.Node.Cast.inputSRegions(new VariableNode(this._build.Symbol.inputSRegions)), multi_result_matches);
			return ProgramSetBuilder.List<multi_result>(new multi_result[] { multi_result });
		}

		// Token: 0x060086EB RID: 34539 RVA: 0x001C5534 File Offset: 0x001C3734
		protected override ProgramCollection<MultiProgram, IEnumerable<string>, IEnumerable<bool>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<bool>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<IEnumerable<string>> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return new ProgramCollection<MultiProgram, IEnumerable<string>, IEnumerable<bool>, TFeatureValue>(this.LearnImpl(constraints, new int?(k), cancel).Set.RealizedPrograms.Select((ProgramNode p) => new MultiProgram(p)), null, null, null);
		}

		// Token: 0x060086EC RID: 34540 RVA: 0x001C5588 File Offset: 0x001C3788
		public override ProgramSet LearnAll(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<bool>>> constraints, IEnumerable<IEnumerable<string>> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnImpl(constraints, null, cancel).Set;
		}

		// Token: 0x060086ED RID: 34541 RVA: 0x001C4F27 File Offset: 0x001C3127
		public override Constraint<IEnumerable<string>, IEnumerable<bool>> BuildNegativeConstraint(IEnumerable<string> input, IEnumerable<bool> output, bool isSoft)
		{
			throw new NotImplementedException("Negative examples are unsupported.");
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x001C55AB File Offset: 0x001C37AB
		public override Constraint<IEnumerable<string>, IEnumerable<bool>> BuildPositiveConstraint(IEnumerable<string> input, IEnumerable<bool> output, bool isSoft)
		{
			if (!isSoft)
			{
				throw new Exception("Non-soft example are unsupported.");
			}
			return base.BuildPositiveConstraint(input, output, isSoft);
		}

		// Token: 0x040037BC RID: 14268
		private readonly GrammarBuilders _build;
	}
}
