using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001868 RID: 6248
	internal class PythonArray : FormulaExpression
	{
		// Token: 0x0600CC02 RID: 52226 RVA: 0x002B8D06 File Offset: 0x002B6F06
		public PythonArray(IEnumerable<FormulaExpression> items)
		{
			this.Items = items.ToReadOnlyList<FormulaExpression>();
			base.Children = this.Items;
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x0600CC03 RID: 52227 RVA: 0x002B8D26 File Offset: 0x002B6F26
		public IReadOnlyList<FormulaExpression> Items { get; }

		// Token: 0x0600CC04 RID: 52228 RVA: 0x002B8D2E File Offset: 0x002B6F2E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonArray(this.Items.Accept(visitor));
		}

		// Token: 0x0600CC05 RID: 52229 RVA: 0x002B8D44 File Offset: 0x002B6F44
		protected override string ToCodeString()
		{
			return "[" + string.Join(", ", this.Items.Select((FormulaExpression i) => i.ToString())) + "]";
		}
	}
}
