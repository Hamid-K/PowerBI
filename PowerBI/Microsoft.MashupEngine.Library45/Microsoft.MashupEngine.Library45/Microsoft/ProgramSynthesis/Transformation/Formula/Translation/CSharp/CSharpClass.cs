using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193D RID: 6461
	internal class CSharpClass : CSharpBlock
	{
		// Token: 0x0600D356 RID: 54102 RVA: 0x002D11B8 File Offset: 0x002CF3B8
		public CSharpClass(string name, IEnumerable<CSharpProperty> properties, IEnumerable<CSharpMethod> methods)
		{
			this.Name = name;
			this.Properties = properties.ToReadOnlyList<CSharpProperty>();
			this.Methods = methods.ToReadOnlyList<CSharpMethod>();
			base.Children = this.Properties.Cast<FormulaExpression>().Concat(this.Methods).ToReadOnlyList<FormulaExpression>();
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x0600D357 RID: 54103 RVA: 0x002D120B File Offset: 0x002CF40B
		public IReadOnlyList<CSharpMethod> Methods { get; }

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x0600D358 RID: 54104 RVA: 0x002D1213 File Offset: 0x002CF413
		// (set) Token: 0x0600D359 RID: 54105 RVA: 0x002D121B File Offset: 0x002CF41B
		public string Name { get; set; }

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x0600D35A RID: 54106 RVA: 0x002D1224 File Offset: 0x002CF424
		public IReadOnlyList<CSharpProperty> Properties { get; }

		// Token: 0x0600D35B RID: 54107 RVA: 0x002D122C File Offset: 0x002CF42C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpClass(this.Name, this.Properties.Accept(visitor), this.Methods.Accept(visitor));
		}

		// Token: 0x0600D35C RID: 54108 RVA: 0x002D1251 File Offset: 0x002CF451
		public CSharpClass With(string name = null, IEnumerable<CSharpProperty> properties = null, IEnumerable<CSharpMethod> methods = null)
		{
			return new CSharpClass(name ?? this.Name, properties ?? this.Properties, methods ?? this.Methods);
		}

		// Token: 0x0600D35D RID: 54109 RVA: 0x002D1279 File Offset: 0x002CF479
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendLine("public static class " + this.Name);
			codeBuilder.AppendIndented(base.ToCodeString());
			return codeBuilder.GetCode();
		}
	}
}
