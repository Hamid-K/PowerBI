using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014DF RID: 5343
	public class Program : Program<IRow, object>, IEquatable<Program>
	{
		// Token: 0x0600A38C RID: 41868 RVA: 0x0022A5B0 File Offset: 0x002287B0
		public Program(ProgramNode programNode, double? score = null, RankingDebugTrace rankingDebugTrace = null)
			: base(programNode, score ?? programNode.GetFeatureValue<double>(Program.ScoreFeature, null), null)
		{
			this._rankingDebugInfo = rankingDebugTrace;
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x0600A38D RID: 41869 RVA: 0x0022A5EC File Offset: 0x002287EC
		// (set) Token: 0x0600A38E RID: 41870 RVA: 0x0022A5F4 File Offset: 0x002287F4
		public LearnConfidenceResult LearnConfidence { get; private set; }

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x0600A38F RID: 41871 RVA: 0x0022A5FD File Offset: 0x002287FD
		// (set) Token: 0x0600A390 RID: 41872 RVA: 0x0022A605 File Offset: 0x00228805
		public ProgramMeta Meta { get; private set; }

		// Token: 0x0600A391 RID: 41873 RVA: 0x0022A60E File Offset: 0x0022880E
		public bool Equals(Program other)
		{
			return other != null && base.ProgramNode.Equals(other.ProgramNode);
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x0022A62C File Offset: 0x0022882C
		public override object Run(IRow input)
		{
			State state = State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, input);
			return base.ProgramNode.Invoke(state);
		}

		// Token: 0x0600A393 RID: 41875 RVA: 0x0022A65C File Offset: 0x0022885C
		public string ToDetailString(bool? indent = null, bool includeScore = true)
		{
			string text;
			if (indent == null)
			{
				text = this.ToString();
				if (text.Length > 100)
				{
					text = this.Serialize(ASTSerializationSettings.HumanReadable.WithIndent);
				}
			}
			else
			{
				text = (indent.Value ? this.Serialize(ASTSerializationSettings.HumanReadable.WithIndent) : this.ToString());
			}
			if (!includeScore)
			{
				return text;
			}
			string text2 = ((this._rankingDebugInfo != null) ? this._rankingDebugInfo.ToString(base.ProgramNode) : string.Format("[{0,11:N4}]", base.Score));
			return text + Environment.NewLine + text2;
		}

		// Token: 0x0600A394 RID: 41876 RVA: 0x0022A700 File Offset: 0x00228900
		public Program With(IEnumerable<Example> examples, IEnumerable<IRow> inputs, LearnConfidenceResult learnConfidence, ILogger logger, CancellationToken cancellation)
		{
			this.LearnConfidence = learnConfidence;
			this.Meta = ProgramMetaFactory.Compute(this, examples, inputs, learnConfidence, logger, cancellation);
			return this;
		}

		// Token: 0x0400425E RID: 16990
		public static readonly Feature<double> ScoreFeature = new RankingScoreFeature(Language.Grammar, null);

		// Token: 0x0400425F RID: 16991
		private readonly RankingDebugTrace _rankingDebugInfo;
	}
}
