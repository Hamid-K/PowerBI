using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002B1 RID: 689
	public class AlternatePublicFolderId : AlternateIdBase
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x0004347D File Offset: 0x0004247D
		public AlternatePublicFolderId()
		{
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00043485 File Offset: 0x00042485
		public AlternatePublicFolderId(IdFormat format, string folderId)
			: base(format)
		{
			this.FolderId = folderId;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00043495 File Offset: 0x00042495
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x0004349D File Offset: 0x0004249D
		public string FolderId { get; set; }

		// Token: 0x060018A4 RID: 6308 RVA: 0x000434A6 File Offset: 0x000424A6
		internal override string GetXmlElementName()
		{
			return "AlternatePublicFolderId";
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000434AD File Offset: 0x000424AD
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("FolderId", this.FolderId);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000434C7 File Offset: 0x000424C7
		internal override void InternalToJson(JsonObject jsonObject)
		{
			base.InternalToJson(jsonObject);
			jsonObject.Add("FolderId", this.FolderId);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000434E1 File Offset: 0x000424E1
		internal override void LoadAttributesFromXml(EwsServiceXmlReader reader)
		{
			base.LoadAttributesFromXml(reader);
			this.FolderId = reader.ReadAttributeValue("FolderId");
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000434FB File Offset: 0x000424FB
		internal override void LoadAttributesFromJson(JsonObject responseObject)
		{
			base.LoadAttributesFromJson(responseObject);
			this.FolderId = responseObject.ReadAsString("FolderId");
		}

		// Token: 0x040013C5 RID: 5061
		internal const string SchemaTypeName = "AlternatePublicFolderIdType";
	}
}
