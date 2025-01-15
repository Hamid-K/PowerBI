using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018BE RID: 6334
	public class PowerQueryLet : FormulaExpression, IFormulaBlock
	{
		// Token: 0x0600CE6B RID: 52843 RVA: 0x002C0307 File Offset: 0x002BE507
		public PowerQueryLet(IEnumerable<PowerQueryStep> steps, FormulaExpression body = null, IReadOnlyList<KeyValuePair<string, FormulaExpression>> metadata = null)
		{
			this.Steps = steps.ToList<PowerQueryStep>();
			this.Body = body;
			base.Children = this.Steps.AppendItem(this.Body).ToList<FormulaExpression>();
			this.Metadata = metadata;
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x0600CE6C RID: 52844 RVA: 0x002C0345 File Offset: 0x002BE545
		public FormulaExpression Body { get; }

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x0600CE6D RID: 52845 RVA: 0x002C034D File Offset: 0x002BE54D
		public IReadOnlyList<KeyValuePair<string, FormulaExpression>> Metadata { get; }

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600CE6E RID: 52846 RVA: 0x002C0355 File Offset: 0x002BE555
		public IReadOnlyList<PowerQueryStep> Steps { get; }

		// Token: 0x0600CE6F RID: 52847 RVA: 0x002C0360 File Offset: 0x002BE560
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			IEnumerable<PowerQueryStep> enumerable = this.Steps.Select((PowerQueryStep step) => step.Accept<FormulaExpression>(visitor) as PowerQueryStep).ToList<PowerQueryStep>();
			FormulaExpression body = this.Body;
			FormulaExpression formulaExpression = ((body != null) ? body.Accept<FormulaExpression>(visitor) : null);
			IReadOnlyList<KeyValuePair<string, FormulaExpression>> metadata = this.Metadata;
			return new PowerQueryLet(enumerable, formulaExpression, (metadata != null) ? metadata.Select2((string key, FormulaExpression value) => KVP.Create<string, FormulaExpression>(key, value.Accept<FormulaExpression>(visitor))).ToList<KeyValuePair<string, FormulaExpression>>() : null);
		}

		// Token: 0x0600CE70 RID: 52848 RVA: 0x002C03D8 File Offset: 0x002BE5D8
		public virtual void AppendCodeString(CodeBuilder codeBuilder)
		{
			if (this.Steps.Count == 0)
			{
				return;
			}
			codeBuilder.AppendLine("let");
			codeBuilder.PushIndent(1U);
			for (int i = 0; i < this.Steps.Count; i++)
			{
				this.Steps[i].AppendCodeString(codeBuilder);
				if (i == this.Steps.Count - 1)
				{
					codeBuilder.AppendLine();
				}
				else
				{
					codeBuilder.AppendLine(",");
				}
			}
			codeBuilder.PopIndent();
			codeBuilder.AppendLine("in");
			codeBuilder.PushIndent(1U);
			List<PowerQueryStep> list = this.Steps.Where((PowerQueryStep s) => s.IsOutput).ToList<PowerQueryStep>();
			FormulaExpression formulaExpression;
			if ((formulaExpression = this.Body) == null)
			{
				if (!list.Any<PowerQueryStep>())
				{
					formulaExpression = new PowerQueryVariable(this.Steps[this.Steps.Count - 1].StepName);
				}
				else
				{
					formulaExpression = new PowerQueryList(list.Select((PowerQueryStep s) => new PowerQueryVariable(s.StepName)).ToList<PowerQueryVariable>());
				}
			}
			FormulaExpression formulaExpression2 = formulaExpression;
			codeBuilder.AppendIndented(formulaExpression2.ToString());
			IReadOnlyList<KeyValuePair<string, FormulaExpression>> metadata = this.Metadata;
			bool? flag = ((metadata != null) ? new bool?(metadata.Any<KeyValuePair<string, FormulaExpression>>()) : null);
			if (flag != null && flag.GetValueOrDefault())
			{
				codeBuilder.Append(" meta [" + string.Join(", ", this.Metadata.Select((KeyValuePair<string, FormulaExpression> kvp) => string.Format("{0} = {1}", kvp.Key, kvp.Value))) + "]");
			}
			codeBuilder.PopIndent();
		}

		// Token: 0x0600CE71 RID: 52849 RVA: 0x002C058C File Offset: 0x002BE78C
		public string ToString(uint indentLevel, uint indentSize)
		{
			CodeBuilder codeBuilder = new CodeBuilder(indentSize);
			int num = 0;
			while ((long)num < (long)((ulong)indentLevel))
			{
				codeBuilder.PushIndent(1U);
				num++;
			}
			this.AppendCodeString(codeBuilder);
			return codeBuilder.GetCode();
		}

		// Token: 0x0600CE72 RID: 52850 RVA: 0x002C05C2 File Offset: 0x002BE7C2
		protected override string ToCodeString()
		{
			return this.ToString(0U, 4U);
		}
	}
}
