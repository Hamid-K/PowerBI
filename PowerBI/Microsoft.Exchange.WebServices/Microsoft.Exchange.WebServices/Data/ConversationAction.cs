using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200028B RID: 651
	internal class ConversationAction : IJsonSerializable
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0003EDCA File Offset: 0x0003DDCA
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0003EDD2 File Offset: 0x0003DDD2
		internal ConversationActionType Action { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0003EDDB File Offset: 0x0003DDDB
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0003EDE3 File Offset: 0x0003DDE3
		internal ConversationId ConversationId { get; set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0003EDEC File Offset: 0x0003DDEC
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0003EDF4 File Offset: 0x0003DDF4
		internal bool ProcessRightAway { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0003EDFD File Offset: 0x0003DDFD
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0003EE05 File Offset: 0x0003DE05
		internal StringList Categories { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0003EE0E File Offset: 0x0003DE0E
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0003EE16 File Offset: 0x0003DE16
		internal bool EnableAlwaysDelete { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0003EE1F File Offset: 0x0003DE1F
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0003EE27 File Offset: 0x0003DE27
		internal bool? IsRead { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0003EE30 File Offset: 0x0003DE30
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0003EE38 File Offset: 0x0003DE38
		internal bool? SuppressReadReceipts { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0003EE41 File Offset: 0x0003DE41
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0003EE49 File Offset: 0x0003DE49
		internal DeleteMode? DeleteType { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0003EE52 File Offset: 0x0003DE52
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0003EE5A File Offset: 0x0003DE5A
		internal Flag Flag { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0003EE63 File Offset: 0x0003DE63
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0003EE6B File Offset: 0x0003DE6B
		internal DateTime? ConversationLastSyncTime { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0003EE74 File Offset: 0x0003DE74
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x0003EE7C File Offset: 0x0003DE7C
		internal FolderIdWrapper ContextFolderId { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0003EE85 File Offset: 0x0003DE85
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0003EE8D File Offset: 0x0003DE8D
		internal FolderIdWrapper DestinationFolderId { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0003EE96 File Offset: 0x0003DE96
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0003EE9E File Offset: 0x0003DE9E
		internal RetentionType? RetentionPolicyType { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0003EEA7 File Offset: 0x0003DEA7
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0003EEAF File Offset: 0x0003DEAF
		internal Guid? RetentionPolicyTagId { get; set; }

		// Token: 0x06001723 RID: 5923 RVA: 0x0003EEB8 File Offset: 0x0003DEB8
		internal string GetXmlElementName()
		{
			return "ApplyConversationAction";
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0003EEBF File Offset: 0x0003DEBF
		internal void Validate()
		{
			EwsUtilities.ValidateParam(this.ConversationId, "conversationId");
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0003EED4 File Offset: 0x0003DED4
		internal void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "ConversationAction");
			try
			{
				string text = string.Empty;
				switch (this.Action)
				{
				case ConversationActionType.AlwaysCategorize:
					text = "AlwaysCategorize";
					break;
				case ConversationActionType.AlwaysDelete:
					text = "AlwaysDelete";
					break;
				case ConversationActionType.AlwaysMove:
					text = "AlwaysMove";
					break;
				case ConversationActionType.Delete:
					text = "Delete";
					break;
				case ConversationActionType.Move:
					text = "Move";
					break;
				case ConversationActionType.Copy:
					text = "Copy";
					break;
				case ConversationActionType.SetReadState:
					text = "SetReadState";
					break;
				case ConversationActionType.SetRetentionPolicy:
					text = "SetRetentionPolicy";
					break;
				case ConversationActionType.Flag:
					text = "Flag";
					break;
				default:
					throw new ArgumentException("ConversationAction");
				}
				writer.WriteElementValue(XmlNamespace.Types, "Action", text);
				this.ConversationId.WriteToXml(writer, XmlNamespace.Types, "ConversationId");
				if (this.Action == ConversationActionType.AlwaysCategorize || this.Action == ConversationActionType.AlwaysDelete || this.Action == ConversationActionType.AlwaysMove)
				{
					writer.WriteElementValue(XmlNamespace.Types, "ProcessRightAway", EwsUtilities.BoolToXSBool(this.ProcessRightAway));
				}
				if (this.Action == ConversationActionType.AlwaysCategorize)
				{
					if (this.Categories != null && this.Categories.Count > 0)
					{
						this.Categories.WriteToXml(writer, XmlNamespace.Types, "Categories");
					}
				}
				else if (this.Action == ConversationActionType.AlwaysDelete)
				{
					writer.WriteElementValue(XmlNamespace.Types, "EnableAlwaysDelete", EwsUtilities.BoolToXSBool(this.EnableAlwaysDelete));
				}
				else if (this.Action == ConversationActionType.AlwaysMove)
				{
					if (this.DestinationFolderId != null)
					{
						writer.WriteStartElement(XmlNamespace.Types, "DestinationFolderId");
						this.DestinationFolderId.WriteToXml(writer);
						writer.WriteEndElement();
					}
				}
				else
				{
					if (this.ContextFolderId != null)
					{
						writer.WriteStartElement(XmlNamespace.Types, "ContextFolderId");
						this.ContextFolderId.WriteToXml(writer);
						writer.WriteEndElement();
					}
					if (this.ConversationLastSyncTime != null)
					{
						writer.WriteElementValue(XmlNamespace.Types, "ConversationLastSyncTime", this.ConversationLastSyncTime.Value);
					}
					if (this.Action == ConversationActionType.Copy)
					{
						EwsUtilities.Assert(this.DestinationFolderId != null, "ApplyconversationActionRequest", "DestinationFolderId should be set when performing copy action");
						writer.WriteStartElement(XmlNamespace.Types, "DestinationFolderId");
						this.DestinationFolderId.WriteToXml(writer);
						writer.WriteEndElement();
					}
					else if (this.Action == ConversationActionType.Move)
					{
						EwsUtilities.Assert(this.DestinationFolderId != null, "ApplyconversationActionRequest", "DestinationFolderId should be set when performing move action");
						writer.WriteStartElement(XmlNamespace.Types, "DestinationFolderId");
						this.DestinationFolderId.WriteToXml(writer);
						writer.WriteEndElement();
					}
					else if (this.Action == ConversationActionType.Delete)
					{
						EwsUtilities.Assert(this.DeleteType != null, "ApplyconversationActionRequest", "DeleteType should be specified when deleting a conversation.");
						writer.WriteElementValue(XmlNamespace.Types, "DeleteType", this.DeleteType.Value);
					}
					else if (this.Action == ConversationActionType.SetReadState)
					{
						EwsUtilities.Assert(this.IsRead != null, "ApplyconversationActionRequest", "IsRead should be specified when marking/unmarking a conversation as read.");
						writer.WriteElementValue(XmlNamespace.Types, "IsRead", this.IsRead.Value);
						if (this.SuppressReadReceipts != null)
						{
							writer.WriteElementValue(XmlNamespace.Types, "SuppressReadReceipts", this.SuppressReadReceipts.Value);
						}
					}
					else if (this.Action == ConversationActionType.SetRetentionPolicy)
					{
						EwsUtilities.Assert(this.RetentionPolicyType != null, "ApplyconversationActionRequest", "RetentionPolicyType should be specified when setting a retention policy on a conversation.");
						writer.WriteElementValue(XmlNamespace.Types, "RetentionPolicyType", this.RetentionPolicyType.Value);
						if (this.RetentionPolicyTagId != null)
						{
							writer.WriteElementValue(XmlNamespace.Types, "RetentionPolicyTagId", this.RetentionPolicyTagId.Value);
						}
					}
					else if (this.Action == ConversationActionType.Flag)
					{
						EwsUtilities.Assert(this.Flag != null, "ApplyconversationActionRequest", "Flag should be specified when flagging conversation items.");
						writer.WriteStartElement(XmlNamespace.Types, "Flag");
						this.Flag.WriteElementsToXml(writer);
						writer.WriteEndElement();
					}
				}
			}
			finally
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0003F2F0 File Offset: 0x0003E2F0
		public object ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Action", this.Action);
			jsonObject.Add("ConversationId", this.ConversationId.InternalToJson(service));
			if (this.Action == ConversationActionType.AlwaysCategorize || this.Action == ConversationActionType.AlwaysDelete || this.Action == ConversationActionType.AlwaysMove)
			{
				jsonObject.Add("ProcessRightAway", this.ProcessRightAway);
			}
			if (this.Action == ConversationActionType.AlwaysCategorize)
			{
				if (this.Categories != null && this.Categories.Count > 0)
				{
					jsonObject.Add("Categories", this.Categories.InternalToJson(service));
				}
			}
			else if (this.Action == ConversationActionType.AlwaysDelete)
			{
				jsonObject.Add("EnableAlwaysDelete", this.EnableAlwaysDelete);
			}
			else if (this.Action == ConversationActionType.AlwaysMove)
			{
				if (this.DestinationFolderId != null)
				{
					JsonObject jsonObject2 = new JsonObject();
					jsonObject2.Add("BaseFolderId", this.DestinationFolderId.InternalToJson(service));
					jsonObject.Add("DestinationFolderId", jsonObject2);
				}
			}
			else
			{
				if (this.ContextFolderId != null)
				{
					JsonObject jsonObject3 = new JsonObject();
					jsonObject3.Add("BaseFolderId", this.ContextFolderId.InternalToJson(service));
					jsonObject.Add("ContextFolderId", jsonObject3);
				}
				if (this.ConversationLastSyncTime != null)
				{
					jsonObject.Add("ConversationLastSyncTime", this.ConversationLastSyncTime.Value);
				}
				if (this.Action == ConversationActionType.Copy)
				{
					EwsUtilities.Assert(this.DestinationFolderId != null, "ApplyconversationActionRequest", "DestinationFolderId should be set when performing copy action");
					JsonObject jsonObject4 = new JsonObject();
					jsonObject4.Add("BaseFolderId", this.DestinationFolderId.InternalToJson(service));
					jsonObject.Add("DestinationFolderId", jsonObject4);
				}
				else if (this.Action == ConversationActionType.Move)
				{
					EwsUtilities.Assert(this.DestinationFolderId != null, "ApplyconversationActionRequest", "DestinationFolderId should be set when performing move action");
					JsonObject jsonObject5 = new JsonObject();
					jsonObject5.Add("BaseFolderId", this.DestinationFolderId.InternalToJson(service));
					jsonObject.Add("DestinationFolderId", jsonObject5);
				}
				else if (this.Action == ConversationActionType.Delete)
				{
					EwsUtilities.Assert(this.DeleteType != null, "ApplyconversationActionRequest", "DeleteType should be specified when deleting a conversation.");
					jsonObject.Add("DeleteType", this.DeleteType.Value);
				}
				else if (this.Action == ConversationActionType.SetReadState)
				{
					EwsUtilities.Assert(this.IsRead != null, "ApplyconversationActionRequest", "IsRead should be specified when marking/unmarking a conversation as read.");
					jsonObject.Add("IsRead", this.IsRead.Value);
					if (this.SuppressReadReceipts != null)
					{
						jsonObject.Add("SuppressReadReceipts", this.SuppressReadReceipts != null);
					}
				}
				else if (this.Action == ConversationActionType.SetRetentionPolicy)
				{
					EwsUtilities.Assert(this.RetentionPolicyType != null, "ApplyconversationActionRequest", "RetentionPolicyType should be specified when setting a retention policy on a conversation.");
					jsonObject.Add("RetentionPolicyType", this.RetentionPolicyType.Value);
					if (this.RetentionPolicyTagId != null)
					{
						jsonObject.Add("RetentionPolicyTagId", this.RetentionPolicyTagId.Value);
					}
				}
				else if (this.Action == ConversationActionType.Flag)
				{
					EwsUtilities.Assert(this.Flag != null, "ApplyconversationActionRequest", "Flag should be specified when flagging items in a conversation.");
					jsonObject.Add("Flag", this.Flag.InternalToJson(service));
				}
			}
			return jsonObject;
		}
	}
}
