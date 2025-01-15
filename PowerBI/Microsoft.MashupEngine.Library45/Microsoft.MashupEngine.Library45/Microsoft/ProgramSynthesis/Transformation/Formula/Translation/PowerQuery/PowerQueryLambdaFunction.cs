using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189A RID: 6298
	internal class PowerQueryLambdaFunction : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600CDDD RID: 52701 RVA: 0x002BF618 File Offset: 0x002BD818
		public PowerQueryLambdaFunction(IReadOnlyList<FormulaExpression> parameters, FormulaExpression body)
		{
			this.Parameters = parameters;
			this.Body = body;
			List<FormulaExpression> list = ((parameters != null) ? parameters.ToList<FormulaExpression>() : null) ?? new List<FormulaExpression>();
			list.Add(this.Body);
			base.Children = list;
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x0600CDDE RID: 52702 RVA: 0x002BF662 File Offset: 0x002BD862
		public FormulaExpression Body { get; }

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x0600CDDF RID: 52703 RVA: 0x002BF66A File Offset: 0x002BD86A
		public IReadOnlyList<FormulaExpression> Parameters { get; }

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x0600CDE0 RID: 52704 RVA: 0x002BF672 File Offset: 0x002BD872
		public Type Type
		{
			get
			{
				IFormulaTyped formulaTyped = this.Body as IFormulaTyped;
				return ((formulaTyped != null) ? formulaTyped.Type : null) ?? typeof(object);
			}
		}

		// Token: 0x0600CDE1 RID: 52705 RVA: 0x002BF699 File Offset: 0x002BD899
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryLambdaFunction(this.Parameters, this.Body.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CDE2 RID: 52706 RVA: 0x002BF6B4 File Offset: 0x002BD8B4
		protected override string ToCodeString()
		{
			string text = ((this.Parameters == null) ? "each " : ("(" + string.Join<FormulaExpression>(", ", this.Parameters) + ") => "));
			FormulaExpression body = this.Body;
			return text + ((body != null) ? body.ToString() : null);
		}
	}
}
