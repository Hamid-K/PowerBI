using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001848 RID: 6216
	internal class PythonProgram : PythonBlock
	{
		// Token: 0x0600CB85 RID: 52101 RVA: 0x002B7F8C File Offset: 0x002B618C
		public PythonProgram(IEnumerable<PythonImport> imports, IEnumerable<PythonDefinition> definitions, IEnumerable<FormulaExpression> statements, PythonComment comment = null)
			: base(statements)
		{
			this.Imports = imports.ToReadOnlyList<PythonImport>();
			this.Definitions = definitions.ToReadOnlyList<PythonDefinition>();
			this.Comment = comment ?? PythonComment.Empty;
			base.Children = this.Imports.Cast<FormulaExpression>().AppendItem(this.Comment).Concat(this.Definitions)
				.Concat(this.Statements)
				.ToList<FormulaExpression>();
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x0600CB86 RID: 52102 RVA: 0x002B8000 File Offset: 0x002B6200
		public PythonComment Comment { get; }

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x0600CB87 RID: 52103 RVA: 0x002B8008 File Offset: 0x002B6208
		public IReadOnlyList<PythonDefinition> Definitions { get; }

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x0600CB88 RID: 52104 RVA: 0x002B8010 File Offset: 0x002B6210
		public IReadOnlyList<PythonImport> Imports { get; }

		// Token: 0x0600CB89 RID: 52105 RVA: 0x002B8018 File Offset: 0x002B6218
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonProgram(this.Imports.Accept(visitor), this.Definitions.Accept(visitor), this.Statements.Accept(visitor), (PythonComment)this.Comment.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CB8A RID: 52106 RVA: 0x002B8054 File Offset: 0x002B6254
		public override void AppendCodeString(CodeBuilder cb)
		{
			FormulaExpression formulaExpression = null;
			foreach (FormulaExpression formulaExpression2 in base.Children)
			{
				if (formulaExpression is PythonImport && !(formulaExpression2 is PythonImport))
				{
					cb.AppendLine();
				}
				IFormulaBlock formulaBlock = formulaExpression2 as IFormulaBlock;
				if (formulaBlock != null)
				{
					formulaBlock.AppendCodeString(cb);
				}
				else
				{
					cb.Append(formulaExpression2.ToString());
				}
				if (!(formulaExpression2 is PythonComment))
				{
					cb.AppendLine();
				}
				formulaExpression = formulaExpression2;
			}
		}

		// Token: 0x0600CB8B RID: 52107 RVA: 0x002B80E4 File Offset: 0x002B62E4
		public PythonProgram With(IEnumerable<PythonImport> imports = null, IEnumerable<PythonDefinition> definitions = null, IEnumerable<FormulaExpression> statements = null, PythonComment comment = null)
		{
			return new PythonProgram(imports ?? this.Imports, definitions ?? this.Definitions, statements ?? this.Statements, comment ?? this.Comment);
		}
	}
}
