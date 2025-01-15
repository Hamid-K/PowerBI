using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200027F RID: 639
	public sealed class SearchableMailbox
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x0003DD8B File Offset: 0x0003CD8B
		public SearchableMailbox()
		{
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0003DD93 File Offset: 0x0003CD93
		public SearchableMailbox(Guid guid, string smtpAddress, bool isExternalMailbox, string externalEmailAddress, string displayName, bool isMembershipGroup, string referenceId)
		{
			this.Guid = guid;
			this.SmtpAddress = smtpAddress;
			this.IsExternalMailbox = isExternalMailbox;
			this.ExternalEmailAddress = externalEmailAddress;
			this.DisplayName = displayName;
			this.IsMembershipGroup = isMembershipGroup;
			this.ReferenceId = referenceId;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0003DDD0 File Offset: 0x0003CDD0
		internal static SearchableMailbox LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "SearchableMailbox");
			SearchableMailbox searchableMailbox = new SearchableMailbox();
			searchableMailbox.Guid = new Guid(reader.ReadElementValue(XmlNamespace.Types, "Guid"));
			searchableMailbox.SmtpAddress = reader.ReadElementValue(XmlNamespace.Types, "PrimarySmtpAddress");
			bool flag = false;
			bool.TryParse(reader.ReadElementValue(XmlNamespace.Types, "IsExternalMailbox"), ref flag);
			searchableMailbox.IsExternalMailbox = flag;
			searchableMailbox.ExternalEmailAddress = reader.ReadElementValue(XmlNamespace.Types, "ExternalEmailAddress");
			searchableMailbox.DisplayName = reader.ReadElementValue(XmlNamespace.Types, "DisplayName");
			bool flag2 = false;
			bool.TryParse(reader.ReadElementValue(XmlNamespace.Types, "IsMembershipGroup"), ref flag2);
			searchableMailbox.IsMembershipGroup = flag2;
			searchableMailbox.ReferenceId = reader.ReadElementValue(XmlNamespace.Types, "ReferenceId");
			return searchableMailbox;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0003DE8C File Offset: 0x0003CE8C
		internal static SearchableMailbox LoadFromJson(JsonObject jsonObject)
		{
			SearchableMailbox searchableMailbox = new SearchableMailbox();
			if (jsonObject.ContainsKey("Guid"))
			{
				searchableMailbox.Guid = new Guid(jsonObject.ReadAsString("Guid"));
			}
			if (jsonObject.ContainsKey("DisplayName"))
			{
				searchableMailbox.DisplayName = jsonObject.ReadAsString("DisplayName");
			}
			if (jsonObject.ContainsKey("PrimarySmtpAddress"))
			{
				searchableMailbox.SmtpAddress = jsonObject.ReadAsString("PrimarySmtpAddress");
			}
			if (jsonObject.ContainsKey("IsExternalMailbox"))
			{
				searchableMailbox.IsExternalMailbox = jsonObject.ReadAsBool("IsExternalMailbox");
			}
			if (jsonObject.ContainsKey("ExternalEmailAddress"))
			{
				searchableMailbox.ExternalEmailAddress = jsonObject.ReadAsString("ExternalEmailAddress");
			}
			if (jsonObject.ContainsKey("IsMembershipGroup"))
			{
				searchableMailbox.IsMembershipGroup = jsonObject.ReadAsBool("IsMembershipGroup");
			}
			if (jsonObject.ContainsKey("ReferenceId"))
			{
				searchableMailbox.ReferenceId = jsonObject.ReadAsString("ReferenceId");
			}
			return searchableMailbox;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0003DF77 File Offset: 0x0003CF77
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x0003DF7F File Offset: 0x0003CF7F
		public Guid Guid { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0003DF88 File Offset: 0x0003CF88
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0003DF90 File Offset: 0x0003CF90
		public string SmtpAddress { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0003DF99 File Offset: 0x0003CF99
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0003DFA1 File Offset: 0x0003CFA1
		public bool IsExternalMailbox { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0003DFAA File Offset: 0x0003CFAA
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0003DFB2 File Offset: 0x0003CFB2
		public string ExternalEmailAddress { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0003DFBB File Offset: 0x0003CFBB
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0003DFC3 File Offset: 0x0003CFC3
		public string DisplayName { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0003DFCC File Offset: 0x0003CFCC
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0003DFD4 File Offset: 0x0003CFD4
		public bool IsMembershipGroup { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0003DFDD File Offset: 0x0003CFDD
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0003DFE5 File Offset: 0x0003CFE5
		public string ReferenceId { get; set; }
	}
}
