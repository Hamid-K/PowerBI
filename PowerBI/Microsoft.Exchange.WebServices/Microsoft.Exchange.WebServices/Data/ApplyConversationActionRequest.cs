using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E8 RID: 232
	internal sealed class ApplyConversationActionRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00028125 File Offset: 0x00027125
		public List<ConversationAction> ConversationActions
		{
			get
			{
				return this.conversationActions;
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002812D File Offset: 0x0002712D
		internal ApplyConversationActionRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00028142 File Offset: 0x00027142
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00028149 File Offset: 0x00027149
		internal override int GetExpectedResponseMessageCount()
		{
			return this.conversationActions.Count;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00028158 File Offset: 0x00027158
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.conversationActions, "conversationActions");
			for (int i = 0; i < this.ConversationActions.Count; i++)
			{
				this.ConversationActions[i].Validate();
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000281A4 File Offset: 0x000271A4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "ConversationActions");
			for (int i = 0; i < this.ConversationActions.Count; i++)
			{
				this.ConversationActions[i].WriteElementsToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000281EC File Offset: 0x000271EC
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			List<object> list = new List<object>();
			foreach (ConversationAction conversationAction in this.conversationActions)
			{
				list.Add(((IJsonSerializable)conversationAction).ToJson(service));
			}
			jsonObject.Add("ConversationActions", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00028264 File Offset: 0x00027264
		internal override string GetXmlElementName()
		{
			return "ApplyConversationAction";
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002826B File Offset: 0x0002726B
		internal override string GetResponseXmlElementName()
		{
			return "ApplyConversationActionResponse";
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00028272 File Offset: 0x00027272
		internal override string GetResponseMessageXmlElementName()
		{
			return "ApplyConversationActionResponseMessage";
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00028279 File Offset: 0x00027279
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x040008B3 RID: 2227
		private List<ConversationAction> conversationActions = new List<ConversationAction>();
	}
}
