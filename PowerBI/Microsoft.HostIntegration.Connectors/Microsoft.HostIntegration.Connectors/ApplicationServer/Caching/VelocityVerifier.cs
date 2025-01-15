using System;
using System.Collections.Generic;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B9 RID: 441
	internal class VelocityVerifier : DiagStateVerifier
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x0003094B File Offset: 0x0002EB4B
		public VelocityVerifier(List<DiagnosticRule> rules)
			: base(rules)
		{
			this._rules = rules;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003095B File Offset: 0x0002EB5B
		public VelocityVerifier()
		{
			this.InitDefaultRules();
			base.Rules = this._rules;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00030978 File Offset: 0x0002EB78
		private void InitDefaultRules()
		{
			this._rules = new List<DiagnosticRule>();
			List<int> list = new List<int> { 10 };
			List<string> list2 = new List<string> { DiagEventName.RespondedDom.ToString() };
			List<string> list3 = new List<string> { "TooMuchLoad" };
			List<long> list4 = new List<long> { 500L };
			this._rules.Add(new DiagnosticRule(list, list2, list3, list4, 1));
			List<int> list5 = new List<int> { 7, 11 };
			List<string> list6 = new List<string>
			{
				DiagEventName.PacketRead.ToString(),
				DiagEventName.ResponseFromStore.ToString()
			};
			this._rules.Add(new DiagnosticRule(list5, list6, list3, list4, 2));
			List<int> list7 = new List<int> { 21 };
			List<string> list8 = new List<string> { DiagEventName.ReplicationCompleted.ToString() };
			List<string> list9 = new List<string> { "$" };
			List<long> list10 = new List<long> { 500L };
			this._rules.Add(new DiagnosticRule(list7, list8, list9, list10, 3));
			List<int> list11 = new List<int> { 1 };
			List<string> list12 = new List<string> { DiagEventName.Completed.ToString() };
			List<string> list13 = new List<string> { "$" };
			List<long> list14 = new List<long> { 500L };
			this._rules.Add(new DiagnosticRule(list11, list12, list13, list14, 4));
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00030B58 File Offset: 0x0002ED58
		public override List<DiagRuleViolation> Validate(DiagOperationState opState)
		{
			bool flag = false;
			List<DiagRuleViolation> list = base.IsViolationFound(opState, out flag);
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			if (flag)
			{
				return list;
			}
			long ticks = DateTime.UtcNow.Ticks;
			long num = (long)TimeSpan.FromTicks(ticks - opState.StartTime).TotalMilliseconds;
			if (num > (long)DiagConfigManager.MaxTimePerRequest)
			{
				EventLogWriter.WriteInfo("DiagnosticStats", "Spent {0}, cur:{1}, opTick:{2} ms:{3}", new object[]
				{
					num,
					ticks,
					opState.StartTime,
					TimeSpan.FromTicks(num).ToString()
				});
				return list;
			}
			opState.IsRuntimeAware = false;
			DiagEventManager.AddRequestStates(opState);
			return null;
		}

		// Token: 0x040009F5 RID: 2549
		private List<DiagnosticRule> _rules;
	}
}
