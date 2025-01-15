using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193C RID: 6460
	internal class CSharpNamespace : CSharpBlock
	{
		// Token: 0x0600D351 RID: 54097 RVA: 0x002D1132 File Offset: 0x002CF332
		public CSharpNamespace(string name, IEnumerable<CSharpClass> classes)
		{
			this.Name = name;
			this.Classes = classes.ToReadOnlyList<CSharpClass>();
			base.Children = this.Classes;
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x0600D352 RID: 54098 RVA: 0x002D1159 File Offset: 0x002CF359
		public IReadOnlyList<CSharpClass> Classes { get; }

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x0600D353 RID: 54099 RVA: 0x002D1161 File Offset: 0x002CF361
		public string Name { get; }

		// Token: 0x0600D354 RID: 54100 RVA: 0x002D1169 File Offset: 0x002CF369
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpNamespace(this.Name, this.Classes.Accept(visitor));
		}

		// Token: 0x0600D355 RID: 54101 RVA: 0x002D1182 File Offset: 0x002CF382
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine("namespace " + this.Name);
			codeBuilder.AppendIndented(base.ToCodeString());
			codeBuilder.AppendLine();
			return codeBuilder.GetCode();
		}
	}
}
