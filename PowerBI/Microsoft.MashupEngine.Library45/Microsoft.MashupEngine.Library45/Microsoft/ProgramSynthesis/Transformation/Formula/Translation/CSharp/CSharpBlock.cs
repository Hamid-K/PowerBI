using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001942 RID: 6466
	internal class CSharpBlock : FormulaExpression
	{
		// Token: 0x0600D378 RID: 54136 RVA: 0x002D1650 File Offset: 0x002CF850
		public CSharpBlock(IEnumerable<FormulaExpression> statements)
		{
			this.Statements = statements.ToReadOnlyList<FormulaExpression>();
			base.Children = this.Statements;
		}

		// Token: 0x0600D379 RID: 54137 RVA: 0x002B2B28 File Offset: 0x002B0D28
		protected CSharpBlock()
		{
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x0600D37A RID: 54138 RVA: 0x002D1670 File Offset: 0x002CF870
		public IReadOnlyList<FormulaExpression> Statements { get; }

		// Token: 0x0600D37B RID: 54139 RVA: 0x002D1678 File Offset: 0x002CF878
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpBlock(this.Statements.Accept(visitor));
		}

		// Token: 0x0600D37C RID: 54140 RVA: 0x002D168C File Offset: 0x002CF88C
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			FormulaExpression formulaExpression = null;
			codeBuilder.AppendLine("{");
			codeBuilder.PushIndent(1U);
			foreach (FormulaExpression formulaExpression2 in base.Children)
			{
				if (formulaExpression is CSharpProperty && formulaExpression2 is CSharpMethod)
				{
					codeBuilder.AppendLine();
				}
				if (formulaExpression is CSharpClass)
				{
					codeBuilder.AppendLine();
				}
				if (formulaExpression is CSharpMethod && formulaExpression2 is CSharpMethod)
				{
					codeBuilder.AppendLine();
				}
				bool flag = formulaExpression is CSharpVar;
				if (flag)
				{
					bool flag2 = formulaExpression2 is CSharpVar || formulaExpression2 is CSharpIf || formulaExpression2 is CSharpReturn;
					flag = !flag2;
				}
				if (flag)
				{
					codeBuilder.AppendLine();
				}
				flag = formulaExpression == null || formulaExpression is CSharpRawLine || formulaExpression is CSharpIf;
				if (!flag && formulaExpression2 is CSharpReturn)
				{
					codeBuilder.AppendLine();
				}
				string text = formulaExpression2.ToString();
				if (formulaExpression2 is CSharpRawLine)
				{
					codeBuilder.AppendIndented(text + Environment.NewLine);
				}
				else if (text.Contains(Environment.NewLine))
				{
					codeBuilder.AppendIndented(text);
				}
				else
				{
					codeBuilder.AppendLine(string.Format("{0};", formulaExpression2));
				}
				if (formulaExpression2 is CSharpIf)
				{
					codeBuilder.AppendLine();
				}
				formulaExpression = formulaExpression2;
			}
			codeBuilder.PopIndent();
			codeBuilder.AppendLine("}");
			return codeBuilder.GetCode();
		}
	}
}
