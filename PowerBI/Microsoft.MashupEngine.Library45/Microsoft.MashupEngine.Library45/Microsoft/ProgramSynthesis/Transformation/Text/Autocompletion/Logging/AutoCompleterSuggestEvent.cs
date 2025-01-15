using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging
{
	// Token: 0x02001E16 RID: 7702
	internal class AutoCompleterSuggestEvent : LoggingEvent
	{
		// Token: 0x17002AB9 RID: 10937
		// (get) Token: 0x06010194 RID: 65940 RVA: 0x00374718 File Offset: 0x00372918
		public override string EventName
		{
			get
			{
				return "AutoCompleterSuggestEvent";
			}
		}

		// Token: 0x17002ABA RID: 10938
		// (get) Token: 0x06010195 RID: 65941 RVA: 0x0037471F File Offset: 0x0037291F
		public AutoCompleterCreateEvent CreateEvent { get; }

		// Token: 0x17002ABB RID: 10939
		// (get) Token: 0x06010196 RID: 65942 RVA: 0x00374727 File Offset: 0x00372927
		public IReadOnlyList<ISuggestion> Suggestions { get; }

		// Token: 0x17002ABC RID: 10940
		// (get) Token: 0x06010197 RID: 65943 RVA: 0x0037472F File Offset: 0x0037292F
		public bool SuggestionsAreIncomplete { get; }

		// Token: 0x17002ABD RID: 10941
		// (get) Token: 0x06010198 RID: 65944 RVA: 0x00374737 File Offset: 0x00372937
		public double SuggestionTime { get; }

		// Token: 0x17002ABE RID: 10942
		// (get) Token: 0x06010199 RID: 65945 RVA: 0x0037473F File Offset: 0x0037293F
		public string UserInput { get; }

		// Token: 0x0601019A RID: 65946 RVA: 0x00374748 File Offset: 0x00372948
		public AutoCompleterSuggestEvent(AutoCompleterCreateEvent createEvent, string suggestEventId, string userInput, IReadOnlyList<ISuggestion> suggestions, double suggestionTime = double.NegativeInfinity)
			: base(createEvent.Id)
		{
			this.SuggestEventId = suggestEventId;
			this.CreateEvent = createEvent;
			this.UserInput = userInput;
			this.Suggestions = suggestions;
			this.SuggestionTime = suggestionTime;
			this.SuggestionsAreIncomplete = this.Suggestions.Count > 8;
			if (this.SuggestionsAreIncomplete)
			{
				this.Suggestions = this.Suggestions.Take(8).ToList<ISuggestion>();
			}
		}

		// Token: 0x17002ABF RID: 10943
		// (get) Token: 0x0601019B RID: 65947 RVA: 0x003747B9 File Offset: 0x003729B9
		public string SuggestEventId { get; }

		// Token: 0x0601019C RID: 65948 RVA: 0x003747C4 File Offset: 0x003729C4
		protected override IReadOnlyCollection<KeyValuePair<string, double>> GetMetrics()
		{
			if (this._metrics == null)
			{
				this._metrics = new List<KeyValuePair<string, double>>
				{
					new KeyValuePair<string, double>("AutoCompleterSuggestTimeInMs", this.SuggestionTime)
				};
				this._metrics.AddRange(base.GetMetrics());
			}
			return this._metrics;
		}

		// Token: 0x0601019D RID: 65949 RVA: 0x00374814 File Offset: 0x00372A14
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetUserDataProperties()
		{
			if (this._userDataProperties == null)
			{
				this._userDataProperties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("AutoCompleterEventId", this.CreateEvent.AutoCompleteEventId.ToString()),
					new KeyValuePair<string, string>("AutoCompleterSuggestEventId", this.SuggestEventId.ToString()),
					new KeyValuePair<string, string>("AutoCompleterSuggestions", JsonConvertUtils.SerializeObject(this.Suggestions)),
					new KeyValuePair<string, string>("AutoCompleterSuggestionsAreIncomplete", this.SuggestionsAreIncomplete.AsLoggingValue()),
					new KeyValuePair<string, string>("AutoCompleterUserInput", this.UserInput)
				};
				this._userDataProperties.AddRange(base.GetUserDataProperties());
			}
			return this._userDataProperties;
		}

		// Token: 0x04006114 RID: 24852
		public const int MaxSuggestionsToLog = 8;

		// Token: 0x0400611B RID: 24859
		private List<KeyValuePair<string, double>> _metrics;

		// Token: 0x0400611C RID: 24860
		private List<KeyValuePair<string, string>> _userDataProperties;
	}
}
