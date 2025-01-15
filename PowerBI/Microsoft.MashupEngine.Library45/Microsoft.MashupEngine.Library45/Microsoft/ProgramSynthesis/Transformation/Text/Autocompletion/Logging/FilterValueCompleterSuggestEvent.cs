using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging
{
	// Token: 0x02001E17 RID: 7703
	internal class FilterValueCompleterSuggestEvent : LoggingEvent
	{
		// Token: 0x0601019E RID: 65950 RVA: 0x003748DE File Offset: 0x00372ADE
		public FilterValueCompleterSuggestEvent(FilterOperator op, string prefix, IReadOnlyList<CompletionResult> suggestionList, double suggestionTime)
		{
			this.Op = op;
			this.Prefix = prefix;
			this.SuggestionList = suggestionList;
			this.SuggestionTime = suggestionTime;
		}

		// Token: 0x17002AC0 RID: 10944
		// (get) Token: 0x0601019F RID: 65951 RVA: 0x00374903 File Offset: 0x00372B03
		public override string EventName
		{
			get
			{
				return "FilterValueSuggestEvent";
			}
		}

		// Token: 0x060101A0 RID: 65952 RVA: 0x0037490C File Offset: 0x00372B0C
		protected override IReadOnlyCollection<KeyValuePair<string, double>> GetMetrics()
		{
			if (this._metrics == null)
			{
				this._metrics = new List<KeyValuePair<string, double>>
				{
					new KeyValuePair<string, double>("FilterValueCompleterTimeInMs", this.SuggestionTime),
					new KeyValuePair<string, double>("FilterValueCompleterSuggestionsCount", (double)this.SuggestionList.Count)
				};
				this._metrics.AddRange(base.GetMetrics());
			}
			return this._metrics;
		}

		// Token: 0x060101A1 RID: 65953 RVA: 0x00374978 File Offset: 0x00372B78
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetProperties()
		{
			if (this._properties == null)
			{
				this._properties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("FilterValueCompleterOp", Enum.GetName(typeof(FilterOperator), this.Op))
				};
				this._properties.AddRange(base.GetProperties());
			}
			return this._properties;
		}

		// Token: 0x060101A2 RID: 65954 RVA: 0x003749DC File Offset: 0x00372BDC
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetUserDataProperties()
		{
			if (this._userDataProperties == null)
			{
				this._userDataProperties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("FilterValueCompleterPrefix", this.Prefix),
					new KeyValuePair<string, string>("FilterValueCompleterSuggestions", JsonConvertUtils.SerializeObject(this.SuggestionList))
				};
				this._userDataProperties.AddRange(base.GetUserDataProperties());
			}
			return this._userDataProperties;
		}

		// Token: 0x0400611D RID: 24861
		private List<KeyValuePair<string, double>> _metrics;

		// Token: 0x0400611E RID: 24862
		private List<KeyValuePair<string, string>> _properties;

		// Token: 0x0400611F RID: 24863
		private List<KeyValuePair<string, string>> _userDataProperties;

		// Token: 0x04006120 RID: 24864
		public FilterOperator Op;

		// Token: 0x04006121 RID: 24865
		public string Prefix;

		// Token: 0x04006122 RID: 24866
		public IReadOnlyList<CompletionResult> SuggestionList;

		// Token: 0x04006123 RID: 24867
		public double SuggestionTime;
	}
}
