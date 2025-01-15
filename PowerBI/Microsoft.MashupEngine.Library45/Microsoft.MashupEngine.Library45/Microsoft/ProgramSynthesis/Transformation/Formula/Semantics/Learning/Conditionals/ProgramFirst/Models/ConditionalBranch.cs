using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001720 RID: 5920
	public class ConditionalBranch : IConditionalBranch
	{
		// Token: 0x0600C514 RID: 50452 RVA: 0x002A6D42 File Offset: 0x002A4F42
		public ConditionalBranch(Predicate predicate, ConditionalCluster outputCluster)
			: this(predicate, outputCluster.Program)
		{
		}

		// Token: 0x0600C515 RID: 50453 RVA: 0x002A6D51 File Offset: 0x002A4F51
		public ConditionalBranch(Predicate predicate, IProgram program)
		{
			this.Predicate = predicate;
			this.Program = program;
		}

		// Token: 0x0600C516 RID: 50454 RVA: 0x00002130 File Offset: 0x00000330
		protected ConditionalBranch()
		{
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x0600C517 RID: 50455 RVA: 0x002A6D67 File Offset: 0x002A4F67
		public Predicate Predicate { get; }

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x0600C518 RID: 50456 RVA: 0x002A6D6F File Offset: 0x002A4F6F
		public IProgram Program { get; }

		// Token: 0x0600C519 RID: 50457 RVA: 0x002A6D78 File Offset: 0x002A4F78
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

		// Token: 0x04004D27 RID: 19751
		private string _toString;
	}
}
