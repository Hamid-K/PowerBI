using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200196E RID: 6510
	internal class CSharpIf : FormulaExpression
	{
		// Token: 0x0600D422 RID: 54306 RVA: 0x002D25BC File Offset: 0x002D07BC
		public CSharpIf(FormulaExpression condition, CSharpBlock trueBlock, CSharpBlock falseBlock)
		{
			if (trueBlock == null && falseBlock == null)
			{
				throw new Exception("Invalid CSharpIf state, True and False branches are both null.");
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

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x0600D423 RID: 54307 RVA: 0x002D264E File Offset: 0x002D084E
		public FormulaExpression Condition { get; }

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x0600D424 RID: 54308 RVA: 0x002D2656 File Offset: 0x002D0856
		public CSharpBlock FalseBlock { get; }

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x0600D425 RID: 54309 RVA: 0x002D265E File Offset: 0x002D085E
		public CSharpBlock TrueBlock { get; }

		// Token: 0x0600D426 RID: 54310 RVA: 0x002D2668 File Offset: 0x002D0868
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Condition.Accept<FormulaExpression>(visitor);
			CSharpBlock trueBlock = this.TrueBlock;
			CSharpBlock csharpBlock = (CSharpBlock)((trueBlock != null) ? trueBlock.Accept<FormulaExpression>(visitor) : null);
			CSharpBlock falseBlock = this.FalseBlock;
			return new CSharpIf(formulaExpression, csharpBlock, (CSharpBlock)((falseBlock != null) ? falseBlock.Accept<FormulaExpression>(visitor) : null));
		}

		// Token: 0x0600D427 RID: 54311 RVA: 0x002D26B8 File Offset: 0x002D08B8
		protected override string ToCodeString()
		{
			CSharpIf.<>c__DisplayClass11_0 CS$<>8__locals1;
			CS$<>8__locals1.output = new CodeBuilder(4U);
			if (this.TrueBlock != null)
			{
				string text = ((this.FalseBlock == null && this.TrueBlock.Statements.Count == 1) ? string.Format("if ({0}) {1};", this.Condition, this.TrueBlock.Statements.Single<FormulaExpression>()) : null);
				if (text != null && text.Length < 80)
				{
					CS$<>8__locals1.output.AppendLine(text);
				}
				else
				{
					CSharpIf.<ToCodeString>g__Render|11_0(string.Format("if ({0})", this.Condition), this.TrueBlock, ref CS$<>8__locals1);
				}
				if (this.FalseBlock != null)
				{
					CSharpIf.<ToCodeString>g__Render|11_0("else", this.FalseBlock, ref CS$<>8__locals1);
				}
			}
			else if (this.FalseBlock != null)
			{
				CSharpIf.<ToCodeString>g__Render|11_0(string.Format("if (!({0}))", this.Condition), this.FalseBlock, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.output.GetCode();
		}

		// Token: 0x0600D428 RID: 54312 RVA: 0x002D27B8 File Offset: 0x002D09B8
		[CompilerGenerated]
		internal static void <ToCodeString>g__Render|11_0(string prefix, CSharpBlock block, ref CSharpIf.<>c__DisplayClass11_0 A_2)
		{
			A_2.output.AppendLine(prefix ?? "");
			A_2.output.AppendIndented(block.ToString());
		}
	}
}
