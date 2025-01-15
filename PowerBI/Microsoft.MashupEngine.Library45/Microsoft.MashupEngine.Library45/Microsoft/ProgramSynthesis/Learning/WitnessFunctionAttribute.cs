using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006D5 RID: 1749
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class WitnessFunctionAttribute : Attribute
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x00069A5F File Offset: 0x00067C5F
		// (set) Token: 0x06002602 RID: 9730 RVA: 0x00069A67 File Offset: 0x00067C67
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string[] DependsOnSymbols { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x00069A70 File Offset: 0x00067C70
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x00069A78 File Offset: 0x00067C78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int[] DependsOnParameters { get; set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00069A81 File Offset: 0x00067C81
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x00069A89 File Offset: 0x00067C89
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Incomplete { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x00069A92 File Offset: 0x00067C92
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x00069A9A File Offset: 0x00067C9A
		public bool Verify { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00069AA3 File Offset: 0x00067CA3
		internal string RuleName { get; }

		// Token: 0x0600260A RID: 9738 RVA: 0x00069AAB File Offset: 0x00067CAB
		public WitnessFunctionAttribute(string parameterSymbolName)
		{
			this._parameterSymbolName = parameterSymbolName;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00069ABA File Offset: 0x00067CBA
		public WitnessFunctionAttribute(int parameterSymbolIndex)
		{
			this._parameterSymbolIndex = new int?(parameterSymbolIndex);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00069ACE File Offset: 0x00067CCE
		public WitnessFunctionAttribute(string ruleName, string parameterSymbolName)
		{
			this._parameterSymbolName = parameterSymbolName;
			this.RuleName = ruleName;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00069AE4 File Offset: 0x00067CE4
		public WitnessFunctionAttribute(string ruleName, int parameterSymbolIndex)
		{
			this._parameterSymbolIndex = new int?(parameterSymbolIndex);
			this.RuleName = ruleName;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00069B00 File Offset: 0x00067D00
		public int ParameterIndex(GrammarRule rule)
		{
			this.InitializeRule(rule);
			if (this._parameterSymbolIndex != null)
			{
				int? num = this._parameterSymbolIndex;
				int num2 = 0;
				if (!((num.GetValueOrDefault() < num2) & (num != null)))
				{
					num = this._parameterSymbolIndex;
					num2 = rule.Body.Count;
					if (!((num.GetValueOrDefault() >= num2) & (num != null)))
					{
						return this._parameterSymbolIndex.Value;
					}
				}
				throw new WitnessFunctionAttribute.ParameterOutOfRangeException();
			}
			if (this._parameterSymbolName == null)
			{
				throw new InvalidOperationException("No parameter information has been provided");
			}
			Symbol specifiedParameter = rule.Grammar.Symbol(this._parameterSymbolName);
			var <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType = rule.Body.Select((Symbol s, int i) => new
			{
				Symbol = s,
				Index = i
			}).SingleOrDefault(p => p.Symbol.Equals(specifiedParameter));
			if (<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType == null)
			{
				throw new WitnessFunctionAttribute.ParameterOutOfRangeException();
			}
			return <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.Index;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00069BF4 File Offset: 0x00067DF4
		public int[] PrerequisiteIndexes(GrammarRule rule)
		{
			this.InitializeRule(rule);
			if (this.DependsOnParameters != null)
			{
				return this.DependsOnParameters;
			}
			if (this.DependsOnSymbols == null)
			{
				return this.DependsOnParameters = new int[0];
			}
			Symbol[] array = this.DependsOnSymbols.Select(new Func<string, Symbol>(rule.Grammar.Symbol)).ToArray<Symbol>();
			HashSet<Symbol> hashSet = array.ConvertToHashSet<Symbol>();
			Dictionary<Symbol, int> occurrence = new Dictionary<Symbol, int>();
			for (int i = 0; i < rule.Body.Count; i++)
			{
				Symbol symbol = rule.Body[i];
				if (occurrence.ContainsKey(symbol) && hashSet.Contains(symbol))
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Ambiguous resolution: there are multiple parameters {0} in the body of the rule {1}", new object[] { symbol, rule })));
				}
				occurrence[symbol] = i;
			}
			this.DependsOnParameters = array.Select((Symbol p) => occurrence[p]).ToArray<int>();
			return this.DependsOnParameters;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00069D04 File Offset: 0x00067F04
		private void InitializeRule(GrammarRule rule)
		{
			if (this.RuleName == null)
			{
				return;
			}
			if (this._rule == null)
			{
				this._rule = rule.Grammar.Rule(this.RuleName);
			}
			if (!this._rule.Equals(rule))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("This witness function is declared for the rule {0}, but called for the rule {1}", new object[] { this.RuleName, rule })), "rule");
			}
		}

		// Token: 0x040011FF RID: 4607
		private int? _parameterSymbolIndex;

		// Token: 0x04001200 RID: 4608
		private readonly string _parameterSymbolName;

		// Token: 0x04001201 RID: 4609
		private GrammarRule _rule;

		// Token: 0x020006D6 RID: 1750
		internal class ParameterOutOfRangeException : Exception
		{
		}
	}
}
