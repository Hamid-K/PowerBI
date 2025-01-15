using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015F RID: 351
	public sealed class GetClientAccessTokenResponse : ServiceResponse
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00030A35 File Offset: 0x0002FA35
		internal GetClientAccessTokenResponse(string id, ClientAccessTokenType tokenType)
		{
			this.Id = id;
			this.TokenType = tokenType;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00030A4C File Offset: 0x0002FA4C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "Token");
			this.Id = reader.ReadElementValue(XmlNamespace.Types, "Id");
			this.TokenType = (ClientAccessTokenType)Enum.Parse(typeof(ClientAccessTokenType), reader.ReadElementValue(XmlNamespace.Types, "TokenType"));
			this.TokenValue = reader.ReadElementValue(XmlNamespace.Types, "TokenValue");
			this.TTL = int.Parse(reader.ReadElementValue(XmlNamespace.Types, "TTL"));
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "Token");
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00030ADC File Offset: 0x0002FADC
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("Token"))
			{
				JsonObject jsonObject = responseObject.ReadAsJsonObject("Token");
				this.Id = jsonObject.ReadAsString("Id");
				this.TokenType = (ClientAccessTokenType)Enum.Parse(typeof(ClientAccessTokenType), jsonObject.ReadAsString("TokenType"));
				this.TokenValue = jsonObject.ReadAsString("TokenValue");
				this.TTL = jsonObject.ReadAsInt("TTL");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00030B62 File Offset: 0x0002FB62
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00030B6A File Offset: 0x0002FB6A
		public string Id { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00030B73 File Offset: 0x0002FB73
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00030B7B File Offset: 0x0002FB7B
		public ClientAccessTokenType TokenType { get; private set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00030B84 File Offset: 0x0002FB84
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00030B8C File Offset: 0x0002FB8C
		public string TokenValue { get; private set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00030B95 File Offset: 0x0002FB95
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00030B9D File Offset: 0x0002FB9D
		public int TTL { get; private set; }
	}
}
