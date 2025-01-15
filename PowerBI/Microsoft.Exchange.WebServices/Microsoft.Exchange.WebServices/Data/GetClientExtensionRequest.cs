using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010A RID: 266
	internal sealed class GetClientExtensionRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x0002A79B File Offset: 0x0002979B
		internal GetClientExtensionRequest(ExchangeService service, StringList requestedExtensionIds, bool shouldReturnEnabledOnly, bool isUserScope, string userId, StringList userEnabledExtensionIds, StringList userDisabledExtensionIds, bool isDebug)
			: base(service)
		{
			this.requestedExtensionIds = requestedExtensionIds;
			this.shouldReturnEnabledOnly = shouldReturnEnabledOnly;
			this.isUserScope = isUserScope;
			this.userId = userId;
			this.userEnabledExtensionIds = userEnabledExtensionIds;
			this.userDisabledExtensionIds = userDisabledExtensionIds;
			this.isDebug = isDebug;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002A7DA File Offset: 0x000297DA
		internal override string GetXmlElementName()
		{
			return "GetClientExtension";
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002A7E4 File Offset: 0x000297E4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.requestedExtensionIds != null && this.requestedExtensionIds.Count > 0)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "RequestedExtensionIds");
				this.requestedExtensionIds.WriteElementsToXml(writer);
				writer.WriteEndElement();
			}
			if (this.isUserScope)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "UserParameters");
				writer.WriteAttributeValue("UserId", this.userId);
				if (this.shouldReturnEnabledOnly)
				{
					writer.WriteAttributeValue("EnabledOnly", this.shouldReturnEnabledOnly);
				}
				if (this.userEnabledExtensionIds != null && this.userEnabledExtensionIds.Count > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "UserEnabledExtensions");
					this.userEnabledExtensionIds.WriteElementsToXml(writer);
					writer.WriteEndElement();
				}
				if (this.userDisabledExtensionIds != null && this.userDisabledExtensionIds.Count > 0)
				{
					writer.WriteStartElement(XmlNamespace.Types, "UserDisabledExtensions");
					this.userDisabledExtensionIds.WriteElementsToXml(writer);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			if (this.isDebug)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "IsDebug", this.isDebug);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002A8F8 File Offset: 0x000298F8
		internal override string GetResponseXmlElementName()
		{
			return "GetClientExtensionResponse";
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002A900 File Offset: 0x00029900
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetClientExtensionResponse getClientExtensionResponse = new GetClientExtensionResponse();
			getClientExtensionResponse.LoadFromXml(reader, "GetClientExtensionResponse");
			return getClientExtensionResponse;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002A920 File Offset: 0x00029920
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002A924 File Offset: 0x00029924
		internal GetClientExtensionResponse Execute()
		{
			GetClientExtensionResponse getClientExtensionResponse = (GetClientExtensionResponse)base.InternalExecute();
			getClientExtensionResponse.ThrowIfNecessary();
			return getClientExtensionResponse;
		}

		// Token: 0x040008F1 RID: 2289
		private readonly StringList requestedExtensionIds;

		// Token: 0x040008F2 RID: 2290
		private readonly bool shouldReturnEnabledOnly;

		// Token: 0x040008F3 RID: 2291
		private readonly bool isUserScope;

		// Token: 0x040008F4 RID: 2292
		private readonly string userId;

		// Token: 0x040008F5 RID: 2293
		private readonly StringList userEnabledExtensionIds;

		// Token: 0x040008F6 RID: 2294
		private readonly StringList userDisabledExtensionIds;

		// Token: 0x040008F7 RID: 2295
		private readonly bool isDebug;
	}
}
