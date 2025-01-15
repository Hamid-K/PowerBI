using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B4 RID: 436
	internal abstract class DiagStateVerifier : IVerifier
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0002FDD4 File Offset: 0x0002DFD4
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		public List<DiagnosticRule> Rules
		{
			get
			{
				return this._rules;
			}
			set
			{
				this._rules = value;
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002FDE5 File Offset: 0x0002DFE5
		public DiagStateVerifier(List<DiagnosticRule> diagRules)
		{
			this._rules = diagRules;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00002061 File Offset: 0x00000261
		public DiagStateVerifier()
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002FDF4 File Offset: 0x0002DFF4
		public List<DiagRuleViolation> IsViolationFound(DiagOperationState opState, out bool isRequestComplete)
		{
			isRequestComplete = false;
			bool flag = false;
			List<DiagRuleViolation> list = new List<DiagRuleViolation>();
			if (opState.IsAnyEventFailed())
			{
				list.Add(new DiagRuleViolation
				{
					IsRuleViolated = true
				});
				return list;
			}
			foreach (DiagnosticRule diagnosticRule in this._rules)
			{
				DiagRuleViolation diagRuleViolation = this.CheckRuleViolation(diagnosticRule, opState);
				if (!diagRuleViolation.IsRuleViolated)
				{
					flag = true;
					break;
				}
				list.Add(diagRuleViolation);
				if (diagRuleViolation.IsFinalStateReached)
				{
					isRequestComplete = true;
				}
			}
			if (flag)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002FE9C File Offset: 0x0002E09C
		private DiagRuleViolation CheckRuleViolation(DiagnosticRule rule, DiagOperationState opState)
		{
			DiagRuleViolation diagRuleViolation = new DiagRuleViolation();
			List<int> list = (List<int>)rule.EventIds;
			List<DiagEvent> events = opState.Events;
			if (events.Count < list.Count)
			{
				diagRuleViolation.IsRuleViolated = true;
			}
			int num = 0;
			bool flag = false;
			int num2 = list[list.Count - 1];
			int num3 = 0;
			while (num3 < events.Count && num < list.Count)
			{
				DiagEvent diagEvent = events[num3];
				if (diagEvent != null)
				{
					int currentEventName = (int)diagEvent.CurrentEventName;
					if (list[num] == currentEventName)
					{
						num++;
					}
					if (!flag && num2 == currentEventName)
					{
						flag = true;
					}
				}
				num3++;
			}
			if (num == list.Count)
			{
				diagRuleViolation.IsFinalStateReached = true;
				long num4 = rule.TimeToNextEvent[rule.TimeToNextEvent.Count - 1];
				long num5 = opState.StartTime - opState.LastEventTime;
				if (num5 >= 0L && (long)TimeSpan.FromTicks(num5).Milliseconds > num4)
				{
					diagRuleViolation.IsRuleViolated = true;
					diagRuleViolation.RuleViolated = rule;
					diagRuleViolation.ViolationIndex = num;
				}
				diagRuleViolation.IsRuleViolated = false;
			}
			else
			{
				diagRuleViolation.IsRuleViolated = true;
				diagRuleViolation.ViolationIndex = num;
				diagRuleViolation.IsFinalStateReached = flag;
				diagRuleViolation.RuleViolated = rule;
			}
			return diagRuleViolation;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00003CAB File Offset: 0x00001EAB
		public bool IsViolationFound(DiagOperationState opState, out bool isRequestComplete, out List<DiagnosticRule> violations)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002FFD0 File Offset: 0x0002E1D0
		public virtual List<DiagRuleViolation> Validate(DiagOperationState opState)
		{
			bool flag;
			return this.IsViolationFound(opState, out flag);
		}

		// Token: 0x040009DE RID: 2526
		private List<DiagnosticRule> _rules;
	}
}
