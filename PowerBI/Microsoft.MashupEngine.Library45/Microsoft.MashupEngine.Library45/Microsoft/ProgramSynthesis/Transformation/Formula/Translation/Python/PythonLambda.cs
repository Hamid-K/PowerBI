using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001851 RID: 6225
	internal class PythonLambda : FormulaExpression
	{
		// Token: 0x0600CBB5 RID: 52149 RVA: 0x002B86DC File Offset: 0x002B68DC
		public PythonLambda(IEnumerable<FormulaExpression> arguments, FormulaExpression body)
		{
			this.Arguments = arguments.ToReadOnlyList<FormulaExpression>();
			this.Body = body;
			base.Children = this.Arguments.AppendItem(this.Body).ToList<FormulaExpression>();
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x0600CBB6 RID: 52150 RVA: 0x002B8713 File Offset: 0x002B6913
		public IReadOnlyList<FormulaExpression> Arguments { get; }

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x0600CBB7 RID: 52151 RVA: 0x002B871B File Offset: 0x002B691B
		public FormulaExpression Body { get; }

		// Token: 0x0600CBB8 RID: 52152 RVA: 0x002B8723 File Offset: 0x002B6923
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonLambda(this.Arguments.Accept(visitor), this.Body.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBB9 RID: 52153 RVA: 0x002B8744 File Offset: 0x002B6944
		protected override string ToCodeString()
		{
			string text = string.Join(", ", this.Arguments.Select((FormulaExpression a) => a.ToString()));
			return string.Format("lambda {0} : {1}", text, this.Body);
		}
	}
}
