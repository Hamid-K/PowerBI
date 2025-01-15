using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x02000330 RID: 816
	public class PowerQueryMCodeBuilder
	{
		// Token: 0x06001206 RID: 4614 RVA: 0x00035189 File Offset: 0x00033389
		private PowerQueryMCodeBuilder(IEscapePowerQueryM escape, IReadOnlyDictionary<MTableFunctionName, string> stepNames, HashSet<string> forbiddenStepNames)
		{
			this._escape = escape;
			this._stepNames = stepNames;
			this._uniqueIdentifierGenerator = new PowerQueryMCodeBuilder.UniqueIdentifierGenerator(forbiddenStepNames);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000351AC File Offset: 0x000333AC
		public PowerQueryMCodeBuilder(string initialTableCode, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape, IReadOnlyDictionary<MTableFunctionName, string> stepNames, HashSet<string> forbiddenStepNames = null, string sourceStepName = null)
			: this(escape, stepNames, forbiddenStepNames)
		{
			this.LastStepName = sourceStepName ?? localizedStrings.Source;
			this._steps = new List<string> { this.EscapedLastStepName + " = " + initialTableCode };
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000351F8 File Offset: 0x000333F8
		public PowerQueryMCodeBuilder(IEnumerable<KeyValuePair<string, string>> initialSteps, IEscapePowerQueryM escape, IReadOnlyDictionary<MTableFunctionName, string> stepNames, HashSet<string> forbiddenStepNames = null)
			: this(escape, stepNames, forbiddenStepNames)
		{
			this._steps = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in initialSteps)
			{
				string text;
				string text2;
				keyValuePair.Deconstruct(out text, out text2);
				string text3 = text;
				string text4 = text2;
				this._steps.Add(this.EscapeIdentifier(text3) + " = " + text4);
				this.LastStepName = text3;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00035280 File Offset: 0x00033480
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x00035288 File Offset: 0x00033488
		private string LastStepName { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00035291 File Offset: 0x00033491
		public string EscapedLastStepName
		{
			get
			{
				return this.EscapeIdentifier(this.LastStepName);
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000352A0 File Offset: 0x000334A0
		public void AddStep(MTableFunctionName funcName, IEnumerable<string> arguments, string stepName = null)
		{
			string text = string.Concat(new string[]
			{
				"Table.",
				funcName.Name,
				"(",
				string.Join(", ", arguments.PrependItem(this.EscapedLastStepName)),
				")"
			});
			this.AddTableFunctionStep(funcName, text, stepName);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00035300 File Offset: 0x00033500
		public string AddStep(string expression, string stepName, bool helper = false)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (stepName == null)
			{
				throw new ArgumentNullException("stepName");
			}
			string text = this._uniqueIdentifierGenerator.GenerateStepName(stepName);
			this._steps.Add(this.EscapeIdentifier(text) + " = " + expression);
			if (!helper)
			{
				this.LastStepName = text;
			}
			return text;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00035360 File Offset: 0x00033560
		public void AddConvertListStep(MTableFunctionName funcName)
		{
			string text = string.Concat(new string[] { "Table.", funcName.Name, "({", this.EscapedLastStepName, "})" });
			this.AddTableFunctionStep(funcName, text, null);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000353AD File Offset: 0x000335AD
		public string EscapeIdentifier(string identifier)
		{
			return this._escape.EscapeIdentifier(identifier);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000353BB File Offset: 0x000335BB
		public string FormatFieldIdentifier(string fieldIdentifier)
		{
			return "[" + this._escape.EscapeFieldIdentifier(fieldIdentifier) + "]";
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000353D8 File Offset: 0x000335D8
		public string EscapeString(string s)
		{
			return this._escape.EscapeString(s);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000353E8 File Offset: 0x000335E8
		public string GetCode()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			using (codeBuilder.NewScope("let", 1U))
			{
				for (int i = 0; i < this._steps.Count - 1; i++)
				{
					codeBuilder.AppendIndented(this._steps[i]);
					codeBuilder.AppendLine(",");
				}
				codeBuilder.AppendLine(this._steps.Last<string>());
			}
			using (codeBuilder.NewScope("in", 1U))
			{
				codeBuilder.AppendLine(this.EscapedLastStepName);
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000354A4 File Offset: 0x000336A4
		private void AddTableFunctionStep(MTableFunctionName funcName, string body, string stepName = null)
		{
			if (stepName == null && !this._stepNames.TryGetValue(funcName, out stepName))
			{
				throw new ArgumentException("No step name found for " + funcName.Name);
			}
			this.AddStep(body, stepName, false);
		}

		// Token: 0x040008E8 RID: 2280
		private readonly IEscapePowerQueryM _escape;

		// Token: 0x040008E9 RID: 2281
		private readonly List<string> _steps;

		// Token: 0x040008EA RID: 2282
		private readonly PowerQueryMCodeBuilder.UniqueIdentifierGenerator _uniqueIdentifierGenerator;

		// Token: 0x040008EB RID: 2283
		private readonly IReadOnlyDictionary<MTableFunctionName, string> _stepNames;

		// Token: 0x02000331 RID: 817
		public class UniqueIdentifierGenerator
		{
			// Token: 0x06001214 RID: 4628 RVA: 0x000354DA File Offset: 0x000336DA
			public UniqueIdentifierGenerator(HashSet<string> forbiddenStepNames = null)
			{
				this._forbiddenStepNames = forbiddenStepNames;
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06001215 RID: 4629 RVA: 0x00035500 File Offset: 0x00033700
			public IEnumerable<string> ForbiddenStepNames
			{
				get
				{
					IEnumerable<string> forbiddenStepNames = this._forbiddenStepNames;
					return forbiddenStepNames ?? Enumerable.Empty<string>();
				}
			}

			// Token: 0x06001216 RID: 4630 RVA: 0x00035520 File Offset: 0x00033720
			public string GenerateStepName(string stepName)
			{
				string text;
				HashSet<string> forbiddenStepNames;
				do
				{
					int num;
					if (this._stepIndexes.TryGetValue(stepName, out num))
					{
						text = stepName + num.ToString();
						this._stepIndexes[stepName] = num + 1;
					}
					else
					{
						text = stepName;
						this._stepIndexes[stepName] = 1;
					}
					forbiddenStepNames = this._forbiddenStepNames;
				}
				while ((forbiddenStepNames != null && forbiddenStepNames.Contains(text)) || this._usedStepNames.Contains(text));
				this._usedStepNames.Add(text);
				return text;
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x0003559C File Offset: 0x0003379C
			public void Reset()
			{
				this._stepIndexes.Clear();
				this._usedStepNames.Clear();
			}

			// Token: 0x040008ED RID: 2285
			private readonly HashSet<string> _forbiddenStepNames;

			// Token: 0x040008EE RID: 2286
			private readonly HashSet<string> _usedStepNames = new HashSet<string>();

			// Token: 0x040008EF RID: 2287
			private readonly Dictionary<string, int> _stepIndexes = new Dictionary<string, int>();
		}
	}
}
