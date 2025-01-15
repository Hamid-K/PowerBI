using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018AB RID: 6315
	internal class PowerQueryRecord : FormulaExpression
	{
		// Token: 0x0600CE16 RID: 52758 RVA: 0x002BFBCB File Offset: 0x002BDDCB
		public PowerQueryRecord(IReadOnlyDictionary<string, FormulaExpression> values)
		{
			this.Values = values;
			base.Children = values.Values.ToList<FormulaExpression>();
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x0600CE17 RID: 52759 RVA: 0x002BFBEB File Offset: 0x002BDDEB
		public IReadOnlyDictionary<string, FormulaExpression> Values { get; }

		// Token: 0x0600CE18 RID: 52760 RVA: 0x002BFBF4 File Offset: 0x002BDDF4
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryRecord(this.Values.ToDictionary((KeyValuePair<string, FormulaExpression> kv) => kv.Key, (KeyValuePair<string, FormulaExpression> kv) => kv.Value.Accept<FormulaExpression>(visitor)));
		}

		// Token: 0x0600CE19 RID: 52761 RVA: 0x002BFC4C File Offset: 0x002BDE4C
		protected override string ToCodeString()
		{
			return "[" + string.Join(", ", this.Values.Select((KeyValuePair<string, FormulaExpression> kv) => string.Format("{0}={1}", kv.Key, kv.Value))) + "]";
		}
	}
}
