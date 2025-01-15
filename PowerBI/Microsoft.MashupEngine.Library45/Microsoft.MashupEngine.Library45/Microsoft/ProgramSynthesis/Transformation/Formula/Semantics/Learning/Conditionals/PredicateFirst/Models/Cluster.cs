using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models
{
	// Token: 0x0200174B RID: 5963
	public class Cluster : IConditionalBranch
	{
		// Token: 0x0600C5FF RID: 50687 RVA: 0x002A96AF File Offset: 0x002A78AF
		public Cluster()
			: this(null, null, new ClusterExample[0], 0)
		{
		}

		// Token: 0x0600C600 RID: 50688 RVA: 0x002A96C0 File Offset: 0x002A78C0
		public Cluster(Predicate predicate, IEnumerable<ClusterExample> examples, int position)
			: this(predicate, null, examples, position)
		{
		}

		// Token: 0x0600C601 RID: 50689 RVA: 0x002A96CC File Offset: 0x002A78CC
		public Cluster(Predicate predicate, IProgram program, IEnumerable<ClusterExample> examples, int position)
		{
			this.Predicate = predicate;
			this.Program = program;
			this.Examples = examples.ToReadOnlyList<ClusterExample>();
			this.Position = position;
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x0600C602 RID: 50690 RVA: 0x002A96F6 File Offset: 0x002A78F6
		// (set) Token: 0x0600C603 RID: 50691 RVA: 0x002A96FE File Offset: 0x002A78FE
		public bool? LearnFailed { get; set; }

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x0600C604 RID: 50692 RVA: 0x002A9707 File Offset: 0x002A7907
		// (set) Token: 0x0600C605 RID: 50693 RVA: 0x002A970F File Offset: 0x002A790F
		public double? LearnTime { get; set; }

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x0600C606 RID: 50694 RVA: 0x002A9718 File Offset: 0x002A7918
		public IReadOnlyList<ClusterExample> Examples { get; }

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x0600C607 RID: 50695 RVA: 0x002A9720 File Offset: 0x002A7920
		// (set) Token: 0x0600C608 RID: 50696 RVA: 0x002A9728 File Offset: 0x002A7928
		public int Position { get; set; }

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x0600C609 RID: 50697 RVA: 0x002A9731 File Offset: 0x002A7931
		public Predicate Predicate { get; }

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x0600C60A RID: 50698 RVA: 0x002A9739 File Offset: 0x002A7939
		// (set) Token: 0x0600C60B RID: 50699 RVA: 0x002A9741 File Offset: 0x002A7941
		public IProgram Program { get; set; }

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x0600C60C RID: 50700 RVA: 0x002A974C File Offset: 0x002A794C
		public IReadOnlyList<Example<IRow, object>> SourceExamples
		{
			get
			{
				IReadOnlyList<Example<IRow, object>> readOnlyList;
				if ((readOnlyList = this._sourceExamples) == null)
				{
					readOnlyList = (this._sourceExamples = this.Examples.Select((ClusterExample e) => e.Example).ToReadOnlyList<Example<IRow, object>>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600C60D RID: 50701 RVA: 0x002A979C File Offset: 0x002A799C
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				Predicate predicate = this.Predicate;
				text = (this._toString = ((predicate != null) ? predicate.ToString() : null) ?? "else");
			}
			return text;
		}

		// Token: 0x04004DB0 RID: 19888
		private IReadOnlyList<Example<IRow, object>> _sourceExamples;

		// Token: 0x04004DB1 RID: 19889
		private string _toString;
	}
}
