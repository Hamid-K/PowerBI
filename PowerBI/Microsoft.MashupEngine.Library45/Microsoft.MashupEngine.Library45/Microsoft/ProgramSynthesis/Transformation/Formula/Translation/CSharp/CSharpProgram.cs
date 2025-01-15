using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200193A RID: 6458
	internal class CSharpProgram : FormulaExpression
	{
		// Token: 0x0600D344 RID: 54084 RVA: 0x002D0EB4 File Offset: 0x002CF0B4
		public CSharpProgram(IEnumerable<CSharpUsing> usingStatements, CSharpNamespace namespaceBlock, IEnumerable<CSharpClass> classes, IEnumerable<CSharpMethod> methods, IEnumerable<FormulaExpression> statements)
		{
			this.Namespace = namespaceBlock;
			this.UsingStatements = usingStatements.ToReadOnlyList<CSharpUsing>();
			this.Classes = classes.ToReadOnlyList<CSharpClass>();
			this.Methods = methods.ToReadOnlyList<CSharpMethod>();
			this.Statements = statements.ToReadOnlyList<FormulaExpression>();
			CSharpNamespace @namespace = this.Namespace;
			IEnumerable<FormulaExpression> enumerable = ((@namespace != null) ? @namespace.Yield<CSharpNamespace>() : null);
			IEnumerable<FormulaExpression> enumerable2 = enumerable ?? Enumerable.Empty<FormulaExpression>();
			base.Children = this.UsingStatements.Concat(enumerable2).Concat(this.Classes).Concat(this.Methods)
				.Concat(this.Statements)
				.ToList<FormulaExpression>();
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x0600D345 RID: 54085 RVA: 0x002D0F56 File Offset: 0x002CF156
		public IReadOnlyList<CSharpClass> Classes { get; }

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x0600D346 RID: 54086 RVA: 0x002D0F5E File Offset: 0x002CF15E
		public IReadOnlyList<CSharpMethod> Methods { get; }

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x0600D347 RID: 54087 RVA: 0x002D0F66 File Offset: 0x002CF166
		public CSharpNamespace Namespace { get; }

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x0600D348 RID: 54088 RVA: 0x002D0F6E File Offset: 0x002CF16E
		public IReadOnlyList<FormulaExpression> Statements { get; }

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x0600D349 RID: 54089 RVA: 0x002D0F76 File Offset: 0x002CF176
		public IReadOnlyList<CSharpUsing> UsingStatements { get; }

		// Token: 0x0600D34A RID: 54090 RVA: 0x002D0F80 File Offset: 0x002CF180
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			IReadOnlyList<CSharpUsing> usingStatements = this.UsingStatements;
			IEnumerable<CSharpUsing> enumerable = ((usingStatements != null) ? usingStatements.Accept(visitor) : null);
			CSharpNamespace @namespace = this.Namespace;
			CSharpNamespace csharpNamespace = ((@namespace != null) ? @namespace.AcceptSingle(visitor) : null);
			IReadOnlyList<CSharpClass> classes = this.Classes;
			IEnumerable<CSharpClass> enumerable2 = ((classes != null) ? classes.Accept(visitor) : null);
			IReadOnlyList<CSharpMethod> methods = this.Methods;
			IEnumerable<CSharpMethod> enumerable3 = ((methods != null) ? methods.Accept(visitor) : null);
			IReadOnlyList<FormulaExpression> statements = this.Statements;
			return new CSharpProgram(enumerable, csharpNamespace, enumerable2, enumerable3, (statements != null) ? statements.Accept(visitor) : null);
		}

		// Token: 0x0600D34B RID: 54091 RVA: 0x002D0FF1 File Offset: 0x002CF1F1
		public CSharpProgram With(IEnumerable<CSharpUsing> usingStatements = null, CSharpNamespace namespaceBlock = null, IEnumerable<CSharpClass> classes = null, IEnumerable<CSharpMethod> methods = null, IEnumerable<FormulaExpression> statements = null)
		{
			return new CSharpProgram(usingStatements ?? this.UsingStatements, namespaceBlock ?? this.Namespace, classes ?? this.Classes, methods ?? this.Methods, statements ?? this.Statements);
		}

		// Token: 0x0600D34C RID: 54092 RVA: 0x002D1034 File Offset: 0x002CF234
		protected override string ToCodeString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			FormulaExpression formulaExpression = null;
			foreach (FormulaExpression formulaExpression2 in base.Children)
			{
				if (formulaExpression is CSharpUsing && !(formulaExpression2 is CSharpUsing))
				{
					codeBuilder.AppendLine();
				}
				bool flag = formulaExpression is CSharpNamespace || formulaExpression is CSharpClass || formulaExpression is CSharpMethod;
				if (flag)
				{
					codeBuilder.AppendLine();
				}
				flag = formulaExpression2 is CSharpUsing || formulaExpression2 is CSharpRawLine;
				if (flag)
				{
					codeBuilder.AppendLine(formulaExpression2.ToString());
				}
				else
				{
					codeBuilder.AppendIndented(formulaExpression2.ToString());
				}
				formulaExpression = formulaExpression2;
			}
			return codeBuilder.GetCode();
		}
	}
}
