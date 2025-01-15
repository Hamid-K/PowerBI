using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B2 RID: 690
	public class AlternatePublicFolderItemId : AlternatePublicFolderId
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x00043515 File Offset: 0x00042515
		public AlternatePublicFolderItemId()
		{
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0004351D File Offset: 0x0004251D
		public AlternatePublicFolderItemId(IdFormat format, string folderId, string itemId)
			: base(format, folderId)
		{
			this.itemId = itemId;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0004352E File Offset: 0x0004252E
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x00043536 File Offset: 0x00042536
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004353F File Offset: 0x0004253F
		internal override string GetXmlElementName()
		{
			return "AlternatePublicFolderItemId";
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00043546 File Offset: 0x00042546
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("ItemId", this.ItemId);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00043560 File Offset: 0x00042560
		internal override void InternalToJson(JsonObject jsonObject)
		{
			base.InternalToJson(jsonObject);
			jsonObject.Add("ItemId", this.ItemId);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0004357A File Offset: 0x0004257A
		internal override void LoadAttributesFromXml(EwsServiceXmlReader reader)
		{
			base.LoadAttributesFromXml(reader);
			this.itemId = reader.ReadAttributeValue("ItemId");
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00043594 File Offset: 0x00042594
		internal override void LoadAttributesFromJson(JsonObject responseObject)
		{
			base.LoadAttributesFromJson(responseObject);
			this.itemId = responseObject.ReadAsString("ItemId");
		}

		// Token: 0x040013C7 RID: 5063
		internal new const string SchemaTypeName = "AlternatePublicFolderItemIdType";

		// Token: 0x040013C8 RID: 5064
		private string itemId;
	}
}
