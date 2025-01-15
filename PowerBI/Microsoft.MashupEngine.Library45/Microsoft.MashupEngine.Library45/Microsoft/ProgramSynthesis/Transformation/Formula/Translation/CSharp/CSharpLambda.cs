using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001964 RID: 6500
	internal class CSharpLambda : FormulaExpression
	{
		// Token: 0x0600D3F3 RID: 54259 RVA: 0x002D2153 File Offset: 0x002D0353
		public CSharpLambda(IEnumerable<FormulaExpression> arguments, FormulaExpression body)
		{
			this.Arguments = arguments.ToReadOnlyList<FormulaExpression>();
			this.Body = body;
			base.Children = this.Arguments.Concat(this.Body.Yield<FormulaExpression>()).ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x0600D3F4 RID: 54260 RVA: 0x002D218F File Offset: 0x002D038F
		public IReadOnlyList<FormulaExpression> Arguments { get; }

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x0600D3F5 RID: 54261 RVA: 0x002D2197 File Offset: 0x002D0397
		public FormulaExpression Body { get; }

		// Token: 0x0600D3F6 RID: 54262 RVA: 0x002D219F File Offset: 0x002D039F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpLambda(this.Arguments.Accept(visitor), this.Body.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D3F7 RID: 54263 RVA: 0x002D21C0 File Offset: 0x002D03C0
		protected override string ToCodeString()
		{
			string text = string.Join(", ", this.Arguments.Select((FormulaExpression a) => a.ToString()));
			if (this.Arguments.Count > 1)
			{
				text = "(" + text + ")";
			}
			return string.Format("{0} => {1}", text, this.Body);
		}
	}
}
