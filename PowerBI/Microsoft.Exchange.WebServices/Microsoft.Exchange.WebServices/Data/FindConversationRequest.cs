using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000104 RID: 260
	internal sealed class FindConversationRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x000299CD File Offset: 0x000289CD
		internal FindConversationRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000299D6 File Offset: 0x000289D6
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000299DE File Offset: 0x000289DE
		public ViewBase View
		{
			get
			{
				return this.view;
			}
			set
			{
				this.view = value;
				if (this.view is SeekToConditionItemView)
				{
					((SeekToConditionItemView)this.view).SetServiceObjectType(ServiceObjectType.Conversation);
				}
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00029A05 File Offset: 0x00028A05
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00029A0D File Offset: 0x00028A0D
		internal FolderIdWrapper FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00029A16 File Offset: 0x00028A16
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00029A1E File Offset: 0x00028A1E
		internal string QueryString
		{
			get
			{
				return this.queryString;
			}
			set
			{
				this.queryString = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00029A27 File Offset: 0x00028A27
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00029A2F File Offset: 0x00028A2F
		internal bool ReturnHighlightTerms
		{
			get
			{
				return this.returnHighlightTerms;
			}
			set
			{
				this.returnHighlightTerms = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00029A38 File Offset: 0x00028A38
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00029A40 File Offset: 0x00028A40
		internal MailboxSearchLocation? MailboxScope
		{
			get
			{
				return this.mailboxScope;
			}
			set
			{
				this.mailboxScope = value;
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00029A4C File Offset: 0x00028A4C
		internal override void Validate()
		{
			base.Validate();
			this.view.InternalValidate(this);
			if (!string.IsNullOrEmpty(this.queryString) && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "queryString", ExchangeVersion.Exchange2013));
			}
			if (this.ReturnHighlightTerms && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "returnHighlightTerms", ExchangeVersion.Exchange2013));
			}
			if (this.View is SeekToConditionItemView && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "SeekToConditionItemView", ExchangeVersion.Exchange2013));
			}
			if (this.MailboxScope != null && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "MailboxScope", ExchangeVersion.Exchange2013));
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00029B55 File Offset: 0x00028B55
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			this.View.WriteAttributesToXml(writer);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00029B64 File Offset: 0x00028B64
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.View.WriteToXml(writer, null);
			this.View.WriteOrderByToXml(writer);
			writer.WriteStartElement(XmlNamespace.Messages, "ParentFolderId");
			this.FolderId.WriteToXml(writer);
			writer.WriteEndElement();
			if (this.MailboxScope != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "MailboxScope", this.MailboxScope.Value);
			}
			if (!string.IsNullOrEmpty(this.queryString))
			{
				writer.WriteStartElement(XmlNamespace.Messages, "QueryString");
				if (this.ReturnHighlightTerms)
				{
					writer.WriteAttributeString("ReturnHighlightTerms", this.ReturnHighlightTerms.ToString().ToLowerInvariant());
				}
				writer.WriteValue(this.queryString, "QueryString");
				writer.WriteEndElement();
			}
			if (base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013 && this.View.PropertySet != null)
			{
				this.View.PropertySet.WriteToXml(writer, ServiceObjectType.Conversation);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00029C5C File Offset: 0x00028C5C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Paging", this.View.WritePagingToJson(service));
			this.View.AddJsonProperties(jsonObject, service);
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.Add("BaseFolderId", this.FolderId.InternalToJson(service));
			jsonObject.Add("ParentFolderId", jsonObject2);
			if (this.MailboxScope != null)
			{
				jsonObject.Add("MailboxScope", this.MailboxScope.Value);
			}
			if (!string.IsNullOrEmpty(this.queryString))
			{
				JsonObject jsonObject3 = new JsonObject();
				jsonObject3.Add("Value", this.QueryString);
				if (this.ReturnHighlightTerms)
				{
					jsonObject3.Add("ReturnHighlightTerms", this.ReturnHighlightTerms.ToString().ToLowerInvariant());
				}
				jsonObject.Add("QueryString", jsonObject3);
			}
			if (base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013 && this.View.PropertySet != null)
			{
				this.View.PropertySet.WriteGetShapeToJson(jsonObject, service, ServiceObjectType.Conversation);
			}
			return jsonObject;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00029D74 File Offset: 0x00028D74
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			FindConversationResponse findConversationResponse = new FindConversationResponse();
			findConversationResponse.LoadFromXml(reader, "FindConversationResponse");
			return findConversationResponse;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00029D94 File Offset: 0x00028D94
		internal override object ParseResponse(JsonObject jsonBody)
		{
			FindConversationResponse findConversationResponse = new FindConversationResponse();
			findConversationResponse.LoadFromJson(jsonBody, base.Service);
			return findConversationResponse;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00029DB5 File Offset: 0x00028DB5
		internal override string GetXmlElementName()
		{
			return "FindConversation";
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00029DBC File Offset: 0x00028DBC
		internal override string GetResponseXmlElementName()
		{
			return "FindConversationResponse";
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00029DC3 File Offset: 0x00028DC3
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00029DC8 File Offset: 0x00028DC8
		internal FindConversationResponse Execute()
		{
			FindConversationResponse findConversationResponse = (FindConversationResponse)base.InternalExecute();
			findConversationResponse.ThrowIfNecessary();
			return findConversationResponse;
		}

		// Token: 0x040008E1 RID: 2273
		private ViewBase view;

		// Token: 0x040008E2 RID: 2274
		private FolderIdWrapper folderId;

		// Token: 0x040008E3 RID: 2275
		private string queryString;

		// Token: 0x040008E4 RID: 2276
		private bool returnHighlightTerms;

		// Token: 0x040008E5 RID: 2277
		private MailboxSearchLocation? mailboxScope;
	}
}
