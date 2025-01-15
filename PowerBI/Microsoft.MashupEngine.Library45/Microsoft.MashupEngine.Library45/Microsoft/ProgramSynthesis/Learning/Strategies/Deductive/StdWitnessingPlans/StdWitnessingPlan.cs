using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000742 RID: 1858
	internal abstract class StdWitnessingPlan<TRule, TSpec> : IWitnessingPlan where TRule : GrammarRule where TSpec : Spec
	{
		// Token: 0x060027DA RID: 10202 RVA: 0x00071427 File Offset: 0x0006F627
		private WitnessFunction PreferredWitnessFunctionForInternal(TRule rule, int parameter, TSpec spec)
		{
			if (this._witnessFunctions == null && !this._performedAttributeLookup)
			{
				this.PerformAttributeLookup(rule);
			}
			if (this._witnessFunctions == null)
			{
				return this.PreferredWitnessFunctionFor(rule, parameter, spec);
			}
			return this._witnessFunctions[parameter];
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00071460 File Offset: 0x0006F660
		private void PerformAttributeLookup(TRule rule)
		{
			Dictionary<int, WitnessFunction> dictionary = (from m in base.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				from attr in m.GetCustomAttributes<WitnessFunctionAttribute>()
				where attr != null
				select new WitnessFunction.Static(m, attr, rule)).ToDictionary((WitnessFunction i) => i.ParameterIndex);
			if (dictionary.Count == rule.Body.Count)
			{
				this._witnessFunctions = dictionary;
			}
			this._performedAttributeLookup = true;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0007154F File Offset: 0x0006F74F
		internal virtual WitnessFunction PreferredWitnessFunctionFor(TRule rule, int parameter, TSpec spec)
		{
			if (this._witnessFunctions == null && !this._performedAttributeLookup)
			{
				this.PerformAttributeLookup(rule);
			}
			if (this._witnessFunctions == null)
			{
				throw new InvalidOperationException("A witness plan should specify as many preferred witness functions as there are parameter symbols on the right hand side of the rule.");
			}
			return this._witnessFunctions[parameter];
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x0006FA3E File Offset: 0x0006DC3E
		public Type RuleType
		{
			get
			{
				return typeof(TRule);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00071587 File Offset: 0x0006F787
		public Type SpecType
		{
			get
			{
				return typeof(TSpec);
			}
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00071593 File Offset: 0x0006F793
		bool IWitnessingPlan.CanCall(GrammarRule rule, Spec spec)
		{
			return this.RuleType.IsInstanceOfType(rule) && this.SpecType.IsInstanceOfType(spec) && this.CanCall((TRule)((object)rule), (TSpec)((object)spec));
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0000A5FD File Offset: 0x000087FD
		public virtual bool CanCall(TRule rule, TSpec spec)
		{
			return true;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000715C8 File Offset: 0x0006F7C8
		WitnessFunction IWitnessingPlan.PreferredWitnessFunctionFor(GrammarRule rule, int parameter, Spec spec)
		{
			this.VerifyCall(rule, spec);
			if (parameter < 0 || parameter >= rule.Body.Count)
			{
				throw new ArgumentOutOfRangeException("parameter");
			}
			return this.PreferredWitnessFunctionForInternal(rule as TRule, parameter, spec as TSpec);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00071617 File Offset: 0x0006F817
		private void VerifyCall(GrammarRule rule, Spec spec)
		{
			if (!((IWitnessingPlan)this).CanCall(rule, spec))
			{
				throw new ArgumentException("The rule and the spec should be derived from the supported base types.");
			}
		}

		// Token: 0x0400136D RID: 4973
		private Dictionary<int, WitnessFunction> _witnessFunctions;

		// Token: 0x0400136E RID: 4974
		private bool _performedAttributeLookup;
	}
}
