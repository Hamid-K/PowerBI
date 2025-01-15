using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001E0 RID: 480
	public sealed class RetentionPolicyTag
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0003C913 File Offset: 0x0003B913
		public RetentionPolicyTag()
		{
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0003C91C File Offset: 0x0003B91C
		internal RetentionPolicyTag(string displayName, Guid retentionId, int retentionPeriod, ElcFolderType type, RetentionActionType retentionAction, bool isVisible, bool optedInto, bool isArchive)
		{
			this.DisplayName = displayName;
			this.RetentionId = retentionId;
			this.RetentionPeriod = retentionPeriod;
			this.Type = type;
			this.RetentionAction = retentionAction;
			this.IsVisible = isVisible;
			this.OptedInto = optedInto;
			this.IsArchive = isArchive;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0003C96C File Offset: 0x0003B96C
		internal static RetentionPolicyTag LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "RetentionPolicyTag");
			RetentionPolicyTag retentionPolicyTag = new RetentionPolicyTag();
			retentionPolicyTag.DisplayName = reader.ReadElementValue(XmlNamespace.Types, "DisplayName");
			retentionPolicyTag.RetentionId = new Guid(reader.ReadElementValue(XmlNamespace.Types, "RetentionId"));
			retentionPolicyTag.RetentionPeriod = reader.ReadElementValue<int>(XmlNamespace.Types, "RetentionPeriod");
			retentionPolicyTag.Type = reader.ReadElementValue<ElcFolderType>(XmlNamespace.Types, "Type");
			retentionPolicyTag.RetentionAction = reader.ReadElementValue<RetentionActionType>(XmlNamespace.Types, "RetentionAction");
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Types, "Description"))
			{
				retentionPolicyTag.Description = reader.ReadElementValue(XmlNamespace.Types, "Description");
			}
			retentionPolicyTag.IsVisible = reader.ReadElementValue<bool>(XmlNamespace.Types, "IsVisible");
			retentionPolicyTag.OptedInto = reader.ReadElementValue<bool>(XmlNamespace.Types, "OptedInto");
			retentionPolicyTag.IsArchive = reader.ReadElementValue<bool>(XmlNamespace.Types, "IsArchive");
			return retentionPolicyTag;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0003CA48 File Offset: 0x0003BA48
		internal static RetentionPolicyTag LoadFromJson(JsonObject jsonObject)
		{
			RetentionPolicyTag retentionPolicyTag = new RetentionPolicyTag();
			if (jsonObject.ContainsKey("DisplayName"))
			{
				retentionPolicyTag.DisplayName = jsonObject.ReadAsString("DisplayName");
			}
			if (jsonObject.ContainsKey("RetentionId"))
			{
				retentionPolicyTag.RetentionId = new Guid(jsonObject.ReadAsString("RetentionId"));
			}
			if (jsonObject.ContainsKey("RetentionPeriod"))
			{
				retentionPolicyTag.RetentionPeriod = jsonObject.ReadAsInt("RetentionPeriod");
			}
			if (jsonObject.ContainsKey("Type"))
			{
				retentionPolicyTag.Type = jsonObject.ReadEnumValue<ElcFolderType>("Type");
			}
			if (jsonObject.ContainsKey("RetentionAction"))
			{
				retentionPolicyTag.RetentionAction = jsonObject.ReadEnumValue<RetentionActionType>("RetentionAction");
			}
			if (jsonObject.ContainsKey("Description"))
			{
				retentionPolicyTag.Description = jsonObject.ReadAsString("Description");
			}
			if (jsonObject.ContainsKey("IsVisible"))
			{
				retentionPolicyTag.IsVisible = jsonObject.ReadAsBool("IsVisible");
			}
			if (jsonObject.ContainsKey("OptedInto"))
			{
				retentionPolicyTag.OptedInto = jsonObject.ReadAsBool("OptedInto");
			}
			if (jsonObject.ContainsKey("IsArchive"))
			{
				retentionPolicyTag.IsArchive = jsonObject.ReadAsBool("IsArchive");
			}
			return retentionPolicyTag;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0003CB6F File Offset: 0x0003BB6F
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x0003CB77 File Offset: 0x0003BB77
		public string DisplayName { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0003CB80 File Offset: 0x0003BB80
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0003CB88 File Offset: 0x0003BB88
		public Guid RetentionId { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0003CB91 File Offset: 0x0003BB91
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x0003CB99 File Offset: 0x0003BB99
		public int RetentionPeriod { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0003CBA2 File Offset: 0x0003BBA2
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x0003CBAA File Offset: 0x0003BBAA
		public ElcFolderType Type { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0003CBB3 File Offset: 0x0003BBB3
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x0003CBBB File Offset: 0x0003BBBB
		public RetentionActionType RetentionAction { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0003CBC4 File Offset: 0x0003BBC4
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x0003CBCC File Offset: 0x0003BBCC
		public string Description { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0003CBD5 File Offset: 0x0003BBD5
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x0003CBDD File Offset: 0x0003BBDD
		public bool IsVisible { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0003CBE6 File Offset: 0x0003BBE6
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0003CBEE File Offset: 0x0003BBEE
		public bool OptedInto { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0003CBF7 File Offset: 0x0003BBF7
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x0003CBFF File Offset: 0x0003BBFF
		public bool IsArchive { get; set; }
	}
}
