using System;
using System.Globalization;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200194B RID: 6475
	internal class CSharpDoubleLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600D39A RID: 54170 RVA: 0x002D1A08 File Offset: 0x002CFC08
		public CSharpDoubleLiteral(double value)
			: base(value)
		{
			this.Type = typeof(double);
		}

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x0600D39B RID: 54171 RVA: 0x002D1A21 File Offset: 0x002CFC21
		public override Type Type { get; }

		// Token: 0x0600D39C RID: 54172 RVA: 0x002D1A29 File Offset: 0x002CFC29
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpDoubleLiteral(base.Value);
		}

		// Token: 0x0600D39D RID: 54173 RVA: 0x002D1A38 File Offset: 0x002CFC38
		protected override string ToCodeString()
		{
			if (base.Value.Scale() != 0)
			{
				return base.ToCodeString();
			}
			return base.Value.ToString(CultureInfo.InvariantCulture) + "d";
		}
	}
}
