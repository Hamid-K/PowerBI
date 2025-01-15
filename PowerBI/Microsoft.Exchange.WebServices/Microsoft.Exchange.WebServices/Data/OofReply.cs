using System;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AD RID: 685
	public sealed class OofReply
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x00042E6E File Offset: 0x00041E6E
		internal static void WriteEmptyReplyToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			writer.WriteEndElement();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00042E7E File Offset: 0x00041E7E
		public OofReply()
		{
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00042E96 File Offset: 0x00041E96
		public OofReply(string message)
		{
			this.message = message;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00042EB5 File Offset: 0x00041EB5
		public static implicit operator OofReply(string message)
		{
			return new OofReply(message);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00042EBD File Offset: 0x00041EBD
		public static implicit operator string(OofReply oofReply)
		{
			EwsUtilities.ValidateParam(oofReply, "oofReply");
			return oofReply.Message;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00042ED0 File Offset: 0x00041ED0
		internal void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, xmlElementName);
			if (reader.HasAttributes)
			{
				this.culture = reader.ReadAttributeValue("xml:lang");
			}
			this.message = reader.ReadElementValue(XmlNamespace.Types, "Message");
			reader.ReadEndElement(XmlNamespace.Types, xmlElementName);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00042F0D File Offset: 0x00041F0D
		internal void LoadFromJson(JsonObject jsonObject, ExchangeService service)
		{
			if (jsonObject.ContainsKey("xml:lang"))
			{
				this.culture = jsonObject.ReadAsString("xml:lang");
			}
			this.message = jsonObject.ReadAsString("Message");
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00042F3E File Offset: 0x00041F3E
		internal void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			if (this.Culture != null)
			{
				writer.WriteAttributeValue("xml", "lang", this.Culture);
			}
			writer.WriteElementValue(XmlNamespace.Types, "Message", this.Message);
			writer.WriteEndElement();
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00042F80 File Offset: 0x00041F80
		internal JsonObject InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.Culture != null)
			{
				jsonObject.Add("xml:lang", this.Culture);
			}
			jsonObject.Add("Message", this.Message);
			return jsonObject;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00042FBE File Offset: 0x00041FBE
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x00042FC6 File Offset: 0x00041FC6
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x00042FCE File Offset: 0x00041FCE
		public string Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00042FD7 File Offset: 0x00041FD7
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x00042FDF File Offset: 0x00041FDF
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x040013BC RID: 5052
		private string culture = CultureInfo.CurrentCulture.Name;

		// Token: 0x040013BD RID: 5053
		private string message;
	}
}
