using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B0 RID: 688
	public class AlternateId : AlternateIdBase
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x000432A5 File Offset: 0x000422A5
		public AlternateId()
		{
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000432AD File Offset: 0x000422AD
		public AlternateId(IdFormat format, string id, string mailbox)
			: base(format)
		{
			this.UniqueId = id;
			this.Mailbox = mailbox;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000432C4 File Offset: 0x000422C4
		public AlternateId(IdFormat format, string id, string mailbox, bool isArchive)
			: base(format)
		{
			this.UniqueId = id;
			this.Mailbox = mailbox;
			this.IsArchive = isArchive;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000432E3 File Offset: 0x000422E3
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x000432EB File Offset: 0x000422EB
		public string UniqueId { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000432F4 File Offset: 0x000422F4
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x000432FC File Offset: 0x000422FC
		public string Mailbox { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00043305 File Offset: 0x00042305
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x0004330D File Offset: 0x0004230D
		public bool IsArchive { get; set; }

		// Token: 0x0600189A RID: 6298 RVA: 0x00043316 File Offset: 0x00042316
		internal override string GetXmlElementName()
		{
			return "AlternateId";
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00043320 File Offset: 0x00042320
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("Id", this.UniqueId);
			writer.WriteAttributeValue("Mailbox", this.Mailbox);
			if (this.IsArchive)
			{
				writer.WriteAttributeValue("IsArchive", true);
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0004336F File Offset: 0x0004236F
		internal override void InternalToJson(JsonObject jsonObject)
		{
			base.InternalToJson(jsonObject);
			jsonObject.Add("Id", this.UniqueId);
			jsonObject.Add("Mailbox", this.Mailbox);
			if (this.IsArchive)
			{
				jsonObject.Add("IsArchive", true);
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000433B0 File Offset: 0x000423B0
		internal override void LoadAttributesFromXml(EwsServiceXmlReader reader)
		{
			base.LoadAttributesFromXml(reader);
			this.UniqueId = reader.ReadAttributeValue("Id");
			this.Mailbox = reader.ReadAttributeValue("Mailbox");
			string text = reader.ReadAttributeValue("IsArchive");
			if (!string.IsNullOrEmpty(text))
			{
				this.IsArchive = reader.ReadAttributeValue<bool>("IsArchive");
				return;
			}
			this.IsArchive = false;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00043414 File Offset: 0x00042414
		internal override void LoadAttributesFromJson(JsonObject responseObject)
		{
			base.LoadAttributesFromJson(responseObject);
			this.UniqueId = responseObject.ReadAsString("Id");
			this.Mailbox = responseObject.ReadAsString("Mailbox");
			this.IsArchive = responseObject.ContainsKey("IsArchive") && responseObject.ReadAsBool("IsArchive");
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004346B File Offset: 0x0004246B
		internal override void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.Mailbox, "mailbox");
		}

		// Token: 0x040013C1 RID: 5057
		internal const string SchemaTypeName = "AlternateIdType";
	}
}
