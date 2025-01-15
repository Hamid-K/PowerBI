using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models
{
	// Token: 0x02001751 RID: 5969
	public class NullConditionalBranch : Cluster, INullConditionalBranch
	{
		// Token: 0x0600C61E RID: 50718 RVA: 0x002A9B44 File Offset: 0x002A7D44
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = "Null()");
			}
			return text;
		}

		// Token: 0x04004DBF RID: 19903
		private string _toString;
	}
}
