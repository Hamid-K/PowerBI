using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001722 RID: 5922
	public class ConditionalCluster
	{
		// Token: 0x0600C51C RID: 50460 RVA: 0x002A6DE4 File Offset: 0x002A4FE4
		public ConditionalCluster(ConditionalBranchMeta programMeta, IEnumerable<ConditionalClusterSourceExample> sourceExamples, IEnumerable<Predicate> predicates)
			: this(programMeta, sourceExamples, new ClusterSourceAdditionalInput[0], predicates ?? new Predicate[0], new Predicate[0], null)
		{
		}

		// Token: 0x0600C51D RID: 50461 RVA: 0x002A6E1C File Offset: 0x002A501C
		public ConditionalCluster(ConditionalCluster cluster, IEnumerable<Predicate> predicates = null, IEnumerable<Predicate> validPredicates = null)
			: this(cluster.ProgramMeta, cluster.SourceExamples, cluster.SourceInputs, predicates ?? cluster.Predicates ?? new Predicate[0], validPredicates ?? cluster.ValidPredicates ?? new Predicate[0], cluster.Score)
		{
		}

		// Token: 0x0600C51E RID: 50462 RVA: 0x002A6E71 File Offset: 0x002A5071
		public ConditionalCluster(ConditionalCluster cluster, IEnumerable<ClusterSourceAdditionalInput> sourceAdditionalInputs)
			: this(cluster.ProgramMeta, cluster.SourceExamples, sourceAdditionalInputs, cluster.Predicates, cluster.ValidPredicates, cluster.Score)
		{
		}

		// Token: 0x0600C51F RID: 50463 RVA: 0x002A6E98 File Offset: 0x002A5098
		public ConditionalCluster(ConditionalBranchMeta programInfo, IEnumerable<ConditionalClusterSourceExample> sourceExamples, IEnumerable<ClusterSourceAdditionalInput> sourceAdditionalInputs, IEnumerable<Predicate> predicates, IEnumerable<Predicate> validPredicates, double? score)
		{
			this.ProgramMeta = programInfo;
			this.SourceExamples = sourceExamples.ToReadOnlyList<ConditionalClusterSourceExample>();
			this.SourceInputs = sourceAdditionalInputs.ToReadOnlyList<ClusterSourceAdditionalInput>();
			this.Predicates = predicates.ToReadOnlyList<Predicate>();
			this.ValidPredicates = validPredicates.ToReadOnlyList<Predicate>();
			this.Score = score;
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x0600C520 RID: 50464 RVA: 0x002A6EEC File Offset: 0x002A50EC
		public IReadOnlyList<Predicate> Predicates { get; }

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x0600C521 RID: 50465 RVA: 0x002A6EF4 File Offset: 0x002A50F4
		public IReadOnlyList<Predicate> ValidPredicates { get; }

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x0600C522 RID: 50466 RVA: 0x002A6EFC File Offset: 0x002A50FC
		public IProgram Program
		{
			get
			{
				return this.ProgramMeta.Program;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x0600C523 RID: 50467 RVA: 0x002A6F09 File Offset: 0x002A5109
		public ConditionalBranchMeta ProgramMeta { get; }

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x0600C524 RID: 50468 RVA: 0x002A6F11 File Offset: 0x002A5111
		// (set) Token: 0x0600C525 RID: 50469 RVA: 0x002A6F19 File Offset: 0x002A5119
		public double? Score { get; set; }

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x0600C526 RID: 50470 RVA: 0x002A6F22 File Offset: 0x002A5122
		public IReadOnlyList<ConditionalClusterSourceExample> SourceExamples { get; }

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x0600C527 RID: 50471 RVA: 0x002A6F2A File Offset: 0x002A512A
		public IReadOnlyList<ClusterSourceAdditionalInput> SourceInputs { get; }

		// Token: 0x0600C528 RID: 50472 RVA: 0x002A6F34 File Offset: 0x002A5134
		public string ToDetailString()
		{
			IProgram program = this.Program;
			string text = ((program != null) ? program.ToString() : null) ?? "No Program";
			string text2;
			if (!this.SourceExamples.Any<ConditionalClusterSourceExample>())
			{
				text2 = "   None";
			}
			else
			{
				text2 = "[" + string.Join<int>(", ", this.SourceExamples.Select((ConditionalClusterSourceExample e) => e.Position)) + "]";
			}
			string text3 = text2;
			string text4;
			if ((text4 = this._toDetailString) == null)
			{
				text4 = (this._toDetailString = string.Format("{0,-70}{1}", text, text3));
			}
			return text4;
		}

		// Token: 0x0600C529 RID: 50473 RVA: 0x002A6FD4 File Offset: 0x002A51D4
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				IProgram program = this.Program;
				text = (this._toString = ((program != null) ? program.ToString() : null) ?? "No Program");
			}
			return text;
		}

		// Token: 0x04004D2B RID: 19755
		private string _toDetailString;

		// Token: 0x04004D2C RID: 19756
		private string _toString;
	}
}
