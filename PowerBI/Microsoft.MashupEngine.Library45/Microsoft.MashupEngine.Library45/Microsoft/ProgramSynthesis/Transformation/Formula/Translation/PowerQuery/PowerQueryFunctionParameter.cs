using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189B RID: 6299
	internal class PowerQueryFunctionParameter : FormulaExpression
	{
		// Token: 0x0600CDE3 RID: 52707 RVA: 0x002BF706 File Offset: 0x002BD906
		public PowerQueryFunctionParameter(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x0600CDE4 RID: 52708 RVA: 0x002BF71C File Offset: 0x002BD91C
		public string Name { get; }

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x0600CDE5 RID: 52709 RVA: 0x002BF724 File Offset: 0x002BD924
		public string Type { get; }

		// Token: 0x0600CDE6 RID: 52710 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CDE7 RID: 52711 RVA: 0x002BF72C File Offset: 0x002BD92C
		protected override string ToCodeString()
		{
			return this.Name + " as " + this.Type;
		}
	}
}
