using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000173 RID: 371
	public sealed class GetSearchableMailboxesResponse : ServiceResponse
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x000316EB File Offset: 0x000306EB
		internal GetSearchableMailboxesResponse()
		{
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00031700 File Offset: 0x00030700
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.searchableMailboxes.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "SearchableMailboxes");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "SearchableMailbox"))
					{
						this.searchableMailboxes.Add(SearchableMailbox.LoadFromXml(reader));
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "SearchableMailboxes"));
			}
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "FailedMailboxes"))
			{
				this.FailedMailboxes = FailedSearchMailbox.LoadFailedMailboxesXml(XmlNamespace.Messages, reader);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00031788 File Offset: 0x00030788
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.searchableMailboxes.Clear();
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("SearchMailboxes"))
			{
				foreach (object obj in responseObject.ReadAsArray("SearchableMailboxes"))
				{
					JsonObject jsonObject = obj as JsonObject;
					this.searchableMailboxes.Add(SearchableMailbox.LoadFromJson(jsonObject));
				}
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x000317EB File Offset: 0x000307EB
		public SearchableMailbox[] SearchableMailboxes
		{
			get
			{
				return this.searchableMailboxes.ToArray();
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x000317F8 File Offset: 0x000307F8
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00031800 File Offset: 0x00030800
		public FailedSearchMailbox[] FailedMailboxes { get; set; }

		// Token: 0x040009CC RID: 2508
		private List<SearchableMailbox> searchableMailboxes = new List<SearchableMailbox>();
	}
}
