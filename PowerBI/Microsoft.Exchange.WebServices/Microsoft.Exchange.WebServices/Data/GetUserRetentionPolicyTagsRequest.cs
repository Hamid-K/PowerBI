using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200012A RID: 298
	internal sealed class GetUserRetentionPolicyTagsRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0002C5F5 File Offset: 0x0002B5F5
		internal GetUserRetentionPolicyTagsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002C5FE File Offset: 0x0002B5FE
		internal override string GetResponseXmlElementName()
		{
			return "GetUserRetentionPolicyTagsResponse";
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002C605 File Offset: 0x0002B605
		internal override string GetXmlElementName()
		{
			return "GetUserRetentionPolicyTags";
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002C60C File Offset: 0x0002B60C
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetUserRetentionPolicyTagsResponse getUserRetentionPolicyTagsResponse = new GetUserRetentionPolicyTagsResponse();
			getUserRetentionPolicyTagsResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getUserRetentionPolicyTagsResponse;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002C62D File Offset: 0x0002B62D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002C62F File Offset: 0x0002B62F
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002C634 File Offset: 0x0002B634
		internal GetUserRetentionPolicyTagsResponse Execute()
		{
			return (GetUserRetentionPolicyTagsResponse)base.InternalExecute();
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002C650 File Offset: 0x0002B650
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}
	}
}
