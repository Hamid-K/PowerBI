using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001849 RID: 6217
	internal class PythonImport : FormulaExpression
	{
		// Token: 0x0600CB8C RID: 52108 RVA: 0x002B8118 File Offset: 0x002B6318
		public PythonImport(string module, HashSet<string> objects, string alias)
		{
			this.Module = module;
			if (alias != null && objects != null && objects.Count > 1)
			{
				throw new ArgumentException("Only one submodule is allowed if alias is specified.");
			}
			this.Objects = objects;
			this.Alias = alias;
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x0600CB8D RID: 52109 RVA: 0x002B814F File Offset: 0x002B634F
		public string Alias { get; }

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x0600CB8E RID: 52110 RVA: 0x002B8157 File Offset: 0x002B6357
		public string Module { get; }

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x0600CB8F RID: 52111 RVA: 0x002B815F File Offset: 0x002B635F
		public IReadOnlyCollection<string> Objects { get; }

		// Token: 0x0600CB90 RID: 52112 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CB91 RID: 52113 RVA: 0x002B8167 File Offset: 0x002B6367
		public PythonImport WithNewObjects(HashSet<string> newObjects)
		{
			return new PythonImport(this.Module, this.Objects.Concat(newObjects).ConvertToHashSet<string>(), this.Alias);
		}

		// Token: 0x0600CB92 RID: 52114 RVA: 0x002B818C File Offset: 0x002B638C
		protected override string ToCodeString()
		{
			if (this.Objects == null || !this.Objects.Any<string>())
			{
				return "import " + this.Module + ((this.Alias == null) ? null : (" as " + this.Alias));
			}
			if (this.Alias != null && this.Objects.Count == 1)
			{
				return string.Concat(new string[]
				{
					"from ",
					this.Module,
					" import ",
					this.Objects.First<string>(),
					" as ",
					this.Alias
				});
			}
			return "from " + this.Module + " import " + string.Join(", ", this.Objects);
		}
	}
}
