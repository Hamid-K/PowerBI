using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001949 RID: 6473
	internal class CSharpCharLiteral : FormulaExpression, IFormulaLiteral<char>, IFormulaTyped
	{
		// Token: 0x0600D392 RID: 54162 RVA: 0x002D199B File Offset: 0x002CFB9B
		public CSharpCharLiteral(char value)
		{
			this.Value = value;
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x0600D393 RID: 54163 RVA: 0x002D19AA File Offset: 0x002CFBAA
		public Type Type
		{
			get
			{
				return typeof(char);
			}
		}

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x0600D394 RID: 54164 RVA: 0x002D19B6 File Offset: 0x002CFBB6
		public char Value { get; }

		// Token: 0x0600D395 RID: 54165 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D396 RID: 54166 RVA: 0x002D19BE File Offset: 0x002CFBBE
		protected override string ToCodeString()
		{
			return this.Value.ToCSharpLiteral();
		}
	}
}
