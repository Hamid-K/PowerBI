using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200187F RID: 6271
	internal class PythonIf : PythonBlock
	{
		// Token: 0x0600CC5C RID: 52316 RVA: 0x002B93DC File Offset: 0x002B75DC
		public PythonIf(FormulaExpression condition, PythonBlock trueBlock, PythonBlock falseBlock)
		{
			if (trueBlock == null && falseBlock == null)
			{
				throw new Exception("Invalid PythonIf state, True and False branches are both null.");
			}
			this.Condition = condition;
			this.TrueBlock = trueBlock;
			this.FalseBlock = falseBlock;
			List<FormulaExpression> list = new List<FormulaExpression> { this.Condition };
			if (this.TrueBlock != null)
			{
				list.Add(this.TrueBlock);
			}
			if (this.FalseBlock != null)
			{
				list.Add(this.FalseBlock);
			}
			base.Children = list;
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x0600CC5D RID: 52317 RVA: 0x002B946E File Offset: 0x002B766E
		public FormulaExpression Condition { get; }

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x0600CC5E RID: 52318 RVA: 0x002B9476 File Offset: 0x002B7676
		public PythonBlock FalseBlock { get; }

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x0600CC5F RID: 52319 RVA: 0x002B947E File Offset: 0x002B767E
		public PythonBlock TrueBlock { get; }

		// Token: 0x0600CC60 RID: 52320 RVA: 0x002B9488 File Offset: 0x002B7688
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Condition.Accept<FormulaExpression>(visitor);
			PythonBlock trueBlock = this.TrueBlock;
			PythonBlock pythonBlock = (PythonBlock)((trueBlock != null) ? trueBlock.Accept<FormulaExpression>(visitor) : null);
			PythonBlock falseBlock = this.FalseBlock;
			return new PythonIf(formulaExpression, pythonBlock, (PythonBlock)((falseBlock != null) ? falseBlock.Accept<FormulaExpression>(visitor) : null));
		}

		// Token: 0x0600CC61 RID: 52321 RVA: 0x002B94D8 File Offset: 0x002B76D8
		public override void AppendCodeString(CodeBuilder cb)
		{
			if (this.TrueBlock != null)
			{
				cb.AppendLine(string.Format("if {0}:", this.Condition));
				cb.PushIndent(1U);
				this.TrueBlock.AppendCodeString(cb);
				cb.PopIndent();
				if (this.FalseBlock != null)
				{
					cb.AppendLine("else:");
					cb.PushIndent(1U);
					this.FalseBlock.AppendCodeString(cb);
					cb.PopIndent();
					return;
				}
			}
			else if (this.FalseBlock != null)
			{
				cb.AppendLine(string.Format("if not({0}):", this.Condition));
				cb.PushIndent(1U);
				this.FalseBlock.AppendCodeString(cb);
				cb.PopIndent();
			}
		}
	}
}
