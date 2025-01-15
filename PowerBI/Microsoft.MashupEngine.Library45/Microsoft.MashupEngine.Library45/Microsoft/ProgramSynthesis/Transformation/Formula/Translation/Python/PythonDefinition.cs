using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200184D RID: 6221
	internal class PythonDefinition : PythonBlock
	{
		// Token: 0x0600CB9D RID: 52125 RVA: 0x002B83C0 File Offset: 0x002B65C0
		public PythonDefinition(string name, IEnumerable<PythonVariable> parameters, IEnumerable<FormulaExpression> statements, PythonComment comment = null)
			: this(name, parameters, new PythonBlock(statements.ToReadOnlyList<FormulaExpression>()), comment)
		{
		}

		// Token: 0x0600CB9E RID: 52126 RVA: 0x002B83D8 File Offset: 0x002B65D8
		public PythonDefinition(string name, IEnumerable<PythonVariable> parameters, PythonBlock body, PythonComment comment = null)
		{
			this.Name = name;
			this.Comment = comment ?? PythonComment.Empty;
			this.Parameters = parameters.ToReadOnlyList<PythonVariable>();
			this.Body = body;
			base.Children = this.Parameters.Cast<FormulaExpression>().AppendItem(this.Comment).Concat(this.Body.Yield<PythonBlock>())
				.ToList<FormulaExpression>();
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x0600CB9F RID: 52127 RVA: 0x002B8447 File Offset: 0x002B6647
		public PythonBlock Body { get; }

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x0600CBA0 RID: 52128 RVA: 0x002B844F File Offset: 0x002B664F
		public PythonComment Comment { get; }

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x0600CBA1 RID: 52129 RVA: 0x002B8457 File Offset: 0x002B6657
		public string Name { get; }

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x0600CBA2 RID: 52130 RVA: 0x002B845F File Offset: 0x002B665F
		public IReadOnlyList<PythonVariable> Parameters { get; }

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x0600CBA3 RID: 52131 RVA: 0x002B8467 File Offset: 0x002B6667
		public override IReadOnlyList<FormulaExpression> Statements
		{
			get
			{
				return this.Body.Statements;
			}
		}

		// Token: 0x0600CBA4 RID: 52132 RVA: 0x002B8474 File Offset: 0x002B6674
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDefinition(this.Name, this.Parameters.Accept(visitor), (PythonBlock)this.Body.Accept<FormulaExpression>(visitor), (PythonComment)this.Comment.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CBA5 RID: 52133 RVA: 0x002B84B0 File Offset: 0x002B66B0
		public override void AppendCodeString(CodeBuilder cb)
		{
			string text = string.Join(", ", this.Parameters.Select((PythonVariable p) => p.ToString()));
			cb.AppendLine(string.Concat(new string[] { "def ", this.Name, "(", text, "):" }));
			cb.PushIndent(1U);
			this.Comment.AppendCodeString(cb);
			this.Body.AppendCodeString(cb);
			cb.PopIndent();
		}

		// Token: 0x0600CBA6 RID: 52134 RVA: 0x002B854D File Offset: 0x002B674D
		public PythonDefinition With(string name = null, IEnumerable<PythonVariable> parameters = null, PythonBlock body = null, PythonComment comment = null)
		{
			return new PythonDefinition(name ?? this.Name, parameters ?? this.Parameters, body ?? this.Body, comment ?? this.Comment);
		}
	}
}
