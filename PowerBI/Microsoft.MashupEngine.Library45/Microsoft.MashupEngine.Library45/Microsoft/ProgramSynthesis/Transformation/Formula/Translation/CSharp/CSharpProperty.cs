using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193E RID: 6462
	internal class CSharpProperty : FormulaExpression
	{
		// Token: 0x0600D35E RID: 54110 RVA: 0x002D12A8 File Offset: 0x002CF4A8
		public CSharpProperty(string name, string type, FormulaExpression value)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x0600D35F RID: 54111 RVA: 0x002D12C5 File Offset: 0x002CF4C5
		public string Name { get; }

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x0600D360 RID: 54112 RVA: 0x002D12CD File Offset: 0x002CF4CD
		public string Type { get; }

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x0600D361 RID: 54113 RVA: 0x002D12D5 File Offset: 0x002CF4D5
		public FormulaExpression Value { get; }

		// Token: 0x0600D362 RID: 54114 RVA: 0x002D12DD File Offset: 0x002CF4DD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpProperty(this.Name, this.Type, this.Value.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D363 RID: 54115 RVA: 0x002D12FC File Offset: 0x002CF4FC
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.PushIndent(2U);
			codeBuilder.AppendLine(string.Format("private {0} {1} = {2}", this.Type, this.Name, this.Value));
			return codeBuilder.GetCode();
		}
	}
}
