using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018C1 RID: 6337
	public class PowerQueryStep : FormulaExpression, IFormulaBlock
	{
		// Token: 0x0600CE7B RID: 52859 RVA: 0x002C062E File Offset: 0x002BE82E
		public PowerQueryStep(string stepName, FormulaExpression expression, bool isOutput = false)
		{
			this.StepName = stepName;
			this.Expression = expression;
			this.IsOutput = isOutput;
			base.Children = new FormulaExpression[] { expression };
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x0600CE7C RID: 52860 RVA: 0x002C065B File Offset: 0x002BE85B
		// (set) Token: 0x0600CE7D RID: 52861 RVA: 0x002C0663 File Offset: 0x002BE863
		public string StepName { get; set; }

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x0600CE7E RID: 52862 RVA: 0x002C066C File Offset: 0x002BE86C
		// (set) Token: 0x0600CE7F RID: 52863 RVA: 0x002C0674 File Offset: 0x002BE874
		public FormulaExpression Expression { get; set; }

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x0600CE80 RID: 52864 RVA: 0x002C067D File Offset: 0x002BE87D
		// (set) Token: 0x0600CE81 RID: 52865 RVA: 0x002C0685 File Offset: 0x002BE885
		public bool IsOutput { get; set; }

		// Token: 0x0600CE82 RID: 52866 RVA: 0x002C068E File Offset: 0x002BE88E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryStep(this.StepName, this.Expression.Accept<FormulaExpression>(visitor), this.IsOutput);
		}

		// Token: 0x0600CE83 RID: 52867 RVA: 0x002C06B0 File Offset: 0x002BE8B0
		public void AppendCodeString(CodeBuilder codeBuilder)
		{
			IFormulaBlock formulaBlock = this.Expression as IFormulaBlock;
			if (formulaBlock != null)
			{
				codeBuilder.AppendLine("#" + PowerQueryStringLiteral.EscapeString(this.StepName) + " = ");
				formulaBlock.AppendCodeString(codeBuilder);
				return;
			}
			codeBuilder.AppendIndented(string.Format("#{0} = {1}", PowerQueryStringLiteral.EscapeString(this.StepName), this.Expression));
		}

		// Token: 0x0600CE84 RID: 52868 RVA: 0x002C0718 File Offset: 0x002BE918
		public string ToString(uint indentLevel, uint indentSize)
		{
			CodeBuilder codeBuilder = new CodeBuilder(indentSize);
			int num = 0;
			while ((long)num < (long)((ulong)indentLevel))
			{
				codeBuilder.PushIndent(1U);
				num++;
			}
			this.AppendCodeString(codeBuilder);
			return codeBuilder.GetCode();
		}

		// Token: 0x0600CE85 RID: 52869 RVA: 0x002C074E File Offset: 0x002BE94E
		protected override string ToCodeString()
		{
			return this.ToString(0U, 4U);
		}
	}
}
