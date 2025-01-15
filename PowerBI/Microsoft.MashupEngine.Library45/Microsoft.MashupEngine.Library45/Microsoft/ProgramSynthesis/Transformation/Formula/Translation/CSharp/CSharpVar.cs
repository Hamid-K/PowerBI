using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001943 RID: 6467
	internal class CSharpVar : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600D37D RID: 54141 RVA: 0x002D181C File Offset: 0x002CFA1C
		public CSharpVar(FormulaExpression subject, Type type)
		{
			this.Subject = subject;
			this.Type = type;
			base.Children = this.Subject.Yield<FormulaExpression>().ToList<FormulaExpression>();
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x0600D37E RID: 54142 RVA: 0x002D1848 File Offset: 0x002CFA48
		public FormulaExpression Subject { get; }

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x0600D37F RID: 54143 RVA: 0x002D1850 File Offset: 0x002CFA50
		public Type Type { get; }

		// Token: 0x0600D380 RID: 54144 RVA: 0x002D1858 File Offset: 0x002CFA58
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpVar(this.Subject.Accept<FormulaExpression>(visitor), this.Type);
		}

		// Token: 0x0600D381 RID: 54145 RVA: 0x002D1871 File Offset: 0x002CFA71
		protected override string ToCodeString()
		{
			string text = "{0} {1}";
			Type type = this.Type;
			return string.Format(text, ((type != null) ? type.CsName(false) : null) ?? "var", this.Subject);
		}
	}
}
