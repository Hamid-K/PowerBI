using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014E RID: 334
	public sealed class ConvertIdResponse : ServiceResponse
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x0002F819 File Offset: 0x0002E819
		internal ConvertIdResponse()
		{
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002F824 File Offset: 0x0002E824
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "AlternateId");
			string text = reader.ReadAttributeValue(XmlNamespace.XmlSchemaInstance, "type");
			int num = text.IndexOf(':');
			if (num > -1)
			{
				text = text.Substring(num + 1);
			}
			string text2;
			if ((text2 = text) != null)
			{
				if (text2 == "AlternateIdType")
				{
					this.convertedId = new AlternateId();
					goto IL_00A2;
				}
				if (text2 == "AlternatePublicFolderIdType")
				{
					this.convertedId = new AlternatePublicFolderId();
					goto IL_00A2;
				}
				if (text2 == "AlternatePublicFolderItemIdType")
				{
					this.convertedId = new AlternatePublicFolderItemId();
					goto IL_00A2;
				}
			}
			EwsUtilities.Assert(false, "ConvertIdResponse.ReadElementsFromXml", string.Format("Unknown alternate Id class: {0}", text));
			IL_00A2:
			this.convertedId.LoadAttributesFromXml(reader);
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "AlternateId");
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002F8EC File Offset: 0x0002E8EC
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			string text = responseObject.ReadTypeString();
			string text2;
			if ((text2 = text) != null)
			{
				if (text2 == "AlternateIdType")
				{
					this.convertedId = new AlternateId();
					goto IL_0072;
				}
				if (text2 == "AlternatePublicFolderIdType")
				{
					this.convertedId = new AlternatePublicFolderId();
					goto IL_0072;
				}
				if (text2 == "AlternatePublicFolderItemIdType")
				{
					this.convertedId = new AlternatePublicFolderItemId();
					goto IL_0072;
				}
			}
			EwsUtilities.Assert(false, "ConvertIdResponse.ReadElementsFromXml", string.Format("Unknown alternate Id class: {0}", text));
			IL_0072:
			this.convertedId.LoadAttributesFromJson(responseObject);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0002F977 File Offset: 0x0002E977
		public AlternateIdBase ConvertedId
		{
			get
			{
				return this.convertedId;
			}
		}

		// Token: 0x04000993 RID: 2451
		private AlternateIdBase convertedId;
	}
}
