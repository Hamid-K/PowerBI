using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x02000721 RID: 1825
	public class WitnessingPlan
	{
		// Token: 0x06002778 RID: 10104 RVA: 0x0006FC04 File Offset: 0x0006DE04
		internal WitnessingPlan(IWitnessingPlan plan, GrammarRule rule, Spec spec)
		{
			this.WitnessFunctions = rule.Body.Select((Symbol p, int i) => plan.PreferredWitnessFunctionFor(rule, i, spec)).ToArray<WitnessFunction>();
			this.Origin = plan.GetType();
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0006FC6A File Offset: 0x0006DE6A
		public WitnessingPlan(WitnessFunction[] witnessFunctions, Type origin = null)
		{
			this.Origin = origin;
			this.WitnessFunctions = witnessFunctions;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0006FC80 File Offset: 0x0006DE80
		public WitnessingPlan(IEnumerable<WitnessFunction> parameterWitnessFunctions, Type origin = null)
			: this(parameterWitnessFunctions.ToArray<WitnessFunction>(), origin)
		{
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x0006FC8F File Offset: 0x0006DE8F
		public WitnessFunction[] WitnessFunctions { get; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x0006FC97 File Offset: 0x0006DE97
		internal Type Origin { get; }

		// Token: 0x0600277D RID: 10109 RVA: 0x0006FCA0 File Offset: 0x0006DEA0
		internal WitnessingPlan.CompareResult CompareTo(WitnessingPlan other)
		{
			if (other == null || this.WitnessFunctions.Length != other.WitnessFunctions.Length)
			{
				throw new ArgumentException("Plan should not be null and should have the same number of witness functions.", "other");
			}
			if (this.WitnessFunctions.Length == 0)
			{
				return WitnessingPlan.CompareResult.EquallyPreferred;
			}
			int num = this.WitnessFunctions[0].CompareTo(other.WitnessFunctions[0]);
			for (int i = 1; i < this.WitnessFunctions.Length; i++)
			{
				int num2 = this.WitnessFunctions[i].CompareTo(other.WitnessFunctions[i]);
				if (num == 0)
				{
					num = num2;
				}
				else if (num * num2 < 0)
				{
					return WitnessingPlan.CompareResult.Incomparable;
				}
			}
			if (num == 0)
			{
				return WitnessingPlan.CompareResult.EquallyPreferred;
			}
			if (num >= 0)
			{
				return WitnessingPlan.CompareResult.LessPreferred;
			}
			return WitnessingPlan.CompareResult.MorePreferred;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0006FD38 File Offset: 0x0006DF38
		public override string ToString()
		{
			Type origin = this.Origin;
			string text;
			if ((text = ((origin != null) ? origin.Name : null)) == null)
			{
				text = this.WitnessFunctions.Select((WitnessFunction wf) => wf.Origin).DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null);
			}
			return text;
		}

		// Token: 0x02000722 RID: 1826
		internal enum CompareResult
		{
			// Token: 0x04001341 RID: 4929
			EquallyPreferred,
			// Token: 0x04001342 RID: 4930
			LessPreferred,
			// Token: 0x04001343 RID: 4931
			MorePreferred,
			// Token: 0x04001344 RID: 4932
			Incomparable
		}
	}
}
