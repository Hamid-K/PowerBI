using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000109 RID: 265
	internal sealed class GetClientAccessTokenRequest : MultiResponseServiceRequest<GetClientAccessTokenResponse>, IJsonSerializable
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x0002A5D6 File Offset: 0x000295D6
		internal GetClientAccessTokenRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002A5E0 File Offset: 0x000295E0
		internal override GetClientAccessTokenResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetClientAccessTokenResponse(this.TokenRequests[responseIndex].Id, this.TokenRequests[responseIndex].TokenType);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002A601 File Offset: 0x00029601
		internal override string GetXmlElementName()
		{
			return "GetClientAccessToken";
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002A608 File Offset: 0x00029608
		internal override string GetResponseXmlElementName()
		{
			return "GetClientAccessTokenResponse";
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002A60F File Offset: 0x0002960F
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetClientAccessTokenResponseMessage";
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002A616 File Offset: 0x00029616
		internal override int GetExpectedResponseMessageCount()
		{
			return this.TokenRequests.Length;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002A620 File Offset: 0x00029620
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "TokenRequests");
			foreach (ClientAccessTokenRequest clientAccessTokenRequest in this.TokenRequests)
			{
				writer.WriteStartElement(XmlNamespace.Types, "TokenRequest");
				writer.WriteElementValue(XmlNamespace.Types, "Id", clientAccessTokenRequest.Id);
				writer.WriteElementValue(XmlNamespace.Types, "TokenType", clientAccessTokenRequest.TokenType);
				if (!string.IsNullOrEmpty(clientAccessTokenRequest.Scope))
				{
					writer.WriteElementValue(XmlNamespace.Types, "Scope", clientAccessTokenRequest.Scope);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002A6B4 File Offset: 0x000296B4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (ClientAccessTokenRequest clientAccessTokenRequest in this.TokenRequests)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter("TokenRequest");
				jsonObject.Add("Id", clientAccessTokenRequest.Id);
				jsonObject.Add("TokenType", clientAccessTokenRequest.TokenType);
				if (!string.IsNullOrEmpty(clientAccessTokenRequest.Scope))
				{
					jsonObject.Add("Scope", clientAccessTokenRequest.Scope);
				}
				list.Add(jsonObject);
			}
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.Add("TokenRequests", list.ToArray());
			return jsonObject2;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002A75D File Offset: 0x0002975D
		internal override void Validate()
		{
			base.Validate();
			if (this.TokenRequests == null || this.TokenRequests.Length == 0)
			{
				throw new ServiceValidationException(Strings.HoldIdParameterIsNotSpecified);
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002A787 File Offset: 0x00029787
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002A78A File Offset: 0x0002978A
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0002A792 File Offset: 0x00029792
		internal ClientAccessTokenRequest[] TokenRequests { get; set; }
	}
}
