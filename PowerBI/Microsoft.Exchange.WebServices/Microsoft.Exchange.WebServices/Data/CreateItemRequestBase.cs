using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F5 RID: 245
	internal abstract class CreateItemRequestBase<TServiceObject, TResponse> : CreateRequest<TServiceObject, TResponse> where TServiceObject : ServiceObject where TResponse : ServiceResponse
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00028DEF File Offset: 0x00027DEF
		protected CreateItemRequestBase(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00028DFC File Offset: 0x00027DFC
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				foreach (TServiceObject tserviceObject in this.Items)
				{
					if (tserviceObject.GetIsTimeZoneHeaderRequired(false))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00028E5C File Offset: 0x00027E5C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Items, "Items");
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00028E74 File Offset: 0x00027E74
		internal override string GetXmlElementName()
		{
			return "CreateItem";
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00028E7B File Offset: 0x00027E7B
		internal override string GetResponseXmlElementName()
		{
			return "CreateItemResponse";
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00028E82 File Offset: 0x00027E82
		internal override string GetResponseMessageXmlElementName()
		{
			return "CreateItemResponseMessage";
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00028E89 File Offset: 0x00027E89
		internal override string GetParentFolderXmlElementName()
		{
			return "SavedItemFolderId";
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00028E90 File Offset: 0x00027E90
		internal override string GetObjectCollectionXmlElementName()
		{
			return "Items";
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00028E98 File Offset: 0x00027E98
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			if (this.MessageDisposition != null)
			{
				writer.WriteAttributeValue("MessageDisposition", this.MessageDisposition.Value);
			}
			if (this.SendInvitationsMode != null)
			{
				writer.WriteAttributeValue("SendMeetingInvitations", this.SendInvitationsMode.Value);
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00028F08 File Offset: 0x00027F08
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			base.AddJsonProperties(jsonRequest, service);
			if (this.MessageDisposition != null)
			{
				jsonRequest.Add("MessageDisposition", this.MessageDisposition.Value);
			}
			if (this.SendInvitationsMode != null)
			{
				jsonRequest.Add("SendMeetingInvitations", this.SendInvitationsMode.Value);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00028F79 File Offset: 0x00027F79
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00028F81 File Offset: 0x00027F81
		public MessageDisposition? MessageDisposition
		{
			get
			{
				return this.messageDisposition;
			}
			set
			{
				this.messageDisposition = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00028F8A File Offset: 0x00027F8A
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00028F92 File Offset: 0x00027F92
		public SendInvitationsMode? SendInvitationsMode
		{
			get
			{
				return this.sendInvitationsMode;
			}
			set
			{
				this.sendInvitationsMode = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00028F9B File Offset: 0x00027F9B
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00028FA3 File Offset: 0x00027FA3
		public IEnumerable<TServiceObject> Items
		{
			get
			{
				return base.Objects;
			}
			set
			{
				base.Objects = value;
			}
		}

		// Token: 0x040008C8 RID: 2248
		private MessageDisposition? messageDisposition;

		// Token: 0x040008C9 RID: 2249
		private SendInvitationsMode? sendInvitationsMode;
	}
}
