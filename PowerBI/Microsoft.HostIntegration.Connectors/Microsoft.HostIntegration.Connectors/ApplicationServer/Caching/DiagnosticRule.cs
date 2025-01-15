using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001AB RID: 427
	internal class DiagnosticRule
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0002F501 File Offset: 0x0002D701
		public IList<int> EventIds
		{
			get
			{
				return this._eventIds;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0002F509 File Offset: 0x0002D709
		public IList<string> EventDescription
		{
			get
			{
				return this._eventDescription;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0002F511 File Offset: 0x0002D711
		public IList<string> ViolationDescription
		{
			get
			{
				return this._violationDescription;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0002F519 File Offset: 0x0002D719
		public IList<long> TimeToNextEvent
		{
			get
			{
				return this._timeToNextEventInMsec;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0002F521 File Offset: 0x0002D721
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0002F529 File Offset: 0x0002D729
		public int RuleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002F532 File Offset: 0x0002D732
		public DiagnosticRule(IList<int> eventIds, IList<string> eventDescription, IList<string> violationDescription)
		{
			this._eventIds = eventIds;
			this._eventDescription = eventDescription;
			this._violationDescription = violationDescription;
			this._ruleString = this.GetRuleString();
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002F55B File Offset: 0x0002D75B
		public DiagnosticRule(IList<int> eventIds, IList<string> eventDescription, IList<string> violationDescription, IList<long> timeToNext, int ruleId)
			: this(eventIds, eventDescription, violationDescription)
		{
			this._timeToNextEventInMsec = timeToNext;
			this._ruleId = ruleId;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002F578 File Offset: 0x0002D778
		private string GetRuleString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this._eventDescription)
			{
				stringBuilder.Append(" > " + text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		public override string ToString()
		{
			return this._ruleString;
		}

		// Token: 0x040009C0 RID: 2496
		private IList<int> _eventIds;

		// Token: 0x040009C1 RID: 2497
		private IList<string> _eventDescription;

		// Token: 0x040009C2 RID: 2498
		private IList<string> _violationDescription;

		// Token: 0x040009C3 RID: 2499
		private IList<long> _timeToNextEventInMsec;

		// Token: 0x040009C4 RID: 2500
		private string _ruleString;

		// Token: 0x040009C5 RID: 2501
		private int _ruleId;
	}
}
