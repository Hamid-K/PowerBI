using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018C2 RID: 6338
	public class PowerQueryLetExpressionBuilder
	{
		// Token: 0x0600CE86 RID: 52870 RVA: 0x002C0758 File Offset: 0x002BE958
		public PowerQueryLetExpressionBuilder(string initialLastStepName = null, HashSet<string> forbiddenStepNames = null)
		{
			this._initialLastStepName = initialLastStepName;
			this.LastStepName = this._initialLastStepName;
			this._uniqueIdentifierGenerator = new PowerQueryMCodeBuilder.UniqueIdentifierGenerator(forbiddenStepNames);
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x0600CE87 RID: 52871 RVA: 0x002C0795 File Offset: 0x002BE995
		public IEnumerable<string> AllForbiddenStepNames
		{
			get
			{
				return this._uniqueIdentifierGenerator.ForbiddenStepNames.Concat(this._steps.Select((PowerQueryStep step) => step.StepName));
			}
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x0600CE88 RID: 52872 RVA: 0x002C07D1 File Offset: 0x002BE9D1
		// (set) Token: 0x0600CE89 RID: 52873 RVA: 0x002C07D9 File Offset: 0x002BE9D9
		public string LastStepName { get; private set; }

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x0600CE8A RID: 52874 RVA: 0x002C07E2 File Offset: 0x002BE9E2
		public int StepCount
		{
			get
			{
				return this._steps.Count;
			}
		}

		// Token: 0x0600CE8B RID: 52875 RVA: 0x002C07EF File Offset: 0x002BE9EF
		public void AddMetadata(string key, FormulaExpression val)
		{
			this._metadata.Add(new KeyValuePair<string, FormulaExpression>(key, val));
		}

		// Token: 0x0600CE8C RID: 52876 RVA: 0x002C0804 File Offset: 0x002BEA04
		public void AddStep(string baseStepName, FormulaExpression step, bool isOutput = false)
		{
			string text = this._uniqueIdentifierGenerator.GenerateStepName(baseStepName);
			this._steps.Add(new PowerQueryStep(text, step, isOutput));
			this.LastStepName = text;
		}

		// Token: 0x0600CE8D RID: 52877 RVA: 0x002C0838 File Offset: 0x002BEA38
		public void AddSteps(PowerQueryLet powerQueryProgram)
		{
			if (this.AllForbiddenStepNames.Intersect(powerQueryProgram.Steps.Select((PowerQueryStep kv) => kv.StepName)).Any<string>())
			{
				throw new ArgumentException("AddSteps() must be called with a PowerQueryLet with already unique step names.");
			}
			foreach (PowerQueryStep powerQueryStep in powerQueryProgram.Steps)
			{
				this.AddStep(powerQueryStep.StepName, powerQueryStep.Expression, powerQueryStep.IsOutput);
			}
		}

		// Token: 0x0600CE8E RID: 52878 RVA: 0x002C08E0 File Offset: 0x002BEAE0
		public PowerQueryLet CombineSteps()
		{
			return new PowerQueryLet(this._steps, null, this._metadata);
		}

		// Token: 0x0600CE8F RID: 52879 RVA: 0x002C08F4 File Offset: 0x002BEAF4
		public void Reset()
		{
			this._steps.Clear();
			this._uniqueIdentifierGenerator.Reset();
			this.LastStepName = this._initialLastStepName;
		}

		// Token: 0x0600CE90 RID: 52880 RVA: 0x002C0918 File Offset: 0x002BEB18
		internal void AddStep(string baseStepName, Func<PowerQueryVariable, FormulaExpression> stepFunc)
		{
			this.AddStep(baseStepName, stepFunc(new PowerQueryVariable(this.LastStepName)), false);
		}

		// Token: 0x040050B4 RID: 20660
		private readonly string _initialLastStepName;

		// Token: 0x040050B5 RID: 20661
		private readonly List<KeyValuePair<string, FormulaExpression>> _metadata = new List<KeyValuePair<string, FormulaExpression>>();

		// Token: 0x040050B6 RID: 20662
		private readonly List<PowerQueryStep> _steps = new List<PowerQueryStep>();

		// Token: 0x040050B7 RID: 20663
		private readonly PowerQueryMCodeBuilder.UniqueIdentifierGenerator _uniqueIdentifierGenerator;
	}
}
