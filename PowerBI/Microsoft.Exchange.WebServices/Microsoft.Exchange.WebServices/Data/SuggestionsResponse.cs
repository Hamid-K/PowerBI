using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000183 RID: 387
	internal sealed class SuggestionsResponse : ServiceResponse
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x00031F00 File Offset: 0x00030F00
		internal SuggestionsResponse()
		{
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00031F14 File Offset: 0x00030F14
		internal void LoadSuggestedDaysFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "SuggestionDayResultArray");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "SuggestionDayResult"))
				{
					Suggestion suggestion = new Suggestion();
					suggestion.LoadFromXml(reader, reader.LocalName);
					this.daySuggestions.Add(suggestion);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "SuggestionDayResultArray"));
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00031F6E File Offset: 0x00030F6E
		internal Collection<Suggestion> Suggestions
		{
			get
			{
				return this.daySuggestions;
			}
		}

		// Token: 0x040009DC RID: 2524
		private Collection<Suggestion> daySuggestions = new Collection<Suggestion>();
	}
}
