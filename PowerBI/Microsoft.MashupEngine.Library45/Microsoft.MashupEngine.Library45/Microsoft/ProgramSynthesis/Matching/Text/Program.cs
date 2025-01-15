using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011BA RID: 4538
	public class Program : Program<string, bool>
	{
		// Token: 0x0600870D RID: 34573 RVA: 0x001C58C0 File Offset: 0x001C3AC0
		internal Program(ProgramNode node)
			: this(node, node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null))
		{
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x0600870E RID: 34574 RVA: 0x001C58DA File Offset: 0x001C3ADA
		public IReadOnlyDictionary<ProgramNode, IReadOnlyList<string>> Examples { get; }

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x0600870F RID: 34575 RVA: 0x001C58E2 File Offset: 0x001C3AE2
		public IReadOnlyDictionary<ProgramNode, uint> Sizes { get; }

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06008710 RID: 34576 RVA: 0x001C58EA File Offset: 0x001C3AEA
		public IReadOnlyList<string> OutlierSamples { get; }

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06008711 RID: 34577 RVA: 0x001C58F2 File Offset: 0x001C3AF2
		public uint NumberOfInputs { get; }

		// Token: 0x06008712 RID: 34578 RVA: 0x001C58FC File Offset: 0x001C3AFC
		internal Program(ProgramNode node, IReadOnlyDictionary<ProgramNode, IReadOnlyList<string>> examples, IReadOnlyDictionary<ProgramNode, uint> sizes, IReadOnlyList<string> outlierSamples, uint numberOfInputs)
			: this(node)
		{
			this.Examples = examples;
			this.Sizes = sizes;
			this.OutlierSamples = outlierSamples;
			this.NumberOfInputs = numberOfInputs;
			this._disjunctsLazy = new Lazy<IReadOnlyList<ProgramNode>>(() => base.ProgramNode.GetFeatureValue<IEnumerable<ProgramNode>>(new DisjunctsFeature(Language.Grammar), null).ToList<ProgramNode>());
			this._labelsLazy = new Lazy<IReadOnlyList<MatchingLabel>>(() => this.Disjuncts.Select((ProgramNode disjunct) => disjunct.AcceptVisitor<MatchingLabel>(new MatchingLabelCollector(Language.Build))).ToList<MatchingLabel>());
		}

		// Token: 0x06008713 RID: 34579 RVA: 0x001C595C File Offset: 0x001C3B5C
		internal Program(ProgramNode node, double score)
			: base(node, score, null)
		{
			this.LabeledProgramNode = base.ProgramNode.GetFeatureValue<ProgramNode>(Learner.Instance.LabelFeature, null);
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06008714 RID: 34580 RVA: 0x001C5983 File Offset: 0x001C3B83
		public ProgramNode LabeledProgramNode { get; }

		// Token: 0x06008715 RID: 34581 RVA: 0x001C598C File Offset: 0x001C3B8C
		public override bool Run(string input)
		{
			object obj = base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, new SuffixRegion(input, 0U)));
			return obj != null && (bool)obj;
		}

		// Token: 0x06008716 RID: 34582 RVA: 0x001C59C6 File Offset: 0x001C3BC6
		public MatchingLabel GetLabel(string input)
		{
			return this.LabeledProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, new SuffixRegion(input, 0U))) as MatchingLabel;
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06008717 RID: 34583 RVA: 0x001C59EE File Offset: 0x001C3BEE
		public IReadOnlyList<ProgramNode> Disjuncts
		{
			get
			{
				return this._disjunctsLazy.Value;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06008718 RID: 34584 RVA: 0x001C59FB File Offset: 0x001C3BFB
		public IReadOnlyList<MatchingLabel> Labels
		{
			get
			{
				return this._labelsLazy.Value;
			}
		}

		// Token: 0x06008719 RID: 34585 RVA: 0x001C5A08 File Offset: 0x001C3C08
		public override string Serialize(ASTSerializationSettings serializationSettings)
		{
			if (!serializationSettings.HasXml)
			{
				return base.Serialize(serializationSettings);
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("<Program score=\"{0:R}\">{1}</Program>", new object[]
			{
				base.Score,
				base.ProgramNode.PrintAST(serializationSettings)
			}));
		}

		// Token: 0x040037D5 RID: 14293
		private readonly Lazy<IReadOnlyList<ProgramNode>> _disjunctsLazy;

		// Token: 0x040037D6 RID: 14294
		private readonly Lazy<IReadOnlyList<MatchingLabel>> _labelsLazy;
	}
}
