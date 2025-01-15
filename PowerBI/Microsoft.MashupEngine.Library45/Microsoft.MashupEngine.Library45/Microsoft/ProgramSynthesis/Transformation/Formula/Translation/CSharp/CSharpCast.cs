using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200196B RID: 6507
	internal class CSharpCast : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator, IFormulaTyped
	{
		// Token: 0x0600D412 RID: 54290 RVA: 0x002D24A5 File Offset: 0x002D06A5
		public CSharpCast(FormulaExpression subject, Type type)
		{
			this.Subject = subject;
			this.Type = type;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x0600D413 RID: 54291 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x0600D414 RID: 54292 RVA: 0x002D24D0 File Offset: 0x002D06D0
		public FormulaExpression Subject { get; }

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x0600D415 RID: 54293 RVA: 0x002D24D8 File Offset: 0x002D06D8
		public Type Type { get; }

		// Token: 0x0600D416 RID: 54294 RVA: 0x002D24E0 File Offset: 0x002D06E0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpCast(this.Subject.Accept<FormulaExpression>(visitor), this.Type);
		}

		// Token: 0x0600D417 RID: 54295 RVA: 0x002D24F9 File Offset: 0x002D06F9
		protected override string ToCodeString()
		{
			return string.Format("({0}){1}", this.Type.CsName(true), this.Subject);
		}
	}
}
