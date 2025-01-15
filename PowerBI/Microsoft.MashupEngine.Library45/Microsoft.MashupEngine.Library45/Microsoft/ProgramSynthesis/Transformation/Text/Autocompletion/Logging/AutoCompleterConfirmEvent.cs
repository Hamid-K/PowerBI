using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging
{
	// Token: 0x02001E13 RID: 7699
	internal class AutoCompleterConfirmEvent : LoggingEvent
	{
		// Token: 0x17002AAE RID: 10926
		// (get) Token: 0x06010176 RID: 65910 RVA: 0x00374313 File Offset: 0x00372513
		public string SuggestEventId { get; }

		// Token: 0x17002AAF RID: 10927
		// (get) Token: 0x06010177 RID: 65911 RVA: 0x0037431B File Offset: 0x0037251B
		public int SuggestionIndex { get; }

		// Token: 0x06010178 RID: 65912 RVA: 0x00374323 File Offset: 0x00372523
		public AutoCompleterConfirmEvent(AutoCompleterCreateEvent createEvent, string suggestEventId, int suggestionIndex)
			: base(createEvent.Id)
		{
			this.SuggestEventId = suggestEventId;
			this.SuggestionIndex = suggestionIndex;
		}

		// Token: 0x17002AB0 RID: 10928
		// (get) Token: 0x06010179 RID: 65913 RVA: 0x0037433F File Offset: 0x0037253F
		public override string EventName
		{
			get
			{
				return "AutoCompleterConfirmEvent";
			}
		}

		// Token: 0x0601017A RID: 65914 RVA: 0x00374348 File Offset: 0x00372548
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetUserDataProperties()
		{
			if (this._userDataProperties == null)
			{
				this._userDataProperties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("AutoCompleterSuggestEventId", this.SuggestEventId),
					new KeyValuePair<string, string>("AutoCompleterConfirmedSuggestionIndex", this.SuggestionIndex.ToString(CultureInfo.CurrentCulture))
				};
				this._userDataProperties.AddRange(base.GetUserDataProperties());
			}
			return this._userDataProperties;
		}

		// Token: 0x04006101 RID: 24833
		private List<KeyValuePair<string, string>> _userDataProperties;
	}
}
