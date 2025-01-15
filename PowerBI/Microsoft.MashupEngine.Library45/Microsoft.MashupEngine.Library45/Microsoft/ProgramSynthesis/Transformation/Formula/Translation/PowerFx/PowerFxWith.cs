using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D4 RID: 6356
	internal class PowerFxWith : FormulaExpression
	{
		// Token: 0x0600CF74 RID: 53108 RVA: 0x002C4042 File Offset: 0x002C2242
		public PowerFxWith(Dictionary<string, FormulaExpression> record, FormulaExpression body)
		{
			this.Record = record;
			this.Body = body;
			base.Children = record.Values.Concat(this.Body.Yield<FormulaExpression>()).ToList<FormulaExpression>();
		}

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x0600CF75 RID: 53109 RVA: 0x002C4079 File Offset: 0x002C2279
		public FormulaExpression Body { get; }

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x0600CF76 RID: 53110 RVA: 0x002C4081 File Offset: 0x002C2281
		public Dictionary<string, FormulaExpression> Record { get; }

		// Token: 0x0600CF77 RID: 53111 RVA: 0x002C408C File Offset: 0x002C228C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxWith(this.Record.ToDictionary((KeyValuePair<string, FormulaExpression> pair) => pair.Key, (KeyValuePair<string, FormulaExpression> pair) => pair.Value.Accept<FormulaExpression>(visitor)), this.Body.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CF78 RID: 53112 RVA: 0x002C40F4 File Offset: 0x002C22F4
		protected override string ToCodeString()
		{
			int count = this.Record.Keys.Count;
			string text;
			if (count != 0)
			{
				if (count != 1)
				{
					text = string.Join(",", this.Record.Select((KeyValuePair<string, FormulaExpression> item) => string.Format("{0}        {1}: {2}", Environment.NewLine, item.Key, item.Value))) + Environment.NewLine + "    ";
				}
				else
				{
					text = string.Format("{0}: {1}", this.Record.Keys.ElementAt(0), this.Record.Values.ElementAt(0));
				}
			}
			else
			{
				text = string.Empty;
			}
			string text2 = text;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("With({ " + text2 + " },");
			stringBuilder.Append(this.Body.ToString().Indent(5, false) + ")");
			return stringBuilder.ToString();
		}
	}
}
