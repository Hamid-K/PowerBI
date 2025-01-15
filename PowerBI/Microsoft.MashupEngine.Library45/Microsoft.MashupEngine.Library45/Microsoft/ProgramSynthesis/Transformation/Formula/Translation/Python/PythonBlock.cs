using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200184F RID: 6223
	internal class PythonBlock : FormulaExpression, IFormulaBlock
	{
		// Token: 0x0600CBAA RID: 52138 RVA: 0x002B858D File Offset: 0x002B678D
		public PythonBlock(IEnumerable<FormulaExpression> statements)
		{
			this.Statements = statements.ToReadOnlyList<FormulaExpression>();
			base.Children = this.Statements;
		}

		// Token: 0x0600CBAB RID: 52139 RVA: 0x002B2B28 File Offset: 0x002B0D28
		protected PythonBlock()
		{
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x0600CBAC RID: 52140 RVA: 0x002B85AD File Offset: 0x002B67AD
		public virtual IReadOnlyList<FormulaExpression> Statements { get; }

		// Token: 0x0600CBAD RID: 52141 RVA: 0x002B85B5 File Offset: 0x002B67B5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonBlock(this.Statements.Accept(visitor));
		}

		// Token: 0x0600CBAE RID: 52142 RVA: 0x002B85C8 File Offset: 0x002B67C8
		public virtual void AppendCodeString(CodeBuilder codeBuilder)
		{
			foreach (FormulaExpression formulaExpression in base.Children)
			{
				IFormulaBlock formulaBlock = formulaExpression as IFormulaBlock;
				if (formulaBlock != null)
				{
					formulaBlock.AppendCodeString(codeBuilder);
				}
				else
				{
					codeBuilder.AppendIndented(formulaExpression.ToString());
				}
				if (!(formulaExpression is PythonComment))
				{
					codeBuilder.AppendLine();
				}
			}
		}

		// Token: 0x0600CBAF RID: 52143 RVA: 0x002B863C File Offset: 0x002B683C
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

		// Token: 0x0600CBB0 RID: 52144 RVA: 0x002B8672 File Offset: 0x002B6872
		protected override string ToCodeString()
		{
			return this.ToString(0U, 4U);
		}
	}
}
