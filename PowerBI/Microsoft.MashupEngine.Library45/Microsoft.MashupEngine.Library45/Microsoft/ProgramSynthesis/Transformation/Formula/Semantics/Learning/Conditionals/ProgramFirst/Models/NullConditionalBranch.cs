using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001721 RID: 5921
	public class NullConditionalBranch : ConditionalBranch, INullConditionalBranch
	{
		// Token: 0x0600C51A RID: 50458 RVA: 0x002A6DB4 File Offset: 0x002A4FB4
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = "Null()");
			}
			return text;
		}

		// Token: 0x04004D2A RID: 19754
		private string _toString;
	}
}
